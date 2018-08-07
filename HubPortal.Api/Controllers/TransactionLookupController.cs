using HubPortal.Api.DataAccess;
using HubPortal.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HubPortal.Api.Controllers {

    [Route("api/[controller]")]
    public class TransactionLookupController : Controller {

        #region Dummy Data

        private IEnumerable<string> dummyList = new List<string> {
            "first dummy string",
            "first dummy string",
            "first dummy string",
            "second very long string to test the way bootstrap will handle this dummy string",
            "third dummy string",
            "fourth dummy string"
        };

        #endregion Dummy Data

        // GET: api/<controller>
        [HttpGet]
        public string Get() {
            return "The API is up and running";
        }

        [HttpGet("GetProcessNames")]
        public IEnumerable<string> GetProcessNames() {
            return TransactionLookupEngine.GetProcessNameList();
        }

        [HttpGet("GetClientNames")]
        public IEnumerable<string> GetClientNames() {
            return TransactionLookupEngine.GetClientList();
        }

        [HttpGet("GetTransactionTypes")]
        public IEnumerable<string> GetTransactionTypes() {
            return TransactionLookupEngine.GetTransactionTypeList();
        }

        [HttpPost("Post")]
        public JsonResult Post([FromBody]TransactionLookupData data) {
            data.Transactions = TransactionRequestParser.GetTransactions(data);

            return Json(data);
        }
    }
}

// This is reallllyyyyyyy sloooooowwwwwww
// How to filter high frequency transactiona?... Should that even be a thing?...
//if (data.Ignore) {
//    IQueryable<Transaction> trans = data.Transactions.AsQueryable();
//    data.Transactions = trans.Where(transaction => data.Transactions.Count(x => x.ProcessName == transaction.ProcessName) < 100).ToList();
//}