using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.LocalizationManagementDto;

namespace BaltaIoChallenge.WebApi.Services.v1.Localization.Contracts
{
    public interface ILocalizationManagementService
    {
        Task<ResponseDto<LocalizationManagementResponseDto>> CreateLocalizationAsync(LocalizationManagementRequestDto request);

        Task<ResponseDto<LocalizationManagementResponseDto>> UpdateLocalizationByCodeAsync(string code, UpdateLocalizationRequestDto request);

        Task<ResponseDto<LocalizationManagementResponseDto>> DeleteLocalizationByCodeAsync(string code);
    }
}
