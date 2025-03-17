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

                // Verificar se o contato foi preenchido corretamente
                string contato = txtContatoAluno.Text.Trim().Replace(" ", ""); // Remover espaços extras
                if (!ValidarContato(contato))
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


                // Verificar se o email foi preenchido
                // Capturar e validar o email
                string email = txtEmailAluno.Text.Trim();
                if (!ValidarEmail(email))
                {
                    MessageBox.Show("Erro: O e-mail digitado não é válido! Exemplo: exemplo@email.com", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (gestor.Alunos.Count == 0)
                {
                    lstAlunos.Items.Add("Nenhum aluno cadastrado.");
                    return;
                }

                for (int i = 0; i < gestor.Alunos.Count; i++)
                {
                    Aluno aluno = gestor.Alunos[i];

                    // Procurar o nome da turma correspondente
                    string nomeTurma = "Turma não encontrada";

                    for (int j = 0; j < gestor.Turmas.Count; j++)
                    {
                        if (gestor.Turmas[j].Id == aluno.TurmaId)
                        {
                            nomeTurma = gestor.Turmas[j].Id + " - " + gestor.Turmas[j].Curso;
                            break;
                        }
                    }

                    // Criar a string formatada para exibição
                    string infoAluno = aluno.Id + " - " + aluno.Nome + " | Turma: " + nomeTurma;

                    // Adicionar o aluno à ListBox
                    lstAlunos.Items.Add(infoAluno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar a lista de alunos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool ValidarContato(string contato)
        {
            try
            {
                contato = contato.Trim(); // Remover espaços

                // Verificar se o contato tem exatamente 9 dígitos
                if (contato.Length != 9)
                {
                    return false;
                }

                // Verificar se todos os caracteres são números
                for (int i = 0; i < contato.Length; i++)
                {
                    if (!char.IsDigit(contato[i]))
                    {
                        return false;
                    }
                }

                // Verificar se começa com '9' (telemóveis) ou '2' (fixos)
                if (contato[0] != '9' && contato[0] != '2')
                {
                    return false;
                }

                return true; // Contato válido
            }
            catch (Exception)
            {
                return false; // Em caso de erro, retorna falso sem quebrar o sistema
            }
        }

        private void lblContatoAluno_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvarAlteracoesAluno_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAlunos.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um aluno para salvar as alterações!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter aluno selecionado
                Aluno alunoSelecionado = gestor.Alunos[lstAlunos.SelectedIndex];

                // Validar ID
                if (!int.TryParse(txtIdAluno.Text, out int novoId))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar nome
                string novoNome = txtNomeAluno.Text.Trim();
                if (string.IsNullOrEmpty(novoNome))
                {
                    MessageBox.Show("Erro: O nome do aluno não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar data de nascimento
                DateTime novaDataNascimento = dtpNascimentoAluno.Value;

                // Validar contato
                string novoContato = txtContatoAluno.Text.Trim();
                if (novoContato.Length != 9 || (novoContato[0] != '9' && novoContato[0] != '2'))
                {
                    MessageBox.Show("Erro: O contato deve ter 9 dígitos e começar com '9' ou '2'.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar morada
                string novaMorada = txtMoradaAluno.Text.Trim();
                if (string.IsNullOrEmpty(novaMorada))
                {
                    MessageBox.Show("Erro: A morada do aluno não pode estar vazia.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar email
                string novoEmail = txtEmailAluno.Text.Trim();
                if (!ValidarEmail(novoEmail))
                {
                    MessageBox.Show("Erro: O e-mail inserido não é válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar ID da turma
                if (!int.TryParse(txtTurmaAluno.Text, out int novaTurmaId))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se a turma existe
                bool turmaExiste = false;
                for (int i = 0; i < gestor.Turmas.Count; i++)
                {
                    if (gestor.Turmas[i].Id == novaTurmaId)
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

                // Aplicar as alterações ao aluno selecionado
                alunoSelecionado.Id = novoId;
                alunoSelecionado.Nome = novoNome;
                alunoSelecionado.DataNascimento = novaDataNascimento;
                alunoSelecionado.Contato = novoContato;
                alunoSelecionado.Morada = novaMorada;
                alunoSelecionado.Email = novoEmail;
                alunoSelecionado.TurmaId = novaTurmaId;

                // Atualizar a lista de alunos
                AtualizarListaAlunos();

                // Limpar a listBox de edição
                lstEdicaoAluno.Items.Clear();

                MessageBox.Show("Aluno atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar alterações: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
