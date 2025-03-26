namespace Sistema_de_Gestão_Escolar
{
    partial class FormConsultaNotasPresencas
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
            dgvNotas = new DataGridView();
            cmbAluno = new ComboBox();
            dgvPresencas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvNotas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPresencas).BeginInit();
            SuspendLayout();
            // 
            // dgvNotas
            // 
            dgvNotas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNotas.Location = new Point(44, 112);
            dgvNotas.Name = "dgvNotas";
            dgvNotas.RowHeadersWidth = 51;
            dgvNotas.Size = new Size(336, 199);
            dgvNotas.TabIndex = 0;
            // 
            // cmbAluno
            // 
            cmbAluno.FormattingEnabled = true;
            cmbAluno.Location = new Point(724, 208);
            cmbAluno.Name = "cmbAluno";
            cmbAluno.Size = new Size(151, 28);
            cmbAluno.TabIndex = 1;
            cmbAluno.SelectedIndexChanged += cmbAluno_SelectedIndexChanged;
            // 
            // dgvPresencas
            // 
            dgvPresencas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPresencas.Location = new Point(44, 365);
            dgvPresencas.Name = "dgvPresencas";
            dgvPresencas.RowHeadersWidth = 51;
            dgvPresencas.Size = new Size(336, 188);
            dgvPresencas.TabIndex = 2;
            // 
            // FormConsultaNotasPresencas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(963, 626);
            Controls.Add(dgvPresencas);
            Controls.Add(cmbAluno);
            Controls.Add(dgvNotas);
            Name = "FormConsultaNotasPresencas";
            Text = "FormConsultaNotasPresencas";
            ((System.ComponentModel.ISupportInitialize)dgvNotas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPresencas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvNotas;
        private ComboBox cmbAluno;
        private DataGridView dgvPresencas;
    }
}