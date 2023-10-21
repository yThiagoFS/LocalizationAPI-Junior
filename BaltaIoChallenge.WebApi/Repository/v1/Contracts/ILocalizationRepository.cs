using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.SearchLocalizationDto;
using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Entities;
using System;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.LocalizationManagementDto;

namespace BaltaIoChallenge.WebApi.Repository.v1.Contracts
{
    public interface ILocalizationRepository
    {
        Task<IBGE?> GetByCodeAsync(string code);

        Task<List<IBGE>?> GetByStateAsync(string state);

        Task<List<IBGE>?> GetByCityAsync(string city);

        Task CreateAsync(IBGE localization);

        Task<bool> LocalizationExistsAsync(string code);

        Task UpdateAsync(IBGE localization);

        Task DeleteAsync(IBGE localization);
    }
}
