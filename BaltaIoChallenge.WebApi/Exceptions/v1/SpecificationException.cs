namespace BaltaIoChallenge.WebApi.Exceptions.v1
{
    public class SpecificationException : CustomizedException
    {
        const string DefaultMessage = "Something went wrong while trying to validate your request. Please, try again later.";

        public SpecificationException(string errorMessage = DefaultMessage) : base(errorMessage) { }
    }
}
