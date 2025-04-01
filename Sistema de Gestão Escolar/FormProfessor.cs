using Bibilioteca_Sistema_de_Gestão_Escolar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormProfessor : Form
    {
        private GestorEscola gestor;

        public FormProfessor(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaProfessores();
        }
        private void btnRemoverProfessor_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtIdProfessor.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID do professor deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gestor.RemoverProfessor(id))
                {
                    MessageBox.Show("Professor removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: O professor não pode ser removido visto que se encontra associado a uma(s) disciplina(s) ativa(s)!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // ✅ Salvar os dados após remover professor
                gestor.SalvarDados();

                AtualizarListaProfessores();

                LimpaCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao remover professor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAdicionarProfessor_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtIdProfessor.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID do professor deve ser um número inteiro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gestor.Professores.Any(p => p.Id == id))
                {
                    MessageBox.Show("Erro: Já existe um professor com esse ID!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nome = txtNomeProfessor.Text.Trim();
                if (string.IsNullOrEmpty(nome))
                {
                    MessageBox.Show("Erro: O nome do professor não pode estar vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string contato = mtbContatoProfessor.Text.Trim().Replace(" ", "");
                if (contato.Length != 13 || (!contato.StartsWith("+3519") && !contato.StartsWith("+3512")))
                {
                    MessageBox.Show("Erro: O contato deve seguir o formato '+351 9XXXXXXXX' ou '+351 2XXXXXXXX'.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string email = txtEmailProfessor.Text.Trim();
                if (!ValidarEmail(email))
                {
                    MessageBox.Show("Erro: O e-mail digitado não é válido! Exemplo: exemplo@email.com", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cmbAreaEnsino.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma área de ensino!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string areaEnsino = cmbAreaEnsino.SelectedItem.ToString();
                var novoProfessor = new Professor(id, nome, contato, email, areaEnsino);

                gestor.AdicionarProfessor(novoProfessor);
                // ✅ Salvar os dados após adicionar professor 
                gestor.SalvarDados();
                AtualizarListaProfessores();

                MessageBox.Show("Professor adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void AtualizarListaProfessores()
        {
            try
            {
                lstProfessores.Items.Clear();

                foreach (var professor in gestor.Professores)
                {
                    var disciplinasProfessor = gestor.Disciplinas
                        .Where(d => d.ProfessoresIds.Contains(professor.Id))
                        .Select(d => d.Nome)
                        .ToList();

                    string disciplinasTexto = disciplinasProfessor.Any() ? string.Join(", ", disciplinasProfessor) : "Nenhuma";

                    // Criar item da ListView e definir o Tag como o ID do professor
                    ListViewItem item = new ListViewItem(professor.Id.ToString()); // Primeira coluna (ID)
                    item.SubItems.Add(professor.Nome);
                    item.SubItems.Add(professor.Contato);
                    item.SubItems.Add(professor.Email);
                    item.SubItems.Add(professor.AreaEnsino);
                    item.SubItems.Add(disciplinasTexto);

                    item.Tag = professor.Id; // 🔹 Armazena o ID corretamente no Tag

                    lstProfessores.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar a lista de professores: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProfessor_Load(object sender, EventArgs e)
        {
            try
            {
                // Definir as áreas de ensino diretamente no ComboBox
                string[] areasEnsino =
                {
            "Línguas e Humanidades",
            "Ciências e Tecnologias",
            "Ciências Socioeconómicas",
            "Artes Visuais",
            "Educação Física e Desporto",
            "Informática e Tecnologias"
        };

                cmbAreaEnsino.Items.Clear();
                cmbAreaEnsino.Items.AddRange(areasEnsino);

                // Configurar ListView corretamente
                lstProfessores.View = View.Details;
                lstProfessores.FullRowSelect = true;
                lstProfessores.GridLines = true;
                lstProfessores.MultiSelect = false; // Apenas um item pode ser selecionado

                // Limpar e adicionar colunas ao ListView apenas se necessário
                if (lstProfessores.Columns.Count == 0)
                {
                    lstProfessores.Columns.Add("ID", 50);
                    lstProfessores.Columns.Add("Nome", 150);
                    lstProfessores.Columns.Add("Contato", 185);
                    lstProfessores.Columns.Add("Email", 150);
                    lstProfessores.Columns.Add("Área", 200);
                    lstProfessores.Columns.Add("Disciplinas", 200);
                }

                // Atualizar lista de professores ao abrir o formulário
                AtualizarListaProfessores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formulário de professores: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Definir botões redondos
            SetRoundButton(btnSalvarEdicaoProfessor);
            SetRoundButton(btnConsultarProfessor);
            SetRoundButton(btnRemoverProfessor);
            SetRoundButton(btnAdicionarProfessor);
            SetRoundButton(btnEditarProfessor);




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


        private bool ValidarEmail(string email) =>
      Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        private void btnConsultarProfessor_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstProfessores.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Selecione um professor para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtém o ID do professor selecionado a partir da Tag do ListView
                int professorId = (int)lstProfessores.SelectedItems[0].Tag;
                Professor professor = gestor.Professores.FirstOrDefault(p => p.Id == professorId);

                if (professor == null)
                {
                    MessageBox.Show("Erro: Professor não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"ID: {professor.Id}\nNome: {professor.Nome}\nContato: {professor.Contato}\nEmail: {professor.Email}\nÁrea: {professor.AreaEnsino}",
                                "Detalhes do Professor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar professor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarProfessor_Click(object sender, EventArgs e)
        {
            try
            {
                // Obter os dados do formulário
                int idProfessor = int.Parse(txtIdProfessor.Text); // ID do professor
                string nome = txtNomeProfessor.Text;
                string contato = mtbContatoProfessor.Text;
                string email = txtEmailProfessor.Text;
                string areaEnsino = cmbAreaEnsino.SelectedItem.ToString();

                // Validar campos obrigatórios
                if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(contato) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(areaEnsino))
                {
                    MessageBox.Show("Erro: Todos os campos devem ser preenchidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chamar o método EditarProfessor para salvar as alterações
                bool sucesso = gestor.EditarProfessor(idProfessor, nome, areaEnsino, contato, email);

                // Verificar se a edição foi bem-sucedida
                if (sucesso)
                {
                    MessageBox.Show("Professor editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Atualizar a lista na interface gráfica, se necessário
                    AtualizarListaProfessores(); // Este método precisa ser implementado para atualizar os dados na interface

                    // Desabilitar o botão de salvar alterações após a edição
                    btnSalvarEdicaoProfessor.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Erro: Não foi possível editar o professor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar alterações: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvarEdicaoProfessor_Click(object sender, EventArgs e)
        {

            try
            {
                if (lstProfessores.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Erro: Nenhum professor selecionado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Teste se o Tag está definido corretamente
                if (lstProfessores.SelectedItems[0].Tag == null)
                {
                    MessageBox.Show("Erro: O professor selecionado não tem um ID válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔹 Converter o ID corretamente
                if (!int.TryParse(lstProfessores.SelectedItems[0].Tag.ToString(), out int professorId))
                {
                    MessageBox.Show("Erro: ID do professor inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔹 Teste se o professor foi encontrado na lista
                Professor professorSelecionado = gestor.Professores.FirstOrDefault(p => p.Id == professorId);

                if (professorSelecionado == null)
                {
                    MessageBox.Show("Erro: Professor não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar os novos dados
                string novoNome = txtNomeProfessor.Text.Trim();
                string novoEmail = txtEmailProfessor.Text.Trim();
                string novaAreaEnsino = cmbAreaEnsino.SelectedItem?.ToString();
                string novoContato = mtbContatoProfessor.Text.Trim().Replace(" ", "");

                // Verificações de preenchimento
                if (new[] { novoNome, novoEmail, novaAreaEnsino, novoContato }.Any(string.IsNullOrEmpty))
                {
                    MessageBox.Show("Erro: Preencha todos os campos corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar e-mail
                if (!ValidarEmail(novoEmail))
                {
                    MessageBox.Show("Erro: O e-mail informado não é válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar contato
                if (!Regex.IsMatch(novoContato, @"^\+351[92]\d{8}$"))
                {
                    MessageBox.Show("Erro: O contato deve seguir o formato '+351 9XXXXXXXX' ou '+351 2XXXXXXXX'.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atualizar dados do professor
                professorSelecionado.Nome = novoNome;
                professorSelecionado.Email = novoEmail;
                professorSelecionado.AreaEnsino = novaAreaEnsino;
                professorSelecionado.Contato = novoContato;

                // Salvar alterações no sistema
                gestor.SalvarDados();

                // Atualizar lista de professores
                AtualizarListaProfessores();

                // Resetar e habilitar campo ID
                txtIdProfessor.Clear();
                txtIdProfessor.Enabled = true;

                // Desabilitar botão após salvar
                btnSalvarEdicaoProfessor.Enabled = false;

                MessageBox.Show("Professor editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpaCampos();
            

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar alterações do professor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpaCampos()
        {
            txtNomeProfessor.Clear();
            txtEmailProfessor.Clear();
            mtbContatoProfessor.Clear();
            txtIdProfessor.Clear();
        }

        private void lstProfessores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstProfessores.SelectedItems.Count == 0)
                    return;

                // 🔹 Garantir que o Tag não seja nulo antes de converter
                if (lstProfessores.SelectedItems[0].Tag is int professorId)
                {
                    // 🔹 Buscar o professor correto na lista
                    Professor professorSelecionado = gestor.Professores.FirstOrDefault(p => p.Id == professorId);

                    if (professorSelecionado != null)
                    {
                        // Preencher os campos do formulário com os dados do professor
                        txtIdProfessor.Text = professorSelecionado.Id.ToString();
                        txtNomeProfessor.Text = professorSelecionado.Nome;
                        txtEmailProfessor.Text = professorSelecionado.Email;
                        cmbAreaEnsino.SelectedItem = professorSelecionado.AreaEnsino;
                        mtbContatoProfessor.Text = professorSelecionado.Contato;

                        // Habilitar botão de edição
                        btnSalvarEdicaoProfessor.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Erro: Professor não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Erro: ID do professor inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao selecionar professor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }



}
