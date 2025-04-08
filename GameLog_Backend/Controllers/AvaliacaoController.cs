using Microsoft.AspNetCore.Mvc;
using GameLog.Services;
using GameLog.DTOs;

namespace GameLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly AvaliacaoService _service;

        public AvaliacaoController(AvaliacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<AvaliacaoResponseDTO>> Criar(
            [FromQuery] int usuarioId,
            [FromBody] AvaliacaoCreateDTO dto)
        {
            try
            {
                var avaliacao = await _service.CriarAvaliacao(usuarioId, dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = avaliacao.Id }, avaliacao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AvaliacaoResponseDTO>> ObterPorId(int id)
        {
            try
            {
                var avaliacao = await _service.ObterAvaliacaoResponse(id);
                if (avaliacao == null) return NotFound();
                return Ok(avaliacao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}