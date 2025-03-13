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
    public partial class FormDisciplina : Form
    {
        private GestorEscola gestor;

        public FormDisciplina(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaDisciplinas();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdicionarDisciplina_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtIdDisciplina.Text);
            string nome = txtNomeDisciplina.Text;
            int cargaHoraria = int.Parse(txtCargaHoraria.Text);

            Disciplina novaDisciplina = new Disciplina(id, nome, cargaHoraria);

            string[] professoresIds = txtProfessoresDisciplina.Text.Split(',');
            foreach (string idProf in professoresIds)
            {
                int profId;
                if (int.TryParse(idProf.Trim(), out profId))
                {
                    novaDisciplina.ProfessoresIds.Add(profId);
                }
            }

            gestor.AdicionarDisciplina(novaDisciplina);
            AtualizarListaDisciplinas();
        }

        private void btnRemoverDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtIdDisciplina.Text);
                gestor.RemoverDisciplina(id);
                AtualizarListaDisciplinas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarListaDisciplinas()
        {
            lstDisciplinas.Items.Clear();
            foreach (var disciplina in gestor.Disciplinas)
            {
                lstDisciplinas.Items.Add($"{disciplina.Id} - {disciplina.Nome} - {disciplina.CargaHoraria}h - Professores: {string.Join(",", disciplina.ProfessoresIds)}");
            }
        }
    }
}
