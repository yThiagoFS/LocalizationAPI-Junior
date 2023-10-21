using Azure.Core;
using BaltaIoChallenge.WebApi.Data.v1.Contexts;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.LocalizationManagementDto;
using BaltaIoChallenge.WebApi.Models.v1.Entities;
using BaltaIoChallenge.WebApi.Repository.v1.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mono.TextTemplating;
using System;

namespace BaltaIoChallenge.WebApi.Repository.v1.Implementations
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly AppDbContext _context;

        public LocalizationRepository(AppDbContext context) => _context = context;

        public async Task<IBGE?> GetByCodeAsync(string code)
            => await _context
               .Ibge
               .AsNoTracking()
               .FirstOrDefaultAsync(l => l.Id == code);

        public async Task<List<IBGE>?> GetByCityAsync(string city)
        {
            var caseInsensitive = StringComparison.OrdinalIgnoreCase;

            var result = _context
                .Ibge
            .AsEnumerable()
            .Where(l => l.City.Contains(city, caseInsensitive) || l.City.StartsWith(city, caseInsensitive));

            return result.ToList();
        }

        public async Task<List<IBGE>?> GetByStateAsync(string state)
        {
            var caseInsensitive = StringComparison.OrdinalIgnoreCase;

            var result =  _context
                .Ibge
                .AsEnumerable()
                .Where(l => l.State.Contains(state, caseInsensitive) || l.State.StartsWith(state, caseInsensitive))
                .Distinct();

            return result.ToList();
        }

        public async Task CreateAsync(IBGE localization)
        {
            await _context.AddAsync(localization);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> LocalizationExistsAsync(string code)
            => await _context.Ibge.AnyAsync(i => i.Id == code);

        public async Task UpdateAsync(IBGE localization)
        {
            _context.Ibge.Update(localization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IBGE localization)
        {
            _context.Ibge.Remove(localization);
            await _context.SaveChangesAsync();
        }
    }
}
