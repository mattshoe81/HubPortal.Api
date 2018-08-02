using HubPortal.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubPortal.Api.DataAccess
{
    public class TransactionLookupParser {
        public TransactionLookupData SearchData { get; set; }

        private string startTimeAppendix = "AND ";

        public TransactionLookupParser() {

        }

        public TransactionLookupParser(TransactionLookupData data) {
            this.SearchData = data;
        }

        public static IEnumerable<Transaction> GetTransactions(TransactionLookupData searchData) {
            if (searchData.SearchType == "" || searchData.SearchType == null) throw new Exception("Search Type not defined");
            IEnumerable<Transaction> transactions = new List<Transaction>();
            switch (searchData.SearchType) {
                case "process":
                    if (searchData.Process == "All") {
                        transactions = TransactionLookupEngine.GetAllTransactions();
                    } else {
                        transactions = TransactionLookupEngine.GetTransactionsByProcess(searchData.Process);
                    }
                    
                    break;
                case "client":
                    if (searchData.Client == "All") {
                        transactions = TransactionLookupEngine.GetAllTransactions();
                    }
                    transactions = TransactionLookupEngine.GetTransactionsByClient(searchData.Client);
                    break;
                case "source":
                    transactions = TransactionLookupEngine.GetTransactionsByProcess(searchData.Process);
                    break;
                default:
                    break;
            }

            return transactions;


        }
    }
}
