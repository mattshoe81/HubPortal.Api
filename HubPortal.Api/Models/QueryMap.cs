using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubPortal.Api.Models
{
    public class QueryMap {

        private string source = "AND cl.";

        private Dictionary<TransactionLookupProperties, string> queryMap;

        private Dictionary<TransactionLookupProperties, string> propertyMap;

        public QueryMap() {
            this.propertyMap = new Dictionary<TransactionLookupProperties, string>();
            this.queryMap = new Dictionary<TransactionLookupProperties, string>();
        }

        public void Add(TransactionLookupProperties property, string value) {
            this.propertyMap.Add(property, value);
        }

        public string GetValue(TransactionLookupProperties property) {
            return this.propertyMap[property];
        }

        public string GetQueryAppendix(TransactionLookupProperties property) {
            string query = this.queryMap[property];
            return query.Replace("@value", this.propertyMap[property]);
        }

    }
}
