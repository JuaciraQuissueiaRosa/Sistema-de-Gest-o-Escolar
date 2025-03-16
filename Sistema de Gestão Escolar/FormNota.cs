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

                // Verificar o estado do ano letivo
                string anoLetivo = txtPeriodoNota.Text.Trim();
                string estadoAno = VerificarEstadoAnoLetivo(anoLetivo);

                if (estadoAno == "Encerrado")
                {
                    MessageBox.Show("Erro: O ano letivo já foi encerrado. Não é possível adicionar ou alterar notas.",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar a nova nota
                Nota novaNota = new Nota(alunoId, disciplinaId, valorNota, "1º Trimestre");

                // Adicionar a nota ao sistema
                gestor.AdicionarNota(novaNota);

                MessageBox.Show("Nota adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // Recuperar o tipo de avaliação do dicionário (ou usar "Não Informado" se não existir)
                    string tipoAvaliacao = tiposAvaliacao.ContainsKey(i) ? tiposAvaliacao[i] : "Não Informado";

                    // Criar a string formatada para exibição
                    string infoNota = $"Aluno ID: {nota.AlunoId} | Disciplina ID: {nota.DisciplinaId} | Nota: {nota.ValorNota} | Tipo: {tipoAvaliacao} | Período: {nota.PeriodoLetivo}";

                    lstNotas.Items.Add(infoNota);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar lista de notas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormNota_Load(object sender, EventArgs e)
        {
            try
            {
                // Limpar o ComboBox antes de preencher (evita duplicação ao abrir várias vezes)
                cmbProfessorNota.Items.Clear();

                // Adicionar professores ao ComboBox
                for (int i = 0; i < gestor.Professores.Count; i++)
                {
                    Professor professor = gestor.Professores[i];
                    cmbProfessorNota.Items.Add($"{professor.Id} - {professor.Nome}");
                }

                // Limpa o ComboBox antes de preencher (evita duplicação ao abrir várias vezes)
                cmbTipoAvaliacao.Items.Clear();

                // Adiciona os tipos de avaliação permitidos
                cmbTipoAvaliacao.Items.Add("Teste");
                cmbTipoAvaliacao.Items.Add("Trabalho");
                cmbTipoAvaliacao.Items.Add("Exame");

                // Define um valor padrão ao abrir o formulário
                cmbTipoAvaliacao.SelectedIndex = 0; // Define "Teste" como valor inicial
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

    }

}
