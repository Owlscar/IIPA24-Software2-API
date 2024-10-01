using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Software2_API.Dtos;
using Software2_API.Services;

namespace Software2_API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController (UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/api/[Controller]/EstadoApi")]
        public async Task<IActionResult> EstadoApi()
        {
            ResponseGeneralDto resposeGeneralDto = new()
            {
                Respuesta = 200,
                Mensaje = "API EN EJECUCION CORRECTA"
            };
            return Ok(resposeGeneralDto);
        }

        [HttpPost]
        [Route("/api/[Controller]/InicioSesion")]
        [AllowAnonymous]
        public async Task<IActionResult> PostIniciarSesion([FromBody] InicioSesionDto requestInicioSesionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.InicioSesion(requestInicioSesionDto));
        }

        [HttpPost]
        [Route("/api/[Controller]/CrearUsuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CrearUsuarios([FromBody] RequestUserDto requestUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseGeneralDto responseGeneralDto = await _userService.CrearUsuario(requestUserDto);

            return Ok(responseGeneralDto);
        }

        [HttpPost]
        [Route("/api/[Controller]/ObtenerUsuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsuarios()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.ObtenerUsuarios());
        }

        [HttpGet]
        [Route("/api/[Controller]/ReportePDF64")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetReportePDF64()
        {
            ResponseGeneralDto resposeGeneralDto = new();
            resposeGeneralDto.Respuesta = 201;
            resposeGeneralDto.Mensaje = await _userService.ObtenerReporte();
            return Ok(resposeGeneralDto);
        }

        [HttpGet]
        [Route("/api/[Controller]/ReporteAspose")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReporteAspose()
        {
            ResponseGeneralDto resposeGeneralDto = new();
            resposeGeneralDto.Respuesta = 201;
            resposeGeneralDto.Mensaje = await _userService.ObtenerReporteAspose();
            return Ok(resposeGeneralDto);
        }
    }
}
