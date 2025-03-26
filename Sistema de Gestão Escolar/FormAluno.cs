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
                if (lstAlunos.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um aluno para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var alunoSelecionado = gestor.Alunos[lstAlunos.SelectedIndex];

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
                if (lstAlunos.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um aluno primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Aluno alunoSelecionado = gestor.Alunos[lstAlunos.SelectedIndex];

                if (cmbNovaTurmaAluno.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma nova turma!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(cmbNovaTurmaAluno.SelectedItem.ToString().Split('-')[0].Trim(), out int novoTurmaId))
                {
                    MessageBox.Show("Erro: ID da nova turma inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (alunoSelecionado.TurmaId == novoTurmaId)
                {
                    MessageBox.Show("Erro: O aluno já está nessa turma!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!gestor.Turmas.Any(t => t.Id == novoTurmaId))
                {
                    MessageBox.Show("Erro: A turma selecionada não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Atualizar a turma do aluno SEM APAGAR O HISTÓRICO
                alunoSelecionado.TurmaId = novoTurmaId;

                // ✅ Salvar as mudanças
                gestor.SalvarDados();

                MessageBox.Show("Aluno transferido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarListaAlunos();
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
                lstAlunos.Items.Clear();

                if (!gestor.Alunos.Any())
                {
                    lstAlunos.Items.Add("Nenhum aluno cadastrado.");
                    return;
                }
                var alunosFormatados = gestor.Alunos.Select(a =>
                {
                    var nomeTurma = gestor.Turmas.FirstOrDefault(t => t.Id == a.TurmaId)?.Curso ?? "Turma não encontrada";
                    var historicoNotas = gestor.Notas
                        .Where(n => n.AlunoId == a.Id)
                        .Select(n =>
                        {
                            var nomeDisciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == n.DisciplinaId)?.Nome ?? "Disciplina não encontrada";
                            return $"ID: {n.DisciplinaId} | {nomeDisciplina}: {n.ValorNota} ({n.PeriodoLetivo})";
                        })
                        .DefaultIfEmpty("Sem notas registradas")
                        .Aggregate((atual, proximo) => $"{atual} | {proximo}");

                    return $"ID: {a.Id} | Nome: {a.Nome} | Nascimento: {a.DataNascimento.ToShortDateString()} | " +
                           $"Contato: {a.Contato} | Morada: {a.Morada} | Turma: {a.TurmaId} - {nomeTurma} | Histórico: {historicoNotas}";
                });

                lstAlunos.Items.AddRange(alunosFormatados.ToArray());
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
                    .Where(t => t.Id != turmaAtualId)
                    .Select(t => $"{t.Id} - {t.Curso} ({t.AnoLetivo})")
                    .ToList();

                if (turmasDisponiveis.Any())
                    cmbNovaTurmaAluno.Items.AddRange(turmasDisponiveis.ToArray());
                else
                    cmbNovaTurmaAluno.Items.Add("Nenhuma disponível para transferência");

                cmbNovaTurmaAluno.SelectedIndex = 0;
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
                if (lstAlunos.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um aluno para salvar as alterações!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter aluno selecionado
                Aluno alunoSelecionado = gestor.Alunos[lstAlunos.SelectedIndex];

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

                // Aplicar alterações
                alunoSelecionado.Nome = novoNome;
                alunoSelecionado.DataNascimento = novaDataNascimento;
                alunoSelecionado.Contato = novoContato;
                alunoSelecionado.Email = novoEmail;

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
                if (lstAlunos.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um aluno para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter o aluno selecionado
                Aluno alunoSelecionado = gestor.Alunos[lstAlunos.SelectedIndex];

                // Buscar o nome da turma correspondente
                string nomeTurma = "Turma não encontrada";
                for (int j = 0; j < gestor.Turmas.Count; j++)
                {
                    if (gestor.Turmas[j].Id == alunoSelecionado.TurmaId)
                    {
                        nomeTurma = gestor.Turmas[j].Id + " - " + gestor.Turmas[j].Curso;
                        break;
                    }
                }

                // Construir o histórico de notas do aluno manualmente
                string historicoNotas = "Sem notas registradas";
                List<string> notasLista = new List<string>();

                for (int j = 0; j < gestor.Notas.Count; j++)
                {
                    Nota nota = gestor.Notas[j];

                    if (nota.AlunoId == alunoSelecionado.Id)
                    {
                        // Buscar o nome da disciplina associada à nota
                        string nomeDisciplina = "Disciplina não encontrada";
                        for (int k = 0; k < gestor.Disciplinas.Count; k++)
                        {
                            if (gestor.Disciplinas[k].Id == nota.DisciplinaId)
                            {
                                nomeDisciplina = gestor.Disciplinas[k].Nome;
                                break;
                            }
                        }

                        // Adicionar a nota ao histórico do aluno
                        notasLista.Add("ID: " + nota.DisciplinaId + " | " + nomeDisciplina + ": " + nota.ValorNota + " (" + nota.PeriodoLetivo + ")");
                    }
                }

                if (notasLista.Count > 0)
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
                if (lstAlunos.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um aluno para editar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter aluno selecionado
                Aluno alunoSelecionado = gestor.Alunos[lstAlunos.SelectedIndex];

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
    }
}
