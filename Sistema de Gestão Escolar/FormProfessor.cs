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
            int id = int.Parse(txtIdProfessor.Text);
            gestor.RemoverProfessor(id);
            AtualizarListaProfessores();
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
            lstProfessores.Items.Clear();
            foreach (var professor in gestor.Professores)
            {
                // Buscar disciplinas associadas ao professor
                List<string> disciplinasProfessor = new List<string>();
                foreach (var disciplina in gestor.Disciplinas)
                {
                    if (disciplina.ProfessoresIds.Contains(professor.Id))
                    {
                        disciplinasProfessor.Add(disciplina.Nome);
                    }
                }

                // Exibir todas as informações
                lstProfessores.Items.Add(
                    $"ID: {professor.Id} | Nome: {professor.Nome} | Contato: {professor.Contato} | " +
                    $"Email: {professor.Email} | Área: {professor.AreaEnsino} | Disciplinas: {string.Join(", ", disciplinasProfessor)}");
            }
        }

        private void FormProfessor_Load(object sender, EventArgs e)
        {
            cmbAreaEnsino.Items.Add("Línguas e Humanidades");
            cmbAreaEnsino.Items.Add("Ciências e Tecnologias");
            cmbAreaEnsino.Items.Add("Ciências Socioeconómicas");
            cmbAreaEnsino.Items.Add("Artes Visuais");
            cmbAreaEnsino.Items.Add("Educação Física e Desporto");
            cmbAreaEnsino.Items.Add("Informática e Tecnologias");
            cmbAreaEnsino.Items.Add("Matemática e Física");
            cmbAreaEnsino.Items.Add("Biologia e Geologia");
            cmbAreaEnsino.Items.Add("História e Filosofia");
            cmbAreaEnsino.Items.Add("Educação Especial");
        }

        private bool ValidarEmail(string email)
        {
            // Expressão regular para validar o formato do e-mail
            string padraoEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Verifica se o e-mail corresponde ao padrão
            return Regex.IsMatch(email, padraoEmail);
        }
    }

    

}
