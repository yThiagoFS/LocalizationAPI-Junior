using BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.RegisterDto;
using Flunt.Notifications;
using Flunt.Validations;

namespace BaltaIoChallenge.WebApi.Specifications.v1
{
    public static class RegisterUserRequestSpecifitcation
    {
        public static Contract<Notification> Ensure(RegisterUserRequestDto request)
            => new Contract<Notification>()
                .Requires()
                .IsLowerOrEqualsThan(request.Name.Length, 60, "Name", "Name is too large.")
                .IsGreaterOrEqualsThan(request.Name.Length, 3, "Name", "Name is invalid.")
                .IsLowerOrEqualsThan(request.Password.Length, 40, "Password", "Password cannot contain more than 40 characters.")
                .IsGreaterOrEqualsThan(request.Password.Length, 4, "Password", "Password must be bigger than 4 characters")
                .IsEmail(request.EmailAddress, "Email", "Invalid email.")
                .IsNotNullOrEmpty(request.Role, "Role", "Role cannot be null");
    }
}
