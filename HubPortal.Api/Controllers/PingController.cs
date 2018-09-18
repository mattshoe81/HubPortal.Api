using HubPortal.Data;
using HubPortal.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubPortal.Api.Controllers {

    public class PingController : Controller {

        public JsonResult Test(string processName) {
            Ping ping = PingEngine.GetPing(processName);
            return Json(ping);
        }
    }
}
