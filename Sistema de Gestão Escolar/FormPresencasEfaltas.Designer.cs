namespace Sistema_de_Gestão_Escolar
{
    partial class FormPresencasEfaltas
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
            cbDisciplinas = new ComboBox();
            dgvPresencas = new DataGridView();
            cbAlunos = new ComboBox();
            dtpData = new DateTimePicker();
            chkCompareceu = new CheckBox();
            btnRegistrar = new Button();
            lblAlunos = new Label();
            lblDisciplinas = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPresencas).BeginInit();
            SuspendLayout();
            // 
            // cbDisciplinas
            // 
            cbDisciplinas.FormattingEnabled = true;
            cbDisciplinas.Location = new Point(438, 184);
            cbDisciplinas.Name = "cbDisciplinas";
            cbDisciplinas.Size = new Size(151, 28);
            cbDisciplinas.TabIndex = 0;
            // 
            // dgvPresencas
            // 
            dgvPresencas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPresencas.Location = new Point(54, 50);
            dgvPresencas.Name = "dgvPresencas";
            dgvPresencas.RowHeadersWidth = 51;
            dgvPresencas.Size = new Size(341, 399);
            dgvPresencas.TabIndex = 1;
            // 
            // cbAlunos
            // 
            cbAlunos.FormattingEnabled = true;
            cbAlunos.Location = new Point(438, 270);
            cbAlunos.Name = "cbAlunos";
            cbAlunos.Size = new Size(151, 28);
            cbAlunos.TabIndex = 2;
            // 
            // dtpData
            // 
            dtpData.Location = new Point(438, 107);
            dtpData.Name = "dtpData";
            dtpData.Size = new Size(250, 27);
            dtpData.TabIndex = 3;
            // 
            // chkCompareceu
            // 
            chkCompareceu.AutoSize = true;
            chkCompareceu.Location = new Point(438, 50);
            chkCompareceu.Name = "chkCompareceu";
            chkCompareceu.Size = new Size(115, 24);
            chkCompareceu.TabIndex = 4;
            chkCompareceu.Text = "Compareceu";
            chkCompareceu.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(581, 382);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(94, 38);
            btnRegistrar.TabIndex = 5;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // lblAlunos
            // 
            lblAlunos.AutoSize = true;
            lblAlunos.Location = new Point(444, 243);
            lblAlunos.Name = "lblAlunos";
            lblAlunos.Size = new Size(57, 20);
            lblAlunos.TabIndex = 6;
            lblAlunos.Text = "Alunos:";
            // 
            // lblDisciplinas
            // 
            lblDisciplinas.AutoSize = true;
            lblDisciplinas.Location = new Point(438, 161);
            lblDisciplinas.Name = "lblDisciplinas";
            lblDisciplinas.Size = new Size(83, 20);
            lblDisciplinas.TabIndex = 7;
            lblDisciplinas.Text = "Disciplinas:";
            // 
            // FormPresencasEfaltas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 518);
            Controls.Add(lblDisciplinas);
            Controls.Add(lblAlunos);
            Controls.Add(btnRegistrar);
            Controls.Add(chkCompareceu);
            Controls.Add(dtpData);
            Controls.Add(cbAlunos);
            Controls.Add(dgvPresencas);
            Controls.Add(cbDisciplinas);
            Name = "FormPresencasEfaltas";
            Text = "FormPresencasEfaltas";
            Load += FormPresencasEfaltas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPresencas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbDisciplinas;
        private DataGridView dgvPresencas;
        private ComboBox cbAlunos;
        private DateTimePicker dtpData;
        private CheckBox chkCompareceu;
        private Button btnRegistrar;
        private Label lblAlunos;
        private Label lblDisciplinas;
    }
}