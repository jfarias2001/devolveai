namespace CadeMeu.Forms
{
    partial class FrmEmprestimo
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
            components = new System.ComponentModel.Container();
            lblItem = new Label();
            txtItem = new TextBox();
            lblDataEmprestimo = new Label();
            dtpDataEmprestimo = new DateTimePicker();
            lblDevolucaoPrevista = new Label();
            dtpDevolucaoPrevista = new DateTimePicker();
            grpAmigo = new GroupBox();
            chkNovoAmigo = new CheckBox();
            lblAmigo = new Label();
            cboAmigo = new ComboBox();
            lblNomeAmigo = new Label();
            txtNomeAmigo = new TextBox();
            lblTelefone = new Label();
            mtbTelefone = new MaskedTextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            btnSalvar = new Button();
            btnCancelar = new Button();
            errValidacao = new ErrorProvider(components);
            grpAmigo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errValidacao).BeginInit();
            SuspendLayout();
            // 
            // lblItem
            // 
            lblItem.AutoSize = true;
            lblItem.Location = new Point(20, 23);
            lblItem.Name = "lblItem";
            lblItem.Size = new Size(145, 23);
            lblItem.TabIndex = 0;
            lblItem.Text = "&Item emprestado:";
            // 
            // txtItem
            // 
            txtItem.Location = new Point(210, 20);
            txtItem.MaxLength = 100;
            txtItem.Name = "txtItem";
            txtItem.Size = new Size(260, 30);
            txtItem.TabIndex = 1;
            txtItem.TextChanged += CampoAlterado_LimparErro;
            // 
            // lblDataEmprestimo
            // 
            lblDataEmprestimo.AutoSize = true;
            lblDataEmprestimo.Location = new Point(20, 58);
            lblDataEmprestimo.Name = "lblDataEmprestimo";
            lblDataEmprestimo.Size = new Size(171, 23);
            lblDataEmprestimo.TabIndex = 2;
            lblDataEmprestimo.Text = "Data do &empréstimo:";
            // 
            // dtpDataEmprestimo
            // 
            dtpDataEmprestimo.Format = DateTimePickerFormat.Short;
            dtpDataEmprestimo.Location = new Point(210, 55);
            dtpDataEmprestimo.Name = "dtpDataEmprestimo";
            dtpDataEmprestimo.Size = new Size(140, 30);
            dtpDataEmprestimo.TabIndex = 3;
            // 
            // lblDevolucaoPrevista
            // 
            lblDevolucaoPrevista.AutoSize = true;
            lblDevolucaoPrevista.Location = new Point(20, 93);
            lblDevolucaoPrevista.Name = "lblDevolucaoPrevista";
            lblDevolucaoPrevista.Size = new Size(184, 23);
            lblDevolucaoPrevista.TabIndex = 4;
            lblDevolucaoPrevista.Text = "De&volução combinada:";
            // 
            // dtpDevolucaoPrevista
            // 
            dtpDevolucaoPrevista.Checked = false;
            dtpDevolucaoPrevista.Format = DateTimePickerFormat.Short;
            dtpDevolucaoPrevista.Location = new Point(210, 90);
            dtpDevolucaoPrevista.Name = "dtpDevolucaoPrevista";
            dtpDevolucaoPrevista.ShowCheckBox = true;
            dtpDevolucaoPrevista.Size = new Size(160, 30);
            dtpDevolucaoPrevista.TabIndex = 5;
            dtpDevolucaoPrevista.ValueChanged += dtpDevolucaoPrevista_ValueChanged;
            // 
            // grpAmigo
            // 
            grpAmigo.Controls.Add(chkNovoAmigo);
            grpAmigo.Controls.Add(lblAmigo);
            grpAmigo.Controls.Add(cboAmigo);
            grpAmigo.Controls.Add(lblNomeAmigo);
            grpAmigo.Controls.Add(txtNomeAmigo);
            grpAmigo.Controls.Add(lblTelefone);
            grpAmigo.Controls.Add(mtbTelefone);
            grpAmigo.Controls.Add(lblEmail);
            grpAmigo.Controls.Add(txtEmail);
            grpAmigo.Location = new Point(20, 132);
            grpAmigo.Name = "grpAmigo";
            grpAmigo.Size = new Size(480, 210);
            grpAmigo.TabIndex = 6;
            grpAmigo.TabStop = false;
            grpAmigo.Text = "Com quem está";
            // 
            // chkNovoAmigo
            // 
            chkNovoAmigo.AutoSize = true;
            chkNovoAmigo.Location = new Point(15, 28);
            chkNovoAmigo.Name = "chkNovoAmigo";
            chkNovoAmigo.Size = new Size(306, 27);
            chkNovoAmigo.TabIndex = 0;
            chkNovoAmigo.Text = "&Novo amigo (ainda não cadastrado)";
            chkNovoAmigo.UseVisualStyleBackColor = true;
            chkNovoAmigo.CheckedChanged += chkNovoAmigo_CheckedChanged;
            // 
            // lblAmigo
            // 
            lblAmigo.AutoSize = true;
            lblAmigo.Location = new Point(15, 63);
            lblAmigo.Name = "lblAmigo";
            lblAmigo.Size = new Size(64, 23);
            lblAmigo.TabIndex = 1;
            lblAmigo.Text = "&Amigo:";
            // 
            // cboAmigo
            // 
            cboAmigo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAmigo.Location = new Point(190, 60);
            cboAmigo.Name = "cboAmigo";
            cboAmigo.Size = new Size(250, 31);
            cboAmigo.TabIndex = 2;
            cboAmigo.SelectedIndexChanged += CampoAlterado_LimparErro;
            // 
            // lblNomeAmigo
            // 
            lblNomeAmigo.AutoSize = true;
            lblNomeAmigo.Location = new Point(15, 98);
            lblNomeAmigo.Name = "lblNomeAmigo";
            lblNomeAmigo.Size = new Size(61, 23);
            lblNomeAmigo.TabIndex = 3;
            lblNomeAmigo.Text = "N&ome:";
            // 
            // txtNomeAmigo
            // 
            txtNomeAmigo.Location = new Point(190, 95);
            txtNomeAmigo.MaxLength = 100;
            txtNomeAmigo.Name = "txtNomeAmigo";
            txtNomeAmigo.Size = new Size(250, 30);
            txtNomeAmigo.TabIndex = 4;
            txtNomeAmigo.TextChanged += CampoAlterado_LimparErro;
            // 
            // lblTelefone
            // 
            lblTelefone.AutoSize = true;
            lblTelefone.Location = new Point(15, 133);
            lblTelefone.Name = "lblTelefone";
            lblTelefone.Size = new Size(142, 23);
            lblTelefone.TabIndex = 5;
            lblTelefone.Text = "&Telefone (celular):";
            // 
            // mtbTelefone
            // 
            mtbTelefone.Location = new Point(190, 130);
            mtbTelefone.Mask = "(00) 00000-0000";
            mtbTelefone.Name = "mtbTelefone";
            mtbTelefone.Size = new Size(150, 30);
            mtbTelefone.TabIndex = 6;
            mtbTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mtbTelefone.TextChanged += CampoAlterado_LimparErro;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(15, 168);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(142, 23);
            lblEmail.TabIndex = 7;
            lblEmail.Text = "E-&mail (opcional):";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(190, 165);
            txtEmail.MaxLength = 150;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 30);
            txtEmail.TabIndex = 8;
            txtEmail.TextChanged += CampoAlterado_LimparErro;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(290, 360);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(100, 34);
            btnSalvar.TabIndex = 7;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(400, 360);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 34);
            btnCancelar.TabIndex = 8;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // errValidacao
            // 
            errValidacao.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errValidacao.ContainerControl = this;
            // 
            // FrmEmprestimo
            // 
            AcceptButton = btnSalvar;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancelar;
            ClientSize = new Size(524, 412);
            Controls.Add(lblItem);
            Controls.Add(txtItem);
            Controls.Add(lblDataEmprestimo);
            Controls.Add(dtpDataEmprestimo);
            Controls.Add(lblDevolucaoPrevista);
            Controls.Add(dtpDevolucaoPrevista);
            Controls.Add(grpAmigo);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmEmprestimo";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Novo empréstimo";
            Load += FrmEmprestimo_Load;
            grpAmigo.ResumeLayout(false);
            grpAmigo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errValidacao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblItem;
        private TextBox txtItem;
        private Label lblDataEmprestimo;
        private DateTimePicker dtpDataEmprestimo;
        private Label lblDevolucaoPrevista;
        private DateTimePicker dtpDevolucaoPrevista;
        private GroupBox grpAmigo;
        private CheckBox chkNovoAmigo;
        private Label lblAmigo;
        private ComboBox cboAmigo;
        private Label lblNomeAmigo;
        private TextBox txtNomeAmigo;
        private Label lblTelefone;
        private MaskedTextBox mtbTelefone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnSalvar;
        private Button btnCancelar;
        private ErrorProvider errValidacao;
    }
}
