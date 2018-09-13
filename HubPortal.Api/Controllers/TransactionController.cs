using System.Collections.Generic;
using System.Linq;

using HubPortal.Api.Utilities;
using HubPortal.Data;
using HubPortal.Data.Models;

using Microsoft.AspNetCore.Mvc;

namespace HubPortal.Api.Controllers {

    public class TransactionController : Controller {

        public string Get() {
            return "Proceed with the interwebbing";
        }

        public JsonResult GetById(string transactionid) {
            TransactionDetail detail = TransactionEngine.GetDetail(transactionid);
            return Json(detail);
        }

        /// <summary>
        /// Returns a list of the names of all Transaction Types in the database.
        /// </summary>
        /// <returns>List of Transaction Type names</returns>
        public IEnumerable<string> GetTypes() {
            List<string> transactionTypes = TransactionEngine.GetTransactionTypeList().ToList();
            transactionTypes.RemoveAll(transactionType => transactionType == null);
            return transactionTypes;
        }

        /// <summary>
        /// Given raw form data with fields matching <see
        /// cref="HubPortal.Api.Models.TransactionLookupData"/>, this call will return a list of
        /// transactions that satisfy the criteria specified in the posted form.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult PostData([FromBody]TransactionLookupData data) {
            List<Transaction> transactions = TransactionLookupRequestParser.GetTransactions(data).ToList();
            transactions.RemoveAll(transaction => transaction == null);
            return Json(transactions);
        }

        public JsonResult Query(string queryString) {
            List<Transaction> transactions = TransactionEngine.GetTransactions(queryString).ToList();
            transactions.RemoveAll(transaction => transaction == null);
            return Json(transactions);
        }

        public string Zip(string id) {
            //const string contentType = "application/zip";
            //this.HttpContext.Response.ContentType = contentType;
            //FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(@"{path_to_files}\file.zip"), contentType) {
            //    FileDownloadName = $"hubTransaction.{id}.zip"
            //};

            return $"Not Yet Implemented \n\nTransaction ID: '{id}'";
        }
    }
}
