using BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.LoginDto;
using Flunt.Notifications;
using Flunt.Validations;

namespace BaltaIoChallenge.WebApi.Specifications.v1
{
    public static class LoginRequestSpecification
    {
        public static Contract<Notification> Ensure(LoginRequestDto request)
            => new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(request.Password.Length, 40, "Password", "Password cannot contain more than 40 characters.")
            .IsGreaterOrEqualsThan(request.Password.Length, 4, "Password", "Password must be bigger than 4 characters");
    }
}
