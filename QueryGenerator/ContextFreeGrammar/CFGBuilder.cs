namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Factory for the components needed to construct a Query.
    /// </summary>
    public class CFGBuilder {

        public static IQuery GetQuery(string searchType) {
            return new Query(searchType);
        }

        internal static IContextFreeGrammarParser GetParser() {
            return new CFGParser();
        }
    }
}