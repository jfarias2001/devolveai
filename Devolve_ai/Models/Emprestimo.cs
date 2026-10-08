namespace CadeMeu.Models
{
    /// <summary>
    /// Um item emprestado. Representa uma linha da tabela Emprestimos.
    /// </summary>
    public class Emprestimo
    {
        public int Id { get; set; }

        public string Item { get; set; } = string.Empty;

        public int AmigoId { get; set; }

        // Nome e telefone do amigo vêm do JOIN na consulta da lista,
        // para a tela não precisar buscar o amigo de cada linha.
        public string NomeAmigo { get; set; } = string.Empty;

        public string TelefoneAmigo { get; set; } = string.Empty;

        public DateTime DataEmprestimo { get; set; }

        /// <summary>Data combinada para devolver. Null = sem data combinada.</summary>
        public DateTime? DataDevolucaoPrevista { get; set; }

        /// <summary>Data em que o item voltou. Null = ainda está com o amigo.</summary>
        public DateTime? DataDevolucao { get; set; }

        public bool FoiDevolvido => DataDevolucao.HasValue;

        /// <summary>
        /// Regra do atraso: ainda não voltou, tem data combinada e essa data já passou.
        /// Fica no modelo para que qualquer tela use a mesma regra.
        /// </summary>
        public bool EstaAtrasado =>
            !FoiDevolvido
            && DataDevolucaoPrevista.HasValue
            && DataDevolucaoPrevista.Value.Date < DateTime.Today;

        public string Situacao =>
            FoiDevolvido ? "Devolvido"
            : EstaAtrasado ? "Atrasado"
            : "Emprestado";
    }
}
