using Bibilioteca_Sistema_de_Gestão_Escolar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormCredito : Form
    {
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
        }
    }
}
