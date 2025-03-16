using Bibilioteca_Sistema_de_Gestão_Escolar;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormNota : Form
    {
        private GestorEscola gestor;
        private Dictionary<int, string> tiposAvaliacao = new Dictionary<int, string>(); // Dicionário para armazenar os tipos de avaliação

        public FormNota(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaNotas();
        }

        private void btnRemoverNota_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se o ID do aluno é um número válido
                if (!int.TryParse(txtAlunoIdNota.Text, out int alunoId))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o ID da disciplina é um número válido
                if (!int.TryParse(txtDisciplinaIdNota.Text, out int disciplinaId))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o período letivo foi preenchido corretamente
                string periodo = txtPeriodoNota.Text.Trim();
                if (string.IsNullOrEmpty(periodo))
                {
                    MessageBox.Show("Erro: O período letivo não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o período letivo já foi encerrado
                if (gestor.VerificarSePeriodoEncerrado(periodo))
                {
                    MessageBox.Show("Erro: O período letivo já foi encerrado. Não é possível remover notas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tentar remover a nota
                bool removida = gestor.RemoverNota(alunoId, disciplinaId, periodo);
                if (removida)
                {
                    MessageBox.Show("Nota removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarListaNotas();
                }
                else
                {
                    MessageBox.Show("Erro: Nota não encontrada. Verifique os dados informados!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao remover nota: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionarNota_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se o ID do aluno é válido
                if (!int.TryParse(txtAlunoIdNota.Text, out int alunoId))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o ID da disciplina é válido
                if (!int.TryParse(txtDisciplinaIdNota.Text, out int disciplinaId))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se a nota é válida
                if (!double.TryParse(txtValorNota.Text, out double valorNota))
                {
                    MessageBox.Show("Erro: O valor da nota deve ser um número válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar o tipo de avaliação selecionado no ComboBox
                string tipoAvaliacao = cmbTipoAvaliacao.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(tipoAvaliacao))
                {
                    MessageBox.Show("Erro: Selecione um tipo de avaliação!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar o período letivo
                string periodoLetivo = txtPeriodoNota.Text.Trim();
                if (string.IsNullOrEmpty(periodoLetivo))
                {
                    MessageBox.Show("Erro: O período letivo não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar o estado do ano letivo
                string estadoAno = VerificarEstadoAnoLetivo(periodoLetivo);
                if (estadoAno == "Encerrado")
                {
                    MessageBox.Show("Erro: O ano letivo já foi encerrado. Não é possível adicionar ou alterar notas.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar a nova nota
                Nota novaNota = new Nota(alunoId, disciplinaId, valorNota, periodoLetivo);

                // Adicionar a nota ao sistema
                gestor.AdicionarNota(novaNota);

                // Armazenar o tipo de avaliação no dicionário
                tiposAvaliacao[gestor.Notas.Count - 1] = tipoAvaliacao;

                // Atualizar a lista de notas na ListBox imediatamente
                AtualizarListaNotas();

                MessageBox.Show("Nota adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AtualizarListaNotas()
        {
            try
            {
                lstNotas.Items.Clear(); // Limpa a lista antes de atualizar

                for (int i = 0; i < gestor.Notas.Count; i++)
                {
                    Nota nota = gestor.Notas[i];

                    // Procurar a disciplina correspondente
                    string nomeDisciplina = "Disciplina não encontrada";
                    for (int j = 0; j < gestor.Disciplinas.Count; j++)
                    {
                        if (gestor.Disciplinas[j].Id == nota.DisciplinaId)
                        {
                            nomeDisciplina = gestor.Disciplinas[j].Nome;
                            break;
                        }
                    }

                    // Procurar o aluno correspondente
                    string nomeAluno = "Aluno não encontrado";
                    int turmaAluno = -1;
                    for (int j = 0; j < gestor.Alunos.Count; j++)
                    {
                        if (gestor.Alunos[j].Id == nota.AlunoId)
                        {
                            nomeAluno = gestor.Alunos[j].Nome;
                            turmaAluno = gestor.Alunos[j].TurmaId;
                            break;
                        }
                    }

                    // Procurar o tipo de avaliação no dicionário (ou usar "Não Informado" se não existir)
                    string tipoAvaliacao = "Não Informado";
                    if (tiposAvaliacao.ContainsKey(i))
                    {
                        tipoAvaliacao = tiposAvaliacao[i];
                    }

                    // Criar a string formatada para exibição na ListBox
                    string infoNota = "Ano Letivo: " + nota.PeriodoLetivo +
                                      " | Tipo: " + tipoAvaliacao +
                                      " | Nota: " + nota.ValorNota +
                                      " | Disciplina: " + nota.DisciplinaId + " - " + nomeDisciplina +
                                      " | Aluno: " + nota.AlunoId + " - " + nomeAluno +
                                      " | Turma: " + turmaAluno;

                    lstNotas.Items.Add(infoNota);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar a lista de notas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormNota_Load(object sender, EventArgs e)
        {
            try
            {
                // Limpar os ComboBoxes antes de preencher (evita duplicação)
                cmbProfessorNota.Items.Clear();
                cmbTipoAvaliacao.Items.Clear();

                // Adicionar professores ao ComboBox
                for (int i = 0; i < gestor.Professores.Count; i++)
                {
                    Professor professor = gestor.Professores[i];
                    cmbProfessorNota.Items.Add(professor.Id + " - " + professor.Nome);
                }

                // Adicionar tipos de avaliação ao ComboBox
                cmbTipoAvaliacao.Items.Add("Teste");
                cmbTipoAvaliacao.Items.Add("Trabalho");
                cmbTipoAvaliacao.Items.Add("Exame");

                // Define um valor padrão ao abrir o formulário
                cmbTipoAvaliacao.SelectedIndex = 0; // Define "Teste" como valor inicial

                // Atualizar lista de notas ao abrir o formulário
                AtualizarListaNotas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar formulário de notas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string VerificarEstadoAnoLetivo(string anoLetivo)
        {
            try
            {
                // Verificar se o formato está correto: "AAAA/AAAA"
                string[] anos = anoLetivo.Split('/');
                if (anos.Length != 2 || !int.TryParse(anos[0], out int anoInicio) || !int.TryParse(anos[1], out int anoFim))
                {
                    return "Ano letivo inválido";
                }

                // Criar datas de início e fim para o ano letivo
                DateTime dataInicio = new DateTime(anoInicio, 9, 1); // Começa em setembro do primeiro ano
                DateTime dataFim = new DateTime(anoFim, 7, 31); // Termina em julho do segundo ano

                DateTime hoje = DateTime.Today;

                // Determinar o estado do ano letivo
                if (hoje < dataInicio)
                {
                    return "Não iniciado";
                }
                else if (hoje >= dataInicio && hoje <= dataFim)
                {
                    return "Em andamento";
                }
                else
                {
                    return "Encerrado";
                }
            }
            catch
            {
                return "Erro ao processar ano letivo";
            }
        }

        private void btnEditarNota_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstNotas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione uma nota primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obter nota selecionada
                Nota notaSelecionada = gestor.Notas[lstNotas.SelectedIndex];

                // Preencher os campos com os dados da nota
                txtAlunoIdNota.Text = notaSelecionada.AlunoId.ToString();
                txtDisciplinaIdNota.Text = notaSelecionada.DisciplinaId.ToString();
                txtValorNota.Text = notaSelecionada.ValorNota.ToString();
                txtPeriodoNota.Text = notaSelecionada.PeriodoLetivo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar nota: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarNota_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstNotas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione uma nota para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter a nota selecionada
                Nota notaSelecionada = gestor.Notas[lstNotas.SelectedIndex];

                // Procurar a disciplina correspondente
                string nomeDisciplina = "Disciplina não encontrada";
                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    if (gestor.Disciplinas[i].Id == notaSelecionada.DisciplinaId)
                    {
                        nomeDisciplina = gestor.Disciplinas[i].Nome;
                        break;
                    }
                }

                // Procurar o aluno correspondente
                string nomeAluno = "Aluno não encontrado";
                int turmaAluno = -1;
                for (int i = 0; i < gestor.Alunos.Count; i++)
                {
                    if (gestor.Alunos[i].Id == notaSelecionada.AlunoId)
                    {
                        nomeAluno = gestor.Alunos[i].Nome;
                        turmaAluno = gestor.Alunos[i].TurmaId;
                        break;
                    }
                }

                // Procurar o tipo de avaliação no dicionário (ou usar "Não Informado" se não existir)
                string tipoAvaliacao = "Não Informado";
                if (tiposAvaliacao.ContainsKey(lstNotas.SelectedIndex))
                {
                    tipoAvaliacao = tiposAvaliacao[lstNotas.SelectedIndex];
                }

                // Exibir os detalhes da nota
                MessageBox.Show("Ano Letivo: " + notaSelecionada.PeriodoLetivo +
                                "\nTipo de Avaliação: " + tipoAvaliacao +
                                "\nValor da Nota: " + notaSelecionada.ValorNota +
                                "\nDisciplina: " + notaSelecionada.DisciplinaId + " - " + nomeDisciplina +
                                "\nAluno: " + notaSelecionada.AlunoId + " - " + nomeAluno +
                                "\nTurma: " + turmaAluno,
                                "Detalhes da Nota", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar nota: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
