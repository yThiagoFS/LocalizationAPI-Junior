namespace BaltaIoChallenge.WebApi.Repository.v1.Contracts
{
    public interface IRoleRepository
    {
        Task<bool> RoleExistsAsync(string roleName);
    }
}
