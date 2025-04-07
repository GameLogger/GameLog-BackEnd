using Microsoft.AspNetCore.Mvc;
using GameLog.Models;
using GameLog.Services;
using AutoMapper;
using GameLog_Backend.Entities;

namespace GameLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(UsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        // POST: api/usuario/registrar
        [HttpPost("registrar")]
        public async Task<ActionResult<Usuario>> Registrar(
            [FromBody] Usuario usuario,
            [FromQuery] string senha)
        {
            try
            {
                // Usa o AutoMapper para copiar dados se necessário (ex: atualizações futuras)
                var usuarioRegistro = _mapper.Map<Usuario>(usuario);
                var usuarioCriado = await _usuarioService.Registrar(usuarioRegistro);

                return CreatedAtAction(nameof(ObterPorId), new { id = usuarioCriado.Id }, usuarioCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/usuario/login
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login(
            [FromQuery] string email,
            [FromQuery] string senha)
        {
            try
            {
                var usuario = await _usuarioService.Login(email, senha);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObterPorId(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObterPorId(id);
                if (usuario == null)
                    return NotFound("Usuário não encontrado");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}