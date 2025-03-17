using Bibilioteca_Sistema_de_Gestão_Escolar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                // Verificar se o ID do professor é um número válido
                if (!int.TryParse(txtIdProfessor.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID do professor deve ser um número inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tentar remover o professor
                bool removido = gestor.RemoverProfessor(id);
                if (removido)
                {
                    MessageBox.Show("Professor removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro: O professor não pode ser removido visto que se encontra associado a uma(s) disciplina(s) ativa(s)!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Atualizar a lista de professores
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
                // Verificar se o ID do professor é um número válido
                if (!int.TryParse(txtIdProfessor.Text, out int id))
                {
                    MessageBox.Show("Erro: O ID do professor deve ser um número inteiro.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o nome foi preenchido
                string nome = txtNomeProfessor.Text.Trim();
                if (string.IsNullOrEmpty(nome))
                {
                    MessageBox.Show("Erro: O nome do professor não pode estar vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar o contato do MaskedTextBox
                string contato = mtbContatoProfessor.Text.Trim();

                // Remover espaços extras do contato
                contato = contato.Replace(" ", "");

                // Se o MaskedTextBox não estiver completamente preenchido, exibir erro
                if (contato.Length != 13 || !contato.StartsWith("+3519") && !contato.StartsWith("+3512"))
                {
                    MessageBox.Show("Erro: O contato deve seguir o formato '+351 9XXXXXXXX' ou '+351 2XXXXXXXX'.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar se o email foi preenchido
                // Capturar e validar o email
                string email = txtEmailProfessor.Text.Trim();
                if (!ValidarEmail(email))
                {
                    MessageBox.Show("Erro: O e-mail digitado não é válido! Exemplo: exemplo@email.com", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Capturar a área de ensino do ComboBox
                if (cmbAreaEnsino.SelectedItem == null)
                {
                    MessageBox.Show("Erro: Selecione uma área de ensino!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string areaEnsino = cmbAreaEnsino.SelectedItem.ToString();

                // Criar novo professor
                Professor novoProfessor = new Professor(id, nome, contato, email, areaEnsino);

                // Adicionar ao sistema
                gestor.AdicionarProfessor(novoProfessor);
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

                // Percorrer a lista de professores
                for (int i = 0; i < gestor.Professores.Count; i++)
                {
                    Professor professor = gestor.Professores[i];

                    // Lista para armazenar as disciplinas que o professor leciona
                    List<string> disciplinasProfessor = new List<string>();

                    // Percorrer todas as disciplinas e verificar quais são lecionadas por esse professor
                    for (int j = 0; j < gestor.Disciplinas.Count; j++)
                    {
                        Disciplina disciplina = gestor.Disciplinas[j];

                        // Verificar manualmente se o professor está associado à disciplina
                        bool professorEncontrado = false;
                        for (int k = 0; k < disciplina.ProfessoresIds.Count; k++)
                        {
                            if (disciplina.ProfessoresIds[k] == professor.Id)
                            {
                                professorEncontrado = true;
                                break; // Para evitar múltiplas adições da mesma disciplina
                            }
                        }

                        // Se o professor leciona essa disciplina, adicionar à lista
                        if (professorEncontrado)
                        {
                            disciplinasProfessor.Add(disciplina.Nome);
                        }
                    }

                    // Criar a string de disciplinas sem `string.Join`
                    string disciplinasTexto = "Nenhuma";
                    if (disciplinasProfessor.Count > 0)
                    {
                        disciplinasTexto = "";
                        for (int m = 0; m < disciplinasProfessor.Count; m++)
                        {
                            if (m > 0)
                            {
                                disciplinasTexto += ", ";
                            }
                            disciplinasTexto += disciplinasProfessor[m];
                        }
                    }

                    // Criar a string de exibição na ListBox
                    string infoProfessor = "ID: " + professor.Id + " | Nome: " + professor.Nome +
                                           " | Contato: " + professor.Contato +
                                           " | Email: " + professor.Email +
                                           " | Área: " + professor.AreaEnsino +
                                           " | Disciplinas: " + disciplinasTexto;

                    lstProfessores.Items.Add(infoProfessor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar a lista de professores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProfessor_Load(object sender, EventArgs e)
        {
            try
            {
                // Limpar o ComboBox antes de adicionar novas opções
                cmbAreaEnsino.Items.Clear();

                // Adicionar áreas de ensino ao ComboBox
                string[] areasEnsino =
                {
            "Línguas e Humanidades",
            "Ciências e Tecnologias",
            "Ciências Socioeconómicas",
            "Artes Visuais",
            "Educação Física e Desporto",
            "Informática e Tecnologias"
        };

                foreach (string area in areasEnsino)
                {
                    cmbAreaEnsino.Items.Add(area);
                }

                // Atualizar lista de professores ao abrir o formulário
                AtualizarListaProfessores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formulário de professores: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarEmail(string email)
        {
            // Expressão regular para validar o formato do e-mail
            string padraoEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Verifica se o e-mail corresponde ao padrão
            return Regex.IsMatch(email, padraoEmail);
        }

        private void btnConsultarProfessor_Click(object sender, EventArgs e)
        {
            if (lstProfessores.SelectedIndex == -1)
            {
                MessageBox.Show("Erro: Selecione um professor para consultar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Professor professor = gestor.Professores[lstProfessores.SelectedIndex];

            MessageBox.Show("ID: " + professor.Id + "\nNome: " + professor.Nome + "\nContato: " + professor.Contato +
                            "\nEmail: " + professor.Email + "\nÁrea: " + professor.AreaEnsino,
                            "Detalhes do Professor", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Obter professor selecionado
                Professor professorSelecionado = gestor.Professores[lstProfessores.SelectedIndex];

                // Preencher os campos com os dados do professor
                txtIdProfessor.Text = professorSelecionado.Id.ToString();
                txtNomeProfessor.Text = professorSelecionado.Nome;
                mtbContatoProfessor.Text = professorSelecionado.Contato;
                txtEmailProfessor.Text = professorSelecionado.Email;
                cmbAreaEnsino.SelectedItem = professorSelecionado.AreaEnsino;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar professor para edição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }



}
