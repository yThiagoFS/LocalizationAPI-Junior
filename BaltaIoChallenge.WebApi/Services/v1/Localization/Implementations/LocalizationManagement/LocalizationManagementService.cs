using BaltaIoChallenge.WebApi.Exceptions.v1;
using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.LocalizationManagementDto;
using BaltaIoChallenge.WebApi.Models.v1.Entities;
using BaltaIoChallenge.WebApi.Repository.v1.Contracts;
using BaltaIoChallenge.WebApi.Services.v1.Localization.Contracts;
using BaltaIoChallenge.WebApi.Specifications.v1;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;

namespace BaltaIoChallenge.WebApi.Services.v1.Localization.Implementations.LocationManagement
{
    public class LocalizationManagementService : ILocalizationManagementService
    {
        private readonly ILocalizationRepository _localizationRepository;

        public LocalizationManagementService(ILocalizationRepository localizationRepository)
            => _localizationRepository = localizationRepository;

        public async Task<ResponseDto<LocalizationManagementResponseDto>> CreateLocalizationAsync(LocalizationManagementRequestDto request)
        {
            await ValidateLocalizationAsync(request);
            var newLocalization = await SaveLocalizationAsync(request);

            return new ResponseDto<LocalizationManagementResponseDto>(
                "Localization created successfully"
                , new LocalizationManagementResponseDto(newLocalization.Id, newLocalization.State, newLocalization.City));
        }

        public async Task<ResponseDto<LocalizationManagementResponseDto>> UpdateLocalizationByCodeAsync(string code, UpdateLocalizationRequestDto request)
        {
            if (string.IsNullOrEmpty(code))
                throw new SpecificationException("id must have a value.");

            var localizationUpdated = await UpdateByCodeAsync(code, request);

            if (localizationUpdated is null)
                return new ResponseDto<LocalizationManagementResponseDto>(
                    "Localization not found"
                    , 404);

            return new ResponseDto<LocalizationManagementResponseDto>(
                "Localization updated successfully!"
                , new LocalizationManagementResponseDto(localizationUpdated.Id, localizationUpdated.State, localizationUpdated.City));
        }

        public async Task<ResponseDto<LocalizationManagementResponseDto>> DeleteLocalizationByCodeAsync(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                    throw new SpecificationException("id must have a value.");

                var localization = await _localizationRepository.GetByCodeAsync(code);

                if (localization is null)
                    return new ResponseDto<LocalizationManagementResponseDto>("Localization not found.", 404);

                await _localizationRepository.DeleteAsync(localization);

                return new ResponseDto<LocalizationManagementResponseDto>("Localization deleted successfully.", 200);
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException($"Failed to delete the localization.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wront while trying to create the localization: {ex.Message}.");
            }
        }

        private async Task<IBGE> SaveLocalizationAsync(LocalizationManagementRequestDto request)
        {
            try
            {
                var newLocalization = new IBGE(request.Id, request.State.ToUpper(), request.City);

                await _localizationRepository.CreateAsync(newLocalization);

                return newLocalization;
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException($"Failed to create the localization.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wront while trying to create the localization: {ex.Message}.");
            }
        }

        private async Task<IBGE> UpdateByCodeAsync(string code, UpdateLocalizationRequestDto request)
        {
            var actualLocalization = await _localizationRepository.GetByCodeAsync(code);

            if (actualLocalization is not null)
            {
                if (!string.IsNullOrEmpty(request.State))
                {
                    ValidateState(request.State);
                    actualLocalization.State = request.State!;
                }

                if (!string.IsNullOrEmpty(request.City))
                {
                    ValidateCity(request.City);
                    actualLocalization.City = request.City!;
                }

                await _localizationRepository.UpdateAsync(actualLocalization);

                return actualLocalization;
            }
            return null!;
        }

        private async Task ValidateLocalizationAsync(LocalizationManagementRequestDto request)
        {
            var validator = LocalizationManagementRequestSpecification.Ensure(request);

            if (!validator.IsValid)
            {
                var errors = new StringBuilder();

                foreach (var notification in validator.Notifications)
                    errors.Append($"{notification.Key}: {notification.Message}");

                throw new SpecificationException($"{string.Join(System.Environment.NewLine, errors)}");
            }

            var localizationExists = await _localizationRepository.LocalizationExistsAsync(request.Id);

            if (localizationExists)
                throw new ObjectExistsException("Localization already exists.");
        }

        private static void ValidateState(string state)
        {
            var errors = new StringBuilder();

            if (state.Length != 2)
                errors.Append("State: State should only have 2 characters.");
            else if (!Regex.IsMatch(state, "^[a-zA-Z]*$"))
                errors.Append("State: State must have only letters.");

            if (errors.Length > 0)
                throw new SpecificationException($"{string.Join(System.Environment.NewLine, errors)}");
        }

        private static void ValidateCity(string city)
        {
            if (!Regex.IsMatch(city, "^[^0-9]+$"))
                throw new SpecificationException("City: City must have only letters, accent or '-'.");
        }
    }
}
