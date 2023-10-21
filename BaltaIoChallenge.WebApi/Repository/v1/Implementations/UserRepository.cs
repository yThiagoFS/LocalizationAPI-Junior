using BaltaIoChallenge.WebApi.Data.v1.Contexts;
using BaltaIoChallenge.WebApi.Models.v1.Entities;
using BaltaIoChallenge.WebApi.Repository.v1.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace BaltaIoChallenge.WebApi.Repository.v1.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        public async Task RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string emailAddress)
            => await _context.Users.AnyAsync(us => us.EmailAddress == emailAddress);

        public async Task<User?> GetUserByEmailAsync(string emailAddress)
            => await _context
                    .Users
                    .AsNoTracking()
                    .Include(us => us.Roles)
                    .SingleOrDefaultAsync(us => us.EmailAddress == emailAddress);
    }
}
