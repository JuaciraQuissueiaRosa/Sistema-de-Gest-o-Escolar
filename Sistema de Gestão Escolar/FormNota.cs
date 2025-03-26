using Bibilioteca_Sistema_de_Gestão_Escolar;
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
            ConfiguraListBox();


        }
        private void ConfiguraListBox()
        {
            lstNotas.Width = 200;  // Ajuste a largura
            lstNotas.Height = 500; // Ajuste a altura
        }
        private void btnRemoverNota_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtAlunoIdNota.Text, out int alunoId) || !int.TryParse(txtDisciplinaIdNota.Text, out int disciplinaId))
                {
                    MessageBox.Show("Erro: Os IDs do aluno e da disciplina devem ser números inteiros!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string periodo = txtPeriodoNota.Text.Trim();
                if (string.IsNullOrEmpty(periodo))
                {
                    MessageBox.Show("Erro: O período letivo não pode estar vazio!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gestor.VerificarSePeriodoEncerrado(periodo))
                {
                    MessageBox.Show("Erro: O período letivo já foi encerrado. Não é possível remover notas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (gestor.RemoverNota(alunoId, disciplinaId, periodo))
                {
                    MessageBox.Show("Nota removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    // ✅ Salvar os dados após remover nota 
                    gestor.SalvarDados();
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

                gestor.AdicionarNota(new Nota(alunoId, disciplinaId, valorNota, periodoLetivo, tipoAvaliacao));

                // ✅ Salvar os dados após adicionar nota 
                gestor.SalvarDados();
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
                lstNotas.Items.Clear();

                var notasAgrupadas = gestor.Notas
                    .GroupBy(n => new { n.AlunoId, n.DisciplinaId })
                    .ToDictionary(g => g.Key, g => g.Select(n => n.ValorNota).ToList());

                foreach (var nota in gestor.Notas)
                {
                    string nomeDisciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == nota.DisciplinaId)?.Nome ?? "Disciplina não encontrada";
                    string nomeAluno = gestor.Alunos.FirstOrDefault(a => a.Id == nota.AlunoId)?.Nome ?? "Aluno não encontrado";
                    string turmaInfo = gestor.Turmas.FirstOrDefault(t => t.Id == gestor.Alunos.FirstOrDefault(a => a.Id == nota.AlunoId)?.TurmaId)?.Curso ?? "Turma não encontrada";
                    string tipoAvaliacao = string.IsNullOrEmpty(nota.TipoAvaliacao) ? "Não Informado" : nota.TipoAvaliacao;

                    string infoNota = $"Ano Letivo: {nota.PeriodoLetivo} | Tipo: {tipoAvaliacao} | Nota: {nota.ValorNota} " +
                                      $"| Disciplina: {nota.DisciplinaId} - {nomeDisciplina} | Aluno: {nota.AlunoId} - {nomeAluno} | Turma: {turmaInfo}";

                    lstNotas.Items.Add(infoNota);
                }

                foreach (var chave in notasAgrupadas.Keys)
                {
                    double media = notasAgrupadas[chave].Average();
                    lstNotas.Items.Add($"Aluno ID {chave.AlunoId} | Disciplina ID {chave.DisciplinaId} | Média Final: {media:F2}");
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
                if (lstNotas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione uma nota primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Nota notaSelecionada = gestor.Notas[lstNotas.SelectedIndex];

                bool alunoExiste = gestor.Alunos.Any(a => a.Id == notaSelecionada.AlunoId);
                bool disciplinaExiste = gestor.Disciplinas.Any(d => d.Id == notaSelecionada.DisciplinaId);

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

                txtAlunoIdNota.Text = notaSelecionada.AlunoId.ToString();
                txtDisciplinaIdNota.Text = notaSelecionada.DisciplinaId.ToString();
                txtValorNota.Text = notaSelecionada.ValorNota.ToString();
                txtPeriodoNota.Text = notaSelecionada.PeriodoLetivo;
                cmbTipoAvaliacao.SelectedItem = notaSelecionada.TipoAvaliacao;

                MessageBox.Show("Nota carregada para edição.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar nota para edição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                Nota notaSelecionada = gestor.Notas.ElementAtOrDefault(lstNotas.SelectedIndex);
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
                if (lstNotas.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Nenhuma nota selecionada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Nota notaSelecionada = gestor.Notas[lstNotas.SelectedIndex];

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

                notaSelecionada.AlunoId = novoAlunoId;
                notaSelecionada.DisciplinaId = novaDisciplinaId;
                notaSelecionada.ValorNota = novoValorNota;
                notaSelecionada.TipoAvaliacao = novoTipoAvaliacao;

                // ✅ Salvar os dados após editar nota 
                gestor.SalvarDados();

                AtualizarListaNotas();
                MessageBox.Show("Nota editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar edição da nota: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
    }

}
