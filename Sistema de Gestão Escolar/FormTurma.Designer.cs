namespace Sistema_de_Gestão_Escolar
{
    partial class FormTurma
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
            lblIdTurma = new Label();
            lblCursoTurma = new Label();
            lblAnoLetivoTurma = new Label();
            lblAlunosTurma = new Label();
            lblTurnoTurma = new Label();
            lblDisciplinasTurma = new Label();
            txtAnoLetivoTurma = new TextBox();
            txtAlunosTurma = new TextBox();
            txtIdTurma = new TextBox();
            cmbTurnoTurma = new ComboBox();
            btnRemoverTurma = new Button();
            btnAdicionarTurma = new Button();
            lstTurmas = new ListBox();
            cmbCursoTurma = new ComboBox();
            cmbDisciplinasTurma = new ComboBox();
            btnEditarTurma = new Button();
            btnConsultarTurma = new Button();
            cmbProfessorTurma = new ComboBox();
            label1 = new Label();
            btnSalvarEdicaoTurma = new Button();
            SuspendLayout();
            // 
            // lblIdTurma
            // 
            lblIdTurma.AutoSize = true;
            lblIdTurma.Location = new Point(43, 61);
            lblIdTurma.Name = "lblIdTurma";
            lblIdTurma.Size = new Size(94, 20);
            lblIdTurma.TabIndex = 0;
            lblIdTurma.Text = "ID da Turma:";
            // 
            // lblCursoTurma
            // 
            lblCursoTurma.AutoSize = true;
            lblCursoTurma.Location = new Point(43, 116);
            lblCursoTurma.Name = "lblCursoTurma";
            lblCursoTurma.Size = new Size(49, 20);
            lblCursoTurma.TabIndex = 1;
            lblCursoTurma.Text = "Curso:";
            // 
            // lblAnoLetivoTurma
            // 
            lblAnoLetivoTurma.AutoSize = true;
            lblAnoLetivoTurma.Location = new Point(43, 170);
            lblAnoLetivoTurma.Name = "lblAnoLetivoTurma";
            lblAnoLetivoTurma.Size = new Size(83, 20);
            lblAnoLetivoTurma.TabIndex = 2;
            lblAnoLetivoTurma.Text = "Ano Letivo:";
            // 
            // lblAlunosTurma
            // 
            lblAlunosTurma.AutoSize = true;
            lblAlunosTurma.Location = new Point(43, 236);
            lblAlunosTurma.Name = "lblAlunosTurma";
            lblAlunosTurma.Size = new Size(268, 20);
            lblAlunosTurma.TabIndex = 3;
            lblAlunosTurma.Text = "IDs dos Alunos (separados por vírgula):";
            // 
            // lblTurnoTurma
            // 
            lblTurnoTurma.AutoSize = true;
            lblTurnoTurma.Location = new Point(53, 336);
            lblTurnoTurma.Name = "lblTurnoTurma";
            lblTurnoTurma.Size = new Size(50, 20);
            lblTurnoTurma.TabIndex = 4;
            lblTurnoTurma.Text = "Turno:";
            // 
            // lblDisciplinasTurma
            // 
            lblDisciplinasTurma.AutoSize = true;
            lblDisciplinasTurma.Location = new Point(43, 397);
            lblDisciplinasTurma.Name = "lblDisciplinasTurma";
            lblDisciplinasTurma.Size = new Size(161, 20);
            lblDisciplinasTurma.TabIndex = 5;
            lblDisciplinasTurma.Text = "Disciplinas disponíveis:";
            // 
            // txtAnoLetivoTurma
            // 
            txtAnoLetivoTurma.Location = new Point(145, 163);
            txtAnoLetivoTurma.Name = "txtAnoLetivoTurma";
            txtAnoLetivoTurma.Size = new Size(253, 27);
            txtAnoLetivoTurma.TabIndex = 7;
            // 
            // txtAlunosTurma
            // 
            txtAlunosTurma.Location = new Point(53, 269);
            txtAlunosTurma.Name = "txtAlunosTurma";
            txtAlunosTurma.Size = new Size(253, 27);
            txtAlunosTurma.TabIndex = 8;
            // 
            // txtIdTurma
            // 
            txtIdTurma.Location = new Point(170, 54);
            txtIdTurma.Name = "txtIdTurma";
            txtIdTurma.Size = new Size(259, 27);
            txtIdTurma.TabIndex = 11;
            // 
            // cmbTurnoTurma
            // 
            cmbTurnoTurma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTurnoTurma.FormattingEnabled = true;
            cmbTurnoTurma.Location = new Point(121, 333);
            cmbTurnoTurma.Name = "cmbTurnoTurma";
            cmbTurnoTurma.Size = new Size(268, 28);
            cmbTurnoTurma.TabIndex = 12;
            cmbTurnoTurma.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnRemoverTurma
            // 
            btnRemoverTurma.Location = new Point(53, 657);
            btnRemoverTurma.Name = "btnRemoverTurma";
            btnRemoverTurma.Size = new Size(139, 70);
            btnRemoverTurma.TabIndex = 13;
            btnRemoverTurma.Text = "Remover turma selecionada";
            btnRemoverTurma.UseVisualStyleBackColor = true;
            btnRemoverTurma.Click += btnRemoverTurma_Click;
            // 
            // btnAdicionarTurma
            // 
            btnAdicionarTurma.Location = new Point(233, 657);
            btnAdicionarTurma.Name = "btnAdicionarTurma";
            btnAdicionarTurma.Size = new Size(156, 70);
            btnAdicionarTurma.TabIndex = 14;
            btnAdicionarTurma.Text = "Adicionar nova turma";
            btnAdicionarTurma.UseVisualStyleBackColor = true;
            btnAdicionarTurma.Click += btnAdicionarTurma_Click;
            // 
            // lstTurmas
            // 
            lstTurmas.FormattingEnabled = true;
            lstTurmas.Location = new Point(644, 54);
            lstTurmas.Name = "lstTurmas";
            lstTurmas.Size = new Size(735, 444);
            lstTurmas.TabIndex = 15;
            lstTurmas.SelectedIndexChanged += lstTurmas_SelectedIndexChanged;
            // 
            // cmbCursoTurma
            // 
            cmbCursoTurma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCursoTurma.FormattingEnabled = true;
            cmbCursoTurma.Location = new Point(147, 110);
            cmbCursoTurma.Name = "cmbCursoTurma";
            cmbCursoTurma.Size = new Size(373, 28);
            cmbCursoTurma.TabIndex = 16;
            // 
            // cmbDisciplinasTurma
            // 
            cmbDisciplinasTurma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisciplinasTurma.FormattingEnabled = true;
            cmbDisciplinasTurma.Location = new Point(232, 389);
            cmbDisciplinasTurma.Name = "cmbDisciplinasTurma";
            cmbDisciplinasTurma.Size = new Size(288, 28);
            cmbDisciplinasTurma.TabIndex = 17;
            // 
            // btnEditarTurma
            // 
            btnEditarTurma.Location = new Point(457, 657);
            btnEditarTurma.Name = "btnEditarTurma";
            btnEditarTurma.Size = new Size(149, 70);
            btnEditarTurma.TabIndex = 18;
            btnEditarTurma.Text = "Editar turma";
            btnEditarTurma.UseVisualStyleBackColor = true;
            btnEditarTurma.Click += btnEditarTurma_Click;
            // 
            // btnConsultarTurma
            // 
            btnConsultarTurma.Location = new Point(991, 561);
            btnConsultarTurma.Name = "btnConsultarTurma";
            btnConsultarTurma.Size = new Size(144, 59);
            btnConsultarTurma.TabIndex = 19;
            btnConsultarTurma.Text = "Consultar turma";
            btnConsultarTurma.UseVisualStyleBackColor = true;
            btnConsultarTurma.Click += btnConsultarTurma_Click;
            // 
            // cmbProfessorTurma
            // 
            cmbProfessorTurma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProfessorTurma.FormattingEnabled = true;
            cmbProfessorTurma.Location = new Point(238, 456);
            cmbProfessorTurma.Name = "cmbProfessorTurma";
            cmbProfessorTurma.Size = new Size(204, 28);
            cmbProfessorTurma.TabIndex = 20;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 461);
            label1.Name = "label1";
            label1.Size = new Size(165, 20);
            label1.TabIndex = 21;
            label1.Text = "Professores disponíveis:";
            // 
            // btnSalvarEdicaoTurma
            // 
            btnSalvarEdicaoTurma.Location = new Point(1177, 561);
            btnSalvarEdicaoTurma.Name = "btnSalvarEdicaoTurma";
            btnSalvarEdicaoTurma.Size = new Size(135, 59);
            btnSalvarEdicaoTurma.TabIndex = 22;
            btnSalvarEdicaoTurma.Text = "Salvar edição turma";
            btnSalvarEdicaoTurma.UseVisualStyleBackColor = true;
            btnSalvarEdicaoTurma.Click += btnSalvarEdicaoTurma_Click;
            // 
            // FormTurma
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1409, 788);
            Controls.Add(btnSalvarEdicaoTurma);
            Controls.Add(label1);
            Controls.Add(cmbProfessorTurma);
            Controls.Add(btnConsultarTurma);
            Controls.Add(btnEditarTurma);
            Controls.Add(cmbDisciplinasTurma);
            Controls.Add(cmbCursoTurma);
            Controls.Add(lstTurmas);
            Controls.Add(btnAdicionarTurma);
            Controls.Add(btnRemoverTurma);
            Controls.Add(cmbTurnoTurma);
            Controls.Add(txtIdTurma);
            Controls.Add(txtAlunosTurma);
            Controls.Add(txtAnoLetivoTurma);
            Controls.Add(lblDisciplinasTurma);
            Controls.Add(lblTurnoTurma);
            Controls.Add(lblAlunosTurma);
            Controls.Add(lblAnoLetivoTurma);
            Controls.Add(lblCursoTurma);
            Controls.Add(lblIdTurma);
            Name = "FormTurma";
            Text = "FormTurma";
            Load += FormTurma_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIdTurma;
        private Label lblCursoTurma;
        private Label lblAnoLetivoTurma;
        private Label lblAlunosTurma;
        private Label lblTurnoTurma;
        private Label lblDisciplinasTurma;
        private TextBox txtAnoLetivoTurma;
        private TextBox txtAlunosTurma;
        private TextBox txtIdTurma;
        private ComboBox cmbTurnoTurma;
        private Button btnRemoverTurma;
        private Button btnAdicionarTurma;
        private ListBox lstTurmas;
        private ComboBox cmbCursoTurma;
        private ComboBox cmbDisciplinasTurma;
        private Button btnEditarTurma;
        private Button btnConsultarTurma;
        private ComboBox cmbProfessorTurma;
        private Label label1;
        private Button btnSalvarEdicaoTurma;
    }
}