using HubPortal.QueryGenerator.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace HubPortal.QueryGenerator.Queries {

     class QueryStrings {

        private const string ProcessNameSearch = "ProcessNameSearch.";
        private const string ClientNameSearch = "ClientNameSearch.";
        private const string SourceSearch = "SourceSearch.";
        private const string ProcessName = "ht.process_id in ( select process_id from hts_process where process_name like @ ) ";
        private const string ClientName = "ClientName='@'.";
        private const string TransactionType = "transactionType='@'.";


        public static string GetSearch(string searchType) {
            string search;
            switch (searchType) {
                case "process":
                    search = GetSqlStatement("GetTransactions");
                    break;
                case "client":
                    search = GetSqlStatement("GetTransactions");
                    break;
                case "source":
                    search = GetSqlStatement("GetTransactions");
                    break;
                default:
                    throw new QueryGenerator.Exceptions.QuerySyntaxException(searchType);
            }

            return search;
        }


        private static string GetSqlStatement(string name) {
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

        internal static string GetPropertySearch(string property, string token) {
            string query = "";
            try {
                query = GetSqlStatement("By" + Char.ToUpper(property[0]) + property.Substring(1));
            } catch {
                throw new QueryException($"Cannot load resource for property '{property}'");
            }

            return query.Replace("@", token);

            //switch (property) {
            //    case "processName":
            //        query = GetSqlStatement("ByProcessName");
            //        break;
            //    case "clientName":
            //        query = ClientName;
            //        break;
            //    case "transactionType":
            //        query = TransactionType;
            //        break;
            //    case "startDate":

            //        break;
            //    case "endDate":

            //        break;
            //    case "startTime":

            //        break;
            //    case "endTime":

            //        break;
            //    case "minTime":

            //        break;
            //    case "maxTime":

            //        break;
            //    case "pingOptions":

            //        break;
            //    case "failed":

            //        break;
            //    case "serverName":

            //        break;
            //    case "sessionId":

            //        break;
            //    case "ignore":

            //        break;
            //    case "policyNumber":

            //        break;
            //    case "referralNumber":

            //        break;
            //    case "csr":

            //        break;
            //    case "referralDate":

            //        break;
            //    case "zipCode":

            //        break;
            //    case "promoCode":

            //        break;
            //    case "creditCardNumber":

            //        break;
            //    case "ctu":

            //        break;
            //    case "authorizationCode":

            //        break;
            //    case "accountNumber":

            //        break;
            //    case "orderID":

            //        break;
            //    case "invoiceNumber":

            //        break;
            //    case "genericSearchString":

            //        break;
            //    case "claimNumber":

            //        break;
            //    case "fnolNumber":

            //        break;
            //    case "subcompany":

            //        break;
            //    case "partNumber":

            //        break;
            //    case "carID":

            //        break;
            //    case "amount":

            //        break;
            //    case "workOrderNumber":

            //        break;
            //    case "workOrderID":

            //        break;
            //    case "warehouseNumber":

            //        break;
            //    case "shopNumber":

            //        break;
            //    case "checkpoint":

            //        break;
            //}
        }
    }
}
