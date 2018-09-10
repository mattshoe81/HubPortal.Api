using System;
using System.Collections.Generic;
using System.Text;

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    public abstract class BaseQuery : IQuery {

        public BaseQuery(string query, string queryType) {
            //if (!queryType.IsValidSearchType() && !queryType.IsValidItem()) throw new QuerySyntaxException($"{queryType} must be a valid SearchType or Item");
            this.CFGQuery = $"{query} {queryType} WHERE";
        }

        // The string representation of the context free grammar constructed by the query.
        protected string CFGQuery { get; set; }

        public abstract IEnumerable<T> Execute<T>();

        public abstract void ExecuteNonQuery();

        /// <summary>
        /// Adds a Refinement to this.
        /// <para>For the definition of Refinement, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="property">Valid Property</param>
        /// <param name="value">   Valid Value</param>
        public IQuery Refine(string property, string value) {
            //if (!property.IsValidProperty()) throw new QuerySyntaxException($"{property} is not a valid Property.");
            this.CFGQuery += $" {{ {property} : '{value}' }}";
            return this;
        }

        /// <summary>
        /// Convert the CFG query to a string representation of the database query.
        /// </summary>
        /// <returns>this as a database query</returns>
        public override string ToString() {
            return this.CFGQuery;
        }

        public string ToString(bool asContextFreeGrammar) {
            if (asContextFreeGrammar) return this.CFGQuery;
            else return this.ToString();
        }
    }
}
