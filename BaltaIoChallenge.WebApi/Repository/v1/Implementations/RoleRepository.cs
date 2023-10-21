using BaltaIoChallenge.WebApi.Data.v1.Contexts;
using BaltaIoChallenge.WebApi.Repository.v1.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BaltaIoChallenge.WebApi.Repository.v1.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context) => _context = context;

        public async Task<bool> RoleExistsAsync(string roleName)
            => await _context.Roles.AnyAsync(r => r.Name == roleName);
    }
}
