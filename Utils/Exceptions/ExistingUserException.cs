namespace RssApi.Utils.Exceptions
{
    public class ExistingUserException : Exception
    {
        public ExistingUserException(string? message) : base(message)
        {
        }
    }
}