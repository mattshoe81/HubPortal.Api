using HubPortal.Api.DataAccess;
using HubPortal.Api.Extensions;
using HubPortal.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubPortal.Api.Controllers {
    [Route("api/[controller]")]
    public class TransactionLookupController : Controller {
        private IEnumerable<string> dummyList = new List<string> {
            "first dummy string",
            "first dummy string",
            "first dummy string",
            "second very long string to test the way bootstrap will handle this dummy string",
            "third dummy string",
            "fourth dummy string"
        };

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
            data.Transactions = TransactionLookupParser.GetTransactions(data);


            return Json(data);
        }


    }
}
