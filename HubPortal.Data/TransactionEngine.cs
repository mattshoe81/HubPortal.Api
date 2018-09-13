using System;
using System.Collections.Generic;

using HubPortal.Data.Models;
using HubPortal.Data.Utilities;
using HubPortal.QueryGenerator.ContextFreeGrammar;

using Oracle.ManagedDataAccess.Client;

namespace HubPortal.Data {

    /// <summary>
    /// Class to provide access to the database through queries constructed by <see
    /// cref="HubPortal.QueryGenerator.ContextFreeGrammar.IQuery"/> implementations
    /// </summary>
    public static class TransactionEngine {

        public static TransactionDetail GetDetail(string transactionid) {
            IQuery query = QueryBuilder.GetQuery(Symbols.GET, Symbols.TRANSACTION_DETAIL).Refine(Symbols.TRANSACTION_ID, transactionid);
            TransactionDetail detail = new TransactionDetail();
            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                connection.Open();
                string dbQuery = query.ToString();
                using (OracleCommand command = new OracleCommand(dbQuery, connection)) {
                    OracleDataReader reader = command.ExecuteReader();
                    try {
                        reader.Read();
                        if (reader.HasRows) {
                            detail.TransactionId = OracleDataUtil.ReadString(reader, "TRANS_ID");
                            detail.ProcessName = OracleDataUtil.ReadString(reader, "PROCESS_NAME");
                            detail.TransactionType = OracleDataUtil.ReadString(reader, "TRANSACTION_TYPE_NAME");
                            detail.TransactionTime = OracleDataUtil.ReadDateTime(reader, "TRANS_TIME");
                            detail.Completed = OracleDataUtil.ReadString(reader, "TRANS_COMPLETED");
                            detail.ElapsedTime = OracleDataUtil.ReadNullableInt(reader, "TOTAL_ELAPSED_TIME");
                            detail.URL = OracleDataUtil.ReadString(reader, "URL");
                            detail.Source = OracleDataUtil.ReadString(reader, "SOURCE");
                            detail.Destination = OracleDataUtil.ReadString(reader, "DESTINATION");
                            detail.Ping = OracleDataUtil.ReadString(reader, "PING_FLAG");
                            detail.ServiceLayer = OracleDataUtil.ReadString(reader, "SERVICE_LAYER");
                            detail.Successful = OracleDataUtil.ReadString(reader, "IS_SUCCESSFUL");
                            detail.SessionID = OracleDataUtil.ReadString(reader, "SESSION_ID");
                            detail.SourceType = OracleDataUtil.ReadString(reader, "SOURCE_CONNECTOR");
                            detail.DestinationType = OracleDataUtil.ReadString(reader, "DESTINATION_CONNECTOR");
                        }
                    } finally {
                        reader.Close();
                    }
                }
                query = QueryBuilder.GetQuery(Symbols.GET, Symbols.XREF_DATA).Refine(Symbols.TRANSACTION_ID, transactionid);
                dbQuery = query.ToString();
                Dictionary<string, string> xrefData = new Dictionary<string, string>();
                using (OracleCommand command = new OracleCommand(dbQuery, connection)) {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        string key = OracleDataUtil.ReadString(reader, "XREF_FIELD");
                        string value = OracleDataUtil.ReadString(reader, "XREF_VALUE");
                        xrefData.Add(key, value);
                    }
                    OracleDataUtil.ParseXrefData(xrefData, detail);
                }
            }

            return detail;
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

            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                connection.Open();
                string queryString = query.ToString();
                using (OracleCommand command = new OracleCommand(query.ToString(), connection)) {
                    transactions = ReadTransactions(command);
                }
            }

            return transactions;
        }

        /// <summary>
        /// Returns a list of all the transactions in the database that match the given refinements
        /// in the cfgQuery.
        /// <para>For the definition of the context free grammar, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="query">Well formatted context free grammar string</param>
        /// <returns>List of Transactions</returns>
        public static IEnumerable<Transaction> GetTransactions(string queryString) {
            List<Transaction> transactions = new List<Transaction>();
            IQuery query = QueryBuilder.GetQuery(queryString);
            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                connection.Open();
                using (OracleCommand command = new OracleCommand(query.ToString(), connection)) {
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
            IQuery query = QueryBuilder.GetQuery(Symbols.FINDALL, Symbols.TRANSACTION_TYPE_LIST);
            return OracleDataUtil.GetListOfString(query);
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
                    transaction.TransactionTime = reader.IsDBNull(reader.GetOrdinal("TRANS_TIME")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("TRANS_TIME"));
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
    }
}
