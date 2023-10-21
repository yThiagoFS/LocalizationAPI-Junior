using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.LoginDto;

namespace BaltaIoChallenge.WebApi.Services.v1.Auth.Contracts
{
    public interface ILoginService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    }
}
