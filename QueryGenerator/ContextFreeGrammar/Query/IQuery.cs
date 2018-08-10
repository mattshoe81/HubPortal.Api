namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Interface to specify the methods required to construct a query according to the context free grammar.
    /// </summary>
    public interface IQuery {

        #region Public Methods

        /// <summary>
        /// Adds a
        /// <code>
        /// Refinement
        /// </code>
        /// to
        /// <code>
        /// this
        /// </code>
        /// .
        /// <para>For the definition of Refinement, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="property">Valid Property</param>
        /// <param name="value">   Valid Value</param>
        void Refine(string property, string value);

        /// <summary>
        /// Convert this into its context free grammar query representation.
        /// </summary>
        /// <returns>this represented as a well formed context free grammar query</returns>
        string ToCFGString();

        /// <summary>
        /// Convert the CFG query to a Database query string.
        /// </summary>
        /// <returns>this as a database query</returns>
        string ToString();

        #endregion Public Methods
    }
}