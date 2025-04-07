using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using GameLog.Models;
using GameLog.Services;
using GameLog_Backend.Entities;

namespace GameLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly AvaliacaoService _avaliacaoService;
        private readonly IMapper _mapper;

        public AvaliacaoController(AvaliacaoService avaliacaoService, IMapper mapper)
        {
            _avaliacaoService = avaliacaoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Avaliacao>> CriarAvaliacao([FromBody] Avaliacao avaliacao)
        {
            var novaAvaliacao = await _avaliacaoService.CriarAvaliacao(avaliacao);
            return Ok(novaAvaliacao);
        }
    }
}