using Bibilioteca_Sistema_de_Gestão_Escolar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_Escolar
{

    public partial class FormCredito : Form
    {
        private List<Confete> confetes = new List<Confete>();
        private System.Windows.Forms.Timer timer;

        public FormCredito()
        {
            InitializeComponent();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha a janela de créditos
        }

        private void FormCredito_Load(object sender, EventArgs e)
        {

            Credito credito = new Credito(); // Criação de um objeto da classe

            lblAutor.Text = "Autor: " + credito.Autor;
            lblData.Text = "Data de Criação: " + credito.DataCriacao;
            lblVersao.Text = "Versão: " + credito.Versao;


            // Definir botões redondos

            SetRoundButton(btnFechar);

            this.DoubleBuffered = true; // Ativa o double buffering para evitar flickering

            // Criar o Timer para animar os confetes
            timer = new System.Windows.Forms.Timer
            {
                Interval = 50 // Atualiza a cada 50ms
            };
            timer.Tick += (sender, e) =>
            {
                // Adiciona novos confetes aleatoriamente
                if (new Random().NextDouble() < 0.5)
                {
                    confetes.Add(new Confete(new Random().Next(0, this.Width), 0)); // Confetes começam no topo
                }

                // Atualiza a posição dos confetes
                foreach (var confete in confetes)
                {
                    confete.Atualizar();
                }

                // Remove confetes que saíram da tela
                confetes.RemoveAll(c => c.Y > this.Height);

                // Redesenha o formulário
                this.Invalidate(); // Força o formulário a ser repintado
            };
            timer.Start();

        }

        private void SetRoundButton(Button button)
        {
            // Cria um caminho gráfico para o botão
            GraphicsPath path = new GraphicsPath();

            // Define um retângulo arredondado para o botão
            path.AddEllipse(0, 0, button.Width, button.Height);

            // Atribui a região do botão para o caminho arredondado
            button.Region = new Region(path);

            // Opcional: Define a cor de fundo e borda
            button.BackColor = Color.LightBlue;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
        }


        // Sobrescreve o método OnPaint para desenhar os confetes
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); // Chama o método base para garantir o desenho correto

            // Desenha todos os confetes
            foreach (var confete in confetes)
            {
                confete.Desenhar(e.Graphics); // Passa o Graphics do evento de Paint
            }
        }

    }
}

