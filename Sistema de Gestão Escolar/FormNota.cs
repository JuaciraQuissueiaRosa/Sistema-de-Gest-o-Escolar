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
            int alunoId = int.Parse(txtAlunoIdNota.Text);
            int disciplinaId = int.Parse(txtDisciplinaIdNota.Text);
            double nota = double.Parse(txtValorNota.Text);
            string periodo = txtPeriodoNota.Text;

            if (gestor.VerificarSePeriodoEncerrado(periodo))
            {
                MessageBox.Show("Erro: O período letivo já foi encerrado.");
                return;
            }

            if (!gestor.VerificarSeProfessorPodeLancarNota(disciplinaId, alunoId))
            {
                MessageBox.Show("Erro: Apenas professores da disciplina podem lançar notas.");
                return;
            }

            Nota novaNota = new Nota(alunoId, disciplinaId, nota, periodo);
            gestor.AdicionarNota(novaNota);
            AtualizarListaNotas();
        }
        private void AtualizarListaNotas()
        {
            lstNotas.Items.Clear();
            foreach (var nota in gestor.Notas)
            {
                lstNotas.Items.Add($"Aluno {nota.AlunoId} - Disciplina {nota.DisciplinaId} - Nota: {nota.ValorNota} - {nota.PeriodoLetivo}");
            }
        }
    }
}
