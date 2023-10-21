using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.SearchLocalizationDto;
using BaltaIoChallenge.WebApi.Repository.v1.Contracts;
using BaltaIoChallenge.WebApi.Services.v1.Localization.Contracts;

namespace BaltaIoChallenge.WebApi.Services.v1.Localization.Implementations.SearchLocalization
{
    public class SearchLocalizationService : ISearchLocalizationService
    {
        private readonly ILocalizationRepository _localizationRepository;

        public SearchLocalizationService(ILocalizationRepository localizationRepository)
            => _localizationRepository = localizationRepository;

        public async Task<ResponseDto<List<SearchLocalizationResponseDto>>> SearchByCityAsync(string city)
        {
            List<SearchLocalizationResponseDto> response = new();

            var localization = await _localizationRepository.GetByCityAsync(city);

            if (localization is null || !localization.Any())
                return new ResponseDto<List<SearchLocalizationResponseDto>>("Localization not found", 404);
                
            response.AddRange(localization.Select(l => new SearchLocalizationResponseDto(l.Id, l.State, l.City)));

            return new ResponseDto<List<SearchLocalizationResponseDto>>("Localization found", response, 200);
        }

        public async Task<ResponseDto<SearchLocalizationResponseDto>> SearchByCodeAsync(string code)
        {
            var localization = await _localizationRepository.GetByCodeAsync(code);

            if (localization is null)
                return new ResponseDto<SearchLocalizationResponseDto>("Localization not found", 404);

            return new ResponseDto<SearchLocalizationResponseDto>(
                    "Localization found"
                    ,new SearchLocalizationResponseDto(localization.Id, localization.State, localization.City)
                    ,200);
        }

        public async Task<ResponseDto<List<SearchLocalizationResponseDto>>> SearchByStateAsync(string state)
        {
            List<SearchLocalizationResponseDto> response = new();

            var localization = await _localizationRepository.GetByStateAsync(state);

            if (localization is null || !localization.Any())
                return new ResponseDto<List<SearchLocalizationResponseDto>>("Localization not found", 404);

            response.AddRange(localization.Select(l => new SearchLocalizationResponseDto(l.Id, l.State, l.City)));

            return new ResponseDto<List<SearchLocalizationResponseDto>>("Localization found", response, 200);
        }
    }
}
