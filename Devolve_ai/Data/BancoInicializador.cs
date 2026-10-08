using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace CadeMeu.Data
{
    /// <summary>
    /// Cria o banco e as tabelas na primeira execução, usando o mesmo
    /// Database/CriarBanco.sql que pode ser rodado à mão no SQL Server.
    /// Assim o usuário só instala e usa: não precisa criar nada manualmente.
    /// </summary>
    public static class BancoInicializador
    {
        private static readonly string CaminhoDoScript =
            Path.Combine(AppContext.BaseDirectory, "Database", "CriarBanco.sql");

        public static void CriarSeNaoExistir()
        {
            string script = File.ReadAllText(CaminhoDoScript);

            using SqlConnection conexao = ConexaoFactory.CriarConexaoMaster();
            conexao.Open();

            // Aqui não há parâmetros porque o SQL vem de um arquivo fixo do sistema,
            // e não de algo digitado pelo usuário: não existe risco de SQL Injection.
            foreach (string lote in DividirEmLotes(script))
            {
                using var comando = new SqlCommand(lote, conexao);
                comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// "GO" não é um comando SQL: é um separador de lotes usado pelas ferramentas.
        /// Por isso o script é dividido nele e cada parte é executada separadamente.
        /// </summary>
        private static IEnumerable<string> DividirEmLotes(string script)
        {
            return Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase)
                        .Where(lote => !string.IsNullOrWhiteSpace(lote));
        }
    }
}
