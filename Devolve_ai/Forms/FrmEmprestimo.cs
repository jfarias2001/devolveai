using System.Net.Mail;
using CadeMeu.Exceptions;
using CadeMeu.Models;
using CadeMeu.Services;
using Microsoft.Data.SqlClient;

namespace CadeMeu.Forms
{
    /// <summary>
    /// Cadastro de um novo empréstimo.
    /// Aqui ficam só as validações de FORMATO (campo vazio, máscara incompleta, e-mail mal escrito).
    /// As regras de negócio (ex.: datas coerentes) ficam no EmprestimoService.
    /// </summary>
    public partial class FrmEmprestimo : Form
    {
        private const int DiasSugeridosParaDevolucao = 7;

        private readonly EmprestimoService _servico;

        public FrmEmprestimo(EmprestimoService servico)
        {
            InitializeComponent();
            _servico = servico;
        }

        private void FrmEmprestimo_Load(object? sender, EventArgs e)
        {
            // O calendário não deixa escolher uma data de empréstimo no futuro.
            dtpDataEmprestimo.Value = DateTime.Today;
            dtpDataEmprestimo.MaxDate = DateTime.Today;

            // Sugere uma data, mas deixa desmarcado: a data combinada é opcional.
            // (Atribuir Value marca a caixinha, por isso Checked vem depois.)
            dtpDevolucaoPrevista.Value = DateTime.Today.AddDays(DiasSugeridosParaDevolucao);
            dtpDevolucaoPrevista.Checked = false;

            CarregarAmigos();
            ActiveControl = txtItem; // foco inicial no primeiro campo
        }

        private void CarregarAmigos()
        {
            try
            {
                List<Amigo> amigos = _servico.ListarAmigos();
                cboAmigo.DisplayMember = nameof(Amigo.Nome);
                cboAmigo.DataSource = amigos;
                cboAmigo.SelectedIndex = -1; // obriga o usuário a escolher, em vez de pegar o primeiro sem perceber

                // Sem nenhum amigo cadastrado, a única opção possível é cadastrar um novo.
                bool existemAmigos = amigos.Count > 0;
                chkNovoAmigo.Checked = !existemAmigos;
                chkNovoAmigo.Enabled = existemAmigos;
            }
            catch (SqlException erro)
            {
                Mensagens.MostrarErroBanco(erro);
                chkNovoAmigo.Checked = true;
                chkNovoAmigo.Enabled = false;
            }

            AlternarModoAmigo();
        }

        private void chkNovoAmigo_CheckedChanged(object? sender, EventArgs e)
        {
            AlternarModoAmigo();
        }

        /// <summary>
        /// Amigo existente: escolhe na lista. Amigo novo: digita nome, telefone e e-mail.
        /// Controles desabilitados também saem da navegação por Tab.
        /// </summary>
        private void AlternarModoAmigo()
        {
            bool novoAmigo = chkNovoAmigo.Checked;

            lblAmigo.Enabled = !novoAmigo;
            cboAmigo.Enabled = !novoAmigo;

            lblNomeAmigo.Enabled = novoAmigo;
            txtNomeAmigo.Enabled = novoAmigo;
            lblTelefone.Enabled = novoAmigo;
            mtbTelefone.Enabled = novoAmigo;
            lblEmail.Enabled = novoAmigo;
            txtEmail.Enabled = novoAmigo;

            errValidacao.Clear();
        }

        private void btnSalvar_Click(object? sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                Emprestimo emprestimo = MontarEmprestimo();

                if (chkNovoAmigo.Checked)
                {
                    _servico.RegistrarParaNovoAmigo(emprestimo, MontarNovoAmigo());
                }
                else
                {
                    _servico.RegistrarParaAmigoExistente(emprestimo);
                }

                DialogResult = DialogResult.OK; // fecha a janela e avisa a tela principal para recarregar
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

        /// <summary>
        /// Validação de formato na interface: marca cada campo com problema no ErrorProvider
        /// e coloca o foco no primeiro deles.
        /// </summary>
        private bool ValidarCampos()
        {
            errValidacao.Clear();
            Control? primeiroCampoInvalido = null;

            void MarcarErro(Control campo, string mensagem)
            {
                errValidacao.SetError(campo, mensagem);
                primeiroCampoInvalido ??= campo;
            }

            if (string.IsNullOrWhiteSpace(txtItem.Text))
            {
                MarcarErro(txtItem, "Informe o item emprestado.");
            }

            if (chkNovoAmigo.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtNomeAmigo.Text))
                {
                    MarcarErro(txtNomeAmigo, "Informe o nome do amigo.");
                }

                if (!mtbTelefone.MaskCompleted)
                {
                    MarcarErro(mtbTelefone, "Preencha o celular completo, com DDD.");
                }

                if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !MailAddress.TryCreate(txtEmail.Text.Trim(), out _))
                {
                    MarcarErro(txtEmail, "E-mail em formato inválido (ex.: nome@email.com).");
                }
            }
            else if (cboAmigo.SelectedItem is not Amigo)
            {
                MarcarErro(cboAmigo, "Escolha o amigo ou marque \"Novo amigo\".");
            }

            primeiroCampoInvalido?.Focus();
            return primeiroCampoInvalido == null;
        }

        private Emprestimo MontarEmprestimo()
        {
            return new Emprestimo
            {
                Item = txtItem.Text,
                AmigoId = (cboAmigo.SelectedItem as Amigo)?.Id ?? 0,
                DataEmprestimo = dtpDataEmprestimo.Value.Date,
                DataDevolucaoPrevista = dtpDevolucaoPrevista.Checked ? dtpDevolucaoPrevista.Value.Date : null
            };
        }

        private Amigo MontarNovoAmigo()
        {
            return new Amigo
            {
                Nome = txtNomeAmigo.Text,
                Telefone = mtbTelefone.Text, // TextMaskFormat = ExcludePromptAndLiterals: só os dígitos
                Email = txtEmail.Text
            };
        }

        private void CampoAlterado_LimparErro(object? sender, EventArgs e)
        {
            // Assim que o usuário mexe no campo, o ícone de erro some.
            if (sender is Control campo)
            {
                errValidacao.SetError(campo, string.Empty);
            }
        }

        private void dtpDevolucaoPrevista_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
