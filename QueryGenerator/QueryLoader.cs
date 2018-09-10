using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

using HubPortal.QueryGenerator.ContextFreeGrammar;
using HubPortal.QueryGenerator.Exceptions;
using HubPortal.QueryGenerator.Extensions;

[assembly: InternalsVisibleTo("HubPortal.Tests")]

namespace HubPortal.QueryGenerator {

    /// <summary>
    /// Class used to load the database queries corresponding to the symbols in the context free grammar.
    /// </summary>
    internal static class QueryLoader {

        /// <summary>
        /// Gets the query to get the given Item from the database.
        /// </summary>
        /// <param name="item">Valid Item</param>
        /// <returns>query to get the given Item from the database</returns>
        internal static string GetItemQuery(string item) {
            string query = "";
            try {
                // File naming convention for 'Item' resource files is: 'GetItem' (PascalCase)
                query = GetDatabaseQuery("Get" + Char.ToUpper(item[0]) + item.Substring(1));
            } catch {
                throw new QueryLoaderException($"Cannot load resource for Item '{item}'");
            }

            return query;
        }

        /// <summary>
        /// Returns the database query string corresponding to the given Refinement.
        /// <para>For the definition of Refinement, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="property">Valid Property</param>
        /// <param name="value">   Valid Value</param>
        /// <returns>Database query corresponding to the given Refinement</returns>
        internal static string GetRefinement(string property, string value) {
            string query = "";
            try {
                // File naming convention for 'Refinement' resource files is: 'ByProperty' (PascalCase)
                query = GetDatabaseQuery("By" + Char.ToUpper(property[0]) + property.Substring(1));
            } catch {
                throw new QueryLoaderException($"Cannot load resource for property '{property}'");
            }

            return query.Replace("@", value);
        }

        /// <summary>
        /// Given a valid SearchType, returns the query to retrieve the given searchType from the database.
        /// </summary>
        /// <param name="searchType">Valid SearchType</param>
        /// <returns>Query to retrieve the given searchType from the database</returns>
        internal static string GetSearchTypeQuery(string searchType) {
            string query = "";
            if (searchType.IsValidStringList())
                query = GetStringList(searchType);
            else {
                switch (searchType) {
                    case Symbols.TRANSACTION:
                        query = QueryLoader.GetTransactionQuery();
                        break;

                    case Symbols.OUTAGE:
                        query = QueryLoader.GetOutageQuery();
                        break;

                    case Symbols.SUCCESS:
                        query = QueryLoader.GetSuccessQuery();
                        break;

                    case Symbols.PROCESS:
                        query = QueryLoader.GetProcessQuery();
                        break;

                    case Symbols.CHECKPOINT:
                        query = QueryLoader.GetCheckpointQuery();
                        break;

                    default:
                        throw new QuerySyntaxException($"{searchType} is not a valid SearchType");
                }
            }

            return query;
        }

        /// <summary>
        /// Returns the database query string corresponding to the given StringList.
        /// <para>For the definition of StringList, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="stringList">Valid StringList</param>
        /// <returns>Database query corresponding to the given StringList</returns>
        internal static string GetStringList(string stringList) {
            string search;
            switch (stringList) {
                case Symbols.PROCESS_LIST:
                    search = GetDatabaseQuery("GetProcessList");
                    break;

                case Symbols.CLIENT_LIST:
                    search = GetDatabaseQuery("GetClientList");
                    break;

                case Symbols.TRANSACTION_TYPE_LIST:
                    search = GetDatabaseQuery("GetTransactionTypeList");
                    break;

                default:
                    throw new QuerySyntaxException($"{stringList} is not a valid StringList");
            }

            return search;
        }

        /// <summary>
        /// Returns the query to get checkpoints from the database
        /// </summary>
        /// <returns>query to get checkpoints from the database</returns>
        private static string GetCheckpointQuery() {
            return GetDatabaseQuery("GetCheckpoints");
        }

        /// <summary>
        /// Returns the database query given the name of its embedded resource. The file must be in
        /// the directory HubPortal/QueryGenerator/Queries and have a file name extension of .sql,
        /// with a build action of Embedded Resource specified in its properties.
        /// </summary>
        /// <param name="name">Name of the embedded resource</param>
        /// <returns>Database query</returns>
        private static string GetDatabaseQuery(string name) {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "HubPortal.QueryGenerator.Queries." + name + ".sql";
            string[] names = assembly.GetManifestResourceNames();
            string result = "";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)) {
                result = reader.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Returns the query to get outages from the database
        /// </summary>
        /// <returns>query to get outages from the database</returns>
        private static string GetOutageQuery() {
            return GetDatabaseQuery("GetOutages");
        }

        /// <summary>
        /// Returns the query to get processes from the database
        /// </summary>
        /// <returns>query to get processes from the database</returns>
        private static string GetProcessQuery() {
            return GetDatabaseQuery("GetProcesses");
        }

        /// <summary>
        /// Returns the query to get success statuses from the database
        /// </summary>
        /// <returns>query to get success statuses from the database</returns>
        private static string GetSuccessQuery() {
            return GetDatabaseQuery("GetSucesses");
        }

        /// <summary>
        /// Returns the database query string corresponding to the given TransactionSearch.
        /// <para>For the definition of TransactionSearch, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="transactionSearch">Valid TransactionSearch</param>
        /// <returns>Database query corresponding to the given a valid TransactionSearch</returns>
        private static string GetTransactionQuery() {
            return GetDatabaseQuery("GetTransactions");
        }
    }
}
