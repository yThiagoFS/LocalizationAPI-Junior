using BaltaIoChallenge.WebApi.Models.ValueObjects;

namespace BaltaIoChallenge.WebApi.Models.v1.Entities
{
    public class User : Base
    {
        protected User() { }

        public User(string name, string password, string emailAddress)
        {
            Name = name;
            Password = password;
            EmailAddress = emailAddress;
        }

        public string Name { get; }

        public string Password { get; }

        public string EmailAddress { get; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public List<Role> Roles { get; set; } = new();
    }
}
