namespace BaltaIoChallenge.WebApi.Exceptions.v1
{
    public class ObjectExistsException : CustomizedException
    {
        const string DefaultMessage = "Object already exists";

        public ObjectExistsException(string errorMessage = DefaultMessage) : base(errorMessage) { }
    }
}
