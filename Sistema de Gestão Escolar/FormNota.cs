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
                // ----------- VALIDAR ID DO ALUNO ----------- //
                if (!int.TryParse(txtAlunoIdNota.Text, out int alunoId))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ----------- VALIDAR ID DA DISCIPLINA ----------- //
                if (!int.TryParse(txtDisciplinaIdNota.Text, out int disciplinaId))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ----------- VERIFICAR SE O ALUNO EXISTE ----------- //
                Aluno alunoEncontrado = null;
                for (int i = 0; i < gestor.Alunos.Count; i++)
                {
                    if (gestor.Alunos[i].Id == alunoId)
                    {
                        alunoEncontrado = gestor.Alunos[i];
                        break;
                    }
                }

                if (alunoEncontrado == null)
                {
                    MessageBox.Show($"Erro: O aluno com ID {alunoId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ----------- VERIFICAR SE A DISCIPLINA EXISTE ----------- //
                Disciplina disciplinaEncontrada = null;
                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    if (gestor.Disciplinas[i].Id == disciplinaId)
                    {
                        disciplinaEncontrada = gestor.Disciplinas[i];
                        break;
                    }
                }

                if (disciplinaEncontrada == null)
                {
                    MessageBox.Show($"Erro: A disciplina com ID {disciplinaId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ----------- VALIDAR VALOR DA NOTA ----------- //
                if (!double.TryParse(txtValorNota.Text, out double valorNota) || valorNota < 0 || valorNota > 20)
                {
                    MessageBox.Show("Erro: O valor da nota deve ser um número entre 0 e 20!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ----------- VALIDAR TIPO DE AVALIAÇÃO ----------- //
                string tipoAvaliacao = cmbTipoAvaliacao.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(tipoAvaliacao))
                {
                    MessageBox.Show("Erro: Selecione um tipo de avaliação!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ----------- VALIDAR PERÍODO LETIVO ----------- //
                string periodoLetivo = txtPeriodoNota.Text.Trim();
                if (string.IsNullOrEmpty(periodoLetivo))
                {
                    MessageBox.Show("Erro: O período letivo não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ----------- VERIFICAR O ESTADO DO ANO LETIVO ----------- //
                string estadoAno = VerificarEstadoAnoLetivo(periodoLetivo);
                if (estadoAno == "Encerrado")
                {
                    MessageBox.Show("Erro: O ano letivo já foi encerrado. Não é possível adicionar ou alterar notas.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ----------- CRIAR E ADICIONAR A NOTA ----------- //
                Nota novaNota = new Nota(alunoId, disciplinaId, valorNota, periodoLetivo, tipoAvaliacao);
                gestor.AdicionarNota(novaNota);

                // ----------- GARANTIR QUE O TIPO DE AVALIAÇÃO SEJA SALVO ----------- //
                if (!tiposAvaliacao.ContainsKey(gestor.Notas.Count - 1))
                {
                    tiposAvaliacao.Add(gestor.Notas.Count - 1, tipoAvaliacao);
                }
                else
                {
                    tiposAvaliacao[gestor.Notas.Count - 1] = tipoAvaliacao;
                }

                // ----------- ATUALIZAR A LISTA DE NOTAS ----------- //
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

                    // ----------- BUSCAR O NOME DA DISCIPLINA ----------- //
                    string nomeDisciplina = "Disciplina não encontrada";
                    for (int j = 0; j < gestor.Disciplinas.Count; j++)
                    {
                        if (gestor.Disciplinas[j].Id == nota.DisciplinaId)
                        {
                            nomeDisciplina = gestor.Disciplinas[j].Nome;
                            break;
                        }
                    }

                    // ----------- BUSCAR O NOME DO ALUNO E A TURMA ----------- //
                    string nomeAluno = "Aluno não encontrado";
                    string turmaInfo = "Turma não encontrada";

                    for (int j = 0; j < gestor.Alunos.Count; j++)
                    {
                        if (gestor.Alunos[j].Id == nota.AlunoId)
                        {
                            nomeAluno = gestor.Alunos[j].Nome;

                            // Buscar o curso e o ID da turma do aluno
                            for (int k = 0; k < gestor.Turmas.Count; k++)
                            {
                                if (gestor.Turmas[k].Id == gestor.Alunos[j].TurmaId)
                                {
                                    turmaInfo = gestor.Turmas[k].Id + " - " + gestor.Turmas[k].Curso;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    // ----------- VERIFICAR O TIPO DE AVALIAÇÃO ----------- //
                    string tipoAvaliacao = "Não Informado";
                    if (tiposAvaliacao.ContainsKey(i))
                    {
                        tipoAvaliacao = tiposAvaliacao[i];
                    }

                    // ----------- CRIAR A STRING FORMATADA PARA A LISTBOX ----------- //
                    string infoNota = "Ano Letivo: " + nota.PeriodoLetivo +
                                      " | Tipo: " + tipoAvaliacao +
                                      " | Nota: " + nota.ValorNota +
                                      " | Disciplina: " + nota.DisciplinaId + " - " + nomeDisciplina +
                                      " | Aluno: " + nota.AlunoId + " - " + nomeAluno +
                                      " | Turma: " + turmaInfo; // Agora mostra ID e curso da turma

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

                // ----------------- Professores -----------------
                if (gestor.Professores.Count == 0)
                {
                    cmbProfessorNota.Items.Add("Nenhum professor disponível");
                    cmbProfessorNota.SelectedIndex = 0;
                    return;

                   
                }
                else
                {
                    for (int i = 0; i < gestor.Professores.Count; i++)
                    {
                        Professor professor = gestor.Professores[i];
                        cmbProfessorNota.Items.Add(professor.Id + " - " + professor.Nome);
                    }

                    // Selecionar o primeiro professor disponível por padrão
                    cmbProfessorNota.SelectedIndex = 0;
                }

                // ----------------- Tipos de Avaliação -----------------
                cmbTipoAvaliacao.Items.Add("Teste");
                cmbTipoAvaliacao.Items.Add("Trabalho");
                cmbTipoAvaliacao.Items.Add("Exame");

                // Define "Teste" como opção inicial
                cmbTipoAvaliacao.SelectedIndex = 0;

                // ----------------- Atualizar Notas -----------------
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
                    MessageBox.Show("Erro: Selecione uma nota primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter nota selecionada
                Nota notaSelecionada = gestor.Notas[lstNotas.SelectedIndex];

                // Verificar se o aluno e a disciplina ainda existem
                bool alunoExiste = false, disciplinaExiste = false;

                for (int i = 0; i < gestor.Alunos.Count; i++)
                {
                    if (gestor.Alunos[i].Id == notaSelecionada.AlunoId)
                    {
                        alunoExiste = true;
                        break;
                    }
                }

                for (int i = 0; i < gestor.Disciplinas.Count; i++)
                {
                    if (gestor.Disciplinas[i].Id == notaSelecionada.DisciplinaId)
                    {
                        disciplinaExiste = true;
                        break;
                    }
                }

                if (!alunoExiste)
                {
                    MessageBox.Show("Erro: O aluno associado a essa nota foi removido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!disciplinaExiste)
                {
                    MessageBox.Show("Erro: A disciplina associada a essa nota foi removida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Preencher os campos com os dados da nota selecionada
                txtAlunoIdNota.Text = notaSelecionada.AlunoId.ToString();
                txtDisciplinaIdNota.Text = notaSelecionada.DisciplinaId.ToString();
                txtValorNota.Text = notaSelecionada.ValorNota.ToString();
                txtPeriodoNota.Text = notaSelecionada.PeriodoLetivo;
                cmbTipoAvaliacao.SelectedItem = notaSelecionada.TipoAvaliacao; // Define o tipo de avaliação

                MessageBox.Show("Nota carregada para edição.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar nota para edição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSalvarEdicaoNota_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstNotas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Nenhuma nota selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter nota selecionada
                Nota notaSelecionada = gestor.Notas[lstNotas.SelectedIndex];

                // Validar novo ID do aluno
                if (!int.TryParse(txtAlunoIdNota.Text, out int novoAlunoId))
                {
                    MessageBox.Show("Erro: O ID do aluno deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar novo ID da disciplina
                if (!int.TryParse(txtDisciplinaIdNota.Text, out int novaDisciplinaId))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar novo valor da nota
                if (!double.TryParse(txtValorNota.Text, out double novoValorNota) || novoValorNota < 0 || novoValorNota > 20)
                {
                    MessageBox.Show("Erro: A nota deve ser um número entre 0 e 20!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar o novo tipo de avaliação
                string novoTipoAvaliacao = cmbTipoAvaliacao.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(novoTipoAvaliacao))
                {
                    MessageBox.Show("Erro: Selecione um tipo de avaliação!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Aplicar alterações na nota
                notaSelecionada.AlunoId = novoAlunoId;
                notaSelecionada.DisciplinaId = novaDisciplinaId;
                notaSelecionada.ValorNota = novoValorNota;
                notaSelecionada.TipoAvaliacao = novoTipoAvaliacao;

                // Atualizar a lista de notas
                AtualizarListaNotas();

                MessageBox.Show("Nota editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar edição da nota: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
