using System.Collections.Generic;

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Interface to specify the methods required to construct a query according to the context free grammar.
    /// </summary>
    public interface IQuery {

        IEnumerable<T> Execute<T>();

        void ExecuteNonQuery();

        /// <summary>
        /// Adds a Refinement to this.
        /// <para>For the definition of Refinement, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="property">Valid Property</param>
        /// <param name="value">   Valid Value</param>
        IQuery Refine(string property, string value);

        /// <summary>
        /// Convert the CFG query to a Database query string.
        /// </summary>
        /// <returns>this as a database query</returns>
        string ToString();

        string ToString(bool asContextFreeGrammar);
    }
}
