using BCrypt.Net;
using GameLog.Models;
using GameLog_Backend.Database;
using GameLog_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLog.Services
{
    public class UsuarioService
    {
        private readonly GameLogContext _context;

        public UsuarioService(GameLogContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Registrar(Usuario usuario)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
                throw new Exception("Email já cadastrado");

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Login(string email, string senha)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                throw new Exception("Credenciais inválidas");

            return usuario;
        }

        public async Task<Usuario> ObterPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
    }
}