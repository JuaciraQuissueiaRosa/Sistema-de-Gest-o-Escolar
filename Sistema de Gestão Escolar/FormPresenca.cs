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
    public partial class FormPresenca : Form
    {
        private GestorEscola gestor;

        public FormPresenca(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor ?? throw new ArgumentNullException(nameof(gestor)); // Evita null
        }

        private void FormPresenca_Load(object sender, EventArgs e)
        {
            CarregarDados();
            AtualizarListaPresencas();
        }

        private void btnRegistarPresenca_Click(object sender, EventArgs e)
        {
            RegistrarPresencaFalta(true); // Chama o método passando "true" para presença
        }


        private void btnRegistarFalta_Click(object sender, EventArgs e)
        {
            RegistrarPresencaFalta(false); // Chama o método passando "false" para falta
        }

        private void RegistrarPresencaFalta(bool presente)
        {
            try
            {
                if (cmbAluno.SelectedValue == null || cmbDisciplina.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um aluno e uma disciplina.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(cmbAluno.SelectedValue.ToString(), out int alunoId) ||
                    !int.TryParse(cmbDisciplina.SelectedValue.ToString(), out int disciplinaId))
                {
                    MessageBox.Show("Erro ao obter ID do aluno ou disciplina.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime data = dtpDataPresenca.Value;

                // Registra presença ou falta
                gestor.RegistrarPresenca(alunoId, disciplinaId, data, presente);
                gestor.VerificarFaltasExcessivas();

                // Verifica faltas excessivas e exibe os alertas
                List<string> alertas = gestor.VerificarFaltasExcessivas();
                lstAlertas.Items.Clear();
                lstAlertas.Items.AddRange(alertas.ToArray());

                MessageBox.Show($"{(presente ? "Presença" : "Falta")} registrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AtualizarListaPresencas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar presença/falta: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarDados()
        {
            cmbAluno.DataSource = gestor.Alunos;
            cmbAluno.DisplayMember = "Nome";
            cmbAluno.ValueMember = "Id";

            cmbDisciplina.DataSource = gestor.Disciplinas;
            cmbDisciplina.DisplayMember = "Nome";
            cmbDisciplina.ValueMember = "Id";
        }

      
        private void AtualizarListaPresencas()
        {
            lstPresencas.Items.Clear();

            foreach (var presenca in gestor.Presencas)
            {
                string alunoNome = gestor.Alunos.FirstOrDefault(a => a.Id == presenca.AlunoId)?.Nome ?? "Aluno não encontrado";
                string disciplinaNome = gestor.Disciplinas.FirstOrDefault(d => d.Id == presenca.DisciplinaId)?.Nome ?? "Disciplina não encontrada";
                string status = presenca.Presente ? "Presente" : "Falta";

                lstPresencas.Items.Add($"{presenca.Data:dd/MM/yyyy} | {alunoNome} | {disciplinaNome} | {status}");
            }
        }

        private void FormPresenca_Load_1(object sender, EventArgs e)
        {
            CarregarDados();
            AtualizarListaPresencas();
        }
    }
}
