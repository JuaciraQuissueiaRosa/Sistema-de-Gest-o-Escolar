namespace Sistema_de_Gestão_Escolar
{
    partial class FormProfessor
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
            lblIdProfessor = new Label();
            lblNomeProfessor = new Label();
            lblContatoProfessor = new Label();
            lblEmailProfessor = new Label();
            lblAreaEnsino = new Label();
            txtNomeProfessor = new TextBox();
            txtIdProfessor = new TextBox();
            txtEmailProfessor = new TextBox();
            btnAdicionarProfessor = new Button();
            btnRemoverProfessor = new Button();
            lstProfessores = new ListBox();
            cmbAreaEnsino = new ComboBox();
            mtbContatoProfessor = new MaskedTextBox();
            btnEditarProfessor = new Button();
            btnConsultarProfessor = new Button();
            SuspendLayout();
            // 
            // lblIdProfessor
            // 
            lblIdProfessor.AutoSize = true;
            lblIdProfessor.Location = new Point(71, 44);
            lblIdProfessor.Name = "lblIdProfessor";
            lblIdProfessor.Size = new Size(114, 20);
            lblIdProfessor.TabIndex = 0;
            lblIdProfessor.Text = "ID do Professor:";
            // 
            // lblNomeProfessor
            // 
            lblNomeProfessor.AutoSize = true;
            lblNomeProfessor.Location = new Point(71, 115);
            lblNomeProfessor.Name = "lblNomeProfessor";
            lblNomeProfessor.Size = new Size(140, 20);
            lblNomeProfessor.TabIndex = 1;
            lblNomeProfessor.Text = "Nome do Professor:";
            // 
            // lblContatoProfessor
            // 
            lblContatoProfessor.AutoSize = true;
            lblContatoProfessor.Location = new Point(71, 183);
            lblContatoProfessor.Name = "lblContatoProfessor";
            lblContatoProfessor.Size = new Size(65, 20);
            lblContatoProfessor.TabIndex = 2;
            lblContatoProfessor.Text = "Contato:";
            // 
            // lblEmailProfessor
            // 
            lblEmailProfessor.AutoSize = true;
            lblEmailProfessor.Location = new Point(71, 234);
            lblEmailProfessor.Name = "lblEmailProfessor";
            lblEmailProfessor.Size = new Size(49, 20);
            lblEmailProfessor.TabIndex = 3;
            lblEmailProfessor.Text = "Email:";
            // 
            // lblAreaEnsino
            // 
            lblAreaEnsino.AutoSize = true;
            lblAreaEnsino.Location = new Point(71, 295);
            lblAreaEnsino.Name = "lblAreaEnsino";
            lblAreaEnsino.Size = new Size(111, 20);
            lblAreaEnsino.TabIndex = 4;
            lblAreaEnsino.Text = "Área de Ensino:";
            // 
            // txtNomeProfessor
            // 
            txtNomeProfessor.Location = new Point(214, 115);
            txtNomeProfessor.Name = "txtNomeProfessor";
            txtNomeProfessor.Size = new Size(125, 27);
            txtNomeProfessor.TabIndex = 5;
            // 
            // txtIdProfessor
            // 
            txtIdProfessor.Location = new Point(214, 44);
            txtIdProfessor.Name = "txtIdProfessor";
            txtIdProfessor.Size = new Size(125, 27);
            txtIdProfessor.TabIndex = 6;
            // 
            // txtEmailProfessor
            // 
            txtEmailProfessor.Location = new Point(136, 227);
            txtEmailProfessor.Name = "txtEmailProfessor";
            txtEmailProfessor.Size = new Size(125, 27);
            txtEmailProfessor.TabIndex = 7;
            // 
            // btnAdicionarProfessor
            // 
            btnAdicionarProfessor.Location = new Point(47, 386);
            btnAdicionarProfessor.Name = "btnAdicionarProfessor";
            btnAdicionarProfessor.Size = new Size(129, 59);
            btnAdicionarProfessor.TabIndex = 10;
            btnAdicionarProfessor.Text = "Adicionar novo professor";
            btnAdicionarProfessor.UseVisualStyleBackColor = true;
            btnAdicionarProfessor.Click += btnAdicionarProfessor_Click;
            // 
            // btnRemoverProfessor
            // 
            btnRemoverProfessor.Location = new Point(197, 386);
            btnRemoverProfessor.Name = "btnRemoverProfessor";
            btnRemoverProfessor.Size = new Size(142, 59);
            btnRemoverProfessor.TabIndex = 11;
            btnRemoverProfessor.Text = "Remover professor selecionado";
            btnRemoverProfessor.UseVisualStyleBackColor = true;
            btnRemoverProfessor.Click += btnRemoverProfessor_Click;
            // 
            // lstProfessores
            // 
            lstProfessores.FormattingEnabled = true;
            lstProfessores.Location = new Point(527, 33);
            lstProfessores.Name = "lstProfessores";
            lstProfessores.Size = new Size(766, 424);
            lstProfessores.TabIndex = 12;
            // 
            // cmbAreaEnsino
            // 
            cmbAreaEnsino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAreaEnsino.FormattingEnabled = true;
            cmbAreaEnsino.Location = new Point(197, 295);
            cmbAreaEnsino.Name = "cmbAreaEnsino";
            cmbAreaEnsino.Size = new Size(235, 28);
            cmbAreaEnsino.TabIndex = 13;
            // 
            // mtbContatoProfessor
            // 
            mtbContatoProfessor.Location = new Point(142, 180);
            mtbContatoProfessor.Mask = "+351 000 000 000";
            mtbContatoProfessor.Name = "mtbContatoProfessor";
            mtbContatoProfessor.Size = new Size(165, 27);
            mtbContatoProfessor.TabIndex = 14;
            // 
            // btnEditarProfessor
            // 
            btnEditarProfessor.Location = new Point(197, 470);
            btnEditarProfessor.Name = "btnEditarProfessor";
            btnEditarProfessor.Size = new Size(142, 59);
            btnEditarProfessor.TabIndex = 16;
            btnEditarProfessor.Text = "Editar professor selecionado";
            btnEditarProfessor.UseVisualStyleBackColor = true;
            btnEditarProfessor.Click += btnEditarProfessor_Click;
            // 
            // btnConsultarProfessor
            // 
            btnConsultarProfessor.Location = new Point(47, 470);
            btnConsultarProfessor.Name = "btnConsultarProfessor";
            btnConsultarProfessor.Size = new Size(129, 59);
            btnConsultarProfessor.TabIndex = 15;
            btnConsultarProfessor.Text = "Consultar professor";
            btnConsultarProfessor.UseVisualStyleBackColor = true;
            btnConsultarProfessor.Click += btnConsultarProfessor_Click;
            // 
            // FormProfessor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 552);
            Controls.Add(btnEditarProfessor);
            Controls.Add(btnConsultarProfessor);
            Controls.Add(mtbContatoProfessor);
            Controls.Add(cmbAreaEnsino);
            Controls.Add(lstProfessores);
            Controls.Add(btnRemoverProfessor);
            Controls.Add(btnAdicionarProfessor);
            Controls.Add(txtEmailProfessor);
            Controls.Add(txtIdProfessor);
            Controls.Add(txtNomeProfessor);
            Controls.Add(lblAreaEnsino);
            Controls.Add(lblEmailProfessor);
            Controls.Add(lblContatoProfessor);
            Controls.Add(lblNomeProfessor);
            Controls.Add(lblIdProfessor);
            Name = "FormProfessor";
            Text = "FormProfessor";
            Load += FormProfessor_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIdProfessor;
        private Label lblNomeProfessor;
        private Label lblContatoProfessor;
        private Label lblEmailProfessor;
        private Label lblAreaEnsino;
        private TextBox txtNomeProfessor;
        private TextBox txtIdProfessor;
        private TextBox txtEmailProfessor;
        private Button btnAdicionarProfessor;
        private Button btnRemoverProfessor;
        private ListBox lstProfessores;
        private ComboBox cmbAreaEnsino;
        private MaskedTextBox mtbContatoProfessor;
        private Button btnEditarProfessor;
        private Button btnConsultarProfessor;
    }
}