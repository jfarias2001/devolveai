namespace CadeMeu.Forms
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlBotoes = new Panel();
            btnNovo = new Button();
            btnDevolvido = new Button();
            btnAtualizar = new Button();
            chkMostrarDevolvidos = new CheckBox();
            dgvEmprestimos = new DataGridView();
            colItem = new DataGridViewTextBoxColumn();
            colAmigo = new DataGridViewTextBoxColumn();
            colTelefone = new DataGridViewTextBoxColumn();
            colDataEmprestimo = new DataGridViewTextBoxColumn();
            colDevolucaoPrevista = new DataGridViewTextBoxColumn();
            colDataDevolucao = new DataGridViewTextBoxColumn();
            colSituacao = new DataGridViewTextBoxColumn();
            lblResumo = new Label();
            pnlBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmprestimos).BeginInit();
            SuspendLayout();
            //
            // pnlBotoes
            //
            pnlBotoes.Controls.Add(btnNovo);
            pnlBotoes.Controls.Add(btnDevolvido);
            pnlBotoes.Controls.Add(btnAtualizar);
            pnlBotoes.Controls.Add(chkMostrarDevolvidos);
            pnlBotoes.Dock = DockStyle.Top;
            pnlBotoes.Location = new Point(0, 0);
            pnlBotoes.Name = "pnlBotoes";
            pnlBotoes.Padding = new Padding(8);
            pnlBotoes.Size = new Size(984, 56);
            pnlBotoes.TabIndex = 0;
            //
            // btnNovo
            //
            btnNovo.Location = new Point(12, 12);
            btnNovo.Name = "btnNovo";
            btnNovo.Size = new Size(190, 34);
            btnNovo.TabIndex = 0;
            btnNovo.Text = "&Novo empréstimo";
            btnNovo.UseVisualStyleBackColor = true;
            btnNovo.Click += btnNovo_Click;
            //
            // btnDevolvido
            //
            btnDevolvido.Location = new Point(212, 12);
            btnDevolvido.Name = "btnDevolvido";
            btnDevolvido.Size = new Size(200, 34);
            btnDevolvido.TabIndex = 1;
            btnDevolvido.Text = "Marcar &devolvido";
            btnDevolvido.UseVisualStyleBackColor = true;
            btnDevolvido.Click += btnDevolvido_Click;
            //
            // btnAtualizar
            //
            btnAtualizar.Location = new Point(422, 12);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(150, 34);
            btnAtualizar.TabIndex = 2;
            btnAtualizar.Text = "&Atualizar (F5)";
            btnAtualizar.UseVisualStyleBackColor = true;
            btnAtualizar.Click += btnAtualizar_Click;
            //
            // chkMostrarDevolvidos
            //
            chkMostrarDevolvidos.AutoSize = true;
            chkMostrarDevolvidos.Location = new Point(592, 18);
            chkMostrarDevolvidos.Name = "chkMostrarDevolvidos";
            chkMostrarDevolvidos.Size = new Size(160, 23);
            chkMostrarDevolvidos.TabIndex = 3;
            chkMostrarDevolvidos.Text = "&Mostrar devolvidos";
            chkMostrarDevolvidos.UseVisualStyleBackColor = true;
            chkMostrarDevolvidos.CheckedChanged += chkMostrarDevolvidos_CheckedChanged;
            //
            // dgvEmprestimos
            //
            dgvEmprestimos.AllowUserToAddRows = false;
            dgvEmprestimos.AllowUserToDeleteRows = false;
            dgvEmprestimos.AllowUserToResizeRows = false;
            dgvEmprestimos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmprestimos.BackgroundColor = SystemColors.Window;
            dgvEmprestimos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmprestimos.Columns.AddRange(new DataGridViewColumn[] { colItem, colAmigo, colTelefone, colDataEmprestimo, colDevolucaoPrevista, colDataDevolucao, colSituacao });
            dgvEmprestimos.Dock = DockStyle.Fill;
            dgvEmprestimos.Location = new Point(0, 56);
            dgvEmprestimos.MultiSelect = false;
            dgvEmprestimos.Name = "dgvEmprestimos";
            dgvEmprestimos.ReadOnly = true;
            dgvEmprestimos.RowHeadersVisible = false;
            dgvEmprestimos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmprestimos.StandardTab = true;
            dgvEmprestimos.Size = new Size(984, 471);
            dgvEmprestimos.TabIndex = 1;
            dgvEmprestimos.SelectionChanged += dgvEmprestimos_SelectionChanged;
            //
            // colItem
            //
            colItem.FillWeight = 180F;
            colItem.HeaderText = "Item";
            colItem.Name = "colItem";
            colItem.ReadOnly = true;
            //
            // colAmigo
            //
            colAmigo.FillWeight = 140F;
            colAmigo.HeaderText = "Com quem está";
            colAmigo.Name = "colAmigo";
            colAmigo.ReadOnly = true;
            //
            // colTelefone
            //
            colTelefone.HeaderText = "Telefone";
            colTelefone.Name = "colTelefone";
            colTelefone.ReadOnly = true;
            //
            // colDataEmprestimo
            //
            colDataEmprestimo.HeaderText = "Emprestado em";
            colDataEmprestimo.Name = "colDataEmprestimo";
            colDataEmprestimo.ReadOnly = true;
            //
            // colDevolucaoPrevista
            //
            colDevolucaoPrevista.HeaderText = "Devolver até";
            colDevolucaoPrevista.Name = "colDevolucaoPrevista";
            colDevolucaoPrevista.ReadOnly = true;
            //
            // colDataDevolucao
            //
            colDataDevolucao.HeaderText = "Devolvido em";
            colDataDevolucao.Name = "colDataDevolucao";
            colDataDevolucao.ReadOnly = true;
            //
            // colSituacao
            //
            colSituacao.FillWeight = 90F;
            colSituacao.HeaderText = "Situação";
            colSituacao.Name = "colSituacao";
            colSituacao.ReadOnly = true;
            //
            // lblResumo
            //
            lblResumo.Dock = DockStyle.Bottom;
            lblResumo.Location = new Point(0, 527);
            lblResumo.Name = "lblResumo";
            lblResumo.Padding = new Padding(12, 0, 0, 0);
            lblResumo.Size = new Size(984, 34);
            lblResumo.TabIndex = 2;
            lblResumo.Text = "Carregando...";
            lblResumo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // FrmPrincipal
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(984, 561);
            Controls.Add(dgvEmprestimos);
            Controls.Add(lblResumo);
            Controls.Add(pnlBotoes);
            Font = new Font("Segoe UI", 10F);
            KeyPreview = true;
            MinimumSize = new Size(800, 400);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cadê Meu? - Coisas emprestadas";
            Load += FrmPrincipal_Load;
            KeyDown += FrmPrincipal_KeyDown;
            pnlBotoes.ResumeLayout(false);
            pnlBotoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmprestimos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBotoes;
        private Button btnNovo;
        private Button btnDevolvido;
        private Button btnAtualizar;
        private CheckBox chkMostrarDevolvidos;
        private DataGridView dgvEmprestimos;
        private DataGridViewTextBoxColumn colItem;
        private DataGridViewTextBoxColumn colAmigo;
        private DataGridViewTextBoxColumn colTelefone;
        private DataGridViewTextBoxColumn colDataEmprestimo;
        private DataGridViewTextBoxColumn colDevolucaoPrevista;
        private DataGridViewTextBoxColumn colDataDevolucao;
        private DataGridViewTextBoxColumn colSituacao;
        private Label lblResumo;
    }
}
