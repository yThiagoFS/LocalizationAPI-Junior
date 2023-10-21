namespace BaltaIoChallenge.WebApi.Models.v1.Entities
{
    public class Role : Base
    {
        protected Role() { }

        public Role(string name) => Name = name;

        public string Name { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new();
    }
}
