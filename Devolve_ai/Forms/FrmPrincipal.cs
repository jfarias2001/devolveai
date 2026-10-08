using CadeMeu.Exceptions;
using CadeMeu.Models;
using CadeMeu.Services;
using Microsoft.Data.SqlClient;

namespace CadeMeu.Forms
{
    /// <summary>
    /// Tela inicial: lista as coisas emprestadas.
    /// Só cuida da apresentação; qualquer dado ou regra é pedido ao EmprestimoService.
    /// </summary>
    public partial class FrmPrincipal : Form
    {
        private const string FormatoData = "dd/MM/yyyy";

        // Cores fortes o bastante para aparecer no projetor e mesmo com a linha selecionada.
        private static readonly Color CorFundoAtrasado = Color.FromArgb(255, 205, 210);
        private static readonly Color CorTextoAtrasado = Color.DarkRed;
        private static readonly Color CorSelecaoAtrasado = Color.Firebrick;
        private static readonly Color CorTextoDevolvido = Color.Gray;

        private readonly EmprestimoService _servico = new();

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object? sender, EventArgs e)
        {
            CarregarEmprestimos();
            ActiveControl = btnNovo; // foco inicial na ação principal da tela
        }

        private void FrmPrincipal_KeyDown(object? sender, KeyEventArgs e)
        {
            // KeyPreview = true faz o formulário receber a tecla antes do controle com foco.
            if (e.KeyCode == Keys.F5)
            {
                CarregarEmprestimos();
                e.Handled = true;
            }
        }

        private void btnNovo_Click(object? sender, EventArgs e)
        {
            using var frmEmprestimo = new FrmEmprestimo(_servico);

            if (frmEmprestimo.ShowDialog(this) == DialogResult.OK)
            {
                CarregarEmprestimos();
            }
        }

        private void btnDevolvido_Click(object? sender, EventArgs e)
        {
            Emprestimo? emprestimo = ObterEmprestimoSelecionado();
            if (emprestimo == null)
            {
                Mensagens.MostrarAviso("Selecione na lista o item que foi devolvido.");
                return;
            }

            if (!Mensagens.Confirmar($"Confirmar que \"{emprestimo.Item}\" foi devolvido por {emprestimo.NomeAmigo}?"))
            {
                return;
            }

            try
            {
                _servico.MarcarComoDevolvido(emprestimo.Id);
                CarregarEmprestimos();
            }
            catch (RegraNegocioException erro)
            {
                Mensagens.MostrarAviso(erro.Message);
            }
            catch (SqlException erro)
            {
                Mensagens.MostrarErroBanco(erro);
            }
        }

        private void btnAtualizar_Click(object? sender, EventArgs e)
        {
            CarregarEmprestimos();
        }

        private void chkMostrarDevolvidos_CheckedChanged(object? sender, EventArgs e)
        {
            CarregarEmprestimos();
        }

        private void dgvEmprestimos_SelectionChanged(object? sender, EventArgs e)
        {
            AtualizarBotoes();
        }

        private void CarregarEmprestimos()
        {
            try
            {
                List<Emprestimo> emprestimos = _servico.ListarEmprestimos(chkMostrarDevolvidos.Checked);
                PreencherGrade(emprestimos);
                AtualizarResumo(emprestimos);
            }
            catch (SqlException erro)
            {
                Mensagens.MostrarErroBanco(erro);
            }

            AtualizarBotoes();
        }

        private void PreencherGrade(List<Emprestimo> emprestimos)
        {
            dgvEmprestimos.Rows.Clear();

            foreach (Emprestimo emprestimo in emprestimos)
            {
                int indice = dgvEmprestimos.Rows.Add(
                    emprestimo.Item,
                    emprestimo.NomeAmigo,
                    FormatarTelefone(emprestimo.TelefoneAmigo),
                    emprestimo.DataEmprestimo.ToString(FormatoData),
                    emprestimo.DataDevolucaoPrevista?.ToString(FormatoData) ?? "-",
                    emprestimo.DataDevolucao?.ToString(FormatoData) ?? "-",
                    emprestimo.Situacao);

                DataGridViewRow linha = dgvEmprestimos.Rows[indice];
                linha.Tag = emprestimo; // guarda o objeto para saber qual empréstimo foi selecionado
                PintarLinha(linha, emprestimo);
            }
        }

        /// <summary>
        /// A decisão "está atrasado?" vem do modelo (Emprestimo.EstaAtrasado);
        /// a tela só escolhe a cor.
        /// </summary>
        private static void PintarLinha(DataGridViewRow linha, Emprestimo emprestimo)
        {
            if (emprestimo.EstaAtrasado)
            {
                linha.DefaultCellStyle.BackColor = CorFundoAtrasado;
                linha.DefaultCellStyle.ForeColor = CorTextoAtrasado;
                linha.DefaultCellStyle.SelectionBackColor = CorSelecaoAtrasado;
            }
            else if (emprestimo.FoiDevolvido)
            {
                linha.DefaultCellStyle.ForeColor = CorTextoDevolvido;
            }
        }

        private void AtualizarResumo(List<Emprestimo> emprestimos)
        {
            int emAberto = emprestimos.Count(emprestimo => !emprestimo.FoiDevolvido);
            int atrasados = emprestimos.Count(emprestimo => emprestimo.EstaAtrasado);

            lblResumo.Text = $"{emAberto} item(ns) emprestado(s), {atrasados} atrasado(s).   " +
                             "Vermelho = atrasado  |  Cinza = devolvido";
        }

        private void AtualizarBotoes()
        {
            btnDevolvido.Enabled = ObterEmprestimoSelecionado() != null;
        }

        private Emprestimo? ObterEmprestimoSelecionado()
        {
            return dgvEmprestimos.CurrentRow?.Tag as Emprestimo;
        }

        /// <summary>
        /// O banco guarda só os dígitos; aqui eles são formatados para leitura.
        /// TryParse evita exceção caso chegue algo que não seja número.
        /// </summary>
        private static string FormatarTelefone(string digitos)
        {
            if (!long.TryParse(digitos, out long numero))
            {
                return digitos;
            }

            return digitos.Length == 11
                ? numero.ToString(@"(00) 00000-0000")
                : numero.ToString(@"(00) 0000-0000");
        }
    }
}
