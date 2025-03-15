namespace Sistema_de_Gestão_Escolar
{
    partial class FormNota
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
            lblValorNota = new Label();
            lblDisciplinaIdNota = new Label();
            lblAlunoIdNota = new Label();
            txtValorNota = new TextBox();
            txtDisciplinaIdNota = new TextBox();
            txtAlunoIdNota = new TextBox();
            txtPeriodoNota = new TextBox();
            btnAdicionarNota = new Button();
            btnRemoverNota = new Button();
            lstNotas = new ListBox();
            lblPeriodoNota = new Label();
            lblProfessorResponsavel = new Label();
            cmbProfessorNota = new ComboBox();
            cmbTipoAvaliacao = new ComboBox();
            lblTipoAvaliacao = new Label();
            SuspendLayout();
            // 
            // lblValorNota
            // 
            lblValorNota.AutoSize = true;
            lblValorNota.Location = new Point(88, 171);
            lblValorNota.Name = "lblValorNota";
            lblValorNota.Size = new Size(45, 20);
            lblValorNota.TabIndex = 0;
            lblValorNota.Text = "Nota:";
            // 
            // lblDisciplinaIdNota
            // 
            lblDisciplinaIdNota.AutoSize = true;
            lblDisciplinaIdNota.Location = new Point(87, 233);
            lblDisciplinaIdNota.Name = "lblDisciplinaIdNota";
            lblDisciplinaIdNota.Size = new Size(117, 20);
            lblDisciplinaIdNota.TabIndex = 1;
            lblDisciplinaIdNota.Text = "ID da Disciplina:";
            // 
            // lblAlunoIdNota
            // 
            lblAlunoIdNota.AutoSize = true;
            lblAlunoIdNota.Location = new Point(88, 304);
            lblAlunoIdNota.Name = "lblAlunoIdNota";
            lblAlunoIdNota.Size = new Size(92, 20);
            lblAlunoIdNota.TabIndex = 2;
            lblAlunoIdNota.Text = "ID do Aluno:";
            // 
            // txtValorNota
            // 
            txtValorNota.Location = new Point(296, 171);
            txtValorNota.Name = "txtValorNota";
            txtValorNota.Size = new Size(125, 27);
            txtValorNota.TabIndex = 3;
            // 
            // txtDisciplinaIdNota
            // 
            txtDisciplinaIdNota.Location = new Point(296, 226);
            txtDisciplinaIdNota.Name = "txtDisciplinaIdNota";
            txtDisciplinaIdNota.Size = new Size(125, 27);
            txtDisciplinaIdNota.TabIndex = 4;
            // 
            // txtAlunoIdNota
            // 
            txtAlunoIdNota.Location = new Point(296, 297);
            txtAlunoIdNota.Name = "txtAlunoIdNota";
            txtAlunoIdNota.Size = new Size(125, 27);
            txtAlunoIdNota.TabIndex = 5;
            // 
            // txtPeriodoNota
            // 
            txtPeriodoNota.Location = new Point(296, 49);
            txtPeriodoNota.Name = "txtPeriodoNota";
            txtPeriodoNota.Size = new Size(125, 27);
            txtPeriodoNota.TabIndex = 6;
            // 
            // btnAdicionarNota
            // 
            btnAdicionarNota.Location = new Point(136, 381);
            btnAdicionarNota.Name = "btnAdicionarNota";
            btnAdicionarNota.Size = new Size(99, 72);
            btnAdicionarNota.TabIndex = 7;
            btnAdicionarNota.Text = "Adicionar Nota";
            btnAdicionarNota.UseVisualStyleBackColor = true;
            btnAdicionarNota.Click += btnAdicionarNota_Click;
            // 
            // btnRemoverNota
            // 
            btnRemoverNota.Location = new Point(318, 381);
            btnRemoverNota.Name = "btnRemoverNota";
            btnRemoverNota.Size = new Size(129, 72);
            btnRemoverNota.TabIndex = 8;
            btnRemoverNota.Text = "Remover Nota";
            btnRemoverNota.UseVisualStyleBackColor = true;
            btnRemoverNota.Click += btnRemoverNota_Click;
            // 
            // lstNotas
            // 
            lstNotas.FormattingEnabled = true;
            lstNotas.Location = new Point(543, 42);
            lstNotas.Name = "lstNotas";
            lstNotas.Size = new Size(436, 284);
            lstNotas.TabIndex = 9;
            // 
            // lblPeriodoNota
            // 
            lblPeriodoNota.AutoSize = true;
            lblPeriodoNota.Location = new Point(88, 56);
            lblPeriodoNota.Name = "lblPeriodoNota";
            lblPeriodoNota.Size = new Size(107, 20);
            lblPeriodoNota.TabIndex = 10;
            lblPeriodoNota.Text = "Período Letivo:";
            // 
            // lblProfessorResponsavel
            // 
            lblProfessorResponsavel.AutoSize = true;
            lblProfessorResponsavel.Location = new Point(543, 381);
            lblProfessorResponsavel.Name = "lblProfessorResponsavel";
            lblProfessorResponsavel.Size = new Size(159, 20);
            lblProfessorResponsavel.TabIndex = 11;
            lblProfessorResponsavel.Text = "Professor Responsável:";
            // 
            // cmbProfessorNota
            // 
            cmbProfessorNota.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProfessorNota.FormattingEnabled = true;
            cmbProfessorNota.Location = new Point(725, 378);
            cmbProfessorNota.Name = "cmbProfessorNota";
            cmbProfessorNota.Size = new Size(172, 28);
            cmbProfessorNota.TabIndex = 12;
            // 
            // cmbTipoAvaliacao
            // 
            cmbTipoAvaliacao.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoAvaliacao.FormattingEnabled = true;
            cmbTipoAvaliacao.Location = new Point(260, 110);
            cmbTipoAvaliacao.Name = "cmbTipoAvaliacao";
            cmbTipoAvaliacao.Size = new Size(174, 28);
            cmbTipoAvaliacao.TabIndex = 13;
            // 
            // lblTipoAvaliacao
            // 
            lblTipoAvaliacao.AutoSize = true;
            lblTipoAvaliacao.Location = new Point(87, 118);
            lblTipoAvaliacao.Name = "lblTipoAvaliacao";
            lblTipoAvaliacao.Size = new Size(130, 20);
            lblTipoAvaliacao.TabIndex = 14;
            lblTipoAvaliacao.Text = "Tipo de avaliação:";
            // 
            // FormNota
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 620);
            Controls.Add(lblTipoAvaliacao);
            Controls.Add(cmbTipoAvaliacao);
            Controls.Add(cmbProfessorNota);
            Controls.Add(lblProfessorResponsavel);
            Controls.Add(lblPeriodoNota);
            Controls.Add(lstNotas);
            Controls.Add(btnRemoverNota);
            Controls.Add(btnAdicionarNota);
            Controls.Add(txtPeriodoNota);
            Controls.Add(txtAlunoIdNota);
            Controls.Add(txtDisciplinaIdNota);
            Controls.Add(txtValorNota);
            Controls.Add(lblAlunoIdNota);
            Controls.Add(lblDisciplinaIdNota);
            Controls.Add(lblValorNota);
            Name = "FormNota";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormNota";
            Load += FormNota_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblValorNota;
        private Label lblDisciplinaIdNota;
        private Label lblAlunoIdNota;
        private TextBox txtValorNota;
        private TextBox txtDisciplinaIdNota;
        private TextBox txtAlunoIdNota;
        private TextBox txtPeriodoNota;
        private Button btnAdicionarNota;
        private Button btnRemoverNota;
        private ListBox lstNotas;
        private Label lblPeriodoNota;
        private Label lblProfessorResponsavel;
        private ComboBox cmbProfessorNota;
        private ComboBox cmbTipoAvaliacao;
        private Label lblTipoAvaliacao;
    }
}