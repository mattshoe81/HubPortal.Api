using HubPortal.QueryGenerator.ContextFreeGrammar;

namespace HubPortal.QueryGenerator {

    /// <summary>
    /// Provides indirect access to the database implementation through an intermediary context free grammar used to describe
    /// the criteria of the query.
    /// </summary>
    public static class QueryEngine {

        /// <summary>
        /// Given a string properly formatted to the specifications of the context free grammar, will return a query to the database implementation.
        /// </summary>
        /// <param name="query">ContextFreeGrammar.Query object</param>
        /// <returns>A database query</returns>
        public static string GenerateDatabaseQuery(IQuery query) {
            /*
             * To generate a database query, take the cfg string and give it to the tokenizer which will generate tokens,
             * then the parser will parse those tokens to generate the proper database query
             */
            return CFGBuilder.GetParser().Parse(Tokenizer.GetTokens(query));
        }
    }
}