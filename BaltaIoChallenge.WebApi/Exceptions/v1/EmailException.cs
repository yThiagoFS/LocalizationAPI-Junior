namespace BaltaIoChallenge.WebApi.Exceptions.v1
{
    public class EmailException : CustomizedException
    {
        private const string DefaultMessage = "Invalid email.";

        public EmailException(string errorMessage = DefaultMessage) : base(errorMessage) { }
    }
}
