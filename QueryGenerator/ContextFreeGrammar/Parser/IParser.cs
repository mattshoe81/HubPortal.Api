using System.Collections.Generic;

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    public interface IContextFreeGrammarParser {

        string Parse(Queue<string> tokens);
    }
}