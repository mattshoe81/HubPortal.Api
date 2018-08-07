namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Interface to specify the methods required to construct a query according to the context free grammar.
    /// </summary>
    public interface IQuery {

        /// <summary>
        /// Adds the given '<code>Lookup</code>' to <code>this</code>.
        /// <para>
        /// For the definition of Lookup, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/>
        /// </para>
        /// </summary>
        /// <param name="lookup">Valid Lookup</param>
        void AddLookup(string lookup);

        /// <summary>
        /// Adds a <code>Refinement</code> to <code>this</code>.
        /// <para>
        /// For the definition of Refinement, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/>
        /// </para>
        /// </summary>
        /// <param name="property">Valid Property</param>
        /// <param name="value">Valid Value</param>
        void Refine(string property, string value);

        /// <summary>
        /// Convert the CFG query to a string.
        /// </summary>
        string ToString();
    }
}