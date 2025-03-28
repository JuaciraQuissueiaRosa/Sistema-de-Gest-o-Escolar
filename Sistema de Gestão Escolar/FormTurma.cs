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
                string anoLetivo = txtAnoLetivoTurma.Text.Trim();

                if (string.IsNullOrEmpty(curso) || string.IsNullOrEmpty(turno) || string.IsNullOrEmpty(anoLetivo))
                {
                    MessageBox.Show("Erro: Preencha todos os campos corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!gestor.ValidarAnoLetivo(anoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo deve estar no formato 'AAAA/AAAA'!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Capturar disciplinas selecionadas
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

                // ✅ Capturar professores selecionados
                var professoresSelecionados = lstProfessoresTurma.SelectedItems
                    .Cast<string>()
                    .Select(item => int.Parse(item.Split(' ')[0]))
                    .ToList();

                if (!professoresSelecionados.Any())
                {
                    MessageBox.Show("Erro: Selecione pelo menos um professor!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Criar e adicionar turma
                gestor.Turmas.Add(new Turma(id, curso, anoLetivo, turno)
                {
                    DisciplinasIds = disciplinasSelecionadas
                });

                // ✅ Associar professores às disciplinas
                foreach (var disciplinaId in disciplinasSelecionadas)
                {
                    Disciplina disciplina = gestor.Disciplinas.Find(d => d.Id == disciplinaId);
                    if (disciplina != null)
                    {
                        disciplina.ProfessoresIds = professoresSelecionados;
                    }
                }

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
                    lstTurmas.Items.Add(new ListViewItem("Nenhuma turma cadastrada."));
                    return;
                }

                foreach (var turma in gestor.Turmas)
                {
                    // Cria um novo item para a ListView
                    var item = new ListViewItem(turma.Id.ToString());

                    // Adiciona os subitens para Curso, Ano Letivo, Turno, Disciplinas e Professores
                    item.SubItems.Add(turma.Curso);
                    item.SubItems.Add(turma.AnoLetivo);
                    item.SubItems.Add(turma.Turno);

                    // Disciplinas
                    var disciplinasTexto = gestor.Disciplinas
                        .Where(d => turma.DisciplinasIds.Contains(d.Id))
                        .Select(d => d.Nome)
                        .DefaultIfEmpty("Nenhuma")
                        .Aggregate((atual, proximo) => $"{atual}, {proximo}");
                    item.SubItems.Add(disciplinasTexto);

                    // Professores
                    var professoresTexto = gestor.Disciplinas
                        .Where(d => turma.DisciplinasIds.Contains(d.Id))
                        .SelectMany(d => d.ProfessoresIds)
                        .Distinct()
                        .Select(idProf => gestor.Professores.FirstOrDefault(p => p.Id == idProf)?.Nome ?? "Desconhecido")
                        .DefaultIfEmpty("Nenhum")
                        .Aggregate((atual, proximo) => $"{atual}, {proximo}");
                    item.SubItems.Add(professoresTexto);

                    // Adiciona o item à ListView
                    lstTurmas.Items.Add(item);
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

            CarregarProfessoresNaListBox();
            // Limpar as colunas existentes, se houver
            lstTurmas.Columns.Clear();

            // Adicionando as colunas
            lstTurmas.Columns.Add("ID", 50);               // Coluna ID
            lstTurmas.Columns.Add("Curso", 250);            // Coluna Curso
            lstTurmas.Columns.Add("Ano Letivo", 300);      // Coluna Ano Letivo
            lstTurmas.Columns.Add("Turno", 300);
            lstTurmas.Columns.Add("Disciplinas", 300);
            lstTurmas.Columns.Add("Professores", 300);
     

            // Configurar o modo de exibição do ListView para exibir em detalhes
            lstTurmas.View = View.Details;
            lstTurmas.FullRowSelect = true;// Seleção da linha inteira
            lstTurmas.GridLines = true;

        }


        private void CarregarProfessoresNaListBox()
        {
            lstProfessoresTurma.Items.Clear();
            foreach (var professor in gestor.Professores)
            {
                lstProfessoresTurma.Items.Add($"{professor.Id} - {professor.Nome}");
            }
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
                if (lstTurmas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione uma turma para editar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Obter a turma selecionada pelo ID, que é único e seguro
                ListViewItem itemSelecionado = lstTurmas.SelectedItems[0];
                int idTurma = int.Parse(itemSelecionado.SubItems[0].Text); // O ID está na primeira coluna
                Turma turmaSelecionada = gestor.Turmas.FirstOrDefault(t => t.Id == idTurma);

                if (turmaSelecionada == null)
                {
                    MessageBox.Show("Erro: Turma não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Preencher os campos com os dados da turma selecionada
                txtIdTurma.Text = turmaSelecionada.Id.ToString();
                txtIdTurma.Enabled = false;  // Não permite editar o ID da turma
                txtAnoLetivoTurma.Text = turmaSelecionada.AnoLetivo;
                cmbTurnoTurma.SelectedItem = turmaSelecionada.Turno;
                cmbCursoTurma.SelectedItem = turmaSelecionada.Curso;

                // ✅ Preencher ListBox de Disciplinas
                lstDisciplinasTurma.Items.Clear();
                foreach (Disciplina disciplina in gestor.Disciplinas)
                {
                    string item = $"{disciplina.Id} - {disciplina.Nome}";
                    lstDisciplinasTurma.Items.Add(item);
                    if (turmaSelecionada.DisciplinasIds.Contains(disciplina.Id))
                    {
                        lstDisciplinasTurma.SetSelected(lstDisciplinasTurma.Items.Count - 1, true);
                    }
                }

                // ✅ Preencher ListBox de Professores
                lstProfessoresTurma.ClearSelected();
                foreach (int professorId in gestor.Professores.Select(p => p.Id))
                {
                    if (gestor.Disciplinas.Any(d => turmaSelecionada.DisciplinasIds.Contains(d.Id) && d.ProfessoresIds.Contains(professorId)))
                    {
                        for (int i = 0; i < lstProfessoresTurma.Items.Count; i++)
                        {
                            if (lstProfessoresTurma.Items[i].ToString().StartsWith(professorId.ToString()))
                            {
                                lstProfessoresTurma.SetSelected(i, true);
                            }
                        }
                    }
                }

                // ✅ Habilitar o botão para salvar as alterações
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
                // Verifica se algum item foi selecionado no ListView
                if (lstTurmas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione uma turma para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Acessa o item selecionado no ListView
                ListViewItem itemSelecionado = lstTurmas.SelectedItems[0];

                // Obtém o ID da turma a partir da primeira coluna do ListView
                int idTurma = int.Parse(itemSelecionado.SubItems[0].Text);

                // Recupera a turma a partir do gestor de turmas usando o ID
                var turma = gestor.Turmas.FirstOrDefault(t => t.Id == idTurma);

                // Exibe os detalhes da turma se encontrada
                if (turma != null)
                {
                    MessageBox.Show($"ID: {turma.Id}\nCurso: {turma.Curso}\nAno Letivo: {turma.AnoLetivo}\nTurno: {turma.Turno}",
                                    "Detalhes da Turma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: Turma não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar turma: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


      

        private void btnSalvarEdicaoTurma_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTurmas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Nenhuma turma selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter a turma selecionada
                ListViewItem itemSelecionado = lstTurmas.SelectedItems[0];
                int idTurma = int.Parse(itemSelecionado.SubItems[0].Text); // O ID está na primeira coluna
                var turmaSelecionada = gestor.Turmas.FirstOrDefault(t => t.Id == idTurma);

                if (turmaSelecionada == null)
                {
                    MessageBox.Show("Erro: Turma não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualizar os dados da turma com os valores do formulário
                turmaSelecionada.AnoLetivo = txtAnoLetivoTurma.Text;
                turmaSelecionada.Turno = cmbTurnoTurma.SelectedItem.ToString();
                turmaSelecionada.Curso = cmbCursoTurma.SelectedItem.ToString();

                // Atualizar as disciplinas associadas à turma
                turmaSelecionada.DisciplinasIds.Clear();
                foreach (var item in lstDisciplinasTurma.SelectedItems)
                {
                    int disciplinaId = int.Parse(item.ToString().Split(' ')[0]); // Extrair o ID da disciplina
                    turmaSelecionada.DisciplinasIds.Add(disciplinaId);
                }

                // Aqui, você deve associar os professores às disciplinas de forma indireta.
                // Exemplo: se você tem uma lista de professores para cada disciplina, adicione a lógica de associação.

                // Para cada disciplina associada à turma, adicione os professores associados
                foreach (var disciplinaId in turmaSelecionada.DisciplinasIds)
                {
                    var disciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);
                    if (disciplina != null)
                    {
                        // Atualizar os professores dessa disciplina, se necessário
                        // Exemplo: Adicionar ou remover professores dessa disciplina
                        foreach (var professorId in lstProfessoresTurma.SelectedItems)
                        {
                            int idProfessor = int.Parse(professorId.ToString().Split(' ')[0]); // Extrair o ID do professor
                            if (!disciplina.ProfessoresIds.Contains(idProfessor))
                            {
                                disciplina.ProfessoresIds.Add(idProfessor);
                            }
                        }
                    }
                }

                // Salvar os dados atualizados
                gestor.SalvarDados();

                // Atualizar a ListView com os novos dados da turma
                AtualizarListaTurmas();

                // Exibir uma mensagem de sucesso
                MessageBox.Show("Turma editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSalvarEdicaoTurma.Enabled = false; // Desabilitar o botão após salvar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a edição da turma: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstTurmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTurmas.SelectedItems.Count == 0) return;

            try
            {
                // Pega o item selecionado
                var itemSelecionado = lstTurmas.SelectedItems[0];

                // Obtém o ID da turma
                int turmaId = int.Parse(itemSelecionado.Text);

                // Procura a turma pelo ID
                Turma turmaSelecionada = gestor.Turmas.FirstOrDefault(t => t.Id == turmaId);

                if (turmaSelecionada != null)
                {
                    // Atualiza os campos com as informações da turma selecionada
                    txtIdTurma.Text = turmaSelecionada.Id.ToString();
                    txtAnoLetivoTurma.Text = turmaSelecionada.AnoLetivo;
                    cmbTurnoTurma.SelectedItem = turmaSelecionada.Turno;
                    cmbCursoTurma.SelectedItem = turmaSelecionada.Curso;

                    // Marca as disciplinas e professores corretamente
                    lstDisciplinasTurma.Items.Clear();
                    foreach (Disciplina disciplina in gestor.Disciplinas)
                    {
                        string item = $"{disciplina.Id} - {disciplina.Nome}";
                        lstDisciplinasTurma.Items.Add(item);
                        if (turmaSelecionada.DisciplinasIds.Contains(disciplina.Id))
                        {
                            lstDisciplinasTurma.SetSelected(lstDisciplinasTurma.Items.Count - 1, true);
                        }
                    }

                    lstProfessoresTurma.ClearSelected();
                    foreach (int professorId in gestor.Professores.Select(p => p.Id))
                    {
                        if (gestor.Disciplinas.Any(d => turmaSelecionada.DisciplinasIds.Contains(d.Id) && d.ProfessoresIds.Contains(professorId)))
                        {
                            for (int i = 0; i < lstProfessoresTurma.Items.Count; i++)
                            {
                                if (lstProfessoresTurma.Items[i].ToString().StartsWith(professorId.ToString()))
                                {
                                    lstProfessoresTurma.SetSelected(i, true);
                                }
                            }
                        }
                    }

                    MessageBox.Show("Turma carregada para edição.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar turma para edição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
