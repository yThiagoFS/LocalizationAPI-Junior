using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.LoginDto;
using BaltaIoChallenge.WebApi.Models.v1.Entities;
using BaltaIoChallenge.WebApi.Repository.v1.Contracts;
using BaltaIoChallenge.WebApi.Services.v1.Auth.Contracts;
using BaltaIoChallenge.WebApi.Services.v1.Token;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace BaltaIoChallenge.WebApi.Services.v1.Auth.Implementations.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHandler _tokenHandler;
        private const string UserNotFoundMessage = "We coudn't find any account with the informations provided. Please, check your email or password.";
        public LoginService(
            IUserRepository userRepository
            , TokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            User? user = await GetUser(request);

            if (user is null)
                return new ResponseDto<LoginResponseDto>(UserNotFoundMessage, 404);

            if (!PasswordHasher.Verify(user.Password, request.Password))
            {
                return new ResponseDto<LoginResponseDto>(UserNotFoundMessage, 404);
            }

            var token = _tokenHandler.Generate(user);

            return new ResponseDto<LoginResponseDto>("Login successful!", new LoginResponseDto(user.Id, user.Name, user.EmailAddress, token), 200);
        }

        private async Task<User?> GetUser(LoginRequestDto request)
        {
            try
            {
                User? user = await _userRepository.GetUserByEmailAsync(request.EmailAddress);

                return user;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while searching for the user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }
    }
}
