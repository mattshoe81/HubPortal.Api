using System;
using System.Collections.Generic;
using System.Text;

namespace HubPortal.QueryGenerator.Exceptions
{
    class QuerySyntaxException : QueryException {

        public QuerySyntaxException() { }

        public QuerySyntaxException(string message) : base(message) { }

        public QuerySyntaxException(string found, string expected) : base($"Expected '{expected}', but found '{found}'") { }

    }
}
