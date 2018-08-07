using HubPortal.QueryGenerator.ContextFreeGrammar;
using HubPortal.QueryGenerator.Exceptions;
using HubPortal.QueryGenerator.Extensions;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HubPortal.Tests")]

namespace HubPortal.QueryGenerator {

    /// <summary>
    /// Class used to load the database queries corresponding to the symbols in the context free grammar.
    /// </summary>
    internal static class QueryLoader {

        #region Internal Members

        /// <summary>
        /// Returns the database query string corresponding to the given Lookup.
        /// <para>For the definition of Lookup, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="StringList">Valid Lookup</param>
        /// <returns>Database query corresponding to the given Lookup</returns>
        internal static string GetLookupSearch(string token) {
            return ""; // Not actually necessary to do anything for this one in this implementation
        }

        /// <summary>
        /// Returns the database query string corresponding to the given StringList.
        /// <para>For the definition of StringList, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="stringList">Valid StringList</param>
        /// <returns>Database query corresponding to the given StringList</returns>
        internal static string GetNameList(string stringList) {
            string search;
            switch (stringList) {
                case Symbols.PROCESS_NAMES:
                    search = GetDatabaseQuery("GetProcessList");
                    break;

                case Symbols.CLIENT_NAMES:
                    search = GetDatabaseQuery("GetClientList");
                    break;

                case Symbols.TRANSACTION_TYPES:
                    search = GetDatabaseQuery("GetTransactionTypeList");
                    break;

                default:
                    throw new QuerySyntaxException($"{stringList} is not a valid StringList");
            }

            return search;
        }

        /// <summary>
        /// Returns the database query string corresponding to the given Refinement.
        /// <para>For the definition of Refinement, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="property">Valid Property</param>
        /// <param name="value">Valid Value</param>
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
        /// Returns the database query string corresponding to the given TransactionSearch.
        /// <para>For the definition of TransactionSearch, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="transactionSearch">Valid TransactionSearch</param>
        /// <returns>Database query corresponding to the given a valid TransactionSearch</returns>
        internal static string GetTransactionSearch(string transactionSearch) {
            if (!transactionSearch.IsValidTransactionSearch()) throw new QuerySyntaxException($"{transactionSearch} is not a valid TransactionSearch");

            return GetDatabaseQuery("GetTransactions");
        }

        #endregion Internal Members

        #region Private Members

        /// <summary>
        /// Returns the database query given the name of its embedded resource.
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

        #endregion Private Members
    }
}