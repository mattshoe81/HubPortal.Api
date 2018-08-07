namespace HubPortal.QueryGenerator.Exceptions {

    internal class QuerySyntaxException : QueryException {

        public QuerySyntaxException() {
        }

        public QuerySyntaxException(string message) : base(message) {
        }

        public QuerySyntaxException(string found, string expected) : base($"Expected '{expected}', but found '{found}'") {
        }
    }
}