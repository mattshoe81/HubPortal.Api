namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Factory for the components needed to construct a Query.
    /// </summary>
    public static class QueryBuilder {

        /// <summary>
        /// Get a new instance of an implementation of the IQuery interface.
        /// </summary>
        /// <param name="searchType">The SearchType of the Query</param>
        /// <returns>New instance of a class that implements IQuery</returns>
        public static IQuery GetQuery(string queryType, string searchType) {
            return new OracleQuery(queryType, searchType);
        }

        public static IQuery GetQuery(string queryString) {
            return new OracleQuery(queryString);
        }

        /// <summary>
        /// Get a new instance of an implementation of the IParser interface.
        /// </summary>
        /// <returns>New instance ofa class that implements the IContextFreeGrammarParser interface</returns>
        internal static IContextFreeGrammarParser GetParser() {
            return new OracleParser();
        }
    }
}
