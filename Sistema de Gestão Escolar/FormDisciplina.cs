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

                // Verificar se a disciplina já existe no sistema
                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    if (gestor.Disciplinas[i].Nome.Equals(nomeDisciplina, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Erro: Já existe uma disciplina com esse nome!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

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

                // Lista para armazenar os professores formatados
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
                if (!gestor.AdicionarDisciplina(novaDisciplina))
                {
                    MessageBox.Show("Erro ao adicionar a disciplina.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualizar a lista de disciplinas
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
                // Verificar se o ID da disciplina é um número válido
                if (!int.TryParse(txtIdDisciplina.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tentar remover a disciplina
                bool removida = gestor.RemoverDisciplina(id);
                if (removida)
                {
                    MessageBox.Show("Disciplina removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarListaDisciplinas();
                }
                else
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao remover disciplina: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AtualizarListaDisciplinas()
        {
            try
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

                        professoresNomes.Add(profId + " - " + nomeProfessor);
                    }

                    // Criar a string de exibição sem usar `string.Join`
                    string professoresTexto = "Nenhum";
                    if (professoresNomes.Count > 0)
                    {
                        professoresTexto = "";
                        for (int m = 0; m < professoresNomes.Count; m++)
                        {
                            if (m > 0)
                            {
                                professoresTexto += ", ";
                            }
                            professoresTexto += professoresNomes[m];
                        }
                    }

                    // Exibir disciplina com professores na lista
                    string infoDisciplina = "ID: " + disciplina.Id + " | Nome: " + disciplina.Nome +
                                            " | Carga Horária: " + disciplina.CargaHoraria + "h/semana" +
                                            " | Professores: " + professoresTexto;

                    lstDisciplinas.Items.Add(infoDisciplina);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar lista de disciplinas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void FormDisciplina_Load(object sender, EventArgs e)
        {

            try
            {
                // Carga horária
                cmbCargaHoraria.Items.Clear();
                string[] cargas = { "2", "3", "4", "5" };
                for (int i = 0; i < cargas.Length; i++)
                {
                    cmbCargaHoraria.Items.Add(cargas[i]);
                }

                cmbNomeDisciplina.Items.Clear();

                // Disciplinas organizadas por áreas
                string[] disciplinas =
                {
            // Línguas e Humanidades
            "Português", "Inglês", "Francês", "Espanhol", "Filosofia", "História",

            // Ciências e Tecnologias
            "Matemática", "Física e Química", "Biologia e Geologia", "Geometria Descritiva",

            // Ciências Socioeconómicas
            "Economia", "Geografia", "Sociologia", "Direito",

            // Artes Visuais
            "Educação Visual", "Desenho", "História da Cultura e das Artes",

            // Educação Física e Desporto
            "Educação Física", "Ciências do Desporto",

            // Informática e Tecnologias
            "Tecnologias de Informação e Comunicação (TIC)", "Programação", "Robótica"
        };

                for (int i = 0; i < disciplinas.Length; i++)
                {
                    cmbNomeDisciplina.Items.Add(disciplinas[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formulário de disciplinas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarDisciplina_Click(object sender, EventArgs e)
        {
            if (lstDisciplinas.SelectedIndex == -1)
            {
                MessageBox.Show("Erro: Selecione uma disciplina para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Disciplina disciplina = gestor.Disciplinas[lstDisciplinas.SelectedIndex];

            MessageBox.Show("ID: " + disciplina.Id + "\nNome: " + disciplina.Nome + "\nCarga Horária: " + disciplina.CargaHoraria + "h",
                            "Detalhes da Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditarDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDisciplinas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter disciplina selecionada
                Disciplina disciplinaSelecionada = gestor.Disciplinas[lstDisciplinas.SelectedIndex];

                // Preencher os campos com os dados da disciplina
                txtIdDisciplina.Text = disciplinaSelecionada.Id.ToString();
                cmbNomeDisciplina.SelectedItem = disciplinaSelecionada.Nome;
                cmbCargaHoraria.SelectedItem = disciplinaSelecionada.CargaHoraria.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar disciplina para edição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSalvarEdicaoDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDisciplinas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Nenhuma disciplina selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter disciplina selecionada
                Disciplina disciplinaSelecionada = gestor.Disciplinas[lstDisciplinas.SelectedIndex];

                // Validar ID da disciplina
                if (!int.TryParse(txtIdDisciplina.Text, out int novoId))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string novoNome = cmbNomeDisciplina.SelectedItem?.ToString();
                int novaCargaHoraria;

                if (string.IsNullOrEmpty(novoNome) || !int.TryParse(cmbCargaHoraria.SelectedItem?.ToString(), out novaCargaHoraria))
                {
                    MessageBox.Show("Erro: Preencha todos os campos corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar professores associados
                List<int> novosProfessoresIds = new List<int>();
                if (txtProfessoresDisciplina.Text != null)
                {
                    string professorSelecionado = txtProfessoresDisciplina.Text.ToString();
                    int professorId = int.Parse(professorSelecionado.Split(' ')[0]);
                    novosProfessoresIds.Add(professorId);
                }

                if (!gestor.EditarDisciplina(disciplinaSelecionada.Id, novoNome, novaCargaHoraria, novosProfessoresIds))
                {
                    MessageBox.Show("Erro ao editar a disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AtualizarListaDisciplinas();
                MessageBox.Show("Disciplina editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar disciplina: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
