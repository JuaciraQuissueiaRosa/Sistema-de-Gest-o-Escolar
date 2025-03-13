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
    public partial class FormProfessor : Form
    {
        private GestorEscola gestor;

        public FormProfessor(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaProfessores();
        }
        private void btnRemoverProfessor_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtIdProfessor.Text);
            gestor.RemoverProfessor(id);
            AtualizarListaProfessores();
        }

        private void btnAdicionarProfessor_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtIdProfessor.Text);
            string nome = txtNomeProfessor.Text;
            string contato = txtContatoProfessor.Text;
            string email = txtEmailProfessor.Text;
            string areaEnsino = txtAreaEnsino.Text;

            Professor novoProfessor = new Professor(id, nome, contato, email, areaEnsino);
            gestor.AdicionarProfessor(novoProfessor);
            AtualizarListaProfessores();
        }
        private void AtualizarListaProfessores()
        {
            lstProfessores.Items.Clear();
            foreach (var professor in gestor.Professores)
            {
                lstProfessores.Items.Add($"{professor.Id} - {professor.Nome} - {professor.AreaEnsino}");
            }
        }
    }
}
