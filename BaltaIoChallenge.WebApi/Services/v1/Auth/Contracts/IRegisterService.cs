using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.RegisterDto;

namespace BaltaIoChallenge.WebApi.Services.v1.Auth.Contracts
{
    public interface IRegisterService
    {
        Task<ResponseDto<RegisterUserResponseDto>> RegisterUserAsync(RegisterUserRequestDto request);
    }
}
