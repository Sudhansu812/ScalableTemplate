namespace ScalableApplication.Application.Exceptions
{
    public class EnvironmentVariableNotFoundException : Exception
    {
        public EnvironmentVariableNotFoundException() : base() { }
        public EnvironmentVariableNotFoundException(string message) : base(message) { }
    }
}
