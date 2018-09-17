using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubPortal.Api.Controllers {

    public class SuccessController : Controller {
        // DELETE api/<controller>/5

        public void Delete(int id) {
        }

        // GET: api/<controller>

        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5

        public string Get(int id) {
            return "value";
        }

        // POST api/<controller>

        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5

        public void Put(int id, [FromBody]string value) {
        }
    }
}
