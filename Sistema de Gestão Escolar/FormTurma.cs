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
            try
            {
                // Verificar se o ID da turma é um número válido
                if (!int.TryParse(txtIdTurma.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tentar remover a turma
                bool removida = gestor.RemoverTurma(id);
                if (removida)
                {
                    MessageBox.Show("Turma removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: A turma não pode ser removida. Verifique se há alunos matriculados ou se o ID é válido!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Atualizar a lista de turmas
                AtualizarListaTurmas();

                // Atualizar a lista de turmas no FormAluno (se estiver aberto)
                AtualizarFormAluno();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionarTurma_Click(object sender, EventArgs e)
        {
            try
            {
                // ------------------ VERIFICANDO ID DA TURMA ------------------ //
                if (!int.TryParse(txtIdTurma.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ------------------ VERIFICANDO CURSO ------------------ //
                string curso = cmbCursoTurma.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(curso))
                {
                    MessageBox.Show("Erro: Selecione um curso válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ------------------ VERIFICANDO ANO LETIVO ------------------ //
                string anoLetivo = txtAnoLetivoTurma.Text.Trim();
                if (string.IsNullOrEmpty(anoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!gestor.ValidarAnoLetivo(anoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo deve estar no formato 'AAAA/AAAA' e o primeiro ano deve ser menor que o segundo!",
                                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ------------------ VERIFICANDO TURNO ------------------ //
                string turno = cmbTurnoTurma.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(turno))
                {
                    MessageBox.Show("Erro: Selecione um turno!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ------------------ VERIFICANDO SELEÇÃO DE DISCIPLINAS ------------------ //
                if (lstDisciplinasTurma.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione pelo menos uma disciplina para associar à turma!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar a nova turma
                Turma novaTurma = new Turma(id, curso, anoLetivo, turno);

                // Adicionar todas as disciplinas selecionadas
                for (int i = 0; i < lstDisciplinasTurma.SelectedItems.Count; i++)
                {
                    string disciplinaSelecionada = lstDisciplinasTurma.SelectedItems[i].ToString();
                    string[] disciplinaSplit = disciplinaSelecionada.Split(' ');

                    if (!int.TryParse(disciplinaSplit[0], out int disciplinaId))
                    {
                        MessageBox.Show($"Erro: O ID da disciplina '{disciplinaSelecionada}' não é válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    novaTurma.DisciplinasIds.Add(disciplinaId);
                }

                // ------------------ VERIFICANDO PROFESSOR ------------------ //
                if (cmbProfessorTurma.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione um professor para associar à turma!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string professorSelecionado = cmbProfessorTurma.SelectedItem.ToString();
                string[] professorSplit = professorSelecionado.Split(' ');

                if (!int.TryParse(professorSplit[0], out int professorId))
                {
                    MessageBox.Show("Erro: O ID do professor não é válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ------------------ ADICIONANDO A TURMA ------------------ //
                gestor.AdicionarTurma(novaTurma);

                // Atualizar a lista de turmas
                AtualizarListaTurmas();

                MessageBox.Show("Turma criada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado ao adicionar turma: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AtualizarListaTurmas()
        {
            try
            {
                lstTurmas.Items.Clear(); // Limpa a lista antes de atualizar

                // Se não houver turmas cadastradas, exibir mensagem na ListBox
                if (gestor.Turmas.Count == 0)
                {
                    lstTurmas.Items.Add("Nenhuma turma cadastrada.");
                    return;
                }

                // Percorrer todas as turmas cadastradas
                for (int i = 0; i < gestor.Turmas.Count; i++)
                {
                    Turma turma = gestor.Turmas[i];

                    // Criar strings para armazenar os nomes das disciplinas associadas
                    string disciplinasTexto = "";
                    string professoresTexto = "";

                    // Percorrer todas as disciplinas associadas à turma
                    for (int j = 0; j < turma.DisciplinasIds.Count; j++)
                    {
                        int disciplinaId = turma.DisciplinasIds[j];

                        for (int k = 0; k < gestor.Disciplinas.Count; k++)
                        {
                            if (gestor.Disciplinas[k].Id == disciplinaId)
                            {
                                // Adiciona a disciplina à string, separando por vírgula
                                if (disciplinasTexto != "")
                                {
                                    disciplinasTexto += ", ";
                                }
                                disciplinasTexto += gestor.Disciplinas[k].Nome;

                                // Percorrer os professores associados a essa disciplina
                                for (int p = 0; p < gestor.Disciplinas[k].ProfessoresIds.Count; p++)
                                {
                                    int professorId = gestor.Disciplinas[k].ProfessoresIds[p];

                                    // Buscar o nome do professor pelo ID
                                    for (int q = 0; q < gestor.Professores.Count; q++)
                                    {
                                        if (gestor.Professores[q].Id == professorId)
                                        {
                                            // Adiciona o professor à string, separando por vírgula
                                            if (professoresTexto != "")
                                            {
                                                professoresTexto += ", ";
                                            }
                                            professoresTexto += gestor.Professores[q].Nome;
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }

                    // Criar string formatada para exibição na ListBox
                    string infoTurma = "ID: " + turma.Id +
                                       " | Curso: " + turma.Curso +
                                       " | Ano Letivo: " + turma.AnoLetivo +
                                       " | Turno: " + turma.Turno;

                    // Exibir disciplinas associadas
                    if (disciplinasTexto != "")
                    {
                        infoTurma += " | Disciplinas: " + disciplinasTexto;
                    }
                    else
                    {
                        infoTurma += " | Disciplinas: Nenhuma";
                    }

                    // Exibir professores associados
                    if (professoresTexto != "")
                    {
                        infoTurma += " | Professores: " + professoresTexto;
                    }
                    else
                    {
                        infoTurma += " | Professores: Nenhum";
                    }

                    // Adicionar turma na ListBox
                    lstTurmas.Items.Add(infoTurma);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar a lista de turmas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormTurma_Load(object sender, EventArgs e)
        {
            try
            {
                // Carregar os turnos disponíveis
                cmbTurnoTurma.Items.Clear();
                cmbTurnoTurma.Items.Add("Diurno");
                cmbTurnoTurma.Items.Add("Noturno");

                // Carregar os cursos disponíveis
                cmbCursoTurma.Items.Clear();
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

                // Carregar as disciplinas disponíveis
                CarregarDisciplinasDisponiveis();

                // Atualizar a lista de turmas ao abrir o formulário
                AtualizarListaTurmas();

                // Carregar professores disponíveis na ComboBox
                CarregarProfessoresDisponiveis();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar formulário de turmas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarFormAluno()
        {
            try
            {
                // Verificar se o formulário FormAluno está aberto
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i] is FormAluno)
                    {
                        FormAluno formAluno = (FormAluno)Application.OpenForms[i];
                        formAluno.AtualizarComboBoxTurmas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar formulário de alunos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarDisciplinasDisponiveis()
        {
            try
            {
                lstDisciplinasTurma.Items.Clear(); // Limpa a ListBox antes de atualizar

                // Se não houver disciplinas cadastradas, exibir mensagem
                if (gestor.Disciplinas.Count == 0)
                {
                    lstDisciplinasTurma.Items.Add("Nenhuma disciplina disponível");
                    return;
                }

                // Percorrer todas as disciplinas cadastradas
                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    Disciplina disciplina = gestor.Disciplinas[i];

                    // Adicionar cada disciplina no formato "ID - Nome"
                    lstDisciplinasTurma.Items.Add(disciplina.Id + " - " + disciplina.Nome);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar disciplinas disponíveis: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarTurma_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTurmas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione uma turma para editar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter a turma selecionada
                Turma turmaSelecionada = gestor.Turmas[lstTurmas.SelectedIndex];

                // Preencher os campos com os dados da turma
                txtIdTurma.Text = turmaSelecionada.Id.ToString();
                txtAnoLetivoTurma.Text = turmaSelecionada.AnoLetivo;
                cmbTurnoTurma.SelectedItem = turmaSelecionada.Turno;
                cmbCursoTurma.SelectedItem = turmaSelecionada.Curso;

                // Limpar e preencher ListBox de Disciplinas
                lstDisciplinasTurma.Items.Clear();
                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    Disciplina disciplina = gestor.Disciplinas[i];
                    string item = disciplina.Id + " - " + disciplina.Nome;
                    lstDisciplinasTurma.Items.Add(item);

                    // Marcar as disciplinas que já estão associadas à turma
                    if (turmaSelecionada.DisciplinasIds.Contains(disciplina.Id))
                    {
                        lstDisciplinasTurma.SetSelected(lstDisciplinasTurma.Items.Count - 1, true);
                    }
                }

                // Limpar e preencher ComboBox de Professores
                cmbProfessorTurma.Items.Clear();
                for (int i = 0; i < gestor.Professores.Count; i++)
                {
                    Professor professor = gestor.Professores[i];
                    string item = professor.Id + " - " + professor.Nome;
                    cmbProfessorTurma.Items.Add(item);

                    // Selecionar os professores que já estão associados à turma
                    for (int j = 0; j < turmaSelecionada.DisciplinasIds.Count; j++)
                    {
                        int disciplinaId = turmaSelecionada.DisciplinasIds[j];
                        Disciplina disciplinaEncontrada = gestor.Disciplinas.Find(d => d.Id == disciplinaId);

                        if (disciplinaEncontrada != null && disciplinaEncontrada.ProfessoresIds.Contains(professor.Id))
                        {
                            cmbProfessorTurma.SelectedItem = item;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar turma para edição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarTurma_Click(object sender, EventArgs e)
        {
            if (lstTurmas.SelectedIndex == -1)
            {
                MessageBox.Show("Erro: Selecione uma turma para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Turma turma = gestor.Turmas[lstTurmas.SelectedIndex];

            MessageBox.Show("ID: " + turma.Id + "\nCurso: " + turma.Curso + "\nAno Letivo: " + turma.AnoLetivo +
                            "\nTurno: " + turma.Turno, "Detalhes da Turma", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CarregarProfessoresDisponiveis()
        {
            try
            {
                cmbProfessorTurma.Items.Clear(); // Limpa a ComboBox antes de adicionar novos professores

                if (gestor.Professores.Count == 0)
                {
                    cmbProfessorTurma.Items.Add("Nenhum professor disponível");
                    cmbProfessorTurma.SelectedIndex = 0;
                    return;
                }

                for (int i = 0; i < gestor.Professores.Count; i++)
                {
                    Professor professor = gestor.Professores[i];
                    cmbProfessorTurma.Items.Add(professor.Id + " - " + professor.Nome);
                }

                cmbProfessorTurma.SelectedIndex = 0; // Seleciona o primeiro professor por padrão
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar professores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstTurmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTurmas.SelectedIndex == -1)
            {
                return; // Nenhuma turma selecionada
            }

            try
            {
                // Obter a turma selecionada
                Turma turmaSelecionada = gestor.Turmas[lstTurmas.SelectedIndex];

                // Preencher os campos do formulário com os dados da turma
                txtIdTurma.Text = turmaSelecionada.Id.ToString();
                txtAnoLetivoTurma.Text = turmaSelecionada.AnoLetivo;
                cmbTurnoTurma.SelectedItem = turmaSelecionada.Turno;
                cmbCursoTurma.SelectedItem = turmaSelecionada.Curso;

                MessageBox.Show("Turma carregada para edição.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar turma para edição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvarEdicaoTurma_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTurmas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Nenhuma turma selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter a turma selecionada
                Turma turmaSelecionada = gestor.Turmas[lstTurmas.SelectedIndex];

                // Validar ID da turma
                if (!int.TryParse(txtIdTurma.Text, out int novoId))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string novoCurso = cmbCursoTurma.SelectedItem?.ToString();
                string novoAnoLetivo = txtAnoLetivoTurma.Text.Trim();
                string novoTurno = cmbTurnoTurma.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(novoCurso) || string.IsNullOrEmpty(novoAnoLetivo) || string.IsNullOrEmpty(novoTurno))
                {
                    MessageBox.Show("Erro: Preencha todos os campos corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!gestor.ValidarAnoLetivo(novoAnoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo deve estar no formato 'AAAA/AAAA'!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualizar os dados da turma
                turmaSelecionada.Id = novoId;
                turmaSelecionada.Curso = novoCurso;
                turmaSelecionada.AnoLetivo = novoAnoLetivo;
                turmaSelecionada.Turno = novoTurno;

                // Atualizar disciplinas associadas à turma usando a ListBox
                turmaSelecionada.DisciplinasIds.Clear();
                for (int i = 0; i < lstDisciplinasTurma.SelectedItems.Count; i++)
                {
                    string disciplinaSelecionada = lstDisciplinasTurma.SelectedItems[i].ToString();
                    int disciplinaId = int.Parse(disciplinaSelecionada.Split(' ')[0]);
                    turmaSelecionada.DisciplinasIds.Add(disciplinaId);
                }

                // Atualizar professores associados às disciplinas da turma
                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    Disciplina disciplina = gestor.Disciplinas[i];

                    if (turmaSelecionada.DisciplinasIds.Contains(disciplina.Id))
                    {
                        disciplina.ProfessoresIds.Clear();

                        if (cmbProfessorTurma.SelectedItem != null)
                        {
                            string professorSelecionado = cmbProfessorTurma.SelectedItem.ToString();
                            int professorId = int.Parse(professorSelecionado.Split(' ')[0]);
                            disciplina.ProfessoresIds.Add(professorId);
                        }
                    }
                }

                // Atualizar a lista de turmas na ListBox
                AtualizarListaTurmas();
                MessageBox.Show("Turma editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar turma: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
