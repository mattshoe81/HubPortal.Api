using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HubPortal.Data.Models;
using HubPortal.QueryGenerator.ContextFreeGrammar;

using Oracle.ManagedDataAccess.Client;

namespace HubPortal.Data.Utilities {

    internal static class OracleDataUtil {

        // Connection string
        internal const string CONN_STRING = @"DATA SOURCE = ";

        public static DateTime? ReadDateTime(OracleDataReader reader, string columnName) {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal(columnName));
        }

        public static int ReadInt(OracleDataReader reader, string columnName) {
            // Avoid casting errors by catching as long and casting to int
            return (int)(reader.GetInt64(reader.GetOrdinal(columnName)));
        }

        public static int? ReadNullableInt(OracleDataReader reader, string columnName) {
            // Oracle may return a really large number (which we dont care about), so catch it with
            // long and cast to int
            return (int?)(reader.IsDBNull(reader.GetOrdinal(columnName)) ? (long?)null : reader.GetInt64(reader.GetOrdinal(columnName)));
        }

        public static string ReadString(OracleDataReader reader, string columnName) {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? null : reader.GetString(reader.GetOrdinal(columnName));
        }

        /// <summary>
        /// Given a context free grammar containing a StringList, returns a list of strings from the
        /// database corresponding to the specified StringList.
        /// <para>For the definition of the context free grammar and StringList, see <see cref="HubPortal.QueryGenerator.ContextFreeGrammar.txt"/></para>
        /// </summary>
        /// <param name="query">Well formatted context free grammar with a StringList</param>
        /// <returns>List of Transactions</returns>
        internal static IEnumerable<string> GetListOfString(IQuery query) {
            List<string> items = new List<string>();
            using (OracleConnection connection = new OracleConnection(OracleDataUtil.CONN_STRING)) {
                try {
                    connection.Open();
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

        internal static void ParseXrefData(Dictionary<string, string> xrefData, TransactionDetail detail) {
            foreach (KeyValuePair<string, string> pair in xrefData) {
                switch (pair.Key) {
                    case "POLICY_NUMBER":
                        detail.PolicyNumber = pair.Value;
                        break;

                    case "REFERRAL_DATE":
                        detail.ReferralDate = pair.Value;
                        break;

                    case "AUTHORIZATION_CODE":
                        detail.AuthorizationCode = pair.Value;
                        break;

                    case "AMOUNT":
                        detail.Amount = pair.Value;
                        break;

                    case "WORK_ORDER_NUMBER":
                        detail.WorkOrderNumber = pair.Value;
                        break;

                    case "CTU":
                        detail.Ctu = pair.Value;
                        break;

                    case "INVOICE_NUMBER":
                        detail.InvoiceNumber = pair.Value;
                        break;

                    case "ACCOUNT_NUMBER":
                        detail.AccountNumber = pair.Value;
                        break;

                    case "PHONE_NUMBER":
                        detail.PhoneNumber = pair.Value;
                        break;

                    case "WORK_ORDER_ID":
                        detail.WorkOrderId = pair.Value;
                        break;

                    case "REFERRAL_NUMBER":
                        detail.ReferralNumber = pair.Value;
                        break;

                    case "CSR":
                        detail.Csr = pair.Value;
                        break;

                    case "CLAIM_NUMBER":
                        detail.ClaimNumber = pair.Value;
                        break;

                    case "SHOP_NUMBER":
                        detail.ShopNumber = pair.Value;
                        break;

                    case "CREDIT_CARD_NUMBER":
                        detail.CreditCardNumber = pair.Value;
                        break;

                    case "WAREHOUSE_NUMBER":
                        detail.WarehouseNumber = pair.Value;
                        break;
                }
            }
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
