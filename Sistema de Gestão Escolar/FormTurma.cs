using Bibilioteca_Sistema_de_Gestão_Escolar;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormTurma : Form
    {
        private GestorEscola gestor;

        public FormTurma(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaTurmas();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRemoverTurma_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtIdTurma.Text);
            gestor.RemoverTurma(id);
            AtualizarListaTurmas();
        }

        private void btnAdicionarTurma_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtIdTurma.Text);
            string curso = txtCursoTurma.Text;
            string anoLetivo = txtAnoLetivoTurma.Text;
            string turno = cmbTurnoTurma.SelectedItem.ToString();

            Turma novaTurma = new Turma(id, curso, anoLetivo, turno);

            string[] alunosIds = txtAlunosTurma.Text.Split(',');
            foreach (string idAluno in alunosIds)
            {
                int alunoId;
                if (int.TryParse(idAluno.Trim(), out alunoId))
                {
                    novaTurma.AlunosIds.Add(alunoId);
                }
            }

            string[] disciplinasIds = txtDisciplinasTurma.Text.Split(',');
            foreach (string idDisciplina in disciplinasIds)
            {
                int disciplinaId;
                if (int.TryParse(idDisciplina.Trim(), out disciplinaId))
                {
                    novaTurma.DisciplinasIds.Add(disciplinaId);
                }
            }

            gestor.AdicionarTurma(novaTurma);
            AtualizarListaTurmas();
        }

        private void AtualizarListaTurmas()
        {
            lstTurmas.Items.Clear();
            foreach (var turma in gestor.Turmas)
            {
                lstTurmas.Items.Add($"{turma.Id} - {turma.Curso} - {turma.AnoLetivo} - {turma.Turno} - Alunos: {string.Join(",", turma.AlunosIds)} - Disciplinas: {string.Join(",", turma.DisciplinasIds)}");
            }
        }
    }
}
