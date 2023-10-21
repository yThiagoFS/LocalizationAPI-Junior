namespace BaltaIoChallenge.WebApi.Models.v1.Entities
{
    public class IBGE
    {
        protected IBGE() { }

        public IBGE(string id, string state, string city)
        {
            Id = id;
            State = state;
            City = city;
        }

        public string Id { get; set; }

        public string State { get; set; }

        public string City { get; set; }
    }
}
