using System;
using System.Collections.Generic;
using System.Linq;

namespace HubPortal.QueryGenerator.ContextFreeGrammar {

    /// <summary>
    /// Breaks the context free grammar into its constituent tokens.
    /// </summary>
    internal static class Tokenizer {

        #region Public Methods

        /// <summary>
        /// Given a string that satisfies the context free grammar, returns a Queue containing the tokens.
        /// </summary>
        /// <param name="query">Well formed context free grammar</param>
        /// <returns>Queue of tokens</returns>
        public static Queue<string> GetTokens(string query) {
            return new Queue<string>(query.Split('\'')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToList());
        }

        #endregion Public Methods
    }
}