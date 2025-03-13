namespace Sistema_de_Gestão_Escolar
{
    partial class FormDisciplina
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
            lblNomeDisciplina = new Label();
            lblProfessoresDisc = new Label();
            lblCargaHoraria = new Label();
            lblIdDisciplina = new Label();
            txtIdDisciplina = new TextBox();
            txtNomeDisciplina = new TextBox();
            txtCargaHoraria = new TextBox();
            txtProfessoresDisciplina = new TextBox();
            btnAdicionarDisciplina = new Button();
            btnRemoverDisciplina = new Button();
            lstDisciplinas = new ListBox();
            SuspendLayout();
            // 
            // lblNomeDisciplina
            // 
            lblNomeDisciplina.AutoSize = true;
            lblNomeDisciplina.Location = new Point(62, 97);
            lblNomeDisciplina.Name = "lblNomeDisciplina";
            lblNomeDisciplina.Size = new Size(143, 20);
            lblNomeDisciplina.TabIndex = 0;
            lblNomeDisciplina.Text = "Nome da Disciplina:";
            // 
            // lblProfessoresDisc
            // 
            lblProfessoresDisc.AutoSize = true;
            lblProfessoresDisc.Location = new Point(42, 217);
            lblProfessoresDisc.Name = "lblProfessoresDisc";
            lblProfessoresDisc.Size = new Size(298, 20);
            lblProfessoresDisc.TabIndex = 1;
            lblProfessoresDisc.Text = "IDs dos Professores (separados por vírgula):";
            // 
            // lblCargaHoraria
            // 
            lblCargaHoraria.AutoSize = true;
            lblCargaHoraria.Location = new Point(62, 158);
            lblCargaHoraria.Name = "lblCargaHoraria";
            lblCargaHoraria.Size = new Size(158, 20);
            lblCargaHoraria.TabIndex = 2;
            lblCargaHoraria.Text = "Carga Horária (Horas):";
            // 
            // lblIdDisciplina
            // 
            lblIdDisciplina.AutoSize = true;
            lblIdDisciplina.Location = new Point(62, 45);
            lblIdDisciplina.Name = "lblIdDisciplina";
            lblIdDisciplina.Size = new Size(117, 20);
            lblIdDisciplina.TabIndex = 3;
            lblIdDisciplina.Text = "ID da Disciplina:";
            // 
            // txtIdDisciplina
            // 
            txtIdDisciplina.Location = new Point(210, 42);
            txtIdDisciplina.Name = "txtIdDisciplina";
            txtIdDisciplina.Size = new Size(258, 27);
            txtIdDisciplina.TabIndex = 4;
            // 
            // txtNomeDisciplina
            // 
            txtNomeDisciplina.Location = new Point(227, 90);
            txtNomeDisciplina.Name = "txtNomeDisciplina";
            txtNomeDisciplina.Size = new Size(205, 27);
            txtNomeDisciplina.TabIndex = 5;
            txtNomeDisciplina.TextChanged += textBox2_TextChanged;
            // 
            // txtCargaHoraria
            // 
            txtCargaHoraria.Location = new Point(244, 151);
            txtCargaHoraria.Name = "txtCargaHoraria";
            txtCargaHoraria.Size = new Size(224, 27);
            txtCargaHoraria.TabIndex = 6;
            // 
            // txtProfessoresDisciplina
            // 
            txtProfessoresDisciplina.Location = new Point(62, 254);
            txtProfessoresDisciplina.Name = "txtProfessoresDisciplina";
            txtProfessoresDisciplina.Size = new Size(253, 27);
            txtProfessoresDisciplina.TabIndex = 7;
            // 
            // btnAdicionarDisciplina
            // 
            btnAdicionarDisciplina.Location = new Point(51, 348);
            btnAdicionarDisciplina.Name = "btnAdicionarDisciplina";
            btnAdicionarDisciplina.Size = new Size(119, 68);
            btnAdicionarDisciplina.TabIndex = 8;
            btnAdicionarDisciplina.Text = "Adicionar nova disciplina";
            btnAdicionarDisciplina.UseVisualStyleBackColor = true;
            btnAdicionarDisciplina.Click += btnAdicionarDisciplina_Click;
            // 
            // btnRemoverDisciplina
            // 
            btnRemoverDisciplina.Location = new Point(304, 348);
            btnRemoverDisciplina.Name = "btnRemoverDisciplina";
            btnRemoverDisciplina.Size = new Size(164, 68);
            btnRemoverDisciplina.TabIndex = 9;
            btnRemoverDisciplina.Text = "Remover disciplina selecionada";
            btnRemoverDisciplina.UseVisualStyleBackColor = true;
            btnRemoverDisciplina.Click += btnRemoverDisciplina_Click;
            // 
            // lstDisciplinas
            // 
            lstDisciplinas.FormattingEnabled = true;
            lstDisciplinas.Location = new Point(572, 12);
            lstDisciplinas.Name = "lstDisciplinas";
            lstDisciplinas.Size = new Size(316, 404);
            lstDisciplinas.TabIndex = 10;
            // 
            // FormDisciplina
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1027, 639);
            Controls.Add(lstDisciplinas);
            Controls.Add(btnRemoverDisciplina);
            Controls.Add(btnAdicionarDisciplina);
            Controls.Add(txtProfessoresDisciplina);
            Controls.Add(txtCargaHoraria);
            Controls.Add(txtNomeDisciplina);
            Controls.Add(txtIdDisciplina);
            Controls.Add(lblIdDisciplina);
            Controls.Add(lblCargaHoraria);
            Controls.Add(lblProfessoresDisc);
            Controls.Add(lblNomeDisciplina);
            Name = "FormDisciplina";
            Text = "FormDisciplina";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNomeDisciplina;
        private Label lblProfessoresDisc;
        private Label lblCargaHoraria;
        private Label lblIdDisciplina;
        private TextBox txtIdDisciplina;
        private TextBox txtNomeDisciplina;
        private TextBox txtCargaHoraria;
        private TextBox txtProfessoresDisciplina;
        private Button btnAdicionarDisciplina;
        private Button btnRemoverDisciplina;
        private ListBox lstDisciplinas;
    }
}