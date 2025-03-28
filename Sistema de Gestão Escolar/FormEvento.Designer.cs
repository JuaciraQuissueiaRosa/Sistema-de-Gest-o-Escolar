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
            lstAlunos = new ListBox();
            lstProfessores = new ListBox();
            lstEventos = new ListView();
            SuspendLayout();
            // 
            // btnAdicionarEvento
            // 
            btnAdicionarEvento.Location = new Point(673, 730);
            btnAdicionarEvento.Name = "btnAdicionarEvento";
            btnAdicionarEvento.Size = new Size(175, 51);
            btnAdicionarEvento.TabIndex = 3;
            btnAdicionarEvento.Text = "Adicionar Evento";
            btnAdicionarEvento.UseVisualStyleBackColor = true;
            btnAdicionarEvento.Click += btnAdicionarEvento_Click;
            // 
            // btnRemoverEvento
            // 
            btnRemoverEvento.Location = new Point(61, 730);
            btnRemoverEvento.Name = "btnRemoverEvento";
            btnRemoverEvento.Size = new Size(171, 48);
            btnRemoverEvento.TabIndex = 4;
            btnRemoverEvento.Text = "Remover Evento";
            btnRemoverEvento.UseVisualStyleBackColor = true;
            btnRemoverEvento.Click += btnRemoverEvento_Click;
            // 
            // txtNomeEvento
            // 
            txtNomeEvento.Location = new Point(840, 105);
            txtNomeEvento.Name = "txtNomeEvento";
            txtNomeEvento.Size = new Size(226, 27);
            txtNomeEvento.TabIndex = 5;
            // 
            // lblNomeEvento
            // 
            lblNomeEvento.AutoSize = true;
            lblNomeEvento.Location = new Point(840, 67);
            lblNomeEvento.Name = "lblNomeEvento";
            lblNomeEvento.Size = new Size(124, 20);
            lblNomeEvento.TabIndex = 6;
            lblNomeEvento.Text = "Nome do Evento:";
            // 
            // lblDescricaoEvento
            // 
            lblDescricaoEvento.AutoSize = true;
            lblDescricaoEvento.Location = new Point(593, 142);
            lblDescricaoEvento.Name = "lblDescricaoEvento";
            lblDescricaoEvento.Size = new Size(77, 20);
            lblDescricaoEvento.TabIndex = 7;
            lblDescricaoEvento.Text = "Descrição:";
            // 
            // txtDescricaoEvento
            // 
            txtDescricaoEvento.Location = new Point(593, 174);
            txtDescricaoEvento.Name = "txtDescricaoEvento";
            txtDescricaoEvento.Size = new Size(226, 27);
            txtDescricaoEvento.TabIndex = 8;
            // 
            // lblDataEvento
            // 
            lblDataEvento.AutoSize = true;
            lblDataEvento.Location = new Point(829, 279);
            lblDataEvento.Name = "lblDataEvento";
            lblDataEvento.Size = new Size(115, 20);
            lblDataEvento.TabIndex = 9;
            lblDataEvento.Text = "Data do Evento:";
            // 
            // dtpDataEvento
            // 
            dtpDataEvento.Location = new Point(829, 322);
            dtpDataEvento.Name = "dtpDataEvento";
            dtpDataEvento.Size = new Size(250, 27);
            dtpDataEvento.TabIndex = 10;
            // 
            // btnEditarEvento
            // 
            btnEditarEvento.Location = new Point(264, 730);
            btnEditarEvento.Name = "btnEditarEvento";
            btnEditarEvento.Size = new Size(157, 48);
            btnEditarEvento.TabIndex = 13;
            btnEditarEvento.Text = "Editar Evento";
            btnEditarEvento.UseVisualStyleBackColor = true;
            btnEditarEvento.Click += btnEditarEvento_Click;
            // 
            // lblListaEventos
            // 
            lblListaEventos.AutoSize = true;
            lblListaEventos.Location = new Point(151, 22);
            lblListaEventos.Name = "lblListaEventos";
            lblListaEventos.Size = new Size(149, 20);
            lblListaEventos.TabIndex = 14;
            lblListaEventos.Text = "Eventos Cadastrados:";
            lblListaEventos.Click += lblListaEventos_Click;
            // 
            // btnSelecionarEvento
            // 
            btnSelecionarEvento.Location = new Point(451, 730);
            btnSelecionarEvento.Name = "btnSelecionarEvento";
            btnSelecionarEvento.Size = new Size(197, 48);
            btnSelecionarEvento.TabIndex = 15;
            btnSelecionarEvento.Text = "Selecionar Evento";
            btnSelecionarEvento.UseVisualStyleBackColor = true;
            btnSelecionarEvento.Click += btnSelecionarEvento_Click;
            // 
            // lstAlunos
            // 
            lstAlunos.FormattingEnabled = true;
            lstAlunos.Location = new Point(24, 307);
            lstAlunos.Name = "lstAlunos";
            lstAlunos.Size = new Size(426, 164);
            lstAlunos.TabIndex = 22;
            lstAlunos.DoubleClick += lstAlunos_DoubleClick;
            // 
            // lstProfessores
            // 
            lstProfessores.FormattingEnabled = true;
            lstProfessores.Location = new Point(24, 487);
            lstProfessores.Name = "lstProfessores";
            lstProfessores.Size = new Size(426, 184);
            lstProfessores.TabIndex = 23;
            lstProfessores.DoubleClick += lstProfessores_DoubleClick;
            // 
            // lstEventos
            // 
            lstEventos.Location = new Point(24, 67);
            lstEventos.Name = "lstEventos";
            lstEventos.Size = new Size(426, 226);
            lstEventos.TabIndex = 24;
            lstEventos.UseCompatibleStateImageBehavior = false;
            lstEventos.SelectedIndexChanged += lstEventos_SelectedIndexChanged;
            // 
            // FormEvento
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 807);
            Controls.Add(lstEventos);
            Controls.Add(lstProfessores);
            Controls.Add(lstAlunos);
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
            Name = "FormEvento";
            Text = "FormEvento";
            Load += FormEvento_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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
        private ListBox lstAlunos;
        private ListBox lstProfessores;
        private ListView lstEventos;
    }
}