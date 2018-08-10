using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using HubPortal.QueryGenerator.Exceptions;
using HubPortal.QueryGenerator.Extensions;

[assembly: InternalsVisibleTo("HubPortal.Tests")]

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Parses a Queue of tokens according to a specific context free grammar in order to generate a
    /// database query.
    /// </summary>
    internal class CFGParser : IContextFreeGrammarParser {

        #region Public Methods

        /// <summary>
        /// Parse through the given tokens and produce a database query according to the Context Free
        /// Grammar, based on the information in the tokens.
        /// </summary>
        /// <param name="tokens">Tokenized context free grammar</param>
        /// <returns>Database query as a string</returns>
        public string Parse(Queue<string> tokens) {
            return ParseQuery(tokens);
        }

        #endregion Public Methods

        /*
         *
         * Each method in the parser is designed to parse exactly one rewrite
         * rule in the Context Free Grammar, and then passes the tokens to the appropriate
         * rewrite rule as they are encountered. The database queries are loaded in from the
         * QueryLoader class as needed and built by this parser.
         *
         */

        #region Private Methods

        private string ParseItem(Queue<string> tokens, string query) {
            string token = tokens.Dequeue();
            if (!token.IsValidItem())
                throw new QuerySyntaxException($"{token} is not a valid Item");
            return QueryLoader.GetItemQuery(token);
        }

        private string ParseQuery(Queue<string> tokens) {
            string token = tokens.Dequeue();
            if (token != "FINDALL" && token != "GET")
                throw new QuerySyntaxException($"Invalid token at {token}. Expected GET or FINDALL.");

            string query = String.Empty;

            if (token == "GET")
                query = ParseItem(tokens, query);
            else if (tokens.Peek().IsValidSearchType())
                query = ParseSearchType(tokens, query);
            else
                throw new QuerySyntaxException($"{tokens.Peek()} is not a valid token.");

            return query;
        }

        private string ParseRefinement(Queue<string> tokens, string query) {
            // Dequeue left curly brace token
            string token = tokens.Dequeue();
            if (token != "{") throw new QuerySyntaxException(token, "{");
            // Dequeue Property token
            token = tokens.Dequeue();
            if (!token.IsValidProperty()) throw new QuerySyntaxException($"{token} is not a valid property name");
            string property = token;
            // Dequeue colon token
            token = tokens.Dequeue();
            if (token != ":") throw new QuerySyntaxException(token, ":");
            // Dequeue Value token
            token = tokens.Dequeue();
            // If the token is FAILED, the oracle query must compare null, so it must remove quotes
            // and replace = with IS
            if (property == Symbols.FAILED && token == "null")
                query += QueryLoader.GetRefinement(property, token).Replace("'", "").Replace("=", "IS");
            else
                query += QueryLoader.GetRefinement(property, token);
            // Dequeue the right curly brace token
            token = tokens.Dequeue();
            if (token != "}") throw new QuerySyntaxException(token, "}");

            return query;
        }

        private string ParseSearchType(Queue<string> tokens, string query) {
            if (!tokens.Peek().IsValidSearchType())
                throw new QuerySyntaxException($"{tokens.Peek()} is not a valid SearchType");

            // If next token is a StringList, parse that
            if (tokens.Peek().IsValidStringList()) {
                query = ParseStringList(tokens, query);
            } else {
                string token = tokens.Dequeue();
                query = QueryLoader.GetSearchTypeQuery(token);
                // Dequeue the WHERE token
                tokens.Dequeue();
                while (tokens.Count > 0) query = ParseRefinement(tokens, query);
                if (token == Symbols.TRANSACTION) query += "\n)\n ORDER BY ht.TRANS_START_TIMESTAMP";
            }

            return query;
        }

        private string ParseStringList(Queue<string> tokens, string query) {
            return query += QueryLoader.GetStringList(tokens.Dequeue());
        }

        #endregion Private Methods
    }
}