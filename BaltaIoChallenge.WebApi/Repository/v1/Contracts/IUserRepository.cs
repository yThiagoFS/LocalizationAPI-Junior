using BaltaIoChallenge.WebApi.Models.v1.Entities;

namespace BaltaIoChallenge.WebApi.Repository.v1.Contracts
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(User user);

        Task<bool> UserExists(string emailAddress);

        Task<User?> GetUserByEmailAsync(string emailAddress);
    }
}
