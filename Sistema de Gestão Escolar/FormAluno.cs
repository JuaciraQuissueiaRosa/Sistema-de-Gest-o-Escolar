using Bibilioteca_Sistema_de_Gestão_Escolar;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormAluno : Form
    {
        private GestorEscola gestor;

        public FormAluno(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaAlunos();
        }
        private void btnAdicionarAluno_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Verificar ID válido e único
                if (!int.TryParse(txtIdAluno.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gestor.Alunos.Any(a => a.Id == id))
                {
                    MessageBox.Show("Erro: Já existe um aluno com esse ID!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Verificar preenchimento dos campos
                string nome = txtNomeAluno.Text.Trim();
                if (string.IsNullOrEmpty(nome))
                {
                    MessageBox.Show("Erro: O nome do aluno não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!DateTime.TryParse(dtpNascimentoAluno.Text, out DateTime dataNascimento))
                {
                    MessageBox.Show("Erro: Insira uma data de nascimento válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 📌 ✅ Verificação da idade mínima (12 anos)
                int idade = CalcularIdade(dataNascimento);
                if (idade < 12)
                {
                    MessageBox.Show("Erro: O aluno deve ter pelo menos 12 anos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string contato = txtContatoAluno.Text.Trim();
                if (!ValidarContato(contato))
                {
                    MessageBox.Show("Erro: O contato deve ter 9 dígitos e começar com '9' ou '2'!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string email = txtEmailAluno.Text.Trim();
                if (!ValidarEmail(email))
                {
                    MessageBox.Show("Erro: O e-mail não é válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Verificar se a turma existe
                if (!int.TryParse(txtTurmaAluno.Text, out int turmaId) || !gestor.Turmas.Any(t => t.Id == turmaId))
                {
                    MessageBox.Show("Erro: A turma informada não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Criar e adicionar aluno
                Aluno novoAluno = new Aluno(id, nome, dataNascimento, contato, txtMoradaAluno.Text.Trim(), email, turmaId);
                gestor.AdicionarAluno(novoAluno);

                gestor.SalvarDados();

                // ✅ Atualizar lista e bloquear edição do ID
                txtIdAluno.Enabled = false;
                AtualizarListaAlunos();

                MessageBox.Show("Aluno adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRemoverAluno_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtIdAluno.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool alunoRemovido = gestor.RemoverAluno(id);

                string mensagem = alunoRemovido
                    ? "Aluno removido com sucesso!"
                    : "Erro: O aluno não pode ser removido porque possui notas registradas!";

                MessageBox.Show(mensagem, "Remover Aluno", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (alunoRemovido)
                {
                    // ✅ Salvar os dados após remover aluno
                    gestor.SalvarDados();
                }

                AtualizarListaAlunos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao remover aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarAluno_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAlunos.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione um aluno para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var alunoSelecionado = gestor.Alunos.FirstOrDefault(a => a.Id == (int)lstAlunos.SelectedItems[0].Tag);

                var nomeTurma = gestor.Turmas.FirstOrDefault(t => t.Id == alunoSelecionado.TurmaId)?.Curso ?? "Turma não encontrada";

                var historicoNotas = gestor.Notas
                    .Where(n => n.AlunoId == alunoSelecionado.Id)
                    .Select(n =>
                    {
                        var nomeDisciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == n.DisciplinaId)?.Nome ?? "Disciplina não encontrada";
                        return $"ID: {n.DisciplinaId} | {nomeDisciplina}: {n.ValorNota} ({n.PeriodoLetivo})";
                    })
                    .DefaultIfEmpty("Sem notas registradas")
                    .Aggregate((atual, proximo) => $"{atual}\n{proximo}");

                MessageBox.Show($"ID: {alunoSelecionado.Id}\nNome: {alunoSelecionado.Nome}\nTurma: {alunoSelecionado.TurmaId} - {nomeTurma}\n\n📚 Histórico Escolar:\n{historicoNotas}",
                                "Consulta de Aluno", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMudarTurma_Click(object sender, EventArgs e)
        {

            try
            {
                // Verificar se há um aluno selecionado
                if (lstAlunos.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione um aluno primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Recuperar o item selecionado
                var itemSelecionado = lstAlunos.SelectedItems[0];

                // Verificar se o Tag do item selecionado não é nulo
                if (itemSelecionado.Tag == null)
                {
                    MessageBox.Show("Erro: O aluno selecionado não possui um ID válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Recuperar o ID do aluno a partir do Tag do item selecionado
                int alunoId = (int)itemSelecionado.Tag;

                // Buscar o aluno correspondente
                Aluno alunoSelecionado = gestor.Alunos.FirstOrDefault(a => a.Id == alunoId);

                // Verificar se o aluno foi encontrado
                if (alunoSelecionado == null)
                {
                    MessageBox.Show("Erro: Aluno não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se foi selecionada uma nova turma
                if (cmbNovaTurmaAluno.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma nova turma!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tentar extrair o ID da nova turma
                int novoTurmaId;
                if (!int.TryParse(cmbNovaTurmaAluno.SelectedItem.ToString().Split('-')[0].Trim(), out novoTurmaId))
                {
                    MessageBox.Show("Erro: ID da nova turma inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se a turma selecionada existe
                var turmaSelecionada = gestor.Turmas.FirstOrDefault(t => t.Id == novoTurmaId);
                if (turmaSelecionada == null)
                {
                    MessageBox.Show("Erro: A turma selecionada não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o aluno já está na turma selecionada
                if (alunoSelecionado.TurmaId == novoTurmaId)
                {
                    MessageBox.Show("Erro: O aluno já está nesta turma!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Atualizar a turma do aluno
                alunoSelecionado.TurmaId = novoTurmaId;

                // Salvar as mudanças
                gestor.SalvarDados();

                // Atualizar a lista de alunos na interface
                AtualizarListaAlunos();

                // Exibir mensagem de sucesso
                MessageBox.Show("Aluno transferido para a nova turma com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarListaAlunos()
        {
            try
            {
                // Limpar a ListView antes de atualizar
                lstAlunos.Items.Clear();

                // Verificar se há alunos cadastrados
                if (!gestor.Alunos.Any())
                {
                    var item = new ListViewItem("Nenhum aluno cadastrado.");
                    lstAlunos.Items.Add(item);
                    return;
                }

                // Adicionar dados na ListView
                foreach (var aluno in gestor.Alunos)
                {
                    var nomeTurma = gestor.Turmas.FirstOrDefault(t => t.Id == aluno.TurmaId)?.Curso ?? "Turma não encontrada";
                    var historicoNotas = gestor.Notas
                        .Where(n => n.AlunoId == aluno.Id)
                        .Select(n =>
                        {
                            var nomeDisciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == n.DisciplinaId)?.Nome ?? "Disciplina não encontrada";
                            return $"ID: {n.DisciplinaId} | {nomeDisciplina}: {n.ValorNota} ({n.PeriodoLetivo})";
                        })
                        .DefaultIfEmpty("Sem notas registradas")
                        .Aggregate((atual, proximo) => $"{atual} | {proximo}");

                    // Criar um novo item de ListView com os dados formatados
                    var item = new ListViewItem(aluno.Id.ToString());
                    item.SubItems.Add(aluno.Nome);
                    item.SubItems.Add(aluno.DataNascimento.ToShortDateString());
                    item.SubItems.Add(aluno.Contato);
                    item.SubItems.Add(aluno.Morada);
                    item.SubItems.Add($"{aluno.TurmaId} - {nomeTurma}");
                    item.SubItems.Add(historicoNotas);
                    // Atribuir o ID do aluno ao Tag do item para que possamos recuperá-lo depois
                    item.Tag = aluno.Id;

                    // Adicionar o item à ListView
                    lstAlunos.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar a lista de alunos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarEmail(string email)
        {
            string padraoEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, padraoEmail);
        }

        private void CarregarTurmasDisponiveis(int turmaAtualId)
        {
            try
            {
                cmbNovaTurmaAluno.Items.Clear();

                var turmasDisponiveis = gestor.Turmas
                    .Where(t => t.Id != turmaAtualId) // Excluir a turma atual
                    .Select(t => $"{t.Id} - {t.Curso} ({t.AnoLetivo})")
                    .ToList();

                if (turmasDisponiveis.Any())
                    cmbNovaTurmaAluno.Items.AddRange(turmasDisponiveis.ToArray());
                else
                    cmbNovaTurmaAluno.Items.Add("Nenhuma disponível para transferência");

                // Define o índice inicial para o ComboBox
                cmbNovaTurmaAluno.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar turmas disponíveis: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     

        public void AtualizarComboBoxTurmas()
        {
            try
            {
                cmbNovaTurmaAluno.Items.Clear();

                var turmas = gestor.Turmas
                    .Select(t => $"{t.Id} - {t.Curso} ({t.AnoLetivo})")
                    .ToList();

                cmbNovaTurmaAluno.Items.AddRange(turmas.Any() ? turmas.ToArray() : new string[] { "Nenhuma disponível para transferência" });
                cmbNovaTurmaAluno.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar a lista de turmas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormAluno_Load(object sender, EventArgs e)
        {
            try
            {
                
                AtualizarComboBoxTurmas();
                AtualizarListaAlunos(); // Atualiza a lista ao abrir o formulário
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formulário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Definir botões redondos
            SetRoundButton(btnMudarTurma);
            SetRoundButton(btnEditarAluno);
            SetRoundButton(btnAdicionarAluno);
            SetRoundButton(btnRemoverAluno);
            SetRoundButton(btnBuscarAluno);
            SetRoundButton(btnSalvarAlteracoesAluno);
            SetRoundButton(btnConsultarAluno);



            // Configuração da ListView
            lstAlunos.View = View.Details; // Exibir detalhes com colunas
            lstAlunos.FullRowSelect = true; // Selecionar a linha toda
            lstAlunos.GridLines = true; // Exibir linhas de grade

            // Definir colunas
            lstAlunos.Columns.Clear();
            lstAlunos.Columns.Add("ID", 300);
            lstAlunos.Columns.Add("Nome", 300);
            lstAlunos.Columns.Add("Data de Nascimento", 300);
            lstAlunos.Columns.Add("Contato", 300);
            lstAlunos.Columns.Add("Morada", 300);
            lstAlunos.Columns.Add("Curso", 300);
            lstAlunos.Columns.Add("Histórico Escolar", 1000);





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


        private bool ValidarContato(string contato)
        {

            contato = contato.Trim();
            return contato.Length == 9 && contato.All(char.IsDigit) && (contato.StartsWith("9") || contato.StartsWith("2"));
        }

        private void lblContatoAluno_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvarAlteracoesAluno_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAlunos.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione um aluno para salvar as alterações!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter o item selecionado
                var itemSelecionado = lstAlunos.SelectedItems[0];
                int alunoId = int.Parse(itemSelecionado.Text); // O ID do aluno está na primeira coluna
                Aluno alunoSelecionado = gestor.Alunos.FirstOrDefault(a => a.Id == alunoId);

                if (alunoSelecionado == null)
                {
                    MessageBox.Show("Erro: Aluno não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Garantir que o ID original seja mantido
                int idOriginal = alunoSelecionado.Id;

                // Validar nome
                string novoNome = txtNomeAluno.Text.Trim();
                if (string.IsNullOrEmpty(novoNome))
                {
                    MessageBox.Show("Erro: O nome do aluno não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar data de nascimento
                DateTime novaDataNascimento = dtpNascimentoAluno.Value;
                int idade = CalcularIdade(novaDataNascimento);
                if (idade < 12)
                {
                    MessageBox.Show("Erro: O aluno deve ter pelo menos 12 anos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar contato
                string novoContato = txtContatoAluno.Text.Trim();
                if (novoContato.Length != 9 || (novoContato[0] != '9' && novoContato[0] != '2'))
                {
                    MessageBox.Show("Erro: O contato deve ter 9 dígitos e começar com '9' ou '2'.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar email
                string novoEmail = txtEmailAluno.Text.Trim();
                if (!ValidarEmail(novoEmail))
                {
                    MessageBox.Show("Erro: O e-mail inserido não é válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar morada
                string novaMorada = txtMoradaAluno.Text.Trim();
                if (string.IsNullOrEmpty(novaMorada))
                {
                    MessageBox.Show("Erro: A morada não pode estar vazia!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aplicar alterações
                alunoSelecionado.Nome = novoNome;
                alunoSelecionado.DataNascimento = novaDataNascimento;
                alunoSelecionado.Contato = novoContato;
                alunoSelecionado.Email = novoEmail;
                alunoSelecionado.Morada = novaMorada; // Atualizar a morada

                // ✅ Salvar as mudanças
                gestor.SalvarDados();

                // Atualizar lista
                AtualizarListaAlunos();
                MessageBox.Show("Aluno atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Desativar botão após salvar
                btnSalvarAlteracoesAluno.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar alterações do aluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarAluno_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAlunos.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione um aluno para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter o item selecionado
                var itemSelecionado = lstAlunos.SelectedItems[0];
                int alunoId = int.Parse(itemSelecionado.Text); // O ID do aluno está na primeira coluna
                Aluno alunoSelecionado = gestor.Alunos.FirstOrDefault(a => a.Id == alunoId);

                if (alunoSelecionado == null)
                {
                    MessageBox.Show("Erro: Aluno não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar o nome da turma correspondente
                string nomeTurma = "Turma não encontrada";
                var turma = gestor.Turmas.FirstOrDefault(t => t.Id == alunoSelecionado.TurmaId);
                if (turma != null)
                {
                    nomeTurma = $"{turma.Id} - {turma.Curso}";
                }

                // Construir o histórico de notas do aluno
                string historicoNotas = "Sem notas registradas";
                var notasLista = gestor.Notas
                    .Where(n => n.AlunoId == alunoSelecionado.Id)
                    .Select(n =>
                    {
                        var disciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == n.DisciplinaId);
                        return $"ID: {n.DisciplinaId} | {disciplina?.Nome ?? "Disciplina não encontrada"}: {n.ValorNota} ({n.PeriodoLetivo})";
                    })
                    .ToList();

                if (notasLista.Any())
                {
                    historicoNotas = string.Join("\n", notasLista);
                }

                // Exibir os detalhes do aluno
                MessageBox.Show($"ID: {alunoSelecionado.Id}\nNome: {alunoSelecionado.Nome}\nTurma: {nomeTurma}\n\n📚 Histórico Escolar:\n{historicoNotas}",
                    "Consulta de Aluno", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar aluno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarAluno_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAlunos.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione um aluno para editar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter o item selecionado
                var itemSelecionado = lstAlunos.SelectedItems[0];
                int alunoId = int.Parse(itemSelecionado.Text); // O ID do aluno está na primeira coluna
                Aluno alunoSelecionado = gestor.Alunos.FirstOrDefault(a => a.Id == alunoId);

                if (alunoSelecionado == null)
                {
                    MessageBox.Show("Erro: Aluno não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Preencher os campos
                txtIdAluno.Text = alunoSelecionado.Id.ToString();
                txtIdAluno.Enabled = false; // Bloquear edição do ID

                txtNomeAluno.Text = alunoSelecionado.Nome;
                dtpNascimentoAluno.Value = alunoSelecionado.DataNascimento;
                txtContatoAluno.Text = alunoSelecionado.Contato;
                txtMoradaAluno.Text = alunoSelecionado.Morada;
                txtEmailAluno.Text = alunoSelecionado.Email;
                txtTurmaAluno.Text = alunoSelecionado.TurmaId.ToString();

                // Ativar botão "Salvar Alterações"
                btnSalvarAlteracoesAluno.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar aluno para edição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now < dataNascimento.AddYears(idade)) idade--;
            return idade;
        }

        private void lstAlunos_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            try
            {
                if (lstAlunos.SelectedItems.Count == 0)
                {
                    return; // Se nada estiver selecionado, não faz nada
                }

                // Obter o item selecionado
                var itemSelecionado = lstAlunos.SelectedItems[0];

                // Buscar o aluno correspondente ao ID selecionado
                int alunoId = int.Parse(itemSelecionado.Text); // O ID do aluno está na primeira coluna
                Aluno alunoSelecionado = gestor.Alunos.FirstOrDefault(a => a.Id == alunoId);

                if (alunoSelecionado != null)
                {
                    // Exibir os dados do aluno nos campos de texto (se houver no formulário)
                    txtNomeAluno.Text = alunoSelecionado.Nome;
                    txtIdAluno.Text = alunoSelecionado.Id.ToString();
                    txtTurmaAluno.Text = alunoSelecionado.TurmaId.ToString(); // Mostra a turma atual

                    // Atualizar a lista de turmas disponíveis para transferência
                    CarregarTurmasDisponiveis(alunoSelecionado.TurmaId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao selecionar um aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
