using Bibilioteca_Sistema_de_Gestão_Escolar;
using System.Drawing.Drawing2D;

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
                if (!int.TryParse(txtIdDisciplina.Text.Trim(), out int id))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (gestor.Disciplinas.Any(d => d.Id == id))
                {
                    MessageBox.Show("Erro: Já existe uma disciplina com esse ID!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar nome da disciplina
                if (cmbNomeDisciplina.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nomeDisciplina = cmbNomeDisciplina.SelectedItem.ToString();

                // Verificar se o nome já existe
                if (gestor.Disciplinas.Any(d => d.Nome.Equals(nomeDisciplina, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Erro: Já existe uma disciplina com esse nome!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar carga horária
                if (!int.TryParse(cmbCargaHoraria.SelectedItem?.ToString(), out int cargaHoraria))
                {
                    MessageBox.Show("Erro: Selecione uma carga horária válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar nova disciplina
                Disciplina novaDisciplina = new Disciplina(id, nomeDisciplina, cargaHoraria);

                // Associar professores (se houver)
                if (!string.IsNullOrWhiteSpace(cmbCargaHoraria.Text))
                {
                    List<int> professoresIds = cmbCargaHoraria.Text
                        .Split(',')
                        .Select(p => p.Trim())
                        .Where(p => int.TryParse(p, out _))
                        .Select(int.Parse)
                        .ToList();

                    // Verificar se os professores podem lecionar
                    foreach (int professorId in professoresIds)
                    {
                        if (!gestor.Professores.Any(p => p.Id == professorId))
                        {
                            MessageBox.Show($"Erro: O professor com ID {professorId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string mensagemErro;
                        bool podeLecionar = gestor.PodeLecionarDisciplina(professorId, id, out mensagemErro);
                        if (!podeLecionar)
                        {
                            MessageBox.Show(mensagemErro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    novaDisciplina.ProfessoresIds = professoresIds;
                }

                // Adicionar disciplina ao sistema
                gestor.Disciplinas.Add(novaDisciplina);
                txtIdDisciplina.Enabled = false;
                gestor.SalvarDados();

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
                // Verificar se uma disciplina foi selecionada
                if (lstDisciplinas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina para remover!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter a disciplina selecionada
                ListViewItem itemSelecionado = (ListViewItem)lstDisciplinas.SelectedItems[0];
                int idDisciplina = int.Parse(itemSelecionado.SubItems[0].Text);
                Disciplina disciplinaSelecionada = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplina);

                if (disciplinaSelecionada == null)
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirmar remoção
                DialogResult confirmacao = MessageBox.Show($"Tem certeza que deseja remover a disciplina '{disciplinaSelecionada.Nome}'?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacao == DialogResult.No)
                    return;

                // Remover a disciplina
                bool removida = gestor.RemoverDisciplina(disciplinaSelecionada.Id);
                if (removida)
                {
                    MessageBox.Show("Disciplina removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gestor.SalvarDados();
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
                lstDisciplinas.Items.Clear();

                foreach (var disciplina in gestor.Disciplinas)
                {
                    List<string> professoresNomes = new List<string>();
                    foreach (var profId in disciplina.ProfessoresIds)
                    {
                        var professor = gestor.Professores.FirstOrDefault(p => p.Id == profId);
                        professoresNomes.Add(professor != null ? $"{profId} - {professor.Nome}" : $"{profId} - Desconhecido");
                    }

                    string professoresTexto = professoresNomes.Any() ? string.Join(", ", professoresNomes) : "Nenhum";

                    string infoDisciplina = $"ID: {disciplina.Id} | Nome: {disciplina.Nome} | Carga Horária: {disciplina.CargaHoraria}h/semana | Professores: {professoresTexto}";
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
                // Preencher carga horária
                cmbCargaHoraria.Items.Clear();
                cmbCargaHoraria.Items.AddRange(new[] { "2", "3", "4", "5" });

                // Preencher nomes das disciplinas
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

                // Definir botões redondos
                SetRoundButton(btnEditarDisciplina);
                SetRoundButton(btnAdicionarDisciplina);
                SetRoundButton(btnRemoverDisciplina);
                SetRoundButton(btnSalvarEdicaoDisciplina);
                SetRoundButton(btnConsultarDisciplina);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formulário de disciplinas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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




        private void btnConsultarDisciplina_Click(object sender, EventArgs e)

        {
            try
            {
                // Verifica se algum item foi selecionado no ListView
                if (lstDisciplinas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Acessa o item selecionado no ListView e faz a conversão explícita
                ListViewItem itemSelecionado = (ListViewItem)lstDisciplinas.SelectedItems[0];

                // Obtém o ID da disciplina a partir da primeira coluna do ListView
                int idDisciplina = int.Parse(itemSelecionado.SubItems[0].Text); // Supondo que o ID esteja na primeira coluna

                // Recupera a disciplina a partir do gestor de disciplinas usando o ID
                var disciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplina);

                // Exibe os detalhes da disciplina se encontrada
                if (disciplina != null)
                {
                    MessageBox.Show($"ID: {disciplina.Id}\nNome: {disciplina.Nome}\nCarga Horária: {disciplina.CargaHoraria}h",
                                    "Detalhes da Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                // Verifica se algum item foi selecionado no ListView
                if (lstDisciplinas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Acessa o item selecionado no ListView e faz a conversão explícita
                ListViewItem itemSelecionado = (ListViewItem)lstDisciplinas.SelectedItems[0];

                // Obtém o ID da disciplina a partir da primeira coluna do ListView
                int idDisciplina = int.Parse(itemSelecionado.SubItems[0].Text); // Supondo que o ID esteja na primeira coluna

                // Recupera a disciplina a partir do gestor de disciplinas usando o ID
                var disciplinaSelecionada = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplina);

                // Exibe os dados da disciplina para edição se encontrada
                if (disciplinaSelecionada != null)
                {
                    txtIdDisciplina.Text = disciplinaSelecionada.Id.ToString();
                    txtIdDisciplina.Enabled = false; // Bloquear edição do ID
                    cmbNomeDisciplina.SelectedItem = disciplinaSelecionada.Nome;
                    cmbCargaHoraria.SelectedItem = disciplinaSelecionada.CargaHoraria.ToString();

                    // Preencher os professores da disciplina
                    cmbCargaHoraria.Text = string.Join(", ", disciplinaSelecionada.ProfessoresIds);

                    // Habilitar botão "Salvar Alterações"
                    btnSalvarEdicaoDisciplina.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                // Verificar se a disciplina foi selecionada corretamente
                if (string.IsNullOrEmpty(txtIdDisciplina.Text))
                {
                    MessageBox.Show("Erro: Nenhuma disciplina selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obter o ID da disciplina
                int idDisciplina = int.Parse(txtIdDisciplina.Text.Trim());
                var disciplinaSelecionada = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplina);

                if (disciplinaSelecionada == null)
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar nome da disciplina
                if (cmbNomeDisciplina.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione um nome de disciplina válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar carga horária
                if (!int.TryParse(cmbCargaHoraria.SelectedItem?.ToString(), out int cargaHoraria))
                {
                    MessageBox.Show("Erro: Selecione uma carga horária válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualizar dados da disciplina
                disciplinaSelecionada.Nome = cmbNomeDisciplina.SelectedItem.ToString();
                disciplinaSelecionada.CargaHoraria = cargaHoraria;

                // Atualizar a lista de professores associados
                if (!string.IsNullOrWhiteSpace(cmbCargaHoraria.Text))
                {
                    List<int> professoresIds = cmbCargaHoraria.Text
                        .Split(',')
                        .Select(p => p.Trim())
                        .Where(p => int.TryParse(p, out _))
                        .Select(int.Parse)
                        .ToList();

                    // Verificar se os professores podem lecionar
                    foreach (int professorId in professoresIds)
                    {
                        if (!gestor.Professores.Any(p => p.Id == professorId))
                        {
                            MessageBox.Show($"Erro: O professor com ID {professorId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string mensagemErro;
                        bool podeLecionar = gestor.PodeLecionarDisciplina(professorId, idDisciplina, out mensagemErro);
                        if (!podeLecionar)
                        {
                            MessageBox.Show(mensagemErro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    disciplinaSelecionada.ProfessoresIds = professoresIds;
                }

                // Salvar as alterações
                gestor.SalvarDados();
                AtualizarListaDisciplinas();

                MessageBox.Show("Alterações salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Desabilitar o botão de salvar alteração após o processo
                btnSalvarEdicaoDisciplina.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar alteração: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbNomeDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Verificar se há uma disciplina selecionada
                if (lstDisciplinas.SelectedItems.Count == 0)
                {
                    return; // Se não houver seleção, nada a fazer
                }

                // Obter a disciplina selecionada
                ListViewItem itemSelecionado = (ListViewItem) lstDisciplinas.SelectedItems[0];
                int idDisciplina = int.Parse(itemSelecionado.SubItems[0].Text); // ID da disciplina
                var disciplinaSelecionada = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplina);

                if (disciplinaSelecionada != null)
                {
                    // Preencher os campos de edição com as informações da disciplina
                    txtIdDisciplina.Text = disciplinaSelecionada.Id.ToString();
                    cmbNomeDisciplina.SelectedItem = disciplinaSelecionada.Nome;
                    cmbCargaHoraria.SelectedItem = disciplinaSelecionada.CargaHoraria.ToString();

                    // Preencher lista de professores associados
                    cmbCargaHoraria.Text = string.Join(", ", disciplinaSelecionada.ProfessoresIds);

                    // Habilitar o botão de salvar alteração
                    btnSalvarEdicaoDisciplina.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar disciplina: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
