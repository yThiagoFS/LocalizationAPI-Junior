using BaltaIoChallenge.WebApi.Models.v1.Dtos;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.LocalizationManagementDto;
using BaltaIoChallenge.WebApi.Models.v1.Dtos.LocalizationDto.SearchLocalizationDto;
using BaltaIoChallenge.WebApi.Services.v1.Localization.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BaltaIoChallenge.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/localization")]
    public class LocalizationController : ControllerBase
    {
        /// <summary>
        /// Buscar localizações pelo estado
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/v1/localization/sp
        ///
        /// </remarks>
        /// <response code="200">Localização(ões) encontrada(s).</response>
        /// <response code="400">Erros de validações</response>
        /// <response code="404">Localização(ões) não encontrada(s)</response>
        /// <response code="500">Erros internos</response>
        /// <param name = "state">Estado(s) que será(ão) exibido(s)</param>
        [ProducesResponseType(typeof(ResponseDto<List<SearchLocalizationResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<List<SearchLocalizationResponseDto>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<List<SearchLocalizationResponseDto>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto<List<SearchLocalizationResponseDto>>), StatusCodes.Status500InternalServerError)]
        [HttpGet("getByState")]
        public async Task<IActionResult> GetByStateAsync(
            [FromServices] ISearchLocalizationService _searchLocalizationService
            , [FromQuery] string state)
        {
            var response = await _searchLocalizationService.SearchByStateAsync(state);

            if (response.Status == 404) return NotFound(response);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Buscar localizações pela cidade
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/v1/localization/osasco
        ///
        /// </remarks>
        /// <response code="200">Localização(ões) encontrada(s).</response>
        /// <response code="400">Erros de validações</response>
        /// <response code="404">Localização(ões) não encontrada(s)</response>
        /// <response code="500">Erros internos</response>
        /// <param name = "city">Cidade(s) que será(ão) exibida(s)</param>
        [ProducesResponseType(typeof(ResponseDto<List<SearchLocalizationResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<List<SearchLocalizationResponseDto>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<List<SearchLocalizationResponseDto>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto<List<SearchLocalizationResponseDto>>), StatusCodes.Status500InternalServerError)]
        [HttpGet("getByCity")]
        public async Task<IActionResult> GetByCityAsync(
             [FromServices] ISearchLocalizationService _searchLocalizationService
             , [FromQuery] string city)
        {
            var response = await _searchLocalizationService.SearchByCityAsync(city);

            if (response.Status == 404) return NotFound(response);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Buscar uma localização pelo código
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/v1/localization/1
        ///
        /// </remarks>
        /// <response code="200">Localização encontrada.</response>
        /// <response code="400">Erros de validações</response>
        /// <response code="404">Localização não encontrada</response>
        /// <response code="500">Erros internos</response>
        /// <param name = "id">Código da localização a ser exibida</param>
        [ProducesResponseType(typeof(ResponseDto<SearchLocalizationResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<SearchLocalizationResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<SearchLocalizationResponseDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto<SearchLocalizationResponseDto>), StatusCodes.Status500InternalServerError)]
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] ISearchLocalizationService _searchLocalizationService
            , [FromRoute] string id)
        {
            var response = await _searchLocalizationService.SearchByCodeAsync(id);

            if (response.Status == (int)HttpStatusCode.NotFound) return NotFound(response);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Criar e inserir uma localização na base de dados
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/v1/localization
        ///     {
        ///        "id": 1001,
        ///        "state": "sp",
        ///        "city": "osasco"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Localização criada com uma mensagem de sucesso.</response>
        /// <response code="400">Erros de validações</response>
        /// <response code="500">Erros internos</response>
        /// <param name = "dto">Informações da localização que será adicionada</param>
        [ProducesResponseType(typeof(ResponseDto<SearchLocalizationResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<SearchLocalizationResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<SearchLocalizationResponseDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto<SearchLocalizationResponseDto>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateLocalization(
            [FromServices] ILocalizationManagementService _localizationManagementService
            , [FromBody] LocalizationManagementRequestDto dto)
        {
            var response = await _localizationManagementService.CreateLocalizationAsync(dto);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Atualizar uma localização existente
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/v1/localization/3
        ///     {
        ///        "state": "sp",
        ///        "city": "osasco"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Localização atualizada com uma mensagem de sucesso.</response>
        /// <response code="400">Erros de validações</response>
        /// <response code="404">Localização não encontrada</response>
        /// <response code="500">Erros internos</response>
        /// <param name = "id">Código da localização que será atualizada</param>
        /// <param name = "dto">Informações da localização que será atualizada</param>
        [ProducesResponseType(typeof(ResponseDto<LocalizationManagementResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<LocalizationManagementResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<LocalizationManagementResponseDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto<LocalizationManagementResponseDto>), StatusCodes.Status500InternalServerError)]
        [HttpPut("updateById/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateByIdAsync(
            [FromServices] ILocalizationManagementService _localizationManagementService
            , [FromRoute] string id
            , [FromBody] UpdateLocalizationRequestDto dto)
        {
            var response = await _localizationManagementService.UpdateLocalizationByCodeAsync(id, dto);

            if (response.Status == (int)HttpStatusCode.NotFound) return NotFound(response);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Excluir uma localização existente
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /api/v1/localization/3
        ///
        /// </remarks>
        /// <response code="200">Mensagem com a confirmação da exclusão.</response>
        /// <response code="400">Erros de validações</response>
        /// <response code="404">Localização não encontrada</response>
        /// <response code="500">Erros internos</response>
        /// <param name = "id">Código da localização que será excluida</param>
        [ProducesResponseType(typeof(ResponseDto<LocalizationManagementResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<LocalizationManagementResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<LocalizationManagementResponseDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto<LocalizationManagementResponseDto>), StatusCodes.Status500InternalServerError)]
        [HttpDelete("deleteById/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteByIdAsync(
            [FromServices] ILocalizationManagementService _localizationManagementService,
            [FromRoute] string id)
        {
            var response = await _localizationManagementService.DeleteLocalizationByCodeAsync(id);

            if (response.Status == (int)HttpStatusCode.NotFound) return NotFound(response);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
