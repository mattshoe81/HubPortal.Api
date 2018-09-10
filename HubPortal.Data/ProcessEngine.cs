using System.Collections.Generic;

using HubPortal.Data.Utilities;
using HubPortal.QueryGenerator.ContextFreeGrammar;

namespace HubPortal.Data {

    public class ProcessEngine {

        /// <summary>
        /// Returns a list of strings containing the names of all the processes in the database.
        /// </summary>
        /// <returns>List of strings containing the names of all the processes in the database</returns>
        public static IEnumerable<string> GetProcessNameList() {
            IQuery query = QueryBuilder.GetQuery(Symbols.FINDALL, Symbols.PROCESS_LIST);
            return OracleDataUtil.GetListOfString(query);
        }
    }
}
