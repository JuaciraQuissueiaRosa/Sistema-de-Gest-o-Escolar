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
    public partial class FormNota : Form
    {
        private GestorEscola gestor;
        public FormNota(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaNotas();
        }

        private void btnRemoverNota_Click(object sender, EventArgs e)
        {
            int alunoId = int.Parse(txtAlunoIdNota.Text);
            int disciplinaId = int.Parse(txtDisciplinaIdNota.Text);
            string periodo = txtPeriodoNota.Text;

            if (gestor.VerificarSePeriodoEncerrado(periodo))
            {
                MessageBox.Show("Erro: O período letivo já foi encerrado. Não é possível remover notas.");
                return;
            }

            gestor.RemoverNota(alunoId, disciplinaId, periodo);
            AtualizarListaNotas();
        }

        private void btnAdicionarNota_Click(object sender, EventArgs e)
        {
            try
            {
                int alunoId = int.Parse(txtAlunoIdNota.Text);
                int disciplinaId = int.Parse(txtDisciplinaIdNota.Text);
                double nota = double.Parse(txtValorNota.Text);
                string periodo = txtPeriodoNota.Text;

                // Capturar o ID do professor selecionado no ComboBox
                if (cmbProfessorNota.SelectedItem == null)
                {
                    MessageBox.Show("Selecione um professor!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string professorSelecionado = cmbProfessorNota.SelectedItem.ToString();
                int professorId = int.Parse(professorSelecionado.Split('-')[0].Trim());

                // Verificar se o professor pode lançar a nota
                if (!gestor.VerificarSeProfessorPodeLancarNota(disciplinaId, professorId))
                {
                    MessageBox.Show("Erro: Apenas professores da disciplina podem lançar notas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar e adicionar a nota
                Nota novaNota = new Nota(alunoId, disciplinaId, nota, periodo);
                gestor.AdicionarNota(novaNota);
                AtualizarListaNotas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AtualizarListaNotas()
        {
            lstNotas.Items.Clear();
            foreach (var nota in gestor.Notas)
            {
                lstNotas.Items.Add($"Aluno {nota.AlunoId} - Disciplina {nota.DisciplinaId} - Nota: {nota.ValorNota} - {nota.PeriodoLetivo}");
            }
        }

        private void FormNota_Load(object sender, EventArgs e)
        {
            cmbProfessorNota.Items.Clear();
            foreach (var professor in gestor.Professores)
            {
                cmbProfessorNota.Items.Add($"{professor.Id} - {professor.Nome}");
            }
        }
    }
}
