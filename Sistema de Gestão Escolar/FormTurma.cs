using Bibilioteca_Sistema_de_Gestão_Escolar;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormTurma : Form
    {
        private GestorEscola gestor;

        public FormTurma(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaTurmas();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRemoverTurma_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se o ID da turma é um número válido
                if (!int.TryParse(txtIdTurma.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tentar remover a turma
                bool removida = gestor.RemoverTurma(id);
                if (removida)
                {
                    MessageBox.Show("Turma removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: A turma não pode ser removida. Verifique se há alunos matriculados ou se o ID é válido!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Atualizar a lista de turmas
                AtualizarListaTurmas();

                // Atualizar a lista de turmas no FormAluno (se estiver aberto)
                AtualizarFormAluno();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionarTurma_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se o ID da turma é um número válido
                if (!int.TryParse(txtIdTurma.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID da turma deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se um curso foi selecionado no ComboBox
                string curso = cmbCursoTurma.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(curso))
                {
                    MessageBox.Show("Erro: Selecione um curso válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o ano letivo foi preenchido corretamente
                string anoLetivo = txtAnoLetivoTurma.Text.Trim();
                if (!ValidarAnoLetivo(anoLetivo))
                {
                    MessageBox.Show("Erro: O ano letivo deve estar no formato correto (exemplo: 2023/2024)!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar o turno selecionado no ComboBox
                string turno = cmbTurnoTurma.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(turno))
                {
                    MessageBox.Show("Erro: Selecione um turno!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar a nova turma com os dados corrigidos
                Turma novaTurma = new Turma(id, curso, anoLetivo, turno);

                // Adicionar a turma ao sistema
                gestor.AdicionarTurma(novaTurma);

                // Atualizar a lista de turmas no próprio FormTurma
                AtualizarListaTurmas();

                // Atualizar a lista de turmas no FormAluno (se estiver aberto)
                AtualizarFormAluno();

                MessageBox.Show("Turma adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AtualizarListaTurmas()
        {
            try
            {
                lstTurmas.Items.Clear(); // Limpa a lista antes de atualizar

                // Percorre todas as turmas cadastradas
                for (int i = 0; i < gestor.Turmas.Count; i++)
                {
                    Turma turma = gestor.Turmas[i];

                    // Criar uma lista para armazenar os nomes das disciplinas associadas à turma
                    List<string> disciplinasNomes = new List<string>();

                    // Percorrer todas as disciplinas e encontrar as associadas à turma
                    for (int j = 0; j < gestor.Disciplinas.Count; j++)
                    {
                        Disciplina disciplina = gestor.Disciplinas[j];

                        // Verificar manualmente se a turma está na lista TurmasIds da disciplina
                        for (int k = 0; k < disciplina.TurmasIds.Count; k++)
                        {
                            if (disciplina.TurmasIds[k] == turma.Id)
                            {
                                disciplinasNomes.Add(disciplina.Nome);
                                break; // Paramos a busca quando encontramos a correspondência
                            }
                        }
                    }

                    // Criar a string formatada para exibir na ListBox
                    string infoTurma = "ID: " + turma.Id + " | Curso: " + turma.Curso +
                                       " | Ano Letivo: " + turma.AnoLetivo + " | Turno: " + turma.Turno;

                    // Se houver disciplinas associadas, adicioná-las à exibição
                    if (disciplinasNomes.Count > 0)
                    {
                        infoTurma += " | Disciplinas: ";
                        for (int m = 0; m < disciplinasNomes.Count; m++)
                        {
                            if (m > 0)
                            {
                                infoTurma += ", ";
                            }
                            infoTurma += disciplinasNomes[m];
                        }
                    }
                    else
                    {
                        infoTurma += " | Disciplinas: Nenhuma";
                    }

                    // Adiciona a turma na ListBox
                    lstTurmas.Items.Add(infoTurma);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar a lista de turmas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormTurma_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTurnoTurma.Items.Clear();
                cmbTurnoTurma.Items.Add("Diurno");
                cmbTurnoTurma.Items.Add("Noturno");

                cmbCursoTurma.Items.Clear();

                // Adicionar cursos válidos no ComboBox
                string[] cursos =
                {
            "Ciências e Tecnologias",
            "Línguas e Humanidades",
            "Ciências Socioeconómicas",
            "Artes Visuais",
            "Técnico de Informática e Gestão",
            "Técnico de Eletrónica, Automação e Comando",
            "Técnico de Turismo",
            "Técnico de Cozinha e Pastelaria",
            "Técnico de Restaurante e Bar",
            "Técnico de Mecatrónica"
        };

                for (int i = 0; i < cursos.Length; i++)
                {
                    cmbCursoTurma.Items.Add(cursos[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar formulário de turmas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarFormAluno()
        {
            try
            {
                // Verificar se o formulário FormAluno está aberto
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i] is FormAluno)
                    {
                        FormAluno formAluno = (FormAluno)Application.OpenForms[i];
                        formAluno.AtualizarComboBoxTurmas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar formulário de alunos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarAnoLetivo(string anoLetivo)
        {
            try
            {
                // Verificar se o formato está correto: "AAAA/AAAA"
                string[] anos = anoLetivo.Split('/');

                if (anos.Length != 2)
                {
                    return false; // Deve ter exatamente dois anos separados por "/"
                }

                // Verificar se ambos os anos são números inteiros
                int anoInicio = 0, anoFim = 0;
                bool inicioValido = int.TryParse(anos[0], out anoInicio);
                bool fimValido = int.TryParse(anos[1], out anoFim);

                if (!inicioValido || !fimValido)
                {
                    return false;
                }

                // O primeiro ano deve ser menor que o segundo (exemplo: 2023/2024)
                if (anoInicio >= anoFim)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false; // Em caso de erro, retorna falso sem quebrar o sistema
            }
        }

    }
}
