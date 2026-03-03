namespace ScalableApplication.Application.Exceptions
{
    public class InvalidPaginationException : Exception
    {
        private const string _MESSAGE = "Invalid page number / size.";
        public InvalidPaginationException() : base(_MESSAGE) { }
        public InvalidPaginationException(string message) : base(message) { }
    }
}
