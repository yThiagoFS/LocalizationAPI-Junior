namespace BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.RegisterDto
{
    public record RegisterUserRequestDto(string Name, string EmailAddress, string Password, string Role);
}
