namespace GameLog.Models
{
    public class ModelAvaliacao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public ModelUsuario Usuario { get; set; }
        public int JogoId { get; set; }
        public int Nota { get; set; } // 1-5
        public string Comentario { get; set; }
        public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;
    }
}