using System.Runtime.CompilerServices;

using HubPortal.QueryGenerator.Exceptions;
using HubPortal.QueryGenerator.Extensions;

[assembly: InternalsVisibleTo("HubPortal.Tests")]

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Constructs a query.
    /// </summary>
    public class Query : IQuery {

        #region Public Constructors

        /// <summary>
        /// Given a valid SearchType, instantiates a Query.
        /// </summary>
        /// <param name="queryType"></param>
        public Query(string query, string queryType) {
            if (!queryType.IsValidSearchType() && !queryType.IsValidItem()) throw new QuerySyntaxException($"{queryType} must be a valid SearchType or Item");
            this.CFGQuery = $"{query} {queryType} WHERE";
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Adds a Refinement to this.
        /// <para>For the definition of Refinement, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="property">Valid Property</param>
        /// <param name="value">   Valid Value</param>
        public void Refine(string property, string value) {
            if (!property.IsValidProperty()) throw new QuerySyntaxException($"{property} is not a valid Property.");
            this.CFGQuery += $" {{ {property} : '{value}' }}";
        }

        /// <summary>
        /// Convert this into its context free grammar query representation.
        /// </summary>
        /// <returns>this represented as a well formed context free grammar query</returns>
        public string ToCFGString() {
            return this.CFGQuery;
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
            return QueryBuilder.GetParser().Parse(Tokenizer.GetTokens(this.CFGQuery));
        }

        #endregion Public Methods

        #region Private Properties

        private string CFGQuery { get; set; }

        #endregion Private Properties
    }
}