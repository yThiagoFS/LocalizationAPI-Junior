using BaltaIoChallenge.WebApi.Models.v1.Entities;
using System.Security.Claims;

namespace BaltaIoChallenge.WebApi.Extensions.v1
{
    public static class UserExtensions
    {
        public static ClaimsIdentity GenerateUserClaims(this User user)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("Id", user.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.EmailAddress));
            claimsIdentity.AddClaims(user.Roles.Select(us => new Claim(ClaimTypes.Role, us.Name)));

            return claimsIdentity;
        }
    }
}
