using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubPortal.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubPortal.Api.Controllers {

    [Route("api/[controller]")]
    public class ProcessController : Controller {

        /// <summary>
        /// Returns a list of the names of all Processes in the database.
        /// </summary>
        /// <returns>List of Process names</returns>
        [HttpGet("Get")]
        public IEnumerable<string> Get() {
            List<string> processNames = ProcessEngine.GetProcessNameList().ToList();
            processNames.RemoveAll(process => process == null);
            return processNames;
        }
    }
}