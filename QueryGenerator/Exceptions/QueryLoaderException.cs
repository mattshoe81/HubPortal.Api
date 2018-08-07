namespace HubPortal.QueryGenerator.Exceptions {

    internal class QueryLoaderException : QueryException {

        #region Public Constructors

        public QueryLoaderException() : base() {
        }

        public QueryLoaderException(string message) : base($"Unable to load resource for '{message}'") {
        }

        #endregion Public Constructors
    }
}