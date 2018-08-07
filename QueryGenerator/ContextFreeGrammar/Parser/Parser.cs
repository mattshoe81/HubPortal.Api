using HubPortal.QueryGenerator.Exceptions;
using HubPortal.QueryGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("HubPortal.Tests")]

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    internal class CFGParser : IContextFreeGrammarParser {

        /// <summary>
        /// Parse through the given tokens and produce a database query according to the Context Free Grammar,
        /// based on the information in the tokens.
        /// </summary>
        /// <param name="tokens">Tokenized context free grammar</param>
        /// <returns>Database query as a string</returns>
        public string Parse(Queue<string> tokens) {
            return ParseQuery(tokens);
        }

        #region Recursive Descent Parsers

        /*
         * Each method is designed to parse exactly one rewrite rule in the Context Free Grammar,
         * and then passes the tokens to the appropriate rewrite rule as they are encountered.
         *
         */

        private string ParseLookup(Queue<string> tokens, string query) {
            string token = tokens.Dequeue();
            if (token != "AND") throw new QuerySyntaxException(token, "AND");
            token = tokens.Dequeue();
            if (!token.IsValidLookup()) throw new QuerySyntaxException($"{token} is not a valid lookup value");

            return query + QueryLoader.GetLookupSearch(token);
        }

        private string ParseNameList(Queue<string> tokens, string query) {
            return query += QueryLoader.GetNameList(tokens.Dequeue());
        }

        private string ParseQuery(Queue<string> tokens) {
            string token = tokens.Dequeue();
            string query = String.Empty;

            if (token != "FIND") throw new QuerySyntaxException(token, "FIND");

            return ParseSearchType(tokens, query);
        }

        private string ParseRefinement(Queue<string> tokens, string query) {
            string token = tokens.Dequeue();
            if (token != "{") throw new QuerySyntaxException(token, "{");
            token = tokens.Dequeue();
            if (!token.IsValidProperty()) throw new QuerySyntaxException($"{token} is not a valid property name");
            string property = token;
            token = tokens.Dequeue();
            if (token != ":") throw new QuerySyntaxException(token, ":");
            token = tokens.Dequeue();
            // Need to compare null without string
            if (property == Symbols.FAILED && token == "null")
                query += QueryLoader.GetRefinement(property, token).Replace("'", "").Replace("=", "IS");
            else
                query += QueryLoader.GetRefinement(property, token);
            token = tokens.Dequeue();
            if (token != "}") throw new QuerySyntaxException(token, "}");

            return query;
        }

        private string ParseSearchType(Queue<string> tokens, string query) {
            if (tokens.Peek().IsValidTransactionSearch()) {
                query = ParseTransactionSearch(tokens, query);
                while (tokens.Count > 0) {
                    if (tokens.Peek() == "AND")
                        query = ParseLookup(tokens, query);
                    else
                        query = ParseRefinement(tokens, query);
                }
                // Close parenthesis for where
                query += "\n)\n ORDER BY pr.PROCESS_NAME";
            } else if (tokens.Peek().IsValidNameList()) {
                query = ParseNameList(tokens, query);
            } else {
                throw new QuerySyntaxException($"{tokens.Peek()} is not a valid SearchType");
            }

            return query;
        }

        private string ParseTransactionSearch(Queue<string> tokens, string query) {
            string token = tokens.Dequeue();
            if (!token.IsValidSearchType()) throw new QuerySyntaxException($"{token} is not a valid SearchType");

            return QueryLoader.GetTransactionSearch(token);
        }

        #endregion Recursive Descent Parsers
    }
}