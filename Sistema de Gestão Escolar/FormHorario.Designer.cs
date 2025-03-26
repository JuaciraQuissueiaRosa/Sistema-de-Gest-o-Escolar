namespace Sistema_de_Gestão_Escolar
{
    partial class FormHorario
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
            btnAdicionarHorario = new Button();
            btnRemoverHorario = new Button();
            cmbTurma = new ComboBox();
            cmbDisciplina = new ComboBox();
            cmbProfessor = new ComboBox();
            cmbDiaSemana = new ComboBox();
            lstHorarios = new ListView();
            dtpHoraInicio = new DateTimePicker();
            dtpHoraFim = new DateTimePicker();
            btnEditarHorario = new Button();
            SuspendLayout();
            // 
            // btnAdicionarHorario
            // 
            btnAdicionarHorario.Location = new Point(679, 90);
            btnAdicionarHorario.Name = "btnAdicionarHorario";
            btnAdicionarHorario.Size = new Size(123, 69);
            btnAdicionarHorario.TabIndex = 0;
            btnAdicionarHorario.Text = "Adicionar Horario";
            btnAdicionarHorario.UseVisualStyleBackColor = true;
            btnAdicionarHorario.Click += btnAdicionarHorario_Click;
            // 
            // btnRemoverHorario
            // 
            btnRemoverHorario.Location = new Point(679, 212);
            btnRemoverHorario.Name = "btnRemoverHorario";
            btnRemoverHorario.Size = new Size(129, 62);
            btnRemoverHorario.TabIndex = 1;
            btnRemoverHorario.Text = "Remover Horario";
            btnRemoverHorario.UseVisualStyleBackColor = true;
            btnRemoverHorario.Click += btnRemoverHorario_Click;
            // 
            // cmbTurma
            // 
            cmbTurma.FormattingEnabled = true;
            cmbTurma.Location = new Point(383, 90);
            cmbTurma.Name = "cmbTurma";
            cmbTurma.Size = new Size(151, 28);
            cmbTurma.TabIndex = 2;
            // 
            // cmbDisciplina
            // 
            cmbDisciplina.FormattingEnabled = true;
            cmbDisciplina.Location = new Point(435, 234);
            cmbDisciplina.Name = "cmbDisciplina";
            cmbDisciplina.Size = new Size(151, 28);
            cmbDisciplina.TabIndex = 3;
            // 
            // cmbProfessor
            // 
            cmbProfessor.FormattingEnabled = true;
            cmbProfessor.Location = new Point(420, 176);
            cmbProfessor.Name = "cmbProfessor";
            cmbProfessor.Size = new Size(151, 28);
            cmbProfessor.TabIndex = 4;
            // 
            // cmbDiaSemana
            // 
            cmbDiaSemana.FormattingEnabled = true;
            cmbDiaSemana.Location = new Point(458, 347);
            cmbDiaSemana.Name = "cmbDiaSemana";
            cmbDiaSemana.Size = new Size(151, 28);
            cmbDiaSemana.TabIndex = 5;
            // 
            // lstHorarios
            // 
            lstHorarios.Location = new Point(12, 114);
            lstHorarios.Name = "lstHorarios";
            lstHorarios.Size = new Size(336, 378);
            lstHorarios.TabIndex = 8;
            lstHorarios.UseCompatibleStateImageBehavior = false;
            lstHorarios.SelectedIndexChanged += lstHorarios_SelectedIndexChanged;
            // 
            // dtpHoraInicio
            // 
            dtpHoraInicio.Location = new Point(623, 393);
            dtpHoraInicio.Name = "dtpHoraInicio";
            dtpHoraInicio.Size = new Size(250, 27);
            dtpHoraInicio.TabIndex = 9;
            // 
            // dtpHoraFim
            // 
            dtpHoraFim.Location = new Point(655, 469);
            dtpHoraFim.Name = "dtpHoraFim";
            dtpHoraFim.Size = new Size(250, 27);
            dtpHoraFim.TabIndex = 10;
            // 
            // btnEditarHorario
            // 
            btnEditarHorario.Location = new Point(468, 516);
            btnEditarHorario.Name = "btnEditarHorario";
            btnEditarHorario.Size = new Size(141, 57);
            btnEditarHorario.TabIndex = 11;
            btnEditarHorario.Text = "Editar Horario";
            btnEditarHorario.UseVisualStyleBackColor = true;
            btnEditarHorario.Click += btnEditarHorario_Click;
            // 
            // FormHorario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 664);
            Controls.Add(btnEditarHorario);
            Controls.Add(dtpHoraFim);
            Controls.Add(dtpHoraInicio);
            Controls.Add(lstHorarios);
            Controls.Add(cmbDiaSemana);
            Controls.Add(cmbProfessor);
            Controls.Add(cmbDisciplina);
            Controls.Add(cmbTurma);
            Controls.Add(btnRemoverHorario);
            Controls.Add(btnAdicionarHorario);
            Name = "FormHorario";
            Text = "FormHorario";
            Load += FormHorario_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnAdicionarHorario;
        private Button btnRemoverHorario;
        private ComboBox cmbTurma;
        private ComboBox cmbDisciplina;
        private ComboBox cmbProfessor;
        private ComboBox cmbDiaSemana;
        private ListView lstHorarios;
        private DateTimePicker dtpHoraInicio;
        private DateTimePicker dtpHoraFim;
        private Button btnEditarHorario;
    }
}