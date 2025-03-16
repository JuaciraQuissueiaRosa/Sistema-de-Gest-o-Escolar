using Bibilioteca_Sistema_de_Gestão_Escolar;
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
                // Verificar se o ID do aluno é válido
                if (!int.TryParse(txtIdAluno.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o nome foi preenchido
                string nome = txtNomeAluno.Text.Trim();
                if (string.IsNullOrEmpty(nome))
                {
                    MessageBox.Show("Erro: O nome do aluno não pode estar vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se a data de nascimento é válida
                if (!DateTime.TryParse(dtpNascimentoAluno.Text, out DateTime dataNascimento))
                {
                    MessageBox.Show("Erro: Insira uma data de nascimento válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o contato foi preenchido corretamente (deve ter 9 dígitos e começar com 9 ou 2)
                string contato = txtContatoAluno.Text.Trim().Replace(" ", "");
                if (contato.Length != 9 || (contato[0] != '9' && contato[0] != '2'))
                {
                    MessageBox.Show("Erro: O contato deve ter 9 dígitos e começar com '9' ou '2'.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se a morada foi preenchida
                string morada = txtMoradaAluno.Text.Trim();
                if (string.IsNullOrEmpty(morada))
                {
                    MessageBox.Show("Erro: A morada do aluno não pode estar vazia.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o email é válido
                string email = txtEmailAluno.Text.Trim();
                if (!ValidarEmail(email))
                {
                    MessageBox.Show("Erro: O email inserido não é válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o ID da turma é válido
                if (!int.TryParse(txtTurmaAluno.Text, out int turmaId))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar a turma correspondente
                Turma turmaEncontrada = null;
                for (int i = 0; i < gestor.Turmas.Count; i++)
                {
                    if (gestor.Turmas[i].Id == turmaId)
                    {
                        turmaEncontrada = gestor.Turmas[i];
                        break;
                    }
                }

                if (turmaEncontrada == null)
                {
                    MessageBox.Show("Erro: A turma selecionada não existe.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar novo aluno com todos os campos necessários
                Aluno novoAluno = new Aluno(id, nome, dataNascimento, contato, morada, email, turmaId);

                // Adicionar aluno ao sistema
                gestor.AdicionarAluno(novoAluno);

                // Atualizar a lista de alunos
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
                // Verificar se o ID do aluno é um número válido
                if (!int.TryParse(txtIdAluno.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tentar remover o aluno
                bool removido = gestor.RemoverAluno(id);
                if (removido)
                {
                    MessageBox.Show("Aluno removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: O aluno não pode ser removido. Verifique se ele possui notas registradas!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Atualizar a lista de alunos
                AtualizarListaAlunos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarAluno_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se o campo de busca não está vazio
                string termo = txtBuscarAluno.Text.Trim();
                if (string.IsNullOrEmpty(termo))
                {
                    MessageBox.Show("Erro: Digite um nome, ID ou número de turma para buscar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Buscar os alunos pelo termo digitado
                List<Aluno> resultados = gestor.BuscarAlunos(termo);

                // Limpar a lista antes de adicionar os resultados
                lstAlunos.Items.Clear();

                // Verificar se há resultados
                if (resultados.Count == 0)
                {
                    MessageBox.Show("Nenhum aluno encontrado com esse termo de busca.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Adicionar os alunos encontrados à ListBox
                foreach (var aluno in resultados)
                {
                    lstAlunos.Items.Add($"{aluno.Id} - {aluno.Nome} - Turma: {aluno.TurmaId}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao buscar aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMudarTurma_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se um aluno foi selecionado
                if (lstAlunos.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um aluno primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obter aluno selecionado
                Aluno alunoSelecionado = gestor.Alunos[lstAlunos.SelectedIndex];

                // Verificar se uma nova turma foi escolhida
                if (cmbNovaTurmaAluno.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma nova turma!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obter o ID da nova turma a partir do texto selecionado (o ID sempre vem antes do "-")
                string textoSelecionado = cmbNovaTurmaAluno.SelectedItem.ToString();
                if (!int.TryParse(textoSelecionado.Split('-')[0].Trim(), out int novoTurmaId))
                {
                    MessageBox.Show("Erro: ID da nova turma inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se a nova turma é diferente da atual
                if (alunoSelecionado.TurmaId == novoTurmaId)
                {
                    MessageBox.Show("Erro: O aluno já está nessa turma!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar se a turma existe antes de mudar o aluno
                bool turmaExiste = false;
                for (int i = 0; i < gestor.Turmas.Count; i++)
                {
                    if (gestor.Turmas[i].Id == novoTurmaId)
                    {
                        turmaExiste = true;
                        break;
                    }
                }

                if (!turmaExiste)
                {
                    MessageBox.Show("Erro: A turma selecionada não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualizar a turma do aluno
                alunoSelecionado.TurmaId = novoTurmaId;

                MessageBox.Show("Aluno transferido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Atualizar a exibição dos alunos e turmas
                AtualizarListaAlunos();
                CarregarTurmasDisponiveis(novoTurmaId);
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
                lstAlunos.Items.Clear(); // Limpa a ListBox antes de atualizar

                foreach (var aluno in gestor.ListarAlunos())
                {
                    // Procurar o nome da turma correspondente ao ID da turma do aluno
                    string nomeTurma = "Turma não encontrada";

                    foreach (var turma in gestor.Turmas)
                    {
                        if (turma.Id == aluno.TurmaId)
                        {
                            nomeTurma = $"{turma.Id} - {turma.Curso}"; // Exibir ID e Nome da Turma
                            break;
                        }
                    }

                    // Adicionar o aluno à ListBox com o nome correto da turma
                    lstAlunos.Items.Add($"{aluno.Id} - {aluno.Nome} - Turma: {nomeTurma}");
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
                cmbNovaTurmaAluno.Items.Clear(); // Limpa as opções anteriores

                for (int i = 0; i < gestor.Turmas.Count; i++)
                {
                    Turma turma = gestor.Turmas[i];

                    // Apenas adicionar turmas diferentes da turma atual do aluno
                    if (turma.Id != turmaAtualId)
                    {
                        string itemTurma = $"{turma.Id} - {turma.Curso} ({turma.AnoLetivo})";
                        cmbNovaTurmaAluno.Items.Add(itemTurma);
                    }
                }

                // Seleciona o primeiro item automaticamente (caso exista)
                if (cmbNovaTurmaAluno.Items.Count > 0)
                {
                    cmbNovaTurmaAluno.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar turmas disponíveis: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstAlunos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstAlunos.SelectedIndex == -1)
                {
                    return; // Se nada estiver selecionado, não faz nada
                }

                // Obter aluno selecionado
                Aluno alunoSelecionado = gestor.Alunos[lstAlunos.SelectedIndex];

                // Exibir os dados do aluno nos campos de texto (se houver no formulário)
                txtNomeAluno.Text = alunoSelecionado.Nome;
                txtIdAluno.Text = alunoSelecionado.Id.ToString();
                txtTurmaAluno.Text = alunoSelecionado.TurmaId.ToString(); // Mostra a turma atual

                // Atualizar a lista de turmas disponíveis para transferência
                CarregarTurmasDisponiveis(alunoSelecionado.TurmaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao selecionar um aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AtualizarComboBoxTurmas()
        {
            try
            {
                cmbNovaTurmaAluno.Items.Clear(); // Limpa as opções anteriores

                for (int i = 0; i < gestor.Turmas.Count; i++)
                {
                    Turma turma = gestor.Turmas[i];
                    string itemTurma = $"{turma.Id} - {turma.Curso} ({turma.AnoLetivo})";
                    cmbNovaTurmaAluno.Items.Add(itemTurma);
                }

                if (cmbNovaTurmaAluno.Items.Count > 0)
                {
                    cmbNovaTurmaAluno.SelectedIndex = 0;
                }
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
        }
    }
}
