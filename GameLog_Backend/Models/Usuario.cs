using GameLog_Backend.Entities;

namespace GameLog.Models
{
    public class ModelUsuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string FotoPerfil { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public ICollection<Avaliacao> Avaliacoes { get; set; }
    }
}
