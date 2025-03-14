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
                // Verificar se o ID da turma é um número válido
                if (!int.TryParse(txtIdTurma.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se um curso foi selecionado no ComboBox
                string curso = cmbCursoTurma.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(curso))
                {
                    MessageBox.Show("Erro: Selecione um curso válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o ano letivo foi preenchido
                string anoLetivo = txtAnoLetivoTurma.Text.Trim();
                if (string.IsNullOrEmpty(anoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Captura o turno selecionado no ComboBox
                string turno = cmbTurnoTurma.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(turno))
                {
                    MessageBox.Show("Erro: Selecione um turno!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar a nova turma com os dados corrigidos
                Turma novaTurma = new Turma(id, curso, anoLetivo, turno);

                // Adicionar a turma ao sistema
                gestor.AdicionarTurma(novaTurma);

                // Atualizar a lista de turmas na ListBox
                AtualizarListaTurmas();

                MessageBox.Show("Turma adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarTurmasDisponiveis(int turmaAtualId)
        {
            cmbCursoTurma.Items.Clear(); // Limpa as opções antigas

            for (int i = 0; i < gestor.Turmas.Count; i++)
            {
                Turma turma = gestor.Turmas[i];

                // Adiciona apenas turmas diferentes da atual
                if (turma.Id != turmaAtualId)
                {
                    cmbCursoTurma.Items.Add($"ID: {turma.Id} - {turma.Curso} ({turma.AnoLetivo})");
                }
            }
        }
        private void AtualizarListaTurmas()
        {
            lstTurmas.Items.Clear(); // Limpa a lista antes de atualizar

            // Percorre todas as turmas cadastradas
            for (int i = 0; i < gestor.Turmas.Count; i++)
            {
                Turma turma = gestor.Turmas[i];

                // Criar uma lista para armazenar os nomes das disciplinas associadas à turma
                List<string> disciplinasNomes = new List<string>();

                // Verificar se existem disciplinas e associá-las à turma
                if (gestor.Disciplinas.Count > 0)
                {
                    for (int j = 0; j < gestor.Disciplinas.Count; j++)
                    {
                        Disciplina disciplina = gestor.Disciplinas[j];

                        if (disciplina.TurmasIds == turma.Id) // Se a disciplina pertence à turma
                        {
                            disciplinasNomes.Add(disciplina.Nome);
                        }
                    }
                }

                // Criar a string formatada para exibir na ListBox
                string infoTurma = $"ID: {turma.Id} | Curso: {turma.Curso} | Ano Letivo: {turma.AnoLetivo} | Turno: {turma.Turno}";

                // Se houver disciplinas associadas, adicioná-las à exibição
                if (disciplinasNomes.Count > 0)
                {
                    infoTurma += $" | Disciplinas: {string.Join(", ", disciplinasNomes)}";
                }
                else
                {
                    infoTurma += " | Disciplinas: Nenhuma";
                }

                // Adiciona a turma na ListBox
                lstTurmas.Items.Add(infoTurma);
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
