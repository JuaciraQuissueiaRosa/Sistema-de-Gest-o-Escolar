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

                var professoresFormatados = gestor.Professores.Select(professor =>
                {
                    var disciplinasProfessor = gestor.Disciplinas
                        .Where(d => d.ProfessoresIds.Contains(professor.Id))
                        .Select(d => d.Nome)
                        .ToList();

                    string disciplinasTexto = disciplinasProfessor.Any() ? string.Join(", ", disciplinasProfessor) : "Nenhuma";

                    return $"ID: {professor.Id} | Nome: {professor.Nome} | Contato: {professor.Contato} | Email: {professor.Email} | Área: {professor.AreaEnsino} | Disciplinas: {disciplinasTexto}";
                });

                lstProfessores.Items.AddRange(professoresFormatados.ToArray());
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
                // Definir as áreas de ensino diretamente no ComboBox usando LINQ
                string[] areasEnsino =
                {
            "Línguas e Humanidades",
            "Ciências e Tecnologias",
            "Ciências Socioeconómicas",
            "Artes Visuais",
            "Educação Física e Desporto",
            "Informática e Tecnologias"
        };

                cmbAreaEnsino.Items.AddRange(areasEnsino);

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
                if (lstProfessores.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um professor para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter professor selecionado com LINQ
                Professor professor = gestor.Professores.ElementAtOrDefault(lstProfessores.SelectedIndex);

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
                if (lstProfessores.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Selecione um professor primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter professor selecionado com LINQ
                Professor professorSelecionado = gestor.Professores.ElementAtOrDefault(lstProfessores.SelectedIndex);

                if (professorSelecionado == null)
                {
                    MessageBox.Show("Erro: Professor não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Preencher os campos com os dados do professor
                txtIdProfessor.Text = professorSelecionado.Id.ToString();
                txtIdProfessor.Enabled = false; // Bloquear edição do ID
                txtNomeProfessor.Text = professorSelecionado.Nome;
                mtbContatoProfessor.Text = professorSelecionado.Contato;
                txtEmailProfessor.Text = professorSelecionado.Email;
                cmbAreaEnsino.SelectedItem = professorSelecionado.AreaEnsino;

                // Habilitar botão "Salvar Alterações"
                btnSalvarEdicaoProfessor.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar professor para edição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvarEdicaoProfessor_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstProfessores.SelectedIndex == -1)
                {
                    MessageBox.Show("Erro: Nenhum professor selecionado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter professor selecionado com LINQ
                Professor professorSelecionado = gestor.Professores.ElementAtOrDefault(lstProfessores.SelectedIndex);

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

                // Editar professor no sistema (mantendo o ID original)
                if (!gestor.EditarProfessor(professorSelecionado.Id, novoNome, novaAreaEnsino, novoContato, novoEmail))
                {
                    MessageBox.Show("Erro ao editar o professor!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // ✅ Salvar os dados após editar professor
                gestor.SalvarDados();
                // Atualizar lista de professores
                AtualizarListaProfessores();

                // Resetar e desabilitar o campo ID
                txtIdProfessor.Clear();
                txtIdProfessor.Enabled = true;

                // Desabilitar botão após salvar
                btnSalvarEdicaoProfessor.Enabled = false;

                MessageBox.Show("Professor editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar alterações do professor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }



}
