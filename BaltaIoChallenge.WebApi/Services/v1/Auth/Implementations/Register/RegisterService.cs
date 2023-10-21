using BaltaIoChallenge.WebApi.Exceptions;
using BaltaIoChallenge.WebApi.Exceptions.v1;
using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.RegisterDto;
using BaltaIoChallenge.WebApi.Models.v1.Entities;
using BaltaIoChallenge.WebApi.Models.ValueObjects;
using BaltaIoChallenge.WebApi.Repository.v1.Contracts;
using BaltaIoChallenge.WebApi.Services.v1.Auth.Contracts;
using BaltaIoChallenge.WebApi.Specifications.v1;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using System.Text;

namespace BaltaIoChallenge.WebApi.Services.v1.Auth.Implementations.Register
{
    public class RegisterService : IRegisterService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public RegisterService(
            IUserRepository userRepository
            ,IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ResponseDto<RegisterUserResponseDto>> RegisterUserAsync(RegisterUserRequestDto request)
        {
            await ValidateUserAsync(request);

            var roleExists = await _roleRepository.RoleExistsAsync(request.Role);

            if(!roleExists)
                return new ResponseDto<RegisterUserResponseDto>("Role not found. Please, try to register as user or admin.", 404);

            var user = CreateUser(request);

            await SaveUserAsync(user);

            return new ResponseDto<RegisterUserResponseDto>("User registered successfully", new RegisterUserResponseDto(user.EmailAddress, user.Name), 200);
        }

        private async Task ValidateUserAsync(RegisterUserRequestDto request)
        {
            var validator = RegisterUserRequestSpecifitcation.Ensure(request);

            if (!validator.IsValid)
            {
                var errors = new StringBuilder();

                foreach (var notification in validator.Notifications)
                    errors.Append($"{notification.Key}: {notification.Message}");

                throw new SpecificationException($"{errors}");
            }

            var userExists = await _userRepository.UserExists(request.EmailAddress);

            if (userExists)
                throw new ObjectExistsException("User already exists. Please, try again with another email or try to sign in.");
        }

        private static User CreateUser(RegisterUserRequestDto request)
        {
            try
            {
                var email = new Email(request.EmailAddress);

                var passwordHash = PasswordHasher.Hash(request.Password);

                var user = new User(request.Name, passwordHash, email);
                user.Roles.Add(new Role(request.Role));

                return user;
            }
            catch (EmailException ex)
            {
                throw new EmailException(ex.Message);
            }
        }

        private async Task SaveUserAsync(User user)
        {
            try
            {
                await _userRepository.RegisterUserAsync(user);
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException("Failed to register the user.");
            }
            catch
            {
                throw new Exception("Something went wrong. Please, try again later.");
            }
        }
    }
}
