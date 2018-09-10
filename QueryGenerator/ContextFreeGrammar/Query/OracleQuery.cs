using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HubPortal.Tests")]

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Constructs a query.
    /// </summary>
    public class OracleQuery : BaseQuery {

        /// <summary>
        /// Given a valid SearchType, instantiates a Query.
        /// </summary>
        /// <param name="queryType"></param>
        public OracleQuery(string query, string queryType) : base(query, queryType) { }

        public override IEnumerable<T> Execute<T>() {
            throw new System.NotImplementedException();
        }

        public override void ExecuteNonQuery() {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Convert the CFG query to a Database query string.
        /// </summary>
        /// <returns>this as a database query</returns>
        public override string ToString() {
            /*
             * To generate a database query, take the cfg string and give it to the tokenizer which will generate tokens,
             * then the parser will parse those tokens to generate the proper database query
             */
            return (string)QueryBuilder.GetParser().Parse(Tokenizer.GetTokens(this.CFGQuery));
        }

        public string ToDBString() {
            return (string)QueryBuilder.GetParser().Parse(Tokenizer.GetTokens(this.CFGQuery));
        }
    }
}
