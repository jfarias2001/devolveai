using System.Data;
using CadeMeu.Models;
using Microsoft.Data.SqlClient;

namespace CadeMeu.Data
{
    /// <summary>
    /// Todo o SQL da tabela Amigos. Nenhuma regra de negócio aqui: só leitura e gravação.
    /// </summary>
    public class AmigoRepository
    {
        public List<Amigo> ListarTodos()
        {
            const string sql = "SELECT Id, Nome, Telefone, Email FROM dbo.Amigos ORDER BY Nome;";

            var amigos = new List<Amigo>();

            using SqlConnection conexao = ConexaoFactory.CriarConexao();
            using var comando = new SqlCommand(sql, conexao);
            conexao.Open();

            using SqlDataReader leitor = comando.ExecuteReader();
            while (leitor.Read())
            {
                amigos.Add(new Amigo
                {
                    Id = leitor.GetInt32(0),
                    Nome = leitor.GetString(1),
                    Telefone = leitor.GetString(2),
                    Email = leitor.IsDBNull(3) ? null : leitor.GetString(3)
                });
            }

            return amigos;
        }

        /// <summary>
        /// Insere o amigo usando a conexão e a transação de quem chamou.
        /// Não abre conexão própria porque faz parte de uma operação maior
        /// (amigo + empréstimo), controlada pelo EmprestimoRepository.
        /// </summary>
        /// <returns>O Id gerado pelo banco.</returns>
        public int Inserir(Amigo amigo, SqlConnection conexao, SqlTransaction transacao)
        {
            const string sql =
                "INSERT INTO dbo.Amigos (Nome, Telefone, Email) " +
                "OUTPUT INSERTED.Id " +
                "VALUES (@Nome, @Telefone, @Email);";

            using var comando = new SqlCommand(sql, conexao, transacao);
            comando.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = amigo.Nome;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar, 11).Value = amigo.Telefone;
            comando.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = (object?)amigo.Email ?? DBNull.Value;

            return (int)comando.ExecuteScalar()!;
        }
    }
}
