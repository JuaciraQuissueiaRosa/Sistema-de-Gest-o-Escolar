

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
            btnAdicionarDisciplina = new Button();
            btnRemoverDisciplina = new Button();
            btnConsultarDisciplina = new Button();
            btnEditarDisciplina = new Button();
            btnSalvarEdicaoDisciplina = new Button();
            txtProfessoresDisciplina = new TextBox();
            cmbNomeDisciplina = new ComboBox();
            cmbCargaHoraria = new ComboBox();
            txtIdDisciplina = new TextBox();
            lstDisciplinas = new ListView();
            SuspendLayout();
            // 
            // lblNomeDisciplina
            // 
            lblNomeDisciplina.AutoSize = true;
            lblNomeDisciplina.Location = new Point(40, 94);
            lblNomeDisciplina.Name = "lblNomeDisciplina";
            lblNomeDisciplina.Size = new Size(143, 20);
            lblNomeDisciplina.TabIndex = 0;
            lblNomeDisciplina.Text = "Nome da Disciplina:";
            // 
            // lblProfessoresDisc
            // 
            lblProfessoresDisc.AutoSize = true;
            lblProfessoresDisc.Location = new Point(20, 214);
            lblProfessoresDisc.Name = "lblProfessoresDisc";
            lblProfessoresDisc.Size = new Size(298, 20);
            lblProfessoresDisc.TabIndex = 1;
            lblProfessoresDisc.Text = "IDs dos Professores (separados por vírgula):";
            // 
            // lblCargaHoraria
            // 
            lblCargaHoraria.AutoSize = true;
            lblCargaHoraria.Location = new Point(40, 155);
            lblCargaHoraria.Name = "lblCargaHoraria";
            lblCargaHoraria.Size = new Size(219, 20);
            lblCargaHoraria.TabIndex = 2;
            lblCargaHoraria.Text = "Carga Horária Semanal (Horas):";
            // 
            // lblIdDisciplina
            // 
            lblIdDisciplina.AutoSize = true;
            lblIdDisciplina.Location = new Point(40, 42);
            lblIdDisciplina.Name = "lblIdDisciplina";
            lblIdDisciplina.Size = new Size(117, 20);
            lblIdDisciplina.TabIndex = 3;
            lblIdDisciplina.Text = "ID da Disciplina:";
            // 
            // btnAdicionarDisciplina
            // 
            btnAdicionarDisciplina.Location = new Point(20, 348);
            btnAdicionarDisciplina.Name = "btnAdicionarDisciplina";
            btnAdicionarDisciplina.Size = new Size(119, 68);
            btnAdicionarDisciplina.TabIndex = 8;
            btnAdicionarDisciplina.Text = "Adicionar nova disciplina";
            btnAdicionarDisciplina.UseVisualStyleBackColor = true;
            btnAdicionarDisciplina.Click += btnAdicionarDisciplina_Click;
            // 
            // btnRemoverDisciplina
            // 
            btnRemoverDisciplina.Location = new Point(193, 378);
            btnRemoverDisciplina.Name = "btnRemoverDisciplina";
            btnRemoverDisciplina.Size = new Size(164, 68);
            btnRemoverDisciplina.TabIndex = 9;
            btnRemoverDisciplina.Text = "Remover disciplina selecionada";
            btnRemoverDisciplina.UseVisualStyleBackColor = true;
            btnRemoverDisciplina.Click += btnRemoverDisciplina_Click;
            // 
            // btnConsultarDisciplina
            // 
            btnConsultarDisciplina.Location = new Point(40, 489);
            btnConsultarDisciplina.Name = "btnConsultarDisciplina";
            btnConsultarDisciplina.Size = new Size(128, 51);
            btnConsultarDisciplina.TabIndex = 13;
            btnConsultarDisciplina.Text = "Consultar disciplina";
            btnConsultarDisciplina.UseVisualStyleBackColor = true;
            btnConsultarDisciplina.Click += btnConsultarDisciplina_Click;
            // 
            // btnEditarDisciplina
            // 
            btnEditarDisciplina.Location = new Point(208, 509);
            btnEditarDisciplina.Name = "btnEditarDisciplina";
            btnEditarDisciplina.Size = new Size(126, 67);
            btnEditarDisciplina.TabIndex = 15;
            btnEditarDisciplina.Text = "Editar disciplina";
            btnEditarDisciplina.UseVisualStyleBackColor = true;
            btnEditarDisciplina.Click += btnEditarDisciplina_Click;
            // 
            // btnSalvarEdicaoDisciplina
            // 
            btnSalvarEdicaoDisciplina.Location = new Point(420, 453);
            btnSalvarEdicaoDisciplina.Name = "btnSalvarEdicaoDisciplina";
            btnSalvarEdicaoDisciplina.Size = new Size(132, 76);
            btnSalvarEdicaoDisciplina.TabIndex = 16;
            btnSalvarEdicaoDisciplina.Text = "Salvar disciplina editada";
            btnSalvarEdicaoDisciplina.UseVisualStyleBackColor = true;
            btnSalvarEdicaoDisciplina.Click += btnSalvarEdicaoDisciplina_Click;
            // 
            // txtProfessoresDisciplina
            // 
            txtProfessoresDisciplina.Location = new Point(40, 251);
            txtProfessoresDisciplina.Name = "txtProfessoresDisciplina";
            txtProfessoresDisciplina.Size = new Size(253, 27);
            txtProfessoresDisciplina.TabIndex = 7;
            // 
            // cmbNomeDisciplina
            // 
            cmbNomeDisciplina.FormattingEnabled = true;
            cmbNomeDisciplina.Location = new Point(221, 94);
            cmbNomeDisciplina.Name = "cmbNomeDisciplina";
            cmbNomeDisciplina.Size = new Size(151, 28);
            cmbNomeDisciplina.TabIndex = 21;
            // 
            // cmbCargaHoraria
            // 
            cmbCargaHoraria.FormattingEnabled = true;
            cmbCargaHoraria.Location = new Point(282, 147);
            cmbCargaHoraria.Name = "cmbCargaHoraria";
            cmbCargaHoraria.Size = new Size(151, 28);
            cmbCargaHoraria.TabIndex = 22;
            // 
            // txtIdDisciplina
            // 
            txtIdDisciplina.Location = new Point(208, 39);
            txtIdDisciplina.Name = "txtIdDisciplina";
            txtIdDisciplina.Size = new Size(125, 27);
            txtIdDisciplina.TabIndex = 23;
            // 
            // lstDisciplinas
            // 
            lstDisciplinas.Location = new Point(643, 42);
            lstDisciplinas.Name = "lstDisciplinas";
            lstDisciplinas.Size = new Size(456, 473);
            lstDisciplinas.TabIndex = 24;
            lstDisciplinas.UseCompatibleStateImageBehavior = false;
            // 
            // FormDisciplina
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1412, 623);
            Controls.Add(lstDisciplinas);
            Controls.Add(txtIdDisciplina);
            Controls.Add(cmbCargaHoraria);
            Controls.Add(cmbNomeDisciplina);
            Controls.Add(btnSalvarEdicaoDisciplina);
            Controls.Add(btnEditarDisciplina);
            Controls.Add(btnConsultarDisciplina);
            Controls.Add(btnRemoverDisciplina);
            Controls.Add(btnAdicionarDisciplina);
            Controls.Add(txtProfessoresDisciplina);
            Controls.Add(lblIdDisciplina);
            Controls.Add(lblCargaHoraria);
            Controls.Add(lblProfessoresDisc);
            Controls.Add(lblNomeDisciplina);
            Name = "FormDisciplina";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormDisciplina";
            Load += FormDisciplina_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNomeDisciplina;
        private Label lblProfessoresDisc;
        private Label lblCargaHoraria;
        private Label lblIdDisciplina;

        private Button btnAdicionarDisciplina;
        private Button btnRemoverDisciplina;


        private Button btnConsultarDisciplina;
        private Button btnEditarDisciplina;
        private Button btnSalvarEdicaoDisciplina;
        private TextBox txtProfessoresDisciplina;
        private ComboBox cmbNomeDisciplina;
        private ComboBox cmbCargaHoraria;
        private TextBox txtIdDisciplina;
        private ListView lstDisciplinas;
    }
}
       