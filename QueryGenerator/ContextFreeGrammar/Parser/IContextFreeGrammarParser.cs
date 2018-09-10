using System.Collections.Generic;

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Parses a Queue of tokens according to a specific context free grammar in order to generate a
    /// database query.
    /// </summary>
    public interface IContextFreeGrammarParser {

        /// <summary>
        /// Parse through the given tokens and produce a database query according to the Context Free
        /// Grammar, based on the information in the tokens.
        /// </summary>
        /// <param name="tokens">Tokenized context free grammar</param>
        /// <returns>Database query as a string</returns>
        object Parse(Queue<string> tokens);

        void ParseItem(Queue<string> tokens);

        void ParseQuery(Queue<string> tokens);

        void ParseRefinement(Queue<string> tokens);

        void ParseSearchType(Queue<string> tokens);

        void ParseStringList(Queue<string> tokens);
    }
}
