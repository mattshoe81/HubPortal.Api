using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

using HubPortal.QueryGenerator.Exceptions;
using HubPortal.QueryGenerator.Extensions;

[assembly: InternalsVisibleTo("HubPortal.Tests")]

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Parses a Queue of tokens according to a specific context free grammar in order to generate a
    /// database query.
    /// </summary>
    internal class OracleParser : IContextFreeGrammarParser {
        private string query = "";

        /// <summary>
        /// Parse through the given tokens and produce a database query according to the Context Free
        /// Grammar, based on the information in the tokens.
        /// </summary>
        /// <param name="tokens">Tokenized context free grammar</param>
        /// <returns>Database query as a string</returns>
        public object Parse(Queue<string> tokens) {
            ParseQuery(tokens);
            return this.query;
        }

        /*
         *
         * Each method in the parser is designed to parse exactly one rewrite
         * rule in the Context Free Grammar, and then passes the tokens to the appropriate
         * rewrite rule as they are encountered. The database queries are loaded in from the
         * QueryLoader class as needed and built by this parser.
         *
         */

        public void ParseItem(Queue<string> tokens) {
            string token = tokens.Dequeue();
            this.query = QueryLoader.GetItemQuery(token);
            //Dequeue WHERE
            tokens.Dequeue();
            while (tokens.Count > 0) {
                ParseRefinement(tokens);
            }
        }

        public void ParseQuery(Queue<string> tokens) {
            string token = tokens.Dequeue();

            string query = String.Empty;

            if (token == "GET")
                ParseItem(tokens);
            else if (tokens.Peek().IsValidSearchType())
                ParseSearchType(tokens);
            else
                throw new QuerySyntaxException($"{tokens.Peek()} is not a valid token.");
        }

        public void ParseRefinement(Queue<string> tokens) {
            // Dequeue left curly brace token
            string token = tokens.Dequeue();
            if (token != "{") throw new QuerySyntaxException(token, "{");
            // Dequeue Property token
            token = tokens.Dequeue();
            string property = token;
            // Dequeue colon token
            token = tokens.Dequeue();
            if (token != ":") throw new QuerySyntaxException(token, ":");
            // Dequeue Value token
            token = tokens.Dequeue();

            // Kluge to find checkpoints by transaction id
            if (property == Symbols.TRANSACTION_ID) {
                this.query = this.query.Replace("@", token);
                if (tokens.Dequeue() != "}") throw new QuerySyntaxException(token, "}");
                return;
            }
            // If the token is FAILED, the oracle query must compare null, so it must remove quotes
            // and replace = with IS
            if (property == Symbols.FAILED && token == "null") {
                this.query += QueryLoader.GetRefinement(property, token).Replace("'", "").Replace("=", "IS");
            } else {
                DateTime date;
                if (DateTime.TryParse(token, out date)) {
                    this.query += QueryLoader.GetRefinement(property, date.ToOracleTimeStamp());
                } else {
                    this.query += QueryLoader.GetRefinement(property, token);
                }
            }

            // Dequeue the right curly brace token
            token = tokens.Dequeue();
            if (token != "}") throw new QuerySyntaxException(token, "}");
        }

        public void ParseSearchType(Queue<string> tokens) {
            // If next token is a StringList, parse that
            if (tokens.Peek().IsValidStringList()) {
                ParseStringList(tokens);
            } else {
                string token = tokens.Dequeue();
                this.query = QueryLoader.GetSearchTypeQuery(token);
                // Dequeue the WHERE token
                tokens.Dequeue();
                while (tokens.Count > 0) ParseRefinement(tokens);
                if (token == Symbols.TRANSACTION) this.query += "\n)";
            }
        }

        public void ParseStringList(Queue<string> tokens) {
            this.query += QueryLoader.GetStringList(tokens.Dequeue());
        }
    }
}
