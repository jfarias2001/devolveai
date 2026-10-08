using System.Configuration;
using System.Diagnostics;
using CadeMeu.Data;
using CadeMeu.Forms;
using Microsoft.Data.SqlClient;

namespace CadeMeu
{
    internal static class Program
    {
        private const string TituloSistema = "Cadê Meu?";

        /// <summary>
        ///  Ponto de entrada da aplicação.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Rede de segurança: se escapar algum erro não previsto, o usuário vê uma
            // mensagem amigável em vez da janela técnica do .NET com a pilha de erros.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, evento) =>
            {
                Debug.WriteLine(evento.Exception);
                MessageBox.Show("Ocorreu um erro inesperado. Tente novamente.",
                    TituloSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            if (!PrepararBanco())
            {
                return; // sem banco não há como funcionar: encerra depois de avisar
            }

            Application.Run(new FrmPrincipal());
        }

        /// <summary>
        /// Garante que o banco e as tabelas existam antes de abrir a tela principal.
        /// </summary>
        private static bool PrepararBanco()
        {
            try
            {
                BancoInicializador.CriarSeNaoExistir();
                return true;
            }
            catch (SqlException erro)
            {
                Debug.WriteLine(erro);
                MostrarErroFatal("Não foi possível acessar o banco de dados.\n" +
                                 "Verifique se o SQL Server LocalDB está instalado.");
            }
            catch (ConfigurationErrorsException erro)
            {
                Debug.WriteLine(erro);
                MostrarErroFatal("A configuração do banco de dados não foi encontrada no arquivo de configuração.");
            }
            catch (IOException erro)
            {
                Debug.WriteLine(erro);
                MostrarErroFatal("O arquivo de criação do banco (Database\\CriarBanco.sql) não foi encontrado.\n" +
                                 "Reinstale o sistema.");
            }

            return false;
        }

        private static void MostrarErroFatal(string mensagem)
        {
            MessageBox.Show(mensagem, TituloSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
