using BaltaIoChallenge.WebApi.Exceptions.v1;
using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.LoginDto;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.AuthDto.RegisterDto;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.SearchLocalizationDto;
using BaltaIoChallenge.WebApi.Services.v1.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using Humanizer;
using System.Net;

namespace BaltaIoChallenge.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Registar um usuário
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/v1/auth/register
        ///     {
        ///         "name":"thiago",
        ///         "emailAddress":"thiago123@gmail.com",
        ///         "password":"b8x!@7maz9*#=",
        ///         "role":"admin"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Usuário registrado com sucesso.</response>
        /// <response code="400">Erros de validações</response>
        /// <response code="400">Usuário já existe na base de dados</response>
        /// <response code="500">Erros internos</response>
        /// <param name = "dto">Informações do usuário a ser registrado</param>
        [ProducesResponseType(typeof(ResponseDto<RegisterUserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterService _registerService
            , RegisterUserRequestDto dto)
        {
            var response = await _registerService.RegisterUserAsync(dto);

            if (!response.IsSuccess) return BadRequest(response);

            return Ok(response);
            
            //return BadRequest(new ResponseDto<string>("Invalid email", ex.Message, 400));
        }

        /// <summary>
        /// Autenticar um usuário
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/v1/auth/login 
        ///     {
        ///         "emailAddress":"thiago123@gmail.com",
        ///         "password":"b8x!@7maz9*#=",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Usuário autenticado e token.</response>
        /// <response code="400">Erros de validações</response>
        /// <response code="404">Usuário não encontrado na base de dados</response>
        /// <response code="500">Erros internos</response>
        /// <param name = "dto">Informações do usuário a ser autenticado</param>
        [ProducesResponseType(typeof(ResponseDto<LoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<LoginResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<LoginResponseDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromServices] ILoginService _loginService
            , LoginRequestDto dto)
        {
            var response = await _loginService.LoginAsync(dto);

            if (response.Status == (int)HttpStatusCode.NotFound) return NotFound(response);

            if (!response.IsSuccess) return BadRequest(response);

            return Ok(response);
        }
    }
}
