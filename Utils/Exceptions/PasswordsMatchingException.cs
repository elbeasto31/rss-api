namespace RssApi.Utils.Exceptions
{
    public class PasswordsMatchingException : Exception
    {
        public PasswordsMatchingException(string password, string passwordConfirmation) 
            : base(string.Format(Messages.NoPasswordsMatching, password, passwordConfirmation))
        {
        }
    }
}