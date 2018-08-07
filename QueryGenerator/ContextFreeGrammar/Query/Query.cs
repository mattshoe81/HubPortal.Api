using HubPortal.QueryGenerator.Exceptions;
using HubPortal.QueryGenerator.Extensions;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("HubPortal.Tests")]

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    internal class Query : IQuery {
        private string query { get; set; }

        /// <summary>
        /// Given a valid SearchType, instantiates a CFGFactory.GetQuery.
        /// </summary>
        /// <param name="searchType"></param>
        public Query(string searchType) {
            if (!searchType.IsValidSearchType()) throw new QuerySyntaxException($"{searchType} is not a valid SearchType");
            this.query = searchType.GetCfgQuery();
        }

        /// <summary>
        /// Adds a <code>Refinement</code> to <code>this</code>.
        /// <para>
        /// For the definition of Refinement, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/>
        /// </para>
        /// </summary>
        /// <param name="property">Valid Property</param>
        /// <param name="value">Valid Value</param>
        public void Refine(string property, string value) {
            if (!property.IsValidProperty()) throw new QuerySyntaxException($"{property} is not a valid Property.");
            if (!value.IsValidValue()) throw new QuerySyntaxException($"{value} is not a valid Value.");
            this.query += $" {{ {property} : '{value}' }}";
        }

        /// <summary>
        /// Adds the given '<code>Lookup</code>' to <code>this</code>.
        /// <para>
        /// For the definition of Lookup, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/>
        /// </para>
        /// </summary>
        /// <param name="lookup">Valid Lookup</param>
        public void AddLookup(string lookup) {
            if (!lookup.IsValidLookup()) throw new QuerySyntaxException($"{lookup} is not a valid Lookup");
            this.query += $" AND {lookup}";
        }

        /// <summary>
        /// Convert the CFG query to a string.
        /// </summary>
        public override string ToString() {
            return this.query;
        }
    }
}