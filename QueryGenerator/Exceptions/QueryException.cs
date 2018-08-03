using System;
using System.Collections.Generic;
using System.Text;

namespace HubPortal.QueryGenerator.Exceptions
{
    class QueryException : Exception {

        public QueryException() { }

        public QueryException(string message) : base(message) { }

    }
}
