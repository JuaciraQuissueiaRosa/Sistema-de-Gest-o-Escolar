namespace Sistema_de_Gestão_Escolar
{
    partial class FormPrincipal
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
            btnDisciplinas = new Button();
            btnNotas = new Button();
            btnProfessores = new Button();
            btnAlunos = new Button();
            btnTurmas = new Button();
            btnSair = new Button();
            SuspendLayout();
            // 
            // btnDisciplinas
            // 
            btnDisciplinas.Location = new Point(251, 83);
            btnDisciplinas.Name = "btnDisciplinas";
            btnDisciplinas.Size = new Size(116, 50);
            btnDisciplinas.TabIndex = 0;
            btnDisciplinas.Text = "Disciplinas";
            btnDisciplinas.UseVisualStyleBackColor = true;
            btnDisciplinas.Click += btnDisciplinas_Click;
            // 
            // btnNotas
            // 
            btnNotas.Location = new Point(169, 167);
            btnNotas.Name = "btnNotas";
            btnNotas.Size = new Size(116, 52);
            btnNotas.TabIndex = 1;
            btnNotas.Text = "Notas";
            btnNotas.UseVisualStyleBackColor = true;
            btnNotas.Click += btnNotas_Click;
            // 
            // btnProfessores
            // 
            btnProfessores.Location = new Point(104, 250);
            btnProfessores.Name = "btnProfessores";
            btnProfessores.Size = new Size(109, 54);
            btnProfessores.TabIndex = 2;
            btnProfessores.Text = "Professores";
            btnProfessores.UseVisualStyleBackColor = true;
            btnProfessores.Click += btnProfessores_Click;
            // 
            // btnAlunos
            // 
            btnAlunos.Location = new Point(251, 250);
            btnAlunos.Name = "btnAlunos";
            btnAlunos.Size = new Size(114, 51);
            btnAlunos.TabIndex = 3;
            btnAlunos.Text = "Alunos";
            btnAlunos.UseVisualStyleBackColor = true;
            btnAlunos.Click += btnAlunos_Click;
            // 
            // btnTurmas
            // 
            btnTurmas.Location = new Point(94, 83);
            btnTurmas.Name = "btnTurmas";
            btnTurmas.Size = new Size(104, 50);
            btnTurmas.TabIndex = 4;
            btnTurmas.Text = "Turmas";
            btnTurmas.UseVisualStyleBackColor = true;
            btnTurmas.Click += btnTurmas_Click;
            // 
            // btnSair
            // 
            btnSair.Location = new Point(191, 339);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(94, 44);
            btnSair.TabIndex = 5;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = true;
            btnSair.Click += btnSair_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 450);
            Controls.Add(btnSair);
            Controls.Add(btnTurmas);
            Controls.Add(btnAlunos);
            Controls.Add(btnProfessores);
            Controls.Add(btnNotas);
            Controls.Add(btnDisciplinas);
            Name = "FormPrincipal";
            Text = "FormPrincipal";
            Load += FormPrincipal_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnDisciplinas;
        private Button btnNotas;
        private Button btnProfessores;
        private Button btnAlunos;
        private Button btnTurmas;
        private Button btnSair;
    }
}