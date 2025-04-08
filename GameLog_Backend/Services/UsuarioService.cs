using BCrypt.Net;
using GameLog_Backend.Entities;
using GameLog.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GameLog_Backend.Database;

namespace GameLog.Services
{
    public class UsuarioService
    {
        private readonly GameLogContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(GameLogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsuarioResponseDTO> Registrar(UsuarioRegistroDTO dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email já cadastrado");

            var usuario = new Usuario
            {
                NomeUsuario = dto.Nome,
                Email = dto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                FotoDePerfil = "default.jpg"
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        public async Task<UsuarioResponseDTO> Login(UsuarioLoginDTO dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
                throw new Exception("Credenciais inválidas");

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        public async Task<UsuarioResponseDTO> ObterPorId(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) throw new Exception("Usuário não encontrado");

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }
    }
}