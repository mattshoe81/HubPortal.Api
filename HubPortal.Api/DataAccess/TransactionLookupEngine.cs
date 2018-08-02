using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HubPortal.Api.Models;
using Oracle.ManagedDataAccess.Client;

namespace HubPortal.Api.DataAccess {
    public class TransactionLookupEngine {

        private TransactionLookupEngine() { }

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
                    transaction.TotalElapsedTime = reader.IsDBNull(reader.GetOrdinal("TOTAL_ELAPSED_TIME")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TOTAL_ELAPSED_TIME"));
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

        private const string CONN_STRING = @"DATA SOURCE = middleware_dev_db:1536/middev;PERSIST SECURITY INFO=True;USER ID = WBISUPPORT_USR;Password=cwadmin01";
        public static IEnumerable<Transaction> GetTransactionsByProcess(string processName) {
            List<Transaction> transactions = new List<Transaction>();
            using (OracleConnection connection = new OracleConnection(CONN_STRING)) {
                connection.Open();
                using (OracleCommand command = new OracleCommand(GetSqlStatement("GetTransactionsByProcessName"), connection)) {
                    command.Parameters.Add(":parameter", processName);
                    transactions = ReadTransactions(command);
                }
            }

            return transactions;
        }

        public static IEnumerable<Transaction> GetTransactionsByClient(string client) {            
            List<Transaction> transactions = new List<Transaction>();
            using (OracleConnection connection = new OracleConnection(CONN_STRING)) {
                connection.Open();
                using (OracleCommand command = new OracleCommand(GetSqlStatement("GetTransactionsByClient"), connection)) {
                    command.Parameters.Add(":parameter", client);
                    transactions = ReadTransactions(command);
                }
            }

            return transactions;
        }

        public static IEnumerable<Transaction> GetAllTransactions() {
            List<Transaction> transactions = new List<Transaction>();
            using (OracleConnection connection = new OracleConnection(CONN_STRING)) {
                connection.Open();
                using (OracleCommand command = new OracleCommand(GetSqlStatement("GetAllTransactions"), connection)) {
                    OracleDataReader reader = command.ExecuteReader();
                    transactions = ReadTransactions(command);
                }
            }

            return transactions;
        }

        public static IEnumerable<string> GetListOfString(string scriptName, string columnName) {
            List<string> items = new List<string>();
            using (OracleConnection connection = new OracleConnection(CONN_STRING)) {
                try {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(GetSqlStatement(scriptName), connection)) {
                        OracleDataReader reader = command.ExecuteReader();
                        try {
                            while (reader.Read()) {
                                string item = String.Empty;
                                item = reader.IsDBNull(reader.GetOrdinal(columnName)) ? null : reader.GetString(reader.GetOrdinal(columnName));
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


        public static IEnumerable<string> GetProcessNameList() {
            return GetListOfString("GetProcessNameList", "PROCESS_NAME");
        }

        public static IEnumerable<string> GetClientList() {
            return GetListOfString("GetClientList", "CLIENT_NAME");
        }

        public static IEnumerable<string> GetTransactionTypeList() {
            return GetListOfString("GetTransactionTypeList", "TRANSACTION_TYPE_NAME");
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










    }
}
