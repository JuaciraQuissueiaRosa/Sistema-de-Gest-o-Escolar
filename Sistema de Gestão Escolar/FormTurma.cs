using Bibilioteca_Sistema_de_Gestão_Escolar;
using System.Drawing.Drawing2D;

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
                if (!int.TryParse(txtIdTurma.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string mensagem = gestor.RemoverTurma(id)
                    ? "Turma removida com sucesso!"
                    : "Erro: A turma não pode ser removida. Verifique se há alunos matriculados ou se o ID é válido!";

                MessageBox.Show(mensagem, "Remover Turma", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Salvar os dados após remover turma
                gestor.SalvarDados();
                AtualizarListaTurmas();
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
                if (!int.TryParse(txtIdTurma.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gestor.Turmas.Any(t => t.Id == id))
                {
                    MessageBox.Show("Erro: Já existe uma turma com esse ID!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string curso = cmbCursoTurma.SelectedItem?.ToString();
                string turno = cmbTurnoTurma.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(curso) || string.IsNullOrEmpty(turno))
                {
                    MessageBox.Show("Erro: Preencha todos os campos corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!gestor.ValidarAnoLetivo(txtAnoLetivoTurma.Text.Trim()))
                {
                    MessageBox.Show("Erro: O ano letivo deve estar no formato 'AAAA/AAAA'!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var disciplinasSelecionadas = lstDisciplinasTurma.SelectedItems
                    .Cast<string>()
                    .Select(item => int.TryParse(item.Split(' ')[0], out int disciplinaId) ? disciplinaId : (int?)null)
                    .Where(idDisciplina => idDisciplina.HasValue)
                    .Select(idDisciplina => idDisciplina.Value)
                    .ToList();
                if (!disciplinasSelecionadas.Any())
                {
                    MessageBox.Show("Erro: Selecione pelo menos uma disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gestor.Turmas.Add(new Turma(id, curso, txtAnoLetivoTurma.Text.Trim(), turno)
                {
                    DisciplinasIds = disciplinasSelecionadas
                });

                txtIdTurma.Enabled = false;


                // ✅ Salvar os dados após adicionar turma
                gestor.SalvarDados();
                AtualizarListaTurmas();
                MessageBox.Show("Turma adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao adicionar turma: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void AtualizarListaTurmas()
        {
            try
            {
                lstTurmas.Items.Clear();

                if (!gestor.Turmas.Any())
                {
                    lstTurmas.Items.Add("Nenhuma turma cadastrada.");
                    return;
                }

                lstTurmas.Items.AddRange(gestor.Turmas.Select(t =>
                {
                    var disciplinasTexto = gestor.Disciplinas
                        .Where(d => t.DisciplinasIds.Contains(d.Id))
                        .Select(d => d.Nome)
                        .DefaultIfEmpty("Nenhuma")
                        .Aggregate((atual, proximo) => $"{atual}, {proximo}");

                    var professoresTexto = gestor.Disciplinas
                        .Where(d => t.DisciplinasIds.Contains(d.Id))
                        .SelectMany(d => d.ProfessoresIds)
                        .Distinct()
                        .Select(idProf => gestor.Professores.FirstOrDefault(p => p.Id == idProf)?.Nome ?? "Desconhecido")
                        .DefaultIfEmpty("Nenhum")
                        .Aggregate((atual, proximo) => $"{atual}, {proximo}");

                    return $"ID: {t.Id} | Curso: {t.Curso} | Ano Letivo: {t.AnoLetivo} | Turno: {t.Turno} | " +
                           $"Disciplinas: {disciplinasTexto} | Professores: {professoresTexto}";

                }).ToArray());
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
                cmbTurnoTurma.Items.AddRange(new[] { "Diurno", "Noturno" });

                cmbCursoTurma.Items.AddRange(new[]
                {
            "Ciências e Tecnologias", "Línguas e Humanidades", "Ciências Socioeconómicas",
            "Artes Visuais", "Técnico de Informática e Gestão", "Técnico de Eletrónica, Automação e Comando",
            "Técnico de Turismo", "Técnico de Cozinha e Pastelaria", "Técnico de Restaurante e Bar",
            "Técnico de Mecatrónica"
        });

                CarregarDisciplinasDisponiveis();
                AtualizarListaTurmas();
                CarregarProfessoresDisponiveis();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formulário de turmas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            // Definir botões redondos

            SetRoundButton(btnEditarTurma);
            SetRoundButton(btnAdicionarTurma);
            SetRoundButton(btnRemoverTurma);
            SetRoundButton(btnSalvarEdicaoTurma);
            SetRoundButton(btnConsultarTurma);


        }

        private void SetRoundButton(Button button)
        {
            // Cria um caminho gráfico para o botão
            GraphicsPath path = new GraphicsPath();

            // Define um retângulo arredondado para o botão
            path.AddEllipse(0, 0, button.Width, button.Height);

            // Atribui a região do botão para o caminho arredondado
            button.Region = new Region(path);

            // Opcional: Define a cor de fundo e borda
            button.BackColor = Color.LightBlue;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
        }
    

        private void AtualizarFormAluno()
        {
            try
            {
                var formAluno = Application.OpenForms.OfType<FormAluno>().FirstOrDefault();
                formAluno?.AtualizarComboBoxTurmas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar formulário de alunos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Preencher os campos
                txtIdTurma.Text = turmaSelecionada.Id.ToString();
                txtIdTurma.Enabled = false; // Bloquear edição do ID
                txtAnoLetivoTurma.Text = turmaSelecionada.AnoLetivo;
                cmbTurnoTurma.SelectedItem = turmaSelecionada.Turno;
                cmbCursoTurma.SelectedItem = turmaSelecionada.Curso;

                // Preencher ListBox de Disciplinas
                lstDisciplinasTurma.Items.Clear();
                foreach (Disciplina disciplina in gestor.Disciplinas)
                {
                    string item = $"{disciplina.Id} - {disciplina.Nome}";
                    lstDisciplinasTurma.Items.Add(item);

                    // Marcar as disciplinas associadas à turma
                    if (turmaSelecionada.DisciplinasIds.Contains(disciplina.Id))
                    {
                        lstDisciplinasTurma.SetSelected(lstDisciplinasTurma.Items.Count - 1, true);
                    }
                }

                // Preencher ComboBox de Professores
                cmbProfessorTurma.Items.Clear();
                foreach (Professor professor in gestor.Professores)
                {
                    string item = $"{professor.Id} - {professor.Nome}";
                    cmbProfessorTurma.Items.Add(item);

                    // Selecionar o primeiro professor encontrado para uma disciplina da turma
                    foreach (int disciplinaId in turmaSelecionada.DisciplinasIds)
                    {
                        Disciplina disciplinaEncontrada = gestor.Disciplinas.Find(d => d.Id == disciplinaId);
                        if (disciplinaEncontrada != null && disciplinaEncontrada.ProfessoresIds.Contains(professor.Id))
                        {
                            cmbProfessorTurma.SelectedItem = item;
                            break;
                        }
                    }
                }

                // Habilitar botão de salvar edição
                btnSalvarEdicaoTurma.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar turma para edição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarTurma_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTurmas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione uma turma para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var turma = gestor.Turmas[lstTurmas.SelectedIndex];

                MessageBox.Show($"ID: {turma.Id}\nCurso: {turma.Curso}\nAno Letivo: {turma.AnoLetivo}\nTurno: {turma.Turno}",
                                "Detalhes da Turma", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar turma: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarProfessoresDisponiveis()
        {
            try
            {
                cmbProfessorTurma.Items.Clear();

                var professores = gestor.Professores
                    .Select(p => $"{p.Id} - {p.Nome}")
                    .ToList();

                cmbProfessorTurma.Items.AddRange(professores.Any() ? professores.ToArray() : new[] { "Nenhum professor disponível" });
                cmbProfessorTurma.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar professores: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstTurmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTurmas.SelectedIndex == -1) return;

            try
            {
                Turma turmaSelecionada = gestor.Turmas[lstTurmas.SelectedIndex];

                txtIdTurma.Text = turmaSelecionada.Id.ToString();
                txtAnoLetivoTurma.Text = turmaSelecionada.AnoLetivo;
                cmbTurnoTurma.SelectedItem = turmaSelecionada.Turno;
                cmbCursoTurma.SelectedItem = turmaSelecionada.Curso;

                MessageBox.Show("Turma carregada para edição.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar turma para edição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var turmaSelecionada = gestor.Turmas[lstTurmas.SelectedIndex];

                // Garantir que os campos obrigatórios estão preenchidos
                string novoCurso = cmbCursoTurma.SelectedItem?.ToString();
                string novoAnoLetivo = txtAnoLetivoTurma.Text.Trim();
                string novoTurno = cmbTurnoTurma.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(novoCurso) || string.IsNullOrEmpty(novoAnoLetivo) || string.IsNullOrEmpty(novoTurno))
                {
                    MessageBox.Show("Erro: Preencha todos os campos corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar formato do ano letivo
                if (!gestor.ValidarAnoLetivo(novoAnoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo deve estar no formato 'AAAA/AAAA'!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualizar os dados da turma
                turmaSelecionada.Curso = novoCurso;
                turmaSelecionada.AnoLetivo = novoAnoLetivo;
                turmaSelecionada.Turno = novoTurno;

                // Atualizar disciplinas associadas à turma
                turmaSelecionada.DisciplinasIds = lstDisciplinasTurma.SelectedItems
                    .Cast<string>()
                    .Select(item => int.Parse(item.Split(' ')[0]))
                    .ToList();

                // Atualizar professores associados às disciplinas da turma
                int professorId = cmbProfessorTurma.SelectedItem != null
                    ? int.Parse(cmbProfessorTurma.SelectedItem.ToString().Split(' ')[0])
                    : -1;

                gestor.Disciplinas
                    .Where(d => turmaSelecionada.DisciplinasIds.Contains(d.Id))
                    .ToList()
                    .ForEach(d => d.ProfessoresIds = professorId != -1 ? new List<int> { professorId } : new List<int>());

                AtualizarListaTurmas();
                MessageBox.Show("Turma editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnSalvarEdicaoTurma.Enabled = false;


                // ✅ Salvar os dados após editar turma
                gestor.SalvarDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar alterações da turma: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
