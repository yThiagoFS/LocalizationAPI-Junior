using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.SearchLocalizationDto;

namespace BaltaIoChallenge.WebApi.Services.v1.Localization.Contracts
{
    public interface ISearchLocalizationService
    {
        Task<ResponseDto<SearchLocalizationResponseDto>> SearchByCodeAsync(string code);

        Task<ResponseDto<List<SearchLocalizationResponseDto>>> SearchByStateAsync(string state);

        Task<ResponseDto<List<SearchLocalizationResponseDto>>> SearchByCityAsync(string city);
    }
}
