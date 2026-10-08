namespace CadeMeu.Models
{
    /// <summary>
    /// Pessoa que pegou algo emprestado. Representa uma linha da tabela Amigos.
    /// </summary>
    public class Amigo
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        /// <summary>Somente dígitos (DDD + número), sem parênteses nem traço.</summary>
        public string Telefone { get; set; } = string.Empty;

        /// <summary>Opcional: null quando o amigo não informou e-mail.</summary>
        public string? Email { get; set; }
    }
}
