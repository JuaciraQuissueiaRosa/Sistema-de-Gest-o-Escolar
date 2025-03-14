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
            try
            {
                int id = int.Parse(txtIdTurma.Text);
                string curso = txtCursoTurma.Text;
                string anoLetivo = txtAnoLetivoTurma.Text;

                // Captura o turno selecionado no ComboBox
                string turno = cmbTurnoTurma.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(turno))
                {
                    MessageBox.Show("Selecione um turno!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Turma novaTurma = new Turma(id, curso, anoLetivo, turno);

                gestor.AdicionarTurma(novaTurma);
                AtualizarListaTurmas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarListaTurmas()
        {
            lstTurmas.Items.Clear();
            foreach (var turma in gestor.Turmas)
            {
                lstTurmas.Items.Add($"{turma.Id} - {turma.Curso} - {turma.AnoLetivo} - {turma.Turno} - Alunos: {string.Join(",", turma.AlunosIds)} - Disciplinas: {string.Join(",", turma.DisciplinasIds)}");
            }
        }

        private void FormTurma_Load(object sender, EventArgs e)
        {
            cmbTurnoTurma.Items.Add("Diurno");
            cmbTurnoTurma.Items.Add("Noturno");



            cmbCursoTurma.Items.Clear();

            // Adicionar cursos válidos no ComboBox
            cmbCursoTurma.Items.Add("Ciências e Tecnologias");
            cmbCursoTurma.Items.Add("Línguas e Humanidades");
            cmbCursoTurma.Items.Add("Ciências Socioeconómicas");
            cmbCursoTurma.Items.Add("Artes Visuais");
            cmbCursoTurma.Items.Add("Técnico de Informática e Gestão");
            cmbCursoTurma.Items.Add("Técnico de Eletrónica, Automação e Comando");
            cmbCursoTurma.Items.Add("Técnico de Turismo");
            cmbCursoTurma.Items.Add("Técnico de Cozinha e Pastelaria");
            cmbCursoTurma.Items.Add("Técnico de Restaurante e Bar");
            cmbCursoTurma.Items.Add("Técnico de Mecatrónica");
        }
    }
}
