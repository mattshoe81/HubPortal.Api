using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubPortal.Api.Controllers {

    public class PingController : Controller {

        public JsonResult Test(string processName) {
            string query = $"select p.PING_NAME, s.PING_TYPE, s.CLIENT_ID, s.TYPE, s.PING_STRING, s.EXPECTED_RESULT, s.PING_ENABLED from PING_STRING s, hts_process p where p.ping_name = s.PROCESS_NAME AND p.PROCESS_NAME = '{processName}'";
            return Json("success");
        }
    }
}
