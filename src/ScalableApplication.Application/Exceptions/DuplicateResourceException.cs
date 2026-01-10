namespace ScalableApplication.Application.Exceptions
{
    public class DuplicateResourceException : Exception
    {
        public DuplicateResourceException() : base() { }
        public DuplicateResourceException(string message) : base($"{message} already exists.") { }
    }
}
