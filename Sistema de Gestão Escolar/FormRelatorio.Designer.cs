namespace Sistema_de_Gestão_Escolar
{
    partial class FormRelatorio
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
            btnGerarPauta = new Button();
            cmbAluno = new ComboBox();
            cmbTurma = new ComboBox();
            btnGerarRelatorio = new Button();
            txtRelatorio = new RichTextBox();
            graficoDesempenho = new ScottPlot.FormsPlot();
            SuspendLayout();
            // 
            // btnGerarPauta
            // 
            btnGerarPauta.Location = new Point(777, 128);
            btnGerarPauta.Name = "btnGerarPauta";
            btnGerarPauta.Size = new Size(148, 50);
            btnGerarPauta.TabIndex = 0;
            btnGerarPauta.Text = "Gerar Pauta";
            btnGerarPauta.UseVisualStyleBackColor = true;
            btnGerarPauta.Click += btnGerarPauta_Click;
            // 
            // cmbAluno
            // 
            cmbAluno.FormattingEnabled = true;
            cmbAluno.Location = new Point(749, 56);
            cmbAluno.Name = "cmbAluno";
            cmbAluno.Size = new Size(234, 28);
            cmbAluno.TabIndex = 1;
            // 
            // cmbTurma
            // 
            cmbTurma.FormattingEnabled = true;
            cmbTurma.Location = new Point(431, 56);
            cmbTurma.Name = "cmbTurma";
            cmbTurma.Size = new Size(225, 28);
            cmbTurma.TabIndex = 3;
            cmbTurma.SelectedIndexChanged += cmbTurma_SelectedIndexChanged;
            // 
            // btnGerarRelatorio
            // 
            btnGerarRelatorio.Location = new Point(459, 122);
            btnGerarRelatorio.Name = "btnGerarRelatorio";
            btnGerarRelatorio.Size = new Size(159, 56);
            btnGerarRelatorio.TabIndex = 2;
            btnGerarRelatorio.Text = "Gerar Relatório";
            btnGerarRelatorio.UseVisualStyleBackColor = true;
            btnGerarRelatorio.Click += btnGerarRelatorio_Click;
            // 
            // txtRelatorio
            // 
            txtRelatorio.Location = new Point(78, 56);
            txtRelatorio.Name = "txtRelatorio";
            txtRelatorio.Size = new Size(224, 359);
            txtRelatorio.TabIndex = 4;
            txtRelatorio.Text = "";
            // 
            // graficoDesempenho
            // 
            graficoDesempenho.Location = new Point(329, 277);
            graficoDesempenho.Margin = new Padding(5, 4, 5, 4);
            graficoDesempenho.Name = "graficoDesempenho";
            graficoDesempenho.Size = new Size(649, 345);
            graficoDesempenho.TabIndex = 5;
            // 
            // FormRelatorio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1085, 665);
            Controls.Add(graficoDesempenho);
            Controls.Add(txtRelatorio);
            Controls.Add(cmbTurma);
            Controls.Add(btnGerarRelatorio);
            Controls.Add(cmbAluno);
            Controls.Add(btnGerarPauta);
            Name = "FormRelatorio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormRelatorio";
            Load += FormRelatorio_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnGerarPauta;
        private ComboBox cmbAluno;
        private ComboBox cmbTurma;
        private Button btnGerarRelatorio;
        private RichTextBox txtRelatorio;
        private ScottPlot.FormsPlot graficoDesempenho;
    }
}