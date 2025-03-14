namespace Sistema_de_Gestão_Escolar
{
    partial class FormAluno
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
            lstAlunos = new ListBox();
            btnAdicionarAluno = new Button();
            btnRemoverAluno = new Button();
            btnMudarTurma = new Button();
            btnBuscarAluno = new Button();
            txtNovaTurmaAluno = new TextBox();
            lblNovaTurmaAluno = new Label();
            txtBuscarAluno = new TextBox();
            lblBuscarAluno = new Label();
            txtTurmaAluno = new TextBox();
            lblTurmaAluno = new Label();
            txtEmailAluno = new TextBox();
            lblEmailAluno = new Label();
            lblMoradaAluno = new Label();
            txtMoradaAluno = new TextBox();
            txtContatoAluno = new TextBox();
            lblContatoAluno = new Label();
            dtpNascimentoAluno = new DateTimePicker();
            lblDataNascimento = new Label();
            txtNomeAluno = new TextBox();
            lblNomeAluno = new Label();
            txtIdAluno = new TextBox();
            lblIdAluno = new Label();
            cmbNovaTurmaAluno = new ComboBox();
            SuspendLayout();
            // 
            // lstAlunos
            // 
            lstAlunos.FormattingEnabled = true;
            lstAlunos.Location = new Point(12, 12);
            lstAlunos.Name = "lstAlunos";
            lstAlunos.Size = new Size(307, 404);
            lstAlunos.TabIndex = 0;
            // 
            // btnAdicionarAluno
            // 
            btnAdicionarAluno.Location = new Point(35, 454);
            btnAdicionarAluno.Name = "btnAdicionarAluno";
            btnAdicionarAluno.Size = new Size(188, 53);
            btnAdicionarAluno.TabIndex = 1;
            btnAdicionarAluno.Text = "Adicionar novo aluno";
            btnAdicionarAluno.UseVisualStyleBackColor = true;
            btnAdicionarAluno.Click += btnAdicionarAluno_Click;
            // 
            // btnRemoverAluno
            // 
            btnRemoverAluno.Location = new Point(35, 513);
            btnRemoverAluno.Name = "btnRemoverAluno";
            btnRemoverAluno.Size = new Size(201, 53);
            btnRemoverAluno.TabIndex = 2;
            btnRemoverAluno.Text = "Remover aluno selecionado";
            btnRemoverAluno.UseVisualStyleBackColor = true;
            btnRemoverAluno.Click += btnRemoverAluno_Click;
            // 
            // btnMudarTurma
            // 
            btnMudarTurma.Location = new Point(35, 625);
            btnMudarTurma.Name = "btnMudarTurma";
            btnMudarTurma.Size = new Size(148, 52);
            btnMudarTurma.TabIndex = 3;
            btnMudarTurma.Text = "Mudar turma";
            btnMudarTurma.UseVisualStyleBackColor = true;
            btnMudarTurma.Click += btnMudarTurma_Click;
            // 
            // btnBuscarAluno
            // 
            btnBuscarAluno.Location = new Point(35, 572);
            btnBuscarAluno.Name = "btnBuscarAluno";
            btnBuscarAluno.Size = new Size(109, 47);
            btnBuscarAluno.TabIndex = 4;
            btnBuscarAluno.Text = "Buscar aluno";
            btnBuscarAluno.UseVisualStyleBackColor = true;
            btnBuscarAluno.Click += btnBuscarAluno_Click;
            // 
            // txtNovaTurmaAluno
            // 
            txtNovaTurmaAluno.Location = new Point(902, 81);
            txtNovaTurmaAluno.Name = "txtNovaTurmaAluno";
            txtNovaTurmaAluno.Size = new Size(214, 27);
            txtNovaTurmaAluno.TabIndex = 5;
            // 
            // lblNovaTurmaAluno
            // 
            lblNovaTurmaAluno.AutoSize = true;
            lblNovaTurmaAluno.Location = new Point(334, 35);
            lblNovaTurmaAluno.Name = "lblNovaTurmaAluno";
            lblNovaTurmaAluno.Size = new Size(226, 20);
            lblNovaTurmaAluno.TabIndex = 6;
            lblNovaTurmaAluno.Text = "Nova Turma (para transferência):";
            // 
            // txtBuscarAluno
            // 
            txtBuscarAluno.Location = new Point(592, 97);
            txtBuscarAluno.Name = "txtBuscarAluno";
            txtBuscarAluno.Size = new Size(214, 27);
            txtBuscarAluno.TabIndex = 7;
            // 
            // lblBuscarAluno
            // 
            lblBuscarAluno.AutoSize = true;
            lblBuscarAluno.Location = new Point(334, 97);
            lblBuscarAluno.Name = "lblBuscarAluno";
            lblBuscarAluno.Size = new Size(242, 20);
            lblBuscarAluno.TabIndex = 8;
            lblBuscarAluno.Text = "Buscar Aluno (ID, Nome ou Turma):";
            // 
            // txtTurmaAluno
            // 
            txtTurmaAluno.Location = new Point(592, 155);
            txtTurmaAluno.Name = "txtTurmaAluno";
            txtTurmaAluno.Size = new Size(214, 27);
            txtTurmaAluno.TabIndex = 9;
            // 
            // lblTurmaAluno
            // 
            lblTurmaAluno.AutoSize = true;
            lblTurmaAluno.Location = new Point(482, 162);
            lblTurmaAluno.Name = "lblTurmaAluno";
            lblTurmaAluno.Size = new Size(94, 20);
            lblTurmaAluno.TabIndex = 10;
            lblTurmaAluno.Text = "ID da Turma:";
            // 
            // txtEmailAluno
            // 
            txtEmailAluno.Location = new Point(577, 210);
            txtEmailAluno.Name = "txtEmailAluno";
            txtEmailAluno.Size = new Size(125, 27);
            txtEmailAluno.TabIndex = 11;
            // 
            // lblEmailAluno
            // 
            lblEmailAluno.AutoSize = true;
            lblEmailAluno.Location = new Point(511, 217);
            lblEmailAluno.Name = "lblEmailAluno";
            lblEmailAluno.Size = new Size(49, 20);
            lblEmailAluno.TabIndex = 12;
            lblEmailAluno.Text = "Email:";
            // 
            // lblMoradaAluno
            // 
            lblMoradaAluno.AutoSize = true;
            lblMoradaAluno.Location = new Point(369, 281);
            lblMoradaAluno.Name = "lblMoradaAluno";
            lblMoradaAluno.Size = new Size(64, 20);
            lblMoradaAluno.TabIndex = 13;
            lblMoradaAluno.Text = "Morada:";
            // 
            // txtMoradaAluno
            // 
            txtMoradaAluno.Location = new Point(551, 281);
            txtMoradaAluno.Name = "txtMoradaAluno";
            txtMoradaAluno.Size = new Size(192, 27);
            txtMoradaAluno.TabIndex = 14;
            // 
            // txtContatoAluno
            // 
            txtContatoAluno.Location = new Point(550, 342);
            txtContatoAluno.Name = "txtContatoAluno";
            txtContatoAluno.Size = new Size(125, 27);
            txtContatoAluno.TabIndex = 15;
            // 
            // lblContatoAluno
            // 
            lblContatoAluno.AutoSize = true;
            lblContatoAluno.Location = new Point(415, 346);
            lblContatoAluno.Name = "lblContatoAluno";
            lblContatoAluno.Size = new Size(65, 20);
            lblContatoAluno.TabIndex = 16;
            lblContatoAluno.Text = "Contato:";
            // 
            // dtpNascimentoAluno
            // 
            dtpNascimentoAluno.Location = new Point(567, 396);
            dtpNascimentoAluno.Name = "dtpNascimentoAluno";
            dtpNascimentoAluno.Size = new Size(250, 27);
            dtpNascimentoAluno.TabIndex = 17;
            // 
            // lblDataNascimento
            // 
            lblDataNascimento.AutoSize = true;
            lblDataNascimento.Location = new Point(396, 403);
            lblDataNascimento.Name = "lblDataNascimento";
            lblDataNascimento.Size = new Size(148, 20);
            lblDataNascimento.TabIndex = 18;
            lblDataNascimento.Text = "Data de Nascimento:";
            // 
            // txtNomeAluno
            // 
            txtNomeAluno.Location = new Point(630, 453);
            txtNomeAluno.Name = "txtNomeAluno";
            txtNomeAluno.Size = new Size(125, 27);
            txtNomeAluno.TabIndex = 19;
            // 
            // lblNomeAluno
            // 
            lblNomeAluno.AutoSize = true;
            lblNomeAluno.Location = new Point(426, 456);
            lblNomeAluno.Name = "lblNomeAluno";
            lblNomeAluno.Size = new Size(118, 20);
            lblNomeAluno.TabIndex = 20;
            lblNomeAluno.Text = "Nome do Aluno:";
            // 
            // txtIdAluno
            // 
            txtIdAluno.Location = new Point(661, 513);
            txtIdAluno.Name = "txtIdAluno";
            txtIdAluno.Size = new Size(125, 27);
            txtIdAluno.TabIndex = 21;
            // 
            // lblIdAluno
            // 
            lblIdAluno.AutoSize = true;
            lblIdAluno.Location = new Point(488, 510);
            lblIdAluno.Name = "lblIdAluno";
            lblIdAluno.Size = new Size(92, 20);
            lblIdAluno.TabIndex = 22;
            lblIdAluno.Text = "ID do Aluno:";
            // 
            // cmbNovaTurmaAluno
            // 
            cmbNovaTurmaAluno.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNovaTurmaAluno.FormattingEnabled = true;
            cmbNovaTurmaAluno.Location = new Point(592, 32);
            cmbNovaTurmaAluno.Name = "cmbNovaTurmaAluno";
            cmbNovaTurmaAluno.Size = new Size(214, 28);
            cmbNovaTurmaAluno.TabIndex = 23;
            // 
            // FormAluno
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1251, 783);
            Controls.Add(cmbNovaTurmaAluno);
            Controls.Add(lblIdAluno);
            Controls.Add(txtIdAluno);
            Controls.Add(lblNomeAluno);
            Controls.Add(txtNomeAluno);
            Controls.Add(lblDataNascimento);
            Controls.Add(dtpNascimentoAluno);
            Controls.Add(lblContatoAluno);
            Controls.Add(txtContatoAluno);
            Controls.Add(txtMoradaAluno);
            Controls.Add(lblMoradaAluno);
            Controls.Add(lblEmailAluno);
            Controls.Add(txtEmailAluno);
            Controls.Add(lblTurmaAluno);
            Controls.Add(txtTurmaAluno);
            Controls.Add(lblBuscarAluno);
            Controls.Add(txtBuscarAluno);
            Controls.Add(lblNovaTurmaAluno);
            Controls.Add(txtNovaTurmaAluno);
            Controls.Add(btnBuscarAluno);
            Controls.Add(btnMudarTurma);
            Controls.Add(btnRemoverAluno);
            Controls.Add(btnAdicionarAluno);
            Controls.Add(lstAlunos);
            Name = "FormAluno";
            Text = "FormAluno";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstAlunos;
        private Button btnAdicionarAluno;
        private Button btnRemoverAluno;
        private Button btnMudarTurma;
        private Button btnBuscarAluno;
        private TextBox txtNovaTurmaAluno;
        private Label lblNovaTurmaAluno;
        private TextBox txtBuscarAluno;
        private Label lblBuscarAluno;
        private TextBox txtTurmaAluno;
        private Label lblTurmaAluno;
        private TextBox txtEmailAluno;
        private Label lblEmailAluno;
        private Label lblMoradaAluno;
        private TextBox txtMoradaAluno;
        private TextBox txtContatoAluno;
        private Label lblContatoAluno;
        private DateTimePicker dtpNascimentoAluno;
        private Label lblDataNascimento;
        private TextBox txtNomeAluno;
        private Label lblNomeAluno;
        private TextBox txtIdAluno;
        private Label lblIdAluno;
        private ComboBox cmbNovaTurmaAluno;
    }
}