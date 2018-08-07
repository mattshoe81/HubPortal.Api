namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Factory for the components needed to construct a Query.
    /// </summary>
    public class CFGBuilder {

        /// <summary>
        /// Get a new instance of a class that implements the IQuery interface.
        /// </summary>
        /// <param name="searchType">The SearchType of the Query</param>
        /// <returns>New instance of a class that implements IQuery</returns>
        public static IQuery GetQuery(string searchType) {
            return new Query(searchType);
        }

        /// <summary>
        /// Get a new instance of a class that implements the IParser interface.
        /// </summary>
        /// <returns>New instance ofa class that implements the IContextFreeGrammarParser interface</returns>
        internal static IContextFreeGrammarParser GetParser() {
            return new CFGParser();
        }
    }
}