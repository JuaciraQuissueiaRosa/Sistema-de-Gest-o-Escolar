using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormRelatorio : Form
    {
        private GestorEscola gestor;
        public FormRelatorio(GestorEscola gestor)
        {
            InitializeComponent();

            this.gestor = gestor;
            CarregarDados();
        }

        private void CarregarDados()
        {
            // Carrega alunos no ComboBox
            cmbAluno.DataSource = gestor.Alunos;
            cmbAluno.DisplayMember = "Nome";
            cmbAluno.ValueMember = "Id";

            // Carrega turmas no ComboBox
            cmbTurma.DataSource = gestor.Turmas;
            cmbTurma.DisplayMember = "Curso";
            cmbTurma.ValueMember = "Id";
        }


        private void cmbTurma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

   
        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            if (cmbTurma.SelectedValue != null)
            {
                int turmaId = (int)cmbTurma.SelectedValue;
                string relatorio = gestor.GerarRelatorioTurma(turmaId);
                txtRelatorio.Text = relatorio;
                AtualizarGrafico(turmaId);
            }
        }

        private void AtualizarGrafico(int turmaId)
        {
            graficoDesempenho.Plot.Clear();

            var alunos = gestor.Alunos.Where(a => a.TurmaId == turmaId).ToList();
            var notas = gestor.Notas.Where(n => alunos.Any(a => a.Id == n.AlunoId));

            if (!notas.Any())
            {
                MessageBox.Show("Nenhuma nota disponível para gerar o gráfico.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double[] medias = alunos.Select(aluno =>
            {
                var notasAluno = notas.Where(n => n.AlunoId == aluno.Id).ToList();
                return notasAluno.Any() ? notasAluno.Average(n => n.ValorNota) : 0;
            }).ToArray();

            string[] nomesAlunos = alunos.Select(a => a.Nome).ToArray();

            var bar = graficoDesempenho.Plot.AddBar(medias);
            bar.Labels = nomesAlunos;
            graficoDesempenho.Plot.XTicks(nomesAlunos);
            graficoDesempenho.Plot.YLabel("Média das Notas");

            graficoDesempenho.Refresh();
        }

        private void btnGerarPauta_Click(object sender, EventArgs e)
        {
            if (cmbAluno.SelectedValue != null)
            {
                int alunoId = (int)cmbAluno.SelectedValue;
                string pauta = gestor.GerarPautaAluno(alunoId);
                txtRelatorio.Text = pauta;
            }
        }
    }
}
