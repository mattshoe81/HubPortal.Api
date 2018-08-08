using HubPortal.Api.Models;
using HubPortal.QueryGenerator;
using HubPortal.QueryGenerator.ContextFreeGrammar;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HubPortal.Api.DataAccess {

    /// <summary>
    /// Class to provide access to the database through queries constructed by <see
    /// cref="HubPortal.QueryGenerator.ContextFreeGrammar.IQuery"/> implementations
    /// </summary>
    public static class TransactionLookupEngine {

        #region Public Methods

        /// <summary>
        /// Returns a list of strings containing the names of all the clients in the database.
        /// </summary>
        /// <returns>List of strings containing the names of all the clients in the database</returns>
        public static IEnumerable<string> GetClientList() {
            IQuery query = CFGBuilder.GetQuery(Symbols.CLIENT_NAMES);
            return GetListOfString(query);
        }

        /// <summary>
        /// Returns a list of strings containing the names of all the processes in the database.
        /// </summary>
        /// <returns>List of strings containing the names of all the processes in the database</returns>
        public static IEnumerable<string> GetProcessNameList() {
            IQuery query = CFGBuilder.GetQuery(Symbols.PROCESS_NAMES);
            return GetListOfString(query);
        }

        /// <summary>
        /// Returns a list of all the transactions in the database that match the given refinements
        /// in the cfgQuery.
        /// <para>For the definition of the context free grammar, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="query">Well formatted context free grammar string</param>
        /// <returns>List of Transactions</returns>
        public static IEnumerable<Transaction> GetTransactions(IQuery query) {
            List<Transaction> transactions = new List<Transaction>();
            string sqlQuery = QueryEngine.GenerateDatabaseQuery(query);

            using (OracleConnection connection = new OracleConnection(CONN_STRING)) {
                connection.Open();
                using (OracleCommand command = new OracleCommand(sqlQuery, connection)) {
                    transactions = ReadTransactions(command);
                }
            }

            return transactions;
        }

        /// <summary>
        /// Returns a list of strings containing the names of all the transaction types in the database.
        /// </summary>
        /// <returns>
        /// List of strings containing the names of all the transaction types in the database
        /// </returns>
        public static IEnumerable<string> GetTransactionTypeList() {
            IQuery query = CFGBuilder.GetQuery(Symbols.TRANSACTION_TYPES);
            return GetListOfString(query);
        }

        #endregion Public Methods

        #region Private Members

        // Connection string
        private const string CONN_STRING = @"DATA SOURCE = middleware_dev_db:1536/middev;PERSIST SECURITY INFO=True;USER ID = WBISUPPORT_USR;Password=cwadmin01";

        /// <summary>
        /// Given a context free grammar containing a StringList, returns a list of strings from the
        /// database corresponding to the specified StringList.
        /// <para>For the definition of the context free grammar and StringList, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="query">Well formatted context free grammar with a StringList</param>
        /// <returns>List of Transactions</returns>
        private static IEnumerable<string> GetListOfString(IQuery query) {
            List<string> items = new List<string>();
            using (OracleConnection connection = new OracleConnection(CONN_STRING)) {
                try {
                    connection.Open();
                    string queryString = QueryEngine.GenerateDatabaseQuery(query);
                    using (OracleCommand command = new OracleCommand(queryString, connection)) {
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
            string resourceName = "HubPortal.Api.Queries." + name + ".sql";
            string[] names = assembly.GetManifestResourceNames();
            string result = "";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)) {
                result = reader.ReadToEnd();
            }

            return result;
        }

        private static List<Transaction> ReadTransactions(OracleCommand command) {
            List<Transaction> transactions = new List<Transaction>();
            OracleDataReader reader = command.ExecuteReader();
            try {
                while (reader.Read()) {
                    Transaction transaction = new Transaction();
                    transaction.TransactionID = reader.IsDBNull(reader.GetOrdinal("TRANS_ID")) ? null : reader.GetString(reader.GetOrdinal("TRANS_ID"));
                    transaction.ProcessName = reader.IsDBNull(reader.GetOrdinal("PROCESS_NAME")) ? null : reader.GetString(reader.GetOrdinal("PROCESS_NAME"));
                    transaction.TransactionTypeName = reader.IsDBNull(reader.GetOrdinal("TRANSACTION_TYPE_NAME")) ? null : reader.GetString(reader.GetOrdinal("TRANSACTION_TYPE_NAME"));
                    transaction.TransactionTime = reader.GetDateTime(reader.GetOrdinal("TRANS_TIME"));
                    transaction.TransactionCompleted = reader.IsDBNull(reader.GetOrdinal("TRANS_COMPLETED")) ? (bool?)null : Convert.ToBoolean(reader.GetByte(reader.GetOrdinal("TRANS_COMPLETED")));
                    transaction.TotalElapsedTime = reader.IsDBNull(reader.GetOrdinal("TOTAL_ELAPSED_TIME")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TOTAL_ELAPSED_TIME"));
                    transaction.Url = reader.IsDBNull(reader.GetOrdinal("URL")) ? null : reader.GetString(reader.GetOrdinal("URL"));
                    transaction.ClientName1 = reader.IsDBNull(reader.GetOrdinal("CLNAME1")) ? null : reader.GetString(reader.GetOrdinal("CLNAME1"));
                    transaction.ClientName2 = reader.IsDBNull(reader.GetOrdinal("CLNAME2")) ? null : reader.GetString(reader.GetOrdinal("CLNAME2"));
                    transaction.PingFlag = reader.IsDBNull(reader.GetOrdinal("PING_FLAG")) ? null : reader.GetString(reader.GetOrdinal("PING_FLAG"));
                    transaction.Successful = reader.IsDBNull(reader.GetOrdinal("IS_SUCCESSFUL")) ? null : reader.GetString(reader.GetOrdinal("IS_SUCCESSFUL"));
                    transaction.CheckpointCount = reader.GetInt32(reader.GetOrdinal("CHECKPOINTCOUNT"));
                    transactions.Add(transaction);
                }
            } finally {
                reader.Close();
            }

            return transactions;
        }

        #endregion Private Members
    }
}