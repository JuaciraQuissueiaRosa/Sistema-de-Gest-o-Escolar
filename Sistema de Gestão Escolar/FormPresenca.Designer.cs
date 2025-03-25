namespace Sistema_de_Gestão_Escolar
{
    partial class FormPresenca
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
            cmbDisciplina = new ComboBox();
            cmbAluno = new ComboBox();
            dtpDataPresenca = new DateTimePicker();
            btnRegistrarPresenca = new Button();
            lstPresencas = new ListBox();
            lstAlertas = new ListBox();
            btnRegistarFalta = new Button();
            SuspendLayout();
            // 
            // cmbDisciplina
            // 
            cmbDisciplina.Font = new Font("Segoe UI", 10.8F);
            cmbDisciplina.FormattingEnabled = true;
            cmbDisciplina.Location = new Point(469, 93);
            cmbDisciplina.Name = "cmbDisciplina";
            cmbDisciplina.Size = new Size(183, 33);
            cmbDisciplina.TabIndex = 0;
            // 
            // cmbAluno
            // 
            cmbAluno.Font = new Font("Segoe UI", 10.8F);
            cmbAluno.FormattingEnabled = true;
            cmbAluno.Location = new Point(469, 157);
            cmbAluno.Name = "cmbAluno";
            cmbAluno.Size = new Size(183, 33);
            cmbAluno.TabIndex = 1;
            // 
            // dtpDataPresenca
            // 
            dtpDataPresenca.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDataPresenca.Location = new Point(437, 244);
            dtpDataPresenca.Name = "dtpDataPresenca";
            dtpDataPresenca.Size = new Size(250, 31);
            dtpDataPresenca.TabIndex = 2;
            // 
            // btnRegistrarPresenca
            // 
            btnRegistrarPresenca.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegistrarPresenca.Location = new Point(469, 319);
            btnRegistrarPresenca.Name = "btnRegistrarPresenca";
            btnRegistrarPresenca.Size = new Size(166, 71);
            btnRegistrarPresenca.TabIndex = 4;
            btnRegistrarPresenca.Text = "Registar Presença";
            btnRegistrarPresenca.UseVisualStyleBackColor = true;
            btnRegistrarPresenca.Click += btnRegistarPresenca_Click;
            // 
            // lstPresencas
            // 
            lstPresencas.Location = new Point(65, 106);
            lstPresencas.Name = "lstPresencas";
            lstPresencas.Size = new Size(249, 344);
            lstPresencas.TabIndex = 8;
            // 
            // lstAlertas
            // 
            lstAlertas.FormattingEnabled = true;
            lstAlertas.Location = new Point(778, 51);
            lstAlertas.Name = "lstAlertas";
            lstAlertas.Size = new Size(363, 384);
            lstAlertas.TabIndex = 6;
            // 
            // btnRegistarFalta
            // 
            btnRegistarFalta.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegistarFalta.Location = new Point(469, 426);
            btnRegistarFalta.Name = "btnRegistarFalta";
            btnRegistarFalta.Size = new Size(166, 63);
            btnRegistarFalta.TabIndex = 7;
            btnRegistarFalta.Text = "Registar Falta";
            btnRegistarFalta.UseVisualStyleBackColor = true;
            btnRegistarFalta.Click += btnRegistarFalta_Click;
            // 
            // FormPresenca
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1191, 699);
            Controls.Add(btnRegistarFalta);
            Controls.Add(lstAlertas);
            Controls.Add(lstPresencas);
            Controls.Add(btnRegistrarPresenca);
            Controls.Add(dtpDataPresenca);
            Controls.Add(cmbAluno);
            Controls.Add(cmbDisciplina);
            Name = "FormPresenca";
            Text = "FormPresenca";
            Load += FormPresenca_Load_1;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbDisciplina;
        private ComboBox cmbAluno;
        private DateTimePicker dtpDataPresenca;
        private Button btnRegistrarPresenca;
        private ListBox lstPresencas;
        private ListBox lstAlertas;
        private Button btnRegistarFalta;
    }
}