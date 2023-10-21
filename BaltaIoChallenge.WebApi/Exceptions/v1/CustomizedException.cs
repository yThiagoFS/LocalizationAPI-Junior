namespace BaltaIoChallenge.WebApi.Exceptions.v1
{
    public class CustomizedException : Exception
    {
        public CustomizedException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
