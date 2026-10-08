using System.Data;
using CadeMeu.Models;
using Microsoft.Data.SqlClient;

namespace CadeMeu.Data
{
    /// <summary>
    /// Todo o SQL da tabela Emprestimos.
    /// Regras de uso: toda conexão, comando e transação dentro de "using",
    /// e todo valor passado por @parametro (nunca concatenado no texto do SQL).
    /// </summary>
    public class EmprestimoRepository
    {
        // Colunas usadas por todas as consultas de leitura, na ordem lida em LerEmprestimo().
        private const string SelectComAmigo =
            "SELECT e.Id, e.Item, e.AmigoId, a.Nome, a.Telefone, " +
            "       e.DataEmprestimo, e.DataDevolucaoPrevista, e.DataDevolucao " +
            "FROM dbo.Emprestimos e " +
            "INNER JOIN dbo.Amigos a ON a.Id = e.AmigoId ";

        private readonly AmigoRepository _amigoRepository = new();

        /// <summary>
        /// Lista os empréstimos: primeiro os que ainda não voltaram, depois os mais recentes.
        /// </summary>
        public List<Emprestimo> Listar(bool incluirDevolvidos)
        {
            const string sql = SelectComAmigo +
                "WHERE (@IncluirDevolvidos = 1 OR e.DataDevolucao IS NULL) " +
                "ORDER BY CASE WHEN e.DataDevolucao IS NULL THEN 0 ELSE 1 END, e.DataEmprestimo DESC;";

            var emprestimos = new List<Emprestimo>();

            using SqlConnection conexao = ConexaoFactory.CriarConexao();
            using var comando = new SqlCommand(sql, conexao);
            comando.Parameters.Add("@IncluirDevolvidos", SqlDbType.Bit).Value = incluirDevolvidos;
            conexao.Open();

            using SqlDataReader leitor = comando.ExecuteReader();
            while (leitor.Read())
            {
                emprestimos.Add(LerEmprestimo(leitor));
            }

            return emprestimos;
        }

        public Emprestimo? BuscarPorId(int id)
        {
            const string sql = SelectComAmigo + "WHERE e.Id = @Id;";

            using SqlConnection conexao = ConexaoFactory.CriarConexao();
            using var comando = new SqlCommand(sql, conexao);
            comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            conexao.Open();

            using SqlDataReader leitor = comando.ExecuteReader();
            return leitor.Read() ? LerEmprestimo(leitor) : null;
        }

        /// <summary>
        /// Empréstimo para um amigo que já existe: é um único INSERT, não precisa de transação.
        /// </summary>
        public int Inserir(Emprestimo emprestimo)
        {
            using SqlConnection conexao = ConexaoFactory.CriarConexao();
            conexao.Open();
            return InserirEmprestimo(emprestimo, conexao, transacao: null);
        }

        /// <summary>
        /// Empréstimo para um amigo novo: são dois INSERTs que dependem um do outro
        /// (o empréstimo precisa do Id do amigo). A transação garante "tudo ou nada":
        /// se o segundo falhar, o primeiro é desfeito e não sobra amigo sem empréstimo.
        /// </summary>
        public int InserirComNovoAmigo(Amigo novoAmigo, Emprestimo emprestimo)
        {
            using SqlConnection conexao = ConexaoFactory.CriarConexao();
            conexao.Open();
            using SqlTransaction transacao = conexao.BeginTransaction();

            try
            {
                novoAmigo.Id = _amigoRepository.Inserir(novoAmigo, conexao, transacao);
                emprestimo.AmigoId = novoAmigo.Id;

                int emprestimoId = InserirEmprestimo(emprestimo, conexao, transacao);

                transacao.Commit();
                return emprestimoId;
            }
            catch (SqlException)
            {
                // Desfaz o INSERT do amigo e repassa o erro para a tela avisar o usuário.
                transacao.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Grava a data de devolução. O "DataDevolucao IS NULL" no WHERE impede
        /// devolver duas vezes, mesmo que duas telas tentem ao mesmo tempo.
        /// </summary>
        /// <returns>true se o empréstimo foi atualizado; false se ele já estava devolvido.</returns>
        public bool RegistrarDevolucao(int emprestimoId, DateTime dataDevolucao)
        {
            const string sql =
                "UPDATE dbo.Emprestimos SET DataDevolucao = @DataDevolucao " +
                "WHERE Id = @Id AND DataDevolucao IS NULL;";

            using SqlConnection conexao = ConexaoFactory.CriarConexao();
            using var comando = new SqlCommand(sql, conexao);
            comando.Parameters.Add("@DataDevolucao", SqlDbType.Date).Value = dataDevolucao.Date;
            comando.Parameters.Add("@Id", SqlDbType.Int).Value = emprestimoId;
            conexao.Open();

            int linhasAfetadas = comando.ExecuteNonQuery();
            return linhasAfetadas > 0;
        }

        private static int InserirEmprestimo(Emprestimo emprestimo, SqlConnection conexao, SqlTransaction? transacao)
        {
            const string sql =
                "INSERT INTO dbo.Emprestimos (Item, AmigoId, DataEmprestimo, DataDevolucaoPrevista) " +
                "OUTPUT INSERTED.Id " +
                "VALUES (@Item, @AmigoId, @DataEmprestimo, @DataDevolucaoPrevista);";

            using var comando = new SqlCommand(sql, conexao, transacao);
            comando.Parameters.Add("@Item", SqlDbType.NVarChar, 100).Value = emprestimo.Item;
            comando.Parameters.Add("@AmigoId", SqlDbType.Int).Value = emprestimo.AmigoId;
            comando.Parameters.Add("@DataEmprestimo", SqlDbType.Date).Value = emprestimo.DataEmprestimo.Date;
            comando.Parameters.Add("@DataDevolucaoPrevista", SqlDbType.Date).Value =
                (object?)emprestimo.DataDevolucaoPrevista?.Date ?? DBNull.Value;

            emprestimo.Id = (int)comando.ExecuteScalar()!;
            return emprestimo.Id;
        }

        private static Emprestimo LerEmprestimo(SqlDataReader leitor)
        {
            return new Emprestimo
            {
                Id = leitor.GetInt32(0),
                Item = leitor.GetString(1),
                AmigoId = leitor.GetInt32(2),
                NomeAmigo = leitor.GetString(3),
                TelefoneAmigo = leitor.GetString(4),
                DataEmprestimo = leitor.GetDateTime(5),
                DataDevolucaoPrevista = leitor.IsDBNull(6) ? null : leitor.GetDateTime(6),
                DataDevolucao = leitor.IsDBNull(7) ? null : leitor.GetDateTime(7)
            };
        }
    }
}
