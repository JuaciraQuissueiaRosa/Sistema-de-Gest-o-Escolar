namespace Sistema_de_Gestão_Escolar
{
    partial class FormEvento
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
            cmbAluno = new ComboBox();
            cmbProfessor = new ComboBox();
            lstEventos = new ListBox();
            btnAdicionarEvento = new Button();
            btnRemoverEvento = new Button();
            txtNomeEvento = new TextBox();
            lblNomeEvento = new Label();
            lblDescricaoEvento = new Label();
            txtDescricaoEvento = new TextBox();
            lblDataEvento = new Label();
            dtpDataEvento = new DateTimePicker();
            btnEditarEvento = new Button();
            lblListaEventos = new Label();
            btnSelecionarEvento = new Button();
            lblAlunos = new Label();
            lblProfessores = new Label();
            lstAlunos = new ListBox();
            lstProfessores = new ListBox();
            SuspendLayout();
            // 
            // cmbAluno
            // 
            cmbAluno.FormattingEnabled = true;
            cmbAluno.Location = new Point(437, 221);
            cmbAluno.Name = "cmbAluno";
            cmbAluno.Size = new Size(151, 28);
            cmbAluno.TabIndex = 0;
            // 
            // cmbProfessor
            // 
            cmbProfessor.FormattingEnabled = true;
            cmbProfessor.Location = new Point(437, 326);
            cmbProfessor.Name = "cmbProfessor";
            cmbProfessor.Size = new Size(151, 28);
            cmbProfessor.TabIndex = 1;
            // 
            // lstEventos
            // 
            lstEventos.FormattingEnabled = true;
            lstEventos.Location = new Point(24, 36);
            lstEventos.Name = "lstEventos";
            lstEventos.Size = new Size(338, 244);
            lstEventos.TabIndex = 2;
            // 
            // btnAdicionarEvento
            // 
            btnAdicionarEvento.Location = new Point(417, 456);
            btnAdicionarEvento.Name = "btnAdicionarEvento";
            btnAdicionarEvento.Size = new Size(144, 54);
            btnAdicionarEvento.TabIndex = 3;
            btnAdicionarEvento.Text = "Adicionar Evento";
            btnAdicionarEvento.UseVisualStyleBackColor = true;
            btnAdicionarEvento.Click += btnAdicionarEvento_Click;
            // 
            // btnRemoverEvento
            // 
            btnRemoverEvento.Location = new Point(417, 607);
            btnRemoverEvento.Name = "btnRemoverEvento";
            btnRemoverEvento.Size = new Size(144, 64);
            btnRemoverEvento.TabIndex = 4;
            btnRemoverEvento.Text = "Remover Evento";
            btnRemoverEvento.UseVisualStyleBackColor = true;
            btnRemoverEvento.Click += btnRemoverEvento_Click;
            // 
            // txtNomeEvento
            // 
            txtNomeEvento.Location = new Point(437, 71);
            txtNomeEvento.Name = "txtNomeEvento";
            txtNomeEvento.Size = new Size(144, 27);
            txtNomeEvento.TabIndex = 5;
            // 
            // lblNomeEvento
            // 
            lblNomeEvento.AutoSize = true;
            lblNomeEvento.Location = new Point(450, 36);
            lblNomeEvento.Name = "lblNomeEvento";
            lblNomeEvento.Size = new Size(124, 20);
            lblNomeEvento.TabIndex = 6;
            lblNomeEvento.Text = "Nome do Evento:";
            // 
            // lblDescricaoEvento
            // 
            lblDescricaoEvento.AutoSize = true;
            lblDescricaoEvento.Location = new Point(766, 34);
            lblDescricaoEvento.Name = "lblDescricaoEvento";
            lblDescricaoEvento.Size = new Size(77, 20);
            lblDescricaoEvento.TabIndex = 7;
            lblDescricaoEvento.Text = "Descrição:";
            // 
            // txtDescricaoEvento
            // 
            txtDescricaoEvento.Location = new Point(765, 80);
            txtDescricaoEvento.Name = "txtDescricaoEvento";
            txtDescricaoEvento.Size = new Size(125, 27);
            txtDescricaoEvento.TabIndex = 8;
            // 
            // lblDataEvento
            // 
            lblDataEvento.AutoSize = true;
            lblDataEvento.Location = new Point(641, 221);
            lblDataEvento.Name = "lblDataEvento";
            lblDataEvento.Size = new Size(115, 20);
            lblDataEvento.TabIndex = 9;
            lblDataEvento.Text = "Data do Evento:";
            // 
            // dtpDataEvento
            // 
            dtpDataEvento.Location = new Point(641, 263);
            dtpDataEvento.Name = "dtpDataEvento";
            dtpDataEvento.Size = new Size(250, 27);
            dtpDataEvento.TabIndex = 10;
            // 
            // btnEditarEvento
            // 
            btnEditarEvento.Location = new Point(424, 703);
            btnEditarEvento.Name = "btnEditarEvento";
            btnEditarEvento.Size = new Size(157, 50);
            btnEditarEvento.TabIndex = 13;
            btnEditarEvento.Text = "Editar Evento";
            btnEditarEvento.UseVisualStyleBackColor = true;
            btnEditarEvento.Click += btnEditarEvento_Click;
            // 
            // lblListaEventos
            // 
            lblListaEventos.AutoSize = true;
            lblListaEventos.Location = new Point(96, 56);
            lblListaEventos.Name = "lblListaEventos";
            lblListaEventos.Size = new Size(149, 20);
            lblListaEventos.TabIndex = 14;
            lblListaEventos.Text = "Eventos Cadastrados:";
            // 
            // btnSelecionarEvento
            // 
            btnSelecionarEvento.Location = new Point(424, 516);
            btnSelecionarEvento.Name = "btnSelecionarEvento";
            btnSelecionarEvento.Size = new Size(127, 64);
            btnSelecionarEvento.TabIndex = 15;
            btnSelecionarEvento.Text = "Selecionar Evento";
            btnSelecionarEvento.UseVisualStyleBackColor = true;
            btnSelecionarEvento.Click += btnSelecionarEvento_Click;
            // 
            // lblAlunos
            // 
            lblAlunos.AutoSize = true;
            lblAlunos.Location = new Point(437, 179);
            lblAlunos.Name = "lblAlunos";
            lblAlunos.Size = new Size(124, 20);
            lblAlunos.TabIndex = 16;
            lblAlunos.Text = "Selecionar Aluno:";
            // 
            // lblProfessores
            // 
            lblProfessores.AutoSize = true;
            lblProfessores.Location = new Point(441, 294);
            lblProfessores.Name = "lblProfessores";
            lblProfessores.Size = new Size(146, 20);
            lblProfessores.TabIndex = 17;
            lblProfessores.Text = "Selecionar Professor:";
            // 
            // lstAlunos
            // 
            lstAlunos.FormattingEnabled = true;
            lstAlunos.Location = new Point(24, 294);
            lstAlunos.Name = "lstAlunos";
            lstAlunos.Size = new Size(338, 204);
            lstAlunos.TabIndex = 22;
            lstAlunos.DoubleClick += lstAlunos_DoubleClick;
            // 
            // lstProfessores
            // 
            lstProfessores.FormattingEnabled = true;
            lstProfessores.Location = new Point(19, 541);
            lstProfessores.Name = "lstProfessores";
            lstProfessores.Size = new Size(343, 184);
            lstProfessores.TabIndex = 23;
            lstProfessores.DoubleClick += lstProfessores_DoubleClick;
            // 
            // FormEvento
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 807);
            Controls.Add(lstProfessores);
            Controls.Add(lstAlunos);
            Controls.Add(lblProfessores);
            Controls.Add(lblAlunos);
            Controls.Add(btnSelecionarEvento);
            Controls.Add(lblListaEventos);
            Controls.Add(btnEditarEvento);
            Controls.Add(dtpDataEvento);
            Controls.Add(lblDataEvento);
            Controls.Add(txtDescricaoEvento);
            Controls.Add(lblDescricaoEvento);
            Controls.Add(lblNomeEvento);
            Controls.Add(txtNomeEvento);
            Controls.Add(btnRemoverEvento);
            Controls.Add(btnAdicionarEvento);
            Controls.Add(lstEventos);
            Controls.Add(cmbProfessor);
            Controls.Add(cmbAluno);
            Name = "FormEvento";
            Text = "FormEvento";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbAluno;
        private ComboBox cmbProfessor;
        private ListBox lstEventos;
        private Button btnAdicionarEvento;
        private Button btnRemoverEvento;
        private TextBox txtNomeEvento;
        private Label lblNomeEvento;
        private Label lblDescricaoEvento;
        private TextBox txtDescricaoEvento;
        private Label lblDataEvento;
        private DateTimePicker dtpDataEvento;
        private Button btnEditarEvento;
        private Label lblListaEventos;
        private Button btnSelecionarEvento;
        private Label lblAlunos;
        private Label lblProfessores;
        private ListBox lstAlunos;
        private ListBox lstProfessores;
    }
}