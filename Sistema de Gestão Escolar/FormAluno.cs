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
            int id = int.Parse(txtIdAluno.Text);
            gestor.RemoverAluno(id);
            AtualizarListaAlunos();
        }

        private void btnBuscarAluno_Click(object sender, EventArgs e)
        {
            string termo = txtBuscarAluno.Text;
            List<Aluno> resultados = gestor.BuscarAlunos(termo);
            lstAlunos.Items.Clear();
            foreach (var aluno in resultados)
            {
                lstAlunos.Items.Add($"{aluno.Id} - {aluno.Nome} - Turma: {aluno.TurmaId}");
            }
        }

        private void btnMudarTurma_Click(object sender, EventArgs e)
        {
            int alunoId = int.Parse(txtIdAluno.Text);
            int novaTurmaId = int.Parse(txtNovaTurmaAluno.Text);

            gestor.MudarAlunoDeTurma(alunoId, novaTurmaId);
            AtualizarListaAlunos();

        }

        private void AtualizarListaAlunos()
        {
            lstAlunos.Items.Clear();
            foreach (var aluno in gestor.ListarAlunos())
            {
                lstAlunos.Items.Add($"{aluno.Id} - {aluno.Nome} - Turma: {aluno.TurmaId}");
            }
        }

        private bool ValidarEmail(string email)
        {
            string padraoEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, padraoEmail);
        }
    }
}
