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
            txtAnoLetivoTurma = new TextBox();
            txtAlunosTurma = new TextBox();
            txtIdTurma = new TextBox();
            cmbTurnoTurma = new ComboBox();
            btnRemoverTurma = new Button();
            btnAdicionarTurma = new Button();
            cmbCursoTurma = new ComboBox();
            btnEditarTurma = new Button();
            btnConsultarTurma = new Button();
            btnSalvarEdicaoTurma = new Button();
            lstDisciplinasTurma = new ListBox();
            lstProfessoresTurma = new ListBox();
            lblProfessores = new Label();
            lblDisciplinas = new Label();
            lstTurmas = new ListView();
            SuspendLayout();
            // 
            // lblIdTurma
            // 
            lblIdTurma.AutoSize = true;
            lblIdTurma.Location = new Point(24, 19);
            lblIdTurma.Name = "lblIdTurma";
            lblIdTurma.Size = new Size(94, 20);
            lblIdTurma.TabIndex = 0;
            lblIdTurma.Text = "ID da Turma:";
            // 
            // lblCursoTurma
            // 
            lblCursoTurma.AutoSize = true;
            lblCursoTurma.Location = new Point(24, 75);
            lblCursoTurma.Name = "lblCursoTurma";
            lblCursoTurma.Size = new Size(49, 20);
            lblCursoTurma.TabIndex = 1;
            lblCursoTurma.Text = "Curso:";
            // 
            // lblAnoLetivoTurma
            // 
            lblAnoLetivoTurma.AutoSize = true;
            lblAnoLetivoTurma.Location = new Point(26, 150);
            lblAnoLetivoTurma.Name = "lblAnoLetivoTurma";
            lblAnoLetivoTurma.Size = new Size(83, 20);
            lblAnoLetivoTurma.TabIndex = 2;
            lblAnoLetivoTurma.Text = "Ano Letivo:";
            // 
            // lblAlunosTurma
            // 
            lblAlunosTurma.AutoSize = true;
            lblAlunosTurma.Location = new Point(24, 238);
            lblAlunosTurma.Name = "lblAlunosTurma";
            lblAlunosTurma.Size = new Size(268, 20);
            lblAlunosTurma.TabIndex = 3;
            lblAlunosTurma.Text = "IDs dos Alunos (separados por vírgula):";
            // 
            // lblTurnoTurma
            // 
            lblTurnoTurma.AutoSize = true;
            lblTurnoTurma.Location = new Point(24, 326);
            lblTurnoTurma.Name = "lblTurnoTurma";
            lblTurnoTurma.Size = new Size(50, 20);
            lblTurnoTurma.TabIndex = 4;
            lblTurnoTurma.Text = "Turno:";
            // 
            // txtAnoLetivoTurma
            // 
            txtAnoLetivoTurma.Location = new Point(26, 183);
            txtAnoLetivoTurma.Name = "txtAnoLetivoTurma";
            txtAnoLetivoTurma.Size = new Size(253, 27);
            txtAnoLetivoTurma.TabIndex = 7;
            // 
            // txtAlunosTurma
            // 
            txtAlunosTurma.Location = new Point(24, 277);
            txtAlunosTurma.Name = "txtAlunosTurma";
            txtAlunosTurma.Size = new Size(253, 27);
            txtAlunosTurma.TabIndex = 8;
            // 
            // txtIdTurma
            // 
            txtIdTurma.Location = new Point(138, 19);
            txtIdTurma.Name = "txtIdTurma";
            txtIdTurma.Size = new Size(122, 27);
            txtIdTurma.TabIndex = 11;
            // 
            // cmbTurnoTurma
            // 
            cmbTurnoTurma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTurnoTurma.FormattingEnabled = true;
            cmbTurnoTurma.Location = new Point(24, 364);
            cmbTurnoTurma.Name = "cmbTurnoTurma";
            cmbTurnoTurma.Size = new Size(268, 28);
            cmbTurnoTurma.TabIndex = 12;
            cmbTurnoTurma.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnRemoverTurma
            // 
            btnRemoverTurma.Location = new Point(1012, 390);
            btnRemoverTurma.Name = "btnRemoverTurma";
            btnRemoverTurma.Size = new Size(159, 63);
            btnRemoverTurma.TabIndex = 13;
            btnRemoverTurma.Text = "Remover turma selecionada";
            btnRemoverTurma.UseVisualStyleBackColor = true;
            btnRemoverTurma.Click += btnRemoverTurma_Click;
            // 
            // btnAdicionarTurma
            // 
            btnAdicionarTurma.Location = new Point(1012, 295);
            btnAdicionarTurma.Name = "btnAdicionarTurma";
            btnAdicionarTurma.Size = new Size(156, 59);
            btnAdicionarTurma.TabIndex = 14;
            btnAdicionarTurma.Text = "Adicionar nova turma";
            btnAdicionarTurma.UseVisualStyleBackColor = true;
            btnAdicionarTurma.Click += btnAdicionarTurma_Click;
            // 
            // cmbCursoTurma
            // 
            cmbCursoTurma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCursoTurma.FormattingEnabled = true;
            cmbCursoTurma.Location = new Point(18, 98);
            cmbCursoTurma.Name = "cmbCursoTurma";
            cmbCursoTurma.Size = new Size(373, 28);
            cmbCursoTurma.TabIndex = 16;
            // 
            // btnEditarTurma
            // 
            btnEditarTurma.Location = new Point(1012, 586);
            btnEditarTurma.Name = "btnEditarTurma";
            btnEditarTurma.Size = new Size(156, 59);
            btnEditarTurma.TabIndex = 18;
            btnEditarTurma.Text = "Editar turma";
            btnEditarTurma.UseVisualStyleBackColor = true;
            btnEditarTurma.Click += btnEditarTurma_Click;
            // 
            // btnConsultarTurma
            // 
            btnConsultarTurma.Location = new Point(1012, 688);
            btnConsultarTurma.Name = "btnConsultarTurma";
            btnConsultarTurma.Size = new Size(156, 59);
            btnConsultarTurma.TabIndex = 19;
            btnConsultarTurma.Text = "Consultar turma";
            btnConsultarTurma.UseVisualStyleBackColor = true;
            btnConsultarTurma.Click += btnConsultarTurma_Click;
            // 
            // btnSalvarEdicaoTurma
            // 
            btnSalvarEdicaoTurma.Location = new Point(1012, 486);
            btnSalvarEdicaoTurma.Name = "btnSalvarEdicaoTurma";
            btnSalvarEdicaoTurma.Size = new Size(159, 59);
            btnSalvarEdicaoTurma.TabIndex = 22;
            btnSalvarEdicaoTurma.Text = "Salvar edição turma";
            btnSalvarEdicaoTurma.UseVisualStyleBackColor = true;
            btnSalvarEdicaoTurma.Click += btnSalvarEdicaoTurma_Click;
            // 
            // lstDisciplinasTurma
            // 
            lstDisciplinasTurma.FormattingEnabled = true;
            lstDisciplinasTurma.Location = new Point(18, 480);
            lstDisciplinasTurma.Name = "lstDisciplinasTurma";
            lstDisciplinasTurma.SelectionMode = SelectionMode.MultiExtended;
            lstDisciplinasTurma.Size = new Size(286, 304);
            lstDisciplinasTurma.TabIndex = 23;
            // 
            // lstProfessoresTurma
            // 
            lstProfessoresTurma.FormattingEnabled = true;
            lstProfessoresTurma.Location = new Point(345, 480);
            lstProfessoresTurma.Name = "lstProfessoresTurma";
            lstProfessoresTurma.SelectionMode = SelectionMode.MultiExtended;
            lstProfessoresTurma.Size = new Size(291, 304);
            lstProfessoresTurma.TabIndex = 24;
            // 
            // lblProfessores
            // 
            lblProfessores.AutoSize = true;
            lblProfessores.Location = new Point(345, 444);
            lblProfessores.Name = "lblProfessores";
            lblProfessores.Size = new Size(165, 20);
            lblProfessores.TabIndex = 21;
            lblProfessores.Text = "Professores disponíveis:";
            // 
            // lblDisciplinas
            // 
            lblDisciplinas.AutoSize = true;
            lblDisciplinas.Location = new Point(26, 444);
            lblDisciplinas.Name = "lblDisciplinas";
            lblDisciplinas.Size = new Size(161, 20);
            lblDisciplinas.TabIndex = 25;
            lblDisciplinas.Text = "Disciplinas disponíveis:";
            // 
            // lstTurmas
            // 
            lstTurmas.Location = new Point(463, 19);
            lstTurmas.Name = "lstTurmas";
            lstTurmas.Size = new Size(471, 411);
            lstTurmas.TabIndex = 26;
            lstTurmas.UseCompatibleStateImageBehavior = false;
            lstTurmas.SelectedIndexChanged += lstTurmas_SelectedIndexChanged;
            // 
            // FormTurma
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1208, 796);
            Controls.Add(lstTurmas);
            Controls.Add(lblDisciplinas);
            Controls.Add(lstProfessoresTurma);
            Controls.Add(lstDisciplinasTurma);
            Controls.Add(btnSalvarEdicaoTurma);
            Controls.Add(lblProfessores);
            Controls.Add(btnConsultarTurma);
            Controls.Add(btnEditarTurma);
            Controls.Add(cmbCursoTurma);
            Controls.Add(btnAdicionarTurma);
            Controls.Add(btnRemoverTurma);
            Controls.Add(cmbTurnoTurma);
            Controls.Add(txtIdTurma);
            Controls.Add(txtAlunosTurma);
            Controls.Add(txtAnoLetivoTurma);
            Controls.Add(lblTurnoTurma);
            Controls.Add(lblAlunosTurma);
            Controls.Add(lblAnoLetivoTurma);
            Controls.Add(lblCursoTurma);
            Controls.Add(lblIdTurma);
            Name = "FormTurma";
            StartPosition = FormStartPosition.CenterScreen;
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
        private TextBox txtAnoLetivoTurma;
        private TextBox txtAlunosTurma;
        private TextBox txtIdTurma;
        private ComboBox cmbTurnoTurma;
        private Button btnRemoverTurma;
        private Button btnAdicionarTurma;
        private ComboBox cmbCursoTurma;
        private Button btnEditarTurma;
        private Button btnConsultarTurma;
        private ComboBox cmbProfessorTurma;
        private Button btnSalvarEdicaoTurma;
        private ListBox lstDisciplinasTurma;
        private ListBox lstProfessoresTurma;
        private Label lblProfessores;
        private Label lblDisciplinas;
        private ListView lstTurmas;
    }
}