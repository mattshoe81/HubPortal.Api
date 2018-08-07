using System;

namespace HubPortal.QueryGenerator.Exceptions {

    /// <summary>
    /// Base class for all Query exceptions
    /// </summary>
    internal abstract class QueryException : Exception {

        public QueryException() {
        }

        public QueryException(string message) : base(message) {
        }
    }
}