namespace HubPortal.QueryGenerator.Exceptions {

    internal class QueryLoaderException : QueryException {

        public QueryLoaderException() : base() {
        }

        public QueryLoaderException(string message) : base($"Unable to load resource for '{message}'") {
        }
    }
}