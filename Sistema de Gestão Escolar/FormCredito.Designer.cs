namespace Sistema_de_Gestão_Escolar
{
    partial class FormCredito
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
            lblAutor = new Label();
            lblVersao = new Label();
            lblData = new Label();
            btnFechar = new Button();
            SuspendLayout();
            // 
            // lblAutor
            // 
            lblAutor.AutoSize = true;
            lblAutor.Location = new Point(193, 107);
            lblAutor.Name = "lblAutor";
            lblAutor.Size = new Size(49, 20);
            lblAutor.TabIndex = 0;
            lblAutor.Text = "Autor:";
            // 
            // lblVersao
            // 
            lblVersao.AutoSize = true;
            lblVersao.Location = new Point(193, 249);
            lblVersao.Name = "lblVersao";
            lblVersao.Size = new Size(56, 20);
            lblVersao.TabIndex = 1;
            lblVersao.Text = "Versão:";
            // 
            // lblData
            // 
            lblData.AutoSize = true;
            lblData.Location = new Point(193, 177);
            lblData.Name = "lblData";
            lblData.Size = new Size(44, 20);
            lblData.TabIndex = 2;
            lblData.Text = "Data:";
            // 
            // btnFechar
            // 
            btnFechar.Location = new Point(343, 339);
            btnFechar.Name = "btnFechar";
            btnFechar.Size = new Size(94, 29);
            btnFechar.TabIndex = 3;
            btnFechar.Text = "Fechar";
            btnFechar.UseVisualStyleBackColor = true;
            btnFechar.Click += btnFechar_Click;
            // 
            // FormCredito
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnFechar);
            Controls.Add(lblData);
            Controls.Add(lblVersao);
            Controls.Add(lblAutor);
            Name = "FormCredito";
            Text = "FormCredito";
            Load += FormCredito_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAutor;
        private Label lblVersao;
        private Label lblData;
        private Button btnFechar;
    }
}