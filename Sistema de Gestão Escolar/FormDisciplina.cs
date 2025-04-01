using Bibilioteca_Sistema_de_Gestão_Escolar;
using System.Drawing.Drawing2D;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormDisciplina : Form
    {
        private GestorEscola gestor;

        public FormDisciplina(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaDisciplinas();
        }

        private void btnAdicionarDisciplina_Click(object sender, EventArgs e)
        {

            try
            {
                // Validar ID
                if (!int.TryParse(txtIdDisciplina.Text.Trim(), out int id))
                {
                    MessageBox.Show("Erro: O ID da disciplina deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o ID já existe
                if (gestor.Disciplinas.Any(d => d.Id == id))
                {
                    MessageBox.Show("Erro: Já existe uma disciplina com esse ID!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar nome da disciplina
                if (cmbNomeDisciplina.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string nomeDisciplina = cmbNomeDisciplina.SelectedItem.ToString();

                // Verificar se o nome da disciplina já existe
                if (gestor.Disciplinas.Any(d => d.Nome.Equals(nomeDisciplina, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Erro: Já existe uma disciplina com esse nome!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar carga horária
                if (!int.TryParse(cmbCargaHoraria.SelectedItem?.ToString(), out int cargaHoraria))
                {
                    MessageBox.Show("Erro: Selecione uma carga horária válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar nova disciplina
                Disciplina novaDisciplina = new Disciplina(id, nomeDisciplina, cargaHoraria);

                // Capturar IDs dos professores
                List<int> professoresIds = txtProfessoresDisciplina.Text
                    .Split(',')
                    .Select(p => p.Trim())
                    .Where(p => int.TryParse(p, out _))
                    .Select(int.Parse)
                    .ToList();

                if (professoresIds.Count == 0)
                {
                    MessageBox.Show("Aviso: Nenhum professor foi associado à disciplina.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Associar cada professor à nova disciplina utilizando o método AssociarProfessorADisciplina
                foreach (var professorId in professoresIds)
                {
                    gestor.AssociarProfessorADisciplina(id, professorId); // Chamando o método de associação
                }

                // Adicionar a disciplina ao gestor
                if (!gestor.AdicionarDisciplina(novaDisciplina))
                {
                    MessageBox.Show("Erro: Não foi possível adicionar a disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Salvar os dados
                gestor.SalvarDados();

                // Atualizar ListView
                AtualizarListaDisciplinas();

                MessageBox.Show("Disciplina adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void btnRemoverDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDisciplinas.SelectedItems.Count == 0) return;

                ListViewItem itemSelecionado = lstDisciplinas.SelectedItems[0];
                int idDisciplina = int.Parse(itemSelecionado.SubItems[0].Text);
                var disciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplina);

                if (disciplina != null)
                {
                    gestor.RemoverDisciplina(disciplina.Id);
                    gestor.SalvarDados();
                    AtualizarListaDisciplinas();
                    MessageBox.Show("Disciplina removida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover disciplina: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AtualizarListaDisciplinas()
        {
            lstDisciplinas.Items.Clear();

            foreach (var disciplina in gestor.Disciplinas)
            {
                // Busca os nomes dos professores associados à disciplina
                string professoresNomes = string.Join(", ", disciplina.ProfessoresIds
                    .Select(id => gestor.Professores.FirstOrDefault(p => p.Id == id)?.Nome ?? "Desconhecido"));

                // Cria o item da ListView com as informações da disciplina
                var item = new ListViewItem(new[]
                {
            disciplina.Id.ToString(),
            disciplina.Nome,
            disciplina.CargaHoraria.ToString(),
            professoresNomes // Mostra os nomes dos professores
        });

                item.Tag = disciplina.Id;
                lstDisciplinas.Items.Add(item);
            }
        }



        private void FormDisciplina_Load(object sender, EventArgs e)
        {
            try
            {
                // Preencher carga horária
                cmbCargaHoraria.Items.Clear();
                cmbCargaHoraria.Items.AddRange(new[] { "2", "3", "4", "5" });

                // Preencher nomes das disciplinas
                cmbNomeDisciplina.Items.Clear();
                string[] disciplinas =
                {
                "Português", "Inglês", "Francês", "Espanhol", "Filosofia", "História",
                "Matemática", "Física e Química", "Biologia e Geologia", "Geometria Descritiva",
                "Economia", "Geografia", "Sociologia", "Direito",
                "Educação Visual", "Desenho", "História da Cultura e das Artes",
                "Educação Física", "Ciências do Desporto",
                "Tecnologias de Informação e Comunicação (TIC)", "Programação", "Robótica"
            };
                cmbNomeDisciplina.Items.AddRange(disciplinas);

                // Definir botões redondos
                SetRoundButton(btnEditarDisciplina);
                SetRoundButton(btnAdicionarDisciplina);
                SetRoundButton(btnRemoverDisciplina);
                SetRoundButton(btnSalvarEdicaoDisciplina);
                SetRoundButton(btnConsultarDisciplina);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formulário de disciplinas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lstDisciplinas.GridLines = true;
            lstDisciplinas.View = View.Details;
            lstDisciplinas.FullRowSelect = true;

            lstDisciplinas.Columns.Clear();
            lstDisciplinas.Columns.Add("ID", 80);
            lstDisciplinas.Columns.Add("Nome", 200);
            lstDisciplinas.Columns.Add("Carga Horária", 120);
            lstDisciplinas.Columns.Add("Professores", 250);

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




        private void btnConsultarDisciplina_Click(object sender, EventArgs e)

        {
            try
            {
                if (lstDisciplinas.SelectedItems.Count == 0) return;

                ListViewItem itemSelecionado = lstDisciplinas.SelectedItems[0];
                int idDisciplina = int.Parse(itemSelecionado.SubItems[0].Text);
                var disciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplina);

                if (disciplina != null)
                {
                    MessageBox.Show($"ID: {disciplina.Id}\nNome: {disciplina.Nome}\nCarga Horária: {disciplina.CargaHoraria}h",
                        "Detalhes da Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar disciplina: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se uma disciplina foi selecionada
                if (lstDisciplinas.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina para editar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obter o ID da disciplina selecionada
                int idDisciplinaSelecionada = int.Parse(lstDisciplinas.SelectedItems[0].SubItems[0].Text);
                Disciplina disciplinaSelecionada = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplinaSelecionada);

                if (disciplinaSelecionada == null)
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Preencher os campos com os dados da disciplina
                txtIdDisciplina.Text = disciplinaSelecionada.Id.ToString();
                cmbNomeDisciplina.SelectedItem = disciplinaSelecionada.Nome;
                cmbCargaHoraria.SelectedItem = disciplinaSelecionada.CargaHoraria.ToString();

                // Preencher os IDs dos professores
                txtProfessoresDisciplina.Text = string.Join(",", disciplinaSelecionada.ProfessoresIds);

                // Habilitar o campo para edição
                btnSalvarEdicaoDisciplina.Enabled = true;
                btnAdicionarDisciplina.Enabled = false;

                // Marcar a disciplina em edição
                txtIdDisciplina.Tag = disciplinaSelecionada.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar disciplina: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvarEdicaoDisciplina_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se há uma disciplina em edição
                if (txtIdDisciplina.Tag == null)
                {
                    MessageBox.Show("Erro: Nenhuma disciplina está em edição!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obter o ID da disciplina em edição
                int disciplinaId = (int)txtIdDisciplina.Tag;
                Disciplina disciplinaEditada = gestor.Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);

                if (disciplinaEditada == null)
                {
                    MessageBox.Show("Erro: Disciplina não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar nome da disciplina
                if (cmbNomeDisciplina.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma disciplina válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string nomeDisciplina = cmbNomeDisciplina.SelectedItem.ToString();

                // Verificar se o nome da disciplina já existe (exceto para a disciplina sendo editada)
                if (gestor.Disciplinas.Any(d => d.Nome.Equals(nomeDisciplina, StringComparison.OrdinalIgnoreCase) && d.Id != disciplinaId))
                {
                    MessageBox.Show("Erro: Já existe uma disciplina com esse nome!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar carga horária
                if (!int.TryParse(cmbCargaHoraria.SelectedItem?.ToString(), out int cargaHoraria))
                {
                    MessageBox.Show("Erro: Selecione uma carga horária válida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar professores
                List<int> professoresIds = txtProfessoresDisciplina.Text.Split(",")
                    .Select(p => p.Trim())
                    .Where(p => int.TryParse(p, out _))
                    .Select(int.Parse)
                    .ToList();

                List<string> mensagensErroProfessores = new List<string>();

                // Verificar se os professores podem lecionar a disciplina
                foreach (var professorId in professoresIds)
                {
                    if (gestor.PodeLecionarDisciplina(professorId, disciplinaId, out string mensagemErro))
                    {
                        // Caso o professor possa lecionar, atualizar a disciplina
                        disciplinaEditada.ProfessoresIds.Add(professorId);
                    }
                    else
                    {
                        mensagensErroProfessores.Add(mensagemErro);
                    }
                }

                if (mensagensErroProfessores.Any())
                {
                    MessageBox.Show(string.Join("\n", mensagensErroProfessores), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualizar os valores da disciplina
                bool resultadoEdicao = gestor.EditarDisciplina(disciplinaId, nomeDisciplina, professoresIds);

                if (resultadoEdicao)
                {
                    // Salvar as mudanças
                    gestor.SalvarDados();

                    // Atualizar a lista de disciplinas
                    AtualizarListaDisciplinas();

                    // Limpar os campos e desativar o modo edição
                    txtIdDisciplina.Clear();
                    txtIdDisciplina.Enabled = true;
                    cmbNomeDisciplina.SelectedIndex = -1;
                    cmbCargaHoraria.SelectedIndex = -1;
                    txtProfessoresDisciplina.Clear();
                    txtIdDisciplina.Tag = null;

                    // Restaurar os botões
                    btnSalvarEdicaoDisciplina.Enabled = false;
                    btnAdicionarDisciplina.Enabled = true;

                    MessageBox.Show("Disciplina editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: Não foi possível editar a disciplina!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar edição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbNomeDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Verificar se há uma disciplina selecionada
                if (lstDisciplinas.SelectedItems.Count == 0)
                {
                    return; // Se não houver seleção, nada a fazer
                }

                // Obter a disciplina selecionada
                ListViewItem itemSelecionado = (ListViewItem) lstDisciplinas.SelectedItems[0];
                int idDisciplina = int.Parse(itemSelecionado.SubItems[0].Text); // ID da disciplina
                var disciplinaSelecionada = gestor.Disciplinas.FirstOrDefault(d => d.Id == idDisciplina);

                if (disciplinaSelecionada != null)
                {
                    // Preencher os campos de edição com as informações da disciplina
                    txtIdDisciplina.Text = disciplinaSelecionada.Id.ToString();
                    cmbNomeDisciplina.SelectedItem = disciplinaSelecionada.Nome;
                    cmbCargaHoraria.SelectedItem = disciplinaSelecionada.CargaHoraria.ToString();

                    // Preencher lista de professores associados
                    txtProfessoresDisciplina.Text = string.Join(", ", disciplinaSelecionada.ProfessoresIds);

                    // Habilitar o botão de salvar alteração
                    btnSalvarEdicaoDisciplina.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar disciplina: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
