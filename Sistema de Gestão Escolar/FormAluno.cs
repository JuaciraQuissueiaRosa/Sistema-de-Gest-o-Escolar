using Bibilioteca_Sistema_de_Gestão_Escolar;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormAluno : Form
    {
        private GestorEscola gestor;

        public FormAluno(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaAlunos();
        }
        private void btnAdicionarAluno_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtIdAluno.Text);
            string nome = txtNomeAluno.Text;
            DateTime dataNascimento = dtpNascimentoAluno.Value;
            string contato = txtContatoAluno.Text;
            string morada = txtMoradaAluno.Text;
            string email = txtEmailAluno.Text;
            int turmaId = int.Parse(txtTurmaAluno.Text);

            Aluno novoAluno = new Aluno(id, nome, dataNascimento, contato, morada, email, turmaId);
            gestor.AdicionarAluno(novoAluno);
            AtualizarListaAlunos();
        }

        private void btnRemoverAluno_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtIdAluno.Text);
            gestor.RemoverAluno(id);
            AtualizarListaAlunos();
        }

        private void btnBuscarAluno_Click(object sender, EventArgs e)
        {
            string termo = txtBuscarAluno.Text;
            List<Aluno> resultados = gestor.BuscarAlunos(termo);
            lstAlunos.Items.Clear();
            foreach (var aluno in resultados)
            {
                lstAlunos.Items.Add($"{aluno.Id} - {aluno.Nome} - Turma: {aluno.TurmaId}");
            }
        }

        private void btnMudarTurma_Click(object sender, EventArgs e)
        {
            int alunoId = int.Parse(txtIdAluno.Text);
            int novaTurmaId = int.Parse(txtNovaTurmaAluno.Text);

            gestor.MudarAlunoDeTurma(alunoId, novaTurmaId);
            AtualizarListaAlunos();

        }

        private void AtualizarListaAlunos()
        {
            lstAlunos.Items.Clear();
            foreach (var aluno in gestor.ListarAlunos())
            {
                lstAlunos.Items.Add($"{aluno.Id} - {aluno.Nome} - Turma: {aluno.TurmaId}");
            }
        }
    }
}
