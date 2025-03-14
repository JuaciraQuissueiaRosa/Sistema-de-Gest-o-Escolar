using Bibilioteca_Sistema_de_Gestão_Escolar;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormDisciplina : Form
    {
        private GestorEscola gestor;

        public FormDisciplina(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaDisciplinas();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdicionarDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se o ID da disciplina é válido
                if (!int.TryParse(txtIdDisciplina.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar a disciplina selecionada no ComboBox
                if (cmbNomeDisciplina.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string nomeDisciplina = cmbNomeDisciplina.SelectedItem.ToString();

                // Capturar a carga horária selecionada no ComboBox
                if (cmbCargaHoraria.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma carga horária válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int cargaHoraria = int.Parse(cmbCargaHoraria.SelectedItem.ToString());

                // Criar nova disciplina
                Disciplina novaDisciplina = new Disciplina(id, nomeDisciplina, cargaHoraria);

                // Verificar se há professores informados
                if (string.IsNullOrWhiteSpace(txtProfessoresDisciplina.Text))
                {
                    MessageBox.Show("Erro: Informe pelo menos um professor para esta disciplina.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lista para armazenar os professores da disciplina
                List<string> professoresFormatados = new List<string>();

                // Associar professores à disciplina
                string[] professoresIds = txtProfessoresDisciplina.Text.Split(',');

                for (int i = 0; i < professoresIds.Length; i++)
                {
                    string idProfStr = professoresIds[i].Trim();
                    int profId;

                    if (!int.TryParse(idProfStr, out profId))
                    {
                        MessageBox.Show($"Erro: O ID do professor '{idProfStr}' é inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Buscar o professor na lista SEM LINQ
                    Professor professorEncontrado = null;
                    for (int j = 0; j < gestor.Professores.Count; j++)
                    {
                        if (gestor.Professores[j].Id == profId)
                        {
                            professorEncontrado = gestor.Professores[j];
                            break;
                        }
                    }

                    if (professorEncontrado == null)
                    {
                        MessageBox.Show($"Erro: O professor com ID {profId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verificar se a área de ensino do professor é compatível com a disciplina
                    if (!gestor.ValidarAreaDeEnsino(professorEncontrado.AreaEnsino, nomeDisciplina))
                    {
                        MessageBox.Show($"Erro: O professor {professorEncontrado.Nome} ({professorEncontrado.AreaEnsino}) não pode lecionar {nomeDisciplina}.",
                            "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Adicionar ID e Nome do professor na lista formatada
                    professoresFormatados.Add($"{profId} - {professorEncontrado.Nome}");

                    // Adicionar o professor à disciplina
                    novaDisciplina.ProfessoresIds.Add(profId);
                }

                // Adicionar a disciplina ao sistema
                gestor.AdicionarDisciplina(novaDisciplina);
                AtualizarListaDisciplinas();

                MessageBox.Show("Disciplina adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void btnRemoverDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtIdDisciplina.Text);
                gestor.RemoverDisciplina(id);
                AtualizarListaDisciplinas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
            private void AtualizarListaDisciplinas()
            {
                lstDisciplinas.Items.Clear();

                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    Disciplina disciplina = gestor.Disciplinas[i];

                    // Lista para armazenar os nomes dos professores
                    List<string> professoresNomes = new List<string>();

                    // Buscar os nomes dos professores na lista de professores
                    for (int j = 0; j < disciplina.ProfessoresIds.Count; j++)
                    {
                        int profId = disciplina.ProfessoresIds[j];
                        string nomeProfessor = "Desconhecido";

                        for (int k = 0; k < gestor.Professores.Count; k++)
                        {
                            if (gestor.Professores[k].Id == profId)
                            {
                                nomeProfessor = gestor.Professores[k].Nome;
                                break;
                            }
                        }

                        professoresNomes.Add($"{profId} - {nomeProfessor}");
                    }

                    // Exibir disciplina com professores na lista
                    lstDisciplinas.Items.Add($"ID: {disciplina.Id} | Nome: {disciplina.Nome} | Carga Horária: {disciplina.CargaHoraria}h/semana | Professores: {string.Join(", ", professoresNomes)}");
                }
            }

        

        private void FormDisciplina_Load(object sender, EventArgs e)
        {

            // carga horaria
            cmbCargaHoraria.Items.Clear();

            // Adicionar opções de carga horária
            cmbCargaHoraria.Items.Add("2");
            cmbCargaHoraria.Items.Add("3");
            cmbCargaHoraria.Items.Add("4");
            cmbCargaHoraria.Items.Add("5");




            cmbNomeDisciplina.Items.Clear();

            // Adicionar disciplinas organizadas por áreas
            cmbNomeDisciplina.Items.Add("Português");
            cmbNomeDisciplina.Items.Add("Inglês");
            cmbNomeDisciplina.Items.Add("Francês");
            cmbNomeDisciplina.Items.Add("Espanhol");
            cmbNomeDisciplina.Items.Add("Filosofia");
            cmbNomeDisciplina.Items.Add("História");

            cmbNomeDisciplina.Items.Add("Matemática");
            cmbNomeDisciplina.Items.Add("Física e Química");
            cmbNomeDisciplina.Items.Add("Biologia e Geologia");
            cmbNomeDisciplina.Items.Add("Geometria Descritiva");

            cmbNomeDisciplina.Items.Add("Economia");
            cmbNomeDisciplina.Items.Add("Geografia");
            cmbNomeDisciplina.Items.Add("Sociologia");
            cmbNomeDisciplina.Items.Add("Direito");

            cmbNomeDisciplina.Items.Add("Educação Visual");
            cmbNomeDisciplina.Items.Add("Desenho");
            cmbNomeDisciplina.Items.Add("História da Cultura e das Artes");

            cmbNomeDisciplina.Items.Add("Educação Física");
            cmbNomeDisciplina.Items.Add("Ciências do Desporto");

            cmbNomeDisciplina.Items.Add("Tecnologias de Informação e Comunicação (TIC)");
            cmbNomeDisciplina.Items.Add("Programação");
            cmbNomeDisciplina.Items.Add("Robótica");
        }
    }
}
