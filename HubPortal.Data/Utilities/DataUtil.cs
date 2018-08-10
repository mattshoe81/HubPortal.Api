using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using HubPortal.QueryGenerator.ContextFreeGrammar;
using System.Reflection;
using System.IO;

namespace HubPortal.Data.Utilities {

    internal static class DataUtil {

        // Connection string
        internal const string CONN_STRING = @"DATA SOURCE = middleware_dev_db:1536/middev;PERSIST SECURITY INFO=True;USER ID = WBISUPPORT_USR;Password=cwadmin01";

        /// <summary>
        /// Given a context free grammar containing a StringList, returns a list of strings from the
        /// database corresponding to the specified StringList.
        /// <para>For the definition of the context free grammar and StringList, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="query">Well formatted context free grammar with a StringList</param>
        /// <returns>List of Transactions</returns>
        internal static IEnumerable<string> GetListOfString(IQuery query) {
            List<string> items = new List<string>();
            using (OracleConnection connection = new OracleConnection(DataUtil.CONN_STRING)) {
                try {
                    connection.Open();
                    string cfgQuery = query.ToCFGString();
                    string sqlQuery = query.ToString();
                    using (OracleCommand command = new OracleCommand(query.ToString(), connection)) {
                        OracleDataReader reader = command.ExecuteReader();
                        try {
                            while (reader.Read()) {
                                string item = String.Empty;
                                item = reader.IsDBNull(0) ? null : reader.GetString(0);
                                items.Add(item);
                            }
                        } finally {
                            reader.Close();
                        }
                    }
                } catch (OracleException e) {
                    string ex = e.Message;
                    ex += "";
                }
            }
            return items;
        }

        private static string GetSqlStatement(string name) {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "HubPortal.Queries." + name + ".sql";
            string[] names = assembly.GetManifestResourceNames();
            string result = "";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)) {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}