using System.Collections.Generic;
using System.Linq;
using HubPortal.Data.Models;
using HubPortal.Data;
using Microsoft.AspNetCore.Mvc;
using HubPortal.Api.Utilities;

namespace HubPortal.Api.Controllers {

    [Route("api/[controller]")]
    public class TransactionController : Controller {

        /// <summary>
        /// Given raw form data with fields matching <see
        /// cref="HubPortal.Api.Models.TransactionLookupData"/>, this call will return a list of
        /// transactions that satisfy the criteria specified in the posted form.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("FindTransactions")]
        public JsonResult FindTransactions([FromBody]TransactionLookupData data) {
            List<Transaction> transactions = TransactionLookupRequestParser.GetTransactions(data).ToList();
            transactions.RemoveAll(transaction => transaction == null);
            return Json(transactions);
        }

        [HttpGet]
        public string Get() {
            return "The API is up and running";
        }

        /// <summary>
        /// Returns a list of the names of all Transaction Types in the database.
        /// </summary>
        /// <returns>List of Transaction Type names</returns>
        [HttpGet("GetTypes")]
        public IEnumerable<string> GetTypes() {
            List<string> transactionTypes = TransactionEngine.GetTransactionTypeList().ToList();
            transactionTypes.RemoveAll(transactionType => transactionType == null);
            return transactionTypes;
        }
    }
}