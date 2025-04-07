using AutoMapper;
using GameLog.Models;
using GameLog_Backend.Database;
using GameLog_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLog.Services
{
    public class AvaliacaoService
    {
        private readonly GameLogContext _context;
        private readonly IMapper _mapper;

        public AvaliacaoService(GameLogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Avaliacao> CriarAvaliacao(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();
            return avaliacao;
        }
    }
}