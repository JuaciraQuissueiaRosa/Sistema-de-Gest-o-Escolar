

namespace Sistema_de_Gestão_Escolar
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ScottPlot;

    public partial class FormRelatorio : Form
    {
        private GestorEscola gestor;
        public FormRelatorio(GestorEscola gestor)
        {
            InitializeComponent();

            this.gestor = gestor;
            CarregarDados();
        }


        /// <summary>
        /// Metodo para carregar dados
        /// </summary>
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
        {// Limpar gráficos anteriores
            graficoDesempenho.Plot.Clear();

            // Obter alunos da turma
            var alunos = gestor.Alunos.Where(a => a.TurmaId == turmaId).ToList();
            var notas = gestor.Notas.Where(n => alunos.Any(a => a.Id == n.AlunoId));

            // Verificar se há dados para exibir
            if (!alunos.Any())
            {
                MessageBox.Show("Nenhum aluno encontrado para essa turma.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Criar array de nomes dos alunos
            string[] nomesAlunos = alunos.Select(a => a.Nome).ToArray();
            double[] medias = alunos.Select(a =>
            {
                var notasAluno = notas.Where(n => n.AlunoId == a.Id).ToList();
                return notasAluno.Any() ? notasAluno.Average(n => n.ValorNota) : 0;
            }).ToArray();

            // Criar gráfico de barras
            var bar = graficoDesempenho.Plot.AddBar(medias);
            graficoDesempenho.Plot.XTicks(nomesAlunos); // Definir rótulos no eixo X

            // Configurar rótulos e título
            graficoDesempenho.Plot.Title("Desempenho dos Alunos");
            graficoDesempenho.Plot.YLabel("Média das Notas");

            // Atualizar o gráfico
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

        private void FormRelatorio_Load(object sender, EventArgs e)
        {
            // Definir botões redondos
            SetRoundButton(btnGerarRelatorio);
            SetRoundButton(btnGerarPauta);
          

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
    }
    
}
