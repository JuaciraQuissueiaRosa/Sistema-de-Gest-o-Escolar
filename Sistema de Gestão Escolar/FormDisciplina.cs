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
                // ✅ Verificar se o ID da disciplina é válido e único
                if (!int.TryParse(txtIdDisciplina.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gestor.Disciplinas.Any(d => d.Id == id))
                {
                    MessageBox.Show("Erro: Já existe uma disciplina com esse ID!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Verificar se o nome da disciplina foi selecionado
                if (cmbNomeDisciplina.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nomeDisciplina = cmbNomeDisciplina.SelectedItem.ToString();

                // ✅ Impedir duplicação de nomes
                if (gestor.Disciplinas.Any(d => d.Nome.Equals(nomeDisciplina, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Erro: Já existe uma disciplina com esse nome!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Verificar carga horária
                if (!int.TryParse(cmbCargaHoraria.SelectedItem?.ToString(), out int cargaHoraria))
                {
                    MessageBox.Show("Erro: Selecione uma carga horária válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Criar nova disciplina
                Disciplina novaDisciplina = new Disciplina(id, nomeDisciplina, cargaHoraria);

                // ✅ Associar professores (se houver)
                if (!string.IsNullOrWhiteSpace(txtProfessoresDisciplina.Text))
                {
                    List<int> professoresIds = txtProfessoresDisciplina.Text
                        .Split(',')
                        .Select(p => p.Trim())  // Remover espaços extras
                        .Where(p => int.TryParse(p, out _)) // Verificar se é número válido
                        .Select(int.Parse) // Converter para inteiro
                        .ToList();

                    // ✅ Verificar se os professores existem
                    if (!professoresIds.All(pid => gestor.Professores.Any(prof => prof.Id == pid)))
                    {
                        MessageBox.Show("Erro: Um ou mais IDs de professores são inválidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // ✅ Associar os professores à disciplina
                    novaDisciplina.ProfessoresIds = professoresIds;
                }

                // ✅ Adicionar disciplina ao sistema
                gestor.Disciplinas.Add(novaDisciplina);

                // ✅ Bloquear edição do ID
                txtIdDisciplina.Enabled = false;

                // ✅ Atualizar a lista de disciplinas
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
                // ✅ Verificar se uma disciplina foi selecionada
                if (lstDisciplinas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina para remover!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Obter a disciplina selecionada
                Disciplina disciplinaSelecionada = gestor.Disciplinas.ElementAtOrDefault(lstDisciplinas.SelectedIndex);

                if (disciplinaSelecionada == null)
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Confirmar remoção antes de excluir a disciplina
                DialogResult confirmacao = MessageBox.Show($"Tem certeza que deseja remover a disciplina '{disciplinaSelecionada.Nome}'?",
                    "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacao == DialogResult.No)
                {
                    return;
                }

                // ✅ Remover disciplina
                bool removida = gestor.RemoverDisciplina(disciplinaSelecionada.Id);

                if (removida)
                {
                    MessageBox.Show("Disciplina removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarListaDisciplinas();
                }
                else
                {
                    MessageBox.Show("Erro: Não foi possível remover a disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                lstDisciplinas.Items.Clear(); // Limpa a lista antes de atualizar

                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    Disciplina disciplina = gestor.Disciplinas[i];

                    // Criar lista para armazenar os nomes dos professores vinculados
                    List<string> professoresNomes = new List<string>();

                    if (disciplina.ProfessoresIds.Count > 0)
                    {
                        for (int j = 0; j < disciplina.ProfessoresIds.Count; j++)
                        {
                            int profId = disciplina.ProfessoresIds[j];

                            // Buscar professor pelo ID
                            Professor professorEncontrado = gestor.Professores.Find(p => p.Id == profId);

                            if (professorEncontrado != null)
                            {
                                professoresNomes.Add($"{profId} - {professorEncontrado.Nome}");
                            }
                            else
                            {
                                professoresNomes.Add($"{profId} - Desconhecido"); // Caso o professor tenha sido removido
                            }
                        }
                    }

                    // Verificação extra: Exibir um aviso no console se não houver professores
                    if (professoresNomes.Count == 0)
                    {
                        Console.WriteLine($"⚠ Disciplina '{disciplina.Nome}' (ID {disciplina.Id}) não tem professores vinculados!");
                    }

                    // Montar a string de exibição dos professores
                    string professoresTexto = professoresNomes.Count > 0 ? string.Join(", ", professoresNomes) : "Nenhum";

                    // Criar a string final para exibição
                    string infoDisciplina = $"ID: {disciplina.Id} | Nome: {disciplina.Nome} " +
                                            $"| Carga Horária: {disciplina.CargaHoraria}h/semana " +
                                            $"| Professores: {professoresTexto}";

                    // Adicionar a disciplina formatada na ListBox
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
                // ✅ Preencher carga horária (agora usando LINQ)
                cmbCargaHoraria.Items.Clear();
                cmbCargaHoraria.Items.AddRange(new[] { "2", "3", "4", "5" });

                // ✅ Preencher nomes das disciplinas usando LINQ
                cmbNomeDisciplina.Items.Clear();
                string[] disciplinas =
                {
            "Português", "Inglês", "Francês", "Espanhol", "Filosofia", "História",
            "Matemática", "Física e Química", "Biologia e Geologia", "Geometria Descritiva",
            "Economia", "Geografia", "Sociologia", "Direito",
            "Educação Visual", "Desenho", "História da Cultura e das Artes",
            "Educação Física", "Ciências do Desporto",
            "Tecnologias de Informação e Comunicação (TIC)", "Programação", "Robótica"
        };

                cmbNomeDisciplina.Items.AddRange(disciplinas);
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

            try
            {
                Disciplina disciplina = gestor.Disciplinas[lstDisciplinas.SelectedIndex];

                MessageBox.Show($"ID: {disciplina.Id}\nNome: {disciplina.Nome}\nCarga Horária: {disciplina.CargaHoraria}h",
                                "Detalhes da Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show($"Erro ao consultar disciplina: Índice fora do intervalo. Verifique a lista de disciplinas.\n{ex.Message}",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar disciplina: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                // ✅ Obter disciplina selecionada
                Disciplina disciplinaSelecionada = gestor.Disciplinas[lstDisciplinas.SelectedIndex];

                // ✅ Preencher os campos com os dados da disciplina
                txtIdDisciplina.Text = disciplinaSelecionada.Id.ToString();
                txtIdDisciplina.Enabled = false; // Bloquear edição do ID
                cmbNomeDisciplina.SelectedItem = disciplinaSelecionada.Nome;
                cmbCargaHoraria.SelectedItem = disciplinaSelecionada.CargaHoraria.ToString();

                // ✅ Preencher os professores da disciplina
                txtProfessoresDisciplina.Text = string.Join(", ", disciplinaSelecionada.ProfessoresIds);

                // ✅ Habilitar botão "Salvar Alterações"
                btnSalvarEdicaoDisciplina.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar disciplina para edição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // ✅ Obter disciplina selecionada
                Disciplina disciplinaSelecionada = gestor.Disciplinas[lstDisciplinas.SelectedIndex];

                // ✅ Garantir que o ID original seja mantido
                int idOriginal = disciplinaSelecionada.Id;

                // ✅ Validar nome da disciplina
                string novoNome = cmbNomeDisciplina.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(novoNome))
                {
                    MessageBox.Show("Erro: Selecione um nome válido para a disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Validar carga horária
                if (!int.TryParse(cmbCargaHoraria.SelectedItem?.ToString(), out int novaCargaHoraria))
                {
                    MessageBox.Show("Erro: Selecione uma carga horária válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Impedir duplicação de nomes
                if (gestor.Disciplinas.Any(d => d.Nome.Equals(novoNome, StringComparison.OrdinalIgnoreCase) && d.Id != idOriginal))
                {
                    MessageBox.Show("Erro: Já existe outra disciplina com esse nome!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Capturar IDs dos professores
                List<int> novosProfessoresIds = txtProfessoresDisciplina.Text
                    .Split(',')
                    .Select(p => p.Trim()) // Remover espaços extras
                    .Where(p => int.TryParse(p, out _)) // Verificar se é número válido
                    .Select(int.Parse) // Converter para inteiro
                    .Where(pid => gestor.Professores.Any(p => p.Id == pid)) // Verificar se o professor existe
                    .ToList();

                if (!string.IsNullOrWhiteSpace(txtProfessoresDisciplina.Text) && novosProfessoresIds.Count == 0)
                {
                    MessageBox.Show("Erro: Um ou mais IDs de professores são inválidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Atualizar disciplina no sistema
                disciplinaSelecionada.Nome = novoNome;
                disciplinaSelecionada.CargaHoraria = novaCargaHoraria;
                disciplinaSelecionada.ProfessoresIds = novosProfessoresIds;

                // ✅ Atualizar lista
                AtualizarListaDisciplinas();
                MessageBox.Show("Disciplina editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Desativar o botão após salvar
                btnSalvarEdicaoDisciplina.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar alterações da disciplina: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
