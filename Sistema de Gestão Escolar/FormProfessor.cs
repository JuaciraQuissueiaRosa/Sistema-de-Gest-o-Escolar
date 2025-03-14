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
            try
            {
                int id = int.Parse(txtIdProfessor.Text);
                string nome = txtNomeProfessor.Text;
                string contato = txtContatoProfessor.Text;
                string email = txtEmailProfessor.Text;

                // Captura a área de ensino selecionada no ComboBox
                string areaEnsino = cmbAreaEnsino.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(areaEnsino))
                {
                    MessageBox.Show("Selecione uma área de ensino!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Professor novoProfessor = new Professor(id, nome, contato, email, areaEnsino);

                gestor.AdicionarProfessor(novoProfessor);
                AtualizarListaProfessores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AtualizarListaProfessores()
        {
            lstProfessores.Items.Clear();
            foreach (var professor in gestor.Professores)
            {
                // Buscar disciplinas associadas ao professor
                List<string> disciplinasProfessor = new List<string>();
                foreach (var disciplina in gestor.Disciplinas)
                {
                    if (disciplina.ProfessoresIds.Contains(professor.Id))
                    {
                        disciplinasProfessor.Add(disciplina.Nome);
                    }
                }

                // Exibir todas as informações
                lstProfessores.Items.Add(
                    $"ID: {professor.Id} | Nome: {professor.Nome} | Contato: {professor.Contato} | " +
                    $"Email: {professor.Email} | Área: {professor.AreaEnsino} | Disciplinas: {string.Join(", ", disciplinasProfessor)}");
            }
        }

        private void FormProfessor_Load(object sender, EventArgs e)
        {
            cmbAreaEnsino.Items.Add("Línguas e Humanidades");
            cmbAreaEnsino.Items.Add("Ciências e Tecnologias");
            cmbAreaEnsino.Items.Add("Ciências Socioeconómicas");
            cmbAreaEnsino.Items.Add("Artes Visuais");
            cmbAreaEnsino.Items.Add("Educação Física e Desporto");
            cmbAreaEnsino.Items.Add("Informática e Tecnologias");
            cmbAreaEnsino.Items.Add("Matemática e Física");
            cmbAreaEnsino.Items.Add("Biologia e Geologia");
            cmbAreaEnsino.Items.Add("História e Filosofia");
            cmbAreaEnsino.Items.Add("Educação Especial");
        }
    }
}
