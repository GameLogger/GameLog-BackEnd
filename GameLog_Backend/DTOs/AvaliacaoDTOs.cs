using System.ComponentModel.DataAnnotations;

namespace GameLog.DTOs
{
    public class AvaliacaoCreateDTO
    {
        [Required, Range(1, 5)]
        public int Nota { get; set; }

        [Required, StringLength(500)]
        public string Comentario { get; set; }

        [Required]
        public int JogoId { get; set; }
    }

    public class AvaliacaoResponseDTO
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }
        public DateTime DataPublicacao { get; set; }
        public UsuarioResponseDTO Usuario { get; set; }
        public int JogoId { get; set; }
    }
}