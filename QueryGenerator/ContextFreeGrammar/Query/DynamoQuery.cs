using System;

using System.Collections.Generic;
using System.Text;

namespace HubPortal.QueryGenerator.ContextFreeGrammar.Query {

    internal class DynamoQuery : BaseQuery {

        public DynamoQuery(string query, string queryType) : base(query, queryType) {
        }

        public override IEnumerable<T> Execute<T>() {
            throw new System.NotImplementedException();
        }

        public override void ExecuteNonQuery() {
            throw new System.NotImplementedException();
        }
    }
}
