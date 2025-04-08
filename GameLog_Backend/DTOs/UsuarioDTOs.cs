using System.ComponentModel.DataAnnotations;

namespace GameLog.DTOs
{
    public class UsuarioRegistroDTO
    {
        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }
    }

    public class UsuarioLoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }

    public class UsuarioResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string FotoPerfil { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}