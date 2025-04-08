using GameLog_Backend.Entities;
using GameLog.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GameLog_Backend.Database;

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

        public async Task<AvaliacaoResponseDTO> CriarAvaliacao(int usuarioId, AvaliacaoCreateDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) throw new Exception("Usuário não encontrado");

            var jogo = await _context.Jogos.FindAsync(dto.JogoId);
            if (jogo == null) throw new Exception("Jogo não encontrado");

            var avaliacao = new Avaliacao
            {
                Nota = dto.Nota,
                TextoAvaliacao = dto.Comentario,
                Jogo = jogo,
                Usuario = usuario
            };

            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            return await ObterAvaliacaoResponse(avaliacao.Id);
        }

        public async Task<AvaliacaoResponseDTO> ObterAvaliacaoResponse(int id)
        {
            return await _context.Avaliacoes
                .Include(a => a.Usuario)
                .Include(a => a.Jogo)
                .Where(a => a.Id == id)
                .Select(a => new AvaliacaoResponseDTO
                {
                    Id = a.Id,
                    Nota = a.Nota,
                    Comentario = a.TextoAvaliacao,
                    JogoId = a.Jogo.Id,
                    Usuario = _mapper.Map<UsuarioResponseDTO>(a.Usuario)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<AvaliacaoResponseDTO>> ObterPorUsuario(int usuarioId)
        {
            return await _context.Avaliacoes
                .Where(a => a.Usuario.Id == usuarioId)
                .Select(a => new AvaliacaoResponseDTO
                {
                    Id = a.Id,
                    Nota = a.Nota,
                    Comentario = a.TextoAvaliacao,
                    JogoId = a.Jogo.Id,
                    Usuario = _mapper.Map<UsuarioResponseDTO>(a.Usuario)
                })
                .ToListAsync();
        }
    }
}