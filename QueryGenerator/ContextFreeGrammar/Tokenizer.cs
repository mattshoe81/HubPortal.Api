using System;
using System.Collections.Generic;
using System.Linq;

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    internal static class Tokenizer {

        public static Queue<string> GetTokens(IQuery query) {
            return new Queue<string>(query.ToString().Split('\'')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToList());
        }
    }
}