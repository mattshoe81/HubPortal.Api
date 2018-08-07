using System;

namespace HubPortal.QueryGenerator.Exceptions {

    internal class QueryException : Exception {

        public QueryException() {
        }

        public QueryException(string message) : base(message) {
        }
    }
}