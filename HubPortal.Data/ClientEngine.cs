using System.Collections.Generic;

using HubPortal.Data.Utilities;
using HubPortal.QueryGenerator.ContextFreeGrammar;

namespace HubPortal.Data {

    public class ClientEngine {

        /// <summary>
        /// Returns a list of strings containing the names of all the clients in the database.
        /// </summary>
        /// <returns>List of strings containing the names of all the clients in the database</returns>
        public static IEnumerable<string> GetClientList() {
            IQuery query = QueryBuilder.GetQuery(Symbols.FINDALL, Symbols.CLIENT_LIST);
            return OracleDataUtil.GetListOfString(query);
        }
    }
}
