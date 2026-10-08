using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace CadeMeu.Forms
{
    /// <summary>
    /// Caixas de mensagem padronizadas, para todas as telas falarem com o usuário do mesmo jeito.
    /// </summary>
    internal static class Mensagens
    {
        private const string TituloSistema = "Cadê Meu?";

        public static void MostrarAviso(string texto)
        {
            MessageBox.Show(texto, TituloSistema, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// O usuário recebe uma mensagem amigável. O detalhe técnico (com a pilha de erros)
        /// vai só para a janela de saída do Visual Studio, para o desenvolvedor investigar.
        /// </summary>
        public static void MostrarErroBanco(SqlException erro)
        {
            Debug.WriteLine(erro);
            MessageBox.Show(
                "Não foi possível acessar o banco de dados.\n" +
                "Verifique se o SQL Server LocalDB está instalado e tente novamente.",
                TituloSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirmar(string pergunta)
        {
            DialogResult resposta = MessageBox.Show(
                pergunta, TituloSistema, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return resposta == DialogResult.Yes;
        }
    }
}
