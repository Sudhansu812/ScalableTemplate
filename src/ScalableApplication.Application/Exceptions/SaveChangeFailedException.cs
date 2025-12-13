namespace ScalableApplication.Application.Exceptions
{
    public class SaveChangeFailedException : Exception
    {
        public SaveChangeFailedException() : base() { }
        public SaveChangeFailedException(string message) : base($"{message} failed to save.") { }
    }
}
