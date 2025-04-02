using Bibilioteca_Sistema_de_Gestão_Escolar;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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
            ConfiguraListView();


        }
        private void ConfiguraListView()
        {
         

            // Adicionar as colunas à ListView
            lstNotas.Columns.Add("Aluno ID", 100);
            lstNotas.Columns.Add("Aluno", 200);
            lstNotas.Columns.Add("Disciplina ID", 100);
            lstNotas.Columns.Add("Disciplina", 200);
            lstNotas.Columns.Add("Tipo Avaliação", 150);
            lstNotas.Columns.Add("Nota", 100);
            lstNotas.Columns.Add("Período Letivo", 150);
            lstNotas.Columns.Add("Turma", 300);
            lstNotas.Columns.Add("Média final", 100);

            // Configurações do ListView
            lstNotas.View = View.Details; // Exibir detalhes com colunas
            lstNotas.FullRowSelect = true; // Selecionar a linha toda
            lstNotas.GridLines = true; // Exibir linhas de grade
            lstNotas.MultiSelect = false; // Para selecionar apenas um item por vez (opcional)
        }
        private void btnRemoverNota_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstNotas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione uma nota para remover!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (lstNotas.SelectedItems[0].Tag is Nota notaParaRemover) // Pegamos o objeto diretamente
                {
                    if (gestor.VerificarSePeriodoEncerrado(notaParaRemover.PeriodoLetivo))
                    {
                        MessageBox.Show("Erro: O período letivo já foi encerrado. Não é possível remover notas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Chama o método RemoverNota com os parâmetros corretos
                    bool sucessoRemocao = gestor.RemoverNota(notaParaRemover.AlunoId, notaParaRemover.DisciplinaId, notaParaRemover.PeriodoLetivo);

                    if (sucessoRemocao)
                    {
                        MessageBox.Show("Nota removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AtualizarListaNotas(); // Atualiza a lista de notas após a remoção
                        LimpaCampos();

                    }
                    else
                    {
                        MessageBox.Show("Erro: Não foi possível remover a nota. Verifique os dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Erro: Não foi possível identificar a nota selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!int.TryParse(txtAlunoIdNota.Text, out int alunoId) || !int.TryParse(txtDisciplinaIdNota.Text, out int disciplinaId))
                {
                    MessageBox.Show("Erro: Os IDs do aluno e da disciplina devem ser números inteiros!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Aluno alunoEncontrado = gestor.Alunos.FirstOrDefault(a => a.Id == alunoId);
                Disciplina disciplinaEncontrada = gestor.Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);

                if (alunoEncontrado == null)
                {
                    MessageBox.Show($"Erro: O aluno com ID {alunoId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (disciplinaEncontrada == null)
                {
                    MessageBox.Show($"Erro: A disciplina com ID {disciplinaId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Validar se um professor foi selecionado
                if (cmbProfessorNota.SelectedItem == null || cmbProfessorNota.SelectedItem.ToString().Contains("Nenhum professor disponível"))
                {
                    MessageBox.Show("Erro: Selecione um professor válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int professorId = int.Parse(cmbProfessorNota.SelectedItem.ToString().Split('-')[0].Trim());

                // ✅ Verifica se o professor selecionado leciona a disciplina
                if (!disciplinaEncontrada.ProfessoresIds.Contains(professorId))
                {
                    MessageBox.Show("Erro: O professor selecionado não leciona esta disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtValorNota.Text, out double valorNota) || valorNota < 0 || valorNota > 20)
                {
                    MessageBox.Show("Erro: O valor da nota deve ser um número entre 0 e 20!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tipoAvaliacao = cmbTipoAvaliacao.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(tipoAvaliacao))
                {
                    MessageBox.Show("Erro: Selecione um tipo de avaliação!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gestor.Notas.Any(n => n.AlunoId == alunoId && n.DisciplinaId == disciplinaId && n.TipoAvaliacao == tipoAvaliacao))
                {
                    MessageBox.Show($"Erro: Já existe uma nota para '{tipoAvaliacao}' nesta disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (gestor.Notas.Count(n => n.AlunoId == alunoId && n.DisciplinaId == disciplinaId) >= 3)
                {
                    MessageBox.Show("Erro: Apenas 3 notas (Teste, Trabalho e Exame) podem ser registradas por disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                string periodoLetivo = txtPeriodoNota.Text.Trim();
                if (string.IsNullOrEmpty(periodoLetivo) || VerificarEstadoAnoLetivo(periodoLetivo) == "Encerrado")
                {
                    MessageBox.Show("Erro: O ano letivo já foi encerrado. Não é possível adicionar ou alterar notas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Validar o formato do ano letivo
                if (!gestor.ValidarAnoLetivo(periodoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo deve estar no formato 'AAAA/AAAA'!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gestor.AdicionarNota(new Nota(alunoId, disciplinaId, valorNota, periodoLetivo, tipoAvaliacao));

                gestor.SalvarDados();
                AtualizarListaNotas();
                LimpaCampos();

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
                lstNotas.Items.Clear(); // Limpar a lista antes de atualizar

                foreach (var nota in gestor.Notas)
                {
                    string nomeDisciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == nota.DisciplinaId)?.Nome ?? "Disciplina não encontrada";
                    string nomeAluno = gestor.Alunos.FirstOrDefault(a => a.Id == nota.AlunoId)?.Nome ?? "Aluno não encontrado";
                    string turmaInfo = gestor.Turmas.FirstOrDefault(t => t.Id == gestor.Alunos.FirstOrDefault(a => a.Id == nota.AlunoId)?.TurmaId)?.Curso ?? "Turma não encontrada";
                    string tipoAvaliacao = string.IsNullOrEmpty(nota.TipoAvaliacao) ? "Não Informado" : nota.TipoAvaliacao;

                    ListViewItem item = new ListViewItem(nota.AlunoId.ToString());
                    item.SubItems.Add(nomeAluno);
                    item.SubItems.Add(nota.DisciplinaId.ToString());
                    item.SubItems.Add(nomeDisciplina);
                    item.SubItems.Add(tipoAvaliacao);
                    item.SubItems.Add(nota.ValorNota.ToString("F2"));
                    item.SubItems.Add(nota.PeriodoLetivo);
                  
                    item.SubItems.Add(turmaInfo);
                    item.SubItems.Add(nota.Media.ToString("F2"));


                    item.Tag = nota; // 🔥 Guardamos o objeto Nota diretamente!

                    lstNotas.Items.Add(item);
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
                cmbProfessorNota.Items.Clear();
                cmbTipoAvaliacao.Items.Clear();

                // ✅ Carregar professores disponíveis
                if (gestor.Professores.Any())
                {
                    cmbProfessorNota.Items.AddRange(gestor.Professores
                        .Select(p => $"{p.Id} - {p.Nome}")
                        .ToArray());
                    cmbProfessorNota.SelectedIndex = 0;
                }
                else
                {
                    cmbProfessorNota.Items.Add("Nenhum professor disponível");
                    cmbProfessorNota.SelectedIndex = 0;
                }

                // ✅ Carregar tipos de avaliação
                string[] tiposAvaliacao = { "Teste", "Trabalho", "Exame" };
                cmbTipoAvaliacao.Items.AddRange(tiposAvaliacao);
                cmbTipoAvaliacao.SelectedIndex = 0;

                // ✅ Atualizar lista de notas
                AtualizarListaNotas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formulário de notas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Definir botões redondos
            SetRoundButton(btnEditarNota);
            SetRoundButton(btnAdicionarNota);
            SetRoundButton(btnRemoverNota);
            SetRoundButton(btnSalvarEdicaoNota);
            SetRoundButton(btnConsultarNota);

            lstNotas.GridLines = true;


        }

        private void LimpaCampos()
        {
            txtAlunoIdNota.Clear();
            txtDisciplinaIdNota.Clear();
            txtValorNota.Clear();
            txtPeriodoNota.Clear();
            cmbTipoAvaliacao.SelectedIndex = -1;
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

        private string VerificarEstadoAnoLetivo(string anoLetivo)
        {
            try
            {
                var anos = anoLetivo.Split('/');
                if (anos.Length != 2 || !int.TryParse(anos[0], out int anoInicio) || !int.TryParse(anos[1], out int anoFim))
                    return "Ano letivo inválido";

                var dataInicio = new DateTime(anoInicio, 9, 1);
                var dataFim = new DateTime(anoFim, 7, 31);
                var hoje = DateTime.Today;

                return hoje < dataInicio ? "Não iniciado" : (hoje <= dataFim ? "Em andamento" : "Encerrado");
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
                if (string.IsNullOrWhiteSpace(txtAlunoIdNota.Text) || string.IsNullOrWhiteSpace(txtDisciplinaIdNota.Text) ||
                    string.IsNullOrWhiteSpace(txtValorNota.Text) || string.IsNullOrWhiteSpace(txtPeriodoNota.Text))
                {
                    MessageBox.Show("Erro: Todos os campos devem ser preenchidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtAlunoIdNota.Text, out int alunoId) || !int.TryParse(txtDisciplinaIdNota.Text, out int disciplinaId) ||
                    !double.TryParse(txtValorNota.Text, out double valorNota) || valorNota < 0 || valorNota > 20)
                {
                    MessageBox.Show("Erro: IDs e valores devem ser numéricos válidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string periodoLetivo = txtPeriodoNota.Text.Trim();
                string tipoAvaliacao = cmbTipoAvaliacao.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(tipoAvaliacao))
                {
                    MessageBox.Show("Erro: Selecione um tipo de avaliação.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Disciplina disciplinaEncontrada = gestor.Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);
                if (disciplinaEncontrada == null)
                {
                    MessageBox.Show($"Erro: A disciplina com ID {disciplinaId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int professorId = disciplinaEncontrada.ProfessoresIds.FirstOrDefault();
                if (professorId == 0)
                {
                    MessageBox.Show("Erro: Nenhum professor associado a esta disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!gestor.ProfessorPodeGerirNota(professorId, disciplinaId))
                {
                    MessageBox.Show("Erro: O professor não tem permissão para gerir notas nesta disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
              
                Nota notaExistente = gestor.Notas.FirstOrDefault(n => n.AlunoId == alunoId && n.DisciplinaId == disciplinaId && n.PeriodoLetivo == periodoLetivo);
                if (notaExistente == null)
                {
                    MessageBox.Show("Erro: Nota não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool sucesso = gestor.EditarNota(alunoId, disciplinaId, periodoLetivo, valorNota);
                if (sucesso)
                {
                    notaExistente.TipoAvaliacao = tipoAvaliacao;

                    MessageBox.Show("Nota editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarListaNotas();
                    btnSalvarEdicaoNota.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Erro ao editar a nota.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar edição da nota: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarNota_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se há algum item selecionado
                if (lstNotas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione uma nota para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtém o primeiro item selecionado
                ListViewItem itemSelecionado = lstNotas.SelectedItems[0];

                // Obtém o índice do item selecionado
                int indexSelecionado = itemSelecionado.Index;

                // Acessa a Nota correspondente ao índice selecionado
                Nota notaSelecionada = gestor.Notas.ElementAtOrDefault(indexSelecionado);
                if (notaSelecionada == null)
                {
                    MessageBox.Show("Erro: Selecione uma nota válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nomeDisciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == notaSelecionada.DisciplinaId)?.Nome ?? "Disciplina não encontrada";
                string nomeAluno = gestor.Alunos.FirstOrDefault(a => a.Id == notaSelecionada.AlunoId)?.Nome ?? "Aluno não encontrado";
                string turmaInfo = gestor.Turmas.FirstOrDefault(t => t.Id == gestor.Alunos.FirstOrDefault(a => a.Id == notaSelecionada.AlunoId)?.TurmaId)?.Curso ?? "Turma não encontrada";

                MessageBox.Show($"Ano Letivo: {notaSelecionada.PeriodoLetivo}\n" +
                                 $"Tipo de Avaliação: {notaSelecionada.TipoAvaliacao ?? "Não Informado"}\n" +
                                 $"Valor da Nota: {notaSelecionada.ValorNota}\n" +
                                 $"Disciplina: {notaSelecionada.DisciplinaId} - {nomeDisciplina}\n" +
                                 $"Aluno: {notaSelecionada.AlunoId} - {nomeAluno}\n" +
                                 $"Turma: {turmaInfo}",
                                 "Detalhes da Nota", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar nota: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvarEdicaoNota_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstNotas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Nenhuma nota selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ListViewItem itemSelecionado = lstNotas.SelectedItems[0];
                int indexSelecionado = itemSelecionado.Index;

                Nota notaSelecionada = gestor.Notas.ElementAtOrDefault(indexSelecionado);
                if (notaSelecionada == null)
                {
                    MessageBox.Show("Erro: A nota selecionada não foi encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txtAlunoIdNota.Text, out int novoAlunoId) ||
                    !int.TryParse(txtDisciplinaIdNota.Text, out int novaDisciplinaId) ||
                    !double.TryParse(txtValorNota.Text, out double novoValorNota) || novoValorNota < 0 || novoValorNota > 20)
                {
                    MessageBox.Show("Erro: Dados inválidos! Verifique os campos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string novoTipoAvaliacao = cmbTipoAvaliacao.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(novoTipoAvaliacao))
                {
                    MessageBox.Show("Erro: Selecione um tipo de avaliação!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Disciplina disciplinaEncontrada = gestor.Disciplinas.FirstOrDefault(d => d.Id == novaDisciplinaId);
                if (disciplinaEncontrada == null)
                {
                    MessageBox.Show($"Erro: A disciplina com ID {novaDisciplinaId} não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int professorId = disciplinaEncontrada.ProfessoresIds.FirstOrDefault();
                if (professorId == 0)
                {
                    MessageBox.Show("Erro: Nenhum professor associado a esta disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!gestor.ProfessorPodeGerirNota(professorId, novaDisciplinaId))
                {
                    MessageBox.Show("Erro: O professor não tem permissão para gerir notas nesta disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool notaDuplicada = gestor.Notas.Any(n => n.AlunoId == novoAlunoId && n.DisciplinaId == novaDisciplinaId && n.TipoAvaliacao == novoTipoAvaliacao && n != notaSelecionada);
                if (notaDuplicada)
                {
                    MessageBox.Show($"Erro: Já existe uma nota do tipo '{novoTipoAvaliacao}' para esta disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string periodoLetivo = txtPeriodoNota.Text.Trim();
                if (string.IsNullOrEmpty(periodoLetivo) || VerificarEstadoAnoLetivo(periodoLetivo) == "Encerrado")
                {
                    MessageBox.Show("Erro: O ano letivo já foi encerrado. Não é possível editar notas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Validar o formato do ano letivo
                if (!gestor.ValidarAnoLetivo(periodoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo deve estar no formato 'AAAA/AAAA'!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                notaSelecionada.AlunoId = novoAlunoId;
                notaSelecionada.DisciplinaId = novaDisciplinaId;
                notaSelecionada.ValorNota = novoValorNota;
                notaSelecionada.TipoAvaliacao = novoTipoAvaliacao;
                notaSelecionada.PeriodoLetivo = periodoLetivo;

                gestor.SalvarDados();
                AtualizarListaNotas();
                LimpaCampos();


                MessageBox.Show("Nota editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar edição da nota: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     
        private void lstNotas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (lstNotas.SelectedItems.Count == 0)
                    return;

                if (lstNotas.SelectedItems[0].Tag is Nota notaSelecionada) // 🔥 Agora pegamos a nota diretamente
                {
                    txtAlunoIdNota.Text = notaSelecionada.AlunoId.ToString();
                    txtDisciplinaIdNota.Text = notaSelecionada.DisciplinaId.ToString();
                    txtValorNota.Text = notaSelecionada.ValorNota.ToString();
                    txtPeriodoNota.Text = notaSelecionada.PeriodoLetivo;
                    cmbTipoAvaliacao.SelectedItem = notaSelecionada.TipoAvaliacao;
                }
                else
                {
                    MessageBox.Show("Erro: Nota não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao selecionar nota: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
