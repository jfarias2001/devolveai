using System.Configuration;
using Microsoft.Data.SqlClient;

namespace CadeMeu.Data
{
    /// <summary>
    /// Único lugar do sistema que sabe de onde vem a string de conexão (App.config).
    /// Entrega a conexão FECHADA: quem usa abre com Open() dentro de um "using",
    /// e o "using" garante que ela será fechada mesmo se der erro.
    /// </summary>
    public static class ConexaoFactory
    {
        private const string NomeDaConexao = "CadeMeu";

        public static SqlConnection CriarConexao()
        {
            return new SqlConnection(ObterStringDeConexao());
        }

        /// <summary>
        /// Conexão com o banco "master", usada só para criar o banco CadeMeu
        /// quando ele ainda não existe (primeira execução).
        /// </summary>
        public static SqlConnection CriarConexaoMaster()
        {
            var construtor = new SqlConnectionStringBuilder(ObterStringDeConexao())
            {
                InitialCatalog = "master"
            };
            return new SqlConnection(construtor.ConnectionString);
        }

        private static string ObterStringDeConexao()
        {
            string? stringDeConexao = ConfigurationManager.ConnectionStrings[NomeDaConexao]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(stringDeConexao))
            {
                throw new ConfigurationErrorsException(
                    $"A conexão \"{NomeDaConexao}\" não foi encontrada no App.config.");
            }

            return stringDeConexao;
        }
    }
}
