using Microsoft.AspNetCore.Mvc;
using GameLog.Services;
using GameLog.DTOs;

namespace GameLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioResponseDTO>> Registrar([FromBody] UsuarioRegistroDTO dto)
        {
            try
            {
                var usuario = await _service.Registrar(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioResponseDTO>> Login([FromBody] UsuarioLoginDTO dto)
        {
            try
            {
                var usuario = await _service.Login(dto);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> ObterPorId(int id)
        {
            try
            {
                var usuario = await _service.ObterPorId(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}