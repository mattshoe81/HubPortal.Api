using System;
using System.Collections.Generic;
using System.Text;
using HubPortal.QueryGenerator.Exceptions;
using HubPortal.QueryGenerator.Extensions;
using HubPortal.QueryGenerator.Queries;

namespace HubPortal.QueryGenerator {

    public static class Parser {

        public static string Parse(string queryString) {
            return ParseExpression(Tokenizer.GetTokens(queryString));
        }

        #region Recursive Descent Parsing Methods

        private static string ParseExpression(Queue<string> tokens) {
            string token = tokens.Dequeue();
            string query = String.Empty;

            if (token != "FIND") throw new QuerySyntaxException(token, "FIND");

            // Check if token = process or client
            query = ParseSearchType(tokens, query);
            while (tokens.Count > 0) query = ParseRefinement(tokens, query);

            // Close parenthesis for where
            query += ")";

            return query;
        }

        private static string ParseSearchType(Queue<string> tokens, string query) {
            string token = tokens.Dequeue();
            // Check if token == process or client
            if (token.IsValidSearchType()) {
                query = QueryStrings.GetSearch(token);
            } else {
                throw new QuerySyntaxException("Invalid Search Type. Excpected 'process', 'client', or 'source'");
            }

            return query;
        }


        private static string ParseRefinement(Queue<string> tokens, string query) {
            string token = tokens.Dequeue();
            if (token != "{") throw new QuerySyntaxException(token, "{");
            token = tokens.Dequeue();
            if (!token.IsValidProperty()) throw new QuerySyntaxException("Invalid property name");
            string property = token;
            token = tokens.Dequeue();
            if (token != ":") throw new QuerySyntaxException(token, ":");
            token = tokens.Dequeue();
            query += QueryStrings.GetPropertySearch(property, token);
            token = tokens.Dequeue();
            if (token != "}") throw new QuerySyntaxException(token, "}");

            return query;
        }











        #endregion



        #region Private Methods

        #endregion


    }
}
