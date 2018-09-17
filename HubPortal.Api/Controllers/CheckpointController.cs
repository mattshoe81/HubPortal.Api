using System.Collections.Generic;
using HubPortal.Data;
using HubPortal.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubPortal.Api.Controllers {

    public class CheckpointController : Controller {
        // GET: api/<controller>

        public JsonResult Get(string transactionid) {
            IEnumerable<Checkpoint> checkpoints = CheckpointEngine.GetCheckpoints(transactionid);
            return Json(checkpoints);
        }

        public JsonResult GetEmbeddedMessage(string checkpointid, string location) {
            return Json("the embedded message for the checkpoint");
        }

        public JsonResult GetMessage(string checkpointid) {
            return Json(new string[] { "the message for the checkpoint" });
        }
    }
}
