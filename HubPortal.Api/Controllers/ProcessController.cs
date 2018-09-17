using System;
using System.Collections.Generic;
using System.Linq;

using HubPortal.Data;
using HubPortal.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubPortal.Api.Controllers {

    public class ProcessController : Controller {

        public string DisablePing(string processName) {
            ProcessEngine.UpdatePingEnabled(processName, "N");
            return "finished";
        }

        public Process EnablePing(string processName) {
            Process enabledProcess = ProcessEngine.UpdatePingEnabled(processName, "Y");
            return enabledProcess;
        }

        /// <summary>
        /// Returns a list of the names of all Processes in the database.
        /// </summary>
        /// <returns>List of Process names</returns>
        public IEnumerable<string> Get() {
            List<string> processNames = ProcessEngine.GetProcessNameList().ToList();
            processNames.RemoveAll(process => process == null);
            return processNames;
        }

        public JsonResult GetActiveProcesses() {
            return Json(ProcessEngine.GetActiveProcesses());
        }

        public JsonResult GetInactiveProcesses() {
            return Json(ProcessEngine.GetInactiveProcesses());
        }
    }
}
