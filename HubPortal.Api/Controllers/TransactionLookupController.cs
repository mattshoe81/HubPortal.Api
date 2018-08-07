using HubPortal.Api.DataAccess;
using HubPortal.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HubPortal.Api.Controllers {

    [Route("api/[controller]")]
    public class TransactionLookupController : Controller {

        #region Dummy Data

        public IEnumerable<string> dummyList = new List<string> {
            "first dummy string",
            "first dummy string",
            "first dummy string",
            "second very long string to test the way bootstrap will handle this dummy string",
            "third dummy string",
            "fourth dummy string"
        };

        #endregion Dummy Data

        #region API Methods

        /// <summary>
        /// Given raw form data with fields matching <see
        /// cref="HubPortal.Api.Models.TransactionLookupData"/>, this call will return the same <see
        /// cref="HubPortal.Api.Models.TransactionLookupData"/> object with the transactions field
        /// populated with a list of all transactions in the database that satisfy the criteria
        /// specified in the posted form.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("Post")]
        public JsonResult FindTransactions([FromBody]TransactionLookupData data) {
            data.Transactions = TransactionRequestParser.GetTransactions(data);

            return Json(data);
        }

        // GET: api/<controller>
        [HttpGet]
        public string Get() {
            return "The API is up and running";
        }

        /// <summary>
        /// Returns a list of the names of all Clients in the database.
        /// </summary>
        /// <returns>List of Client Names</returns>
        [HttpGet("GetClientNames")]
        public IEnumerable<string> GetClientNames() {
            return TransactionLookupEngine.GetClientList();
        }

        /// <summary>
        /// Returns a list of the names of all Processes in the database.
        /// </summary>
        /// <returns>List of Process names</returns>
        [HttpGet("GetProcessNames")]
        public IEnumerable<string> GetProcessNames() {
            return TransactionLookupEngine.GetProcessNameList();
        }

        /// <summary>
        /// Returns a list of the names of all Transaction Types in the database.
        /// </summary>
        /// <returns>List of Transaction Type names</returns>
        [HttpGet("GetTransactionTypes")]
        public IEnumerable<string> GetTransactionTypes() {
            return TransactionLookupEngine.GetTransactionTypeList();
        }

        #endregion API Methods
    }
}