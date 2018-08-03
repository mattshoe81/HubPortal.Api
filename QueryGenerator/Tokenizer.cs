using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HubPortal.QueryGenerator {

    public static class Tokenizer {

        public static Queue<string> GetTokens(string query) {
            return new Queue<string>(query.Split('\'')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToList());
        }
    }
}
