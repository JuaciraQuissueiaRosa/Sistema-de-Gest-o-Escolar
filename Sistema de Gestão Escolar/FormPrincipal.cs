namespace Sistema_de_Gestão_Escolar
{
    public partial class FormPrincipal : Form
    {
        private GestorEscola gestor; // Instância única do gestor

        public FormPrincipal()
        {
            InitializeComponent();
            gestor = new GestorEscola(); // Criar apenas um gestor para compartilhar entre os formulários
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnTurmas_Click(object sender, EventArgs e)
        {
            FormTurma formTurmas = new FormTurma(gestor);
            formTurmas.Show();
        }

        private void btnProfessores_Click(object sender, EventArgs e)
        {
            FormProfessor formProfessores = new FormProfessor(gestor);
            formProfessores.Show();
        }

        private void btnNotas_Click(object sender, EventArgs e)
        {
            FormNota formNotas = new FormNota(gestor);
            formNotas.Show();
        }

        private void btnDisciplinas_Click(object sender, EventArgs e)
        {
            FormDisciplina formDisciplinas = new FormDisciplina(gestor);
            formDisciplinas.Show();
        }

        private void btnAlunos_Click(object sender, EventArgs e)
        {
            FormAluno formAlunos = new FormAluno(gestor);
            formAlunos.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Fecha o programa
        }

        private void btnCreditos_Click(object sender, EventArgs e)
        {
            FormCredito formCredito = new FormCredito();

            formCredito.Show();
        }

        private void btnEvento_Click(object sender, EventArgs e)
        {
            FormEvento formEvento = new FormEvento(gestor);
            formEvento.Show();
        }
    }
}
