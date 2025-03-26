using Bibilioteca_Sistema_de_Gestão_Escolar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormEvento : Form
    {
        private GestorEscola gestor;
        private Evento eventoSelecionado;

        public FormEvento(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            AtualizarListaEventos();
            CarregarDados();
        }

        private void btnRemoverEvento_Click(object sender, EventArgs e)
        {

            if (eventoSelecionado == null)
            {
                MessageBox.Show("Selecione um evento primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("Tem certeza que deseja remover este evento?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                gestor.RemoverEvento(eventoSelecionado.Id);
                AtualizarListaEventos();
                LimparCampos();
                MessageBox.Show("Evento removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void btnSelecionarEvento_Click(object sender, EventArgs e)
        {
            if (lstEventos.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um evento!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            eventoSelecionado = gestor.Eventos[lstEventos.SelectedIndex];
            txtNomeEvento.Text = eventoSelecionado.Nome;
            txtDescricaoEvento.Text = eventoSelecionado.Descricao;
            dtpDataEvento.Value = eventoSelecionado.Data;

            AtualizarListaEventos();
        }

        private void btnEditarEvento_Click(object sender, EventArgs e)
        {
            if (eventoSelecionado == null)
            {
                MessageBox.Show("Selecione um evento para editar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            eventoSelecionado.Nome = txtNomeEvento.Text.Trim();
            eventoSelecionado.Descricao = txtDescricaoEvento.Text.Trim();
            eventoSelecionado.Data = dtpDataEvento.Value;

            gestor.SalvarDados();
            AtualizarListaEventos();
            MessageBox.Show("Evento atualizado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAdicionarEvento_Click(object sender, EventArgs e)
        {
            try
            {
                int id = gestor.Eventos.Count + 1;
                string nome = txtNomeEvento.Text.Trim();
                string descricao = txtDescricaoEvento.Text.Trim();
                DateTime data = dtpDataEvento.Value;

                gestor.AdicionarEvento(id, nome, descricao, data);
                AtualizarListaEventos();
                MessageBox.Show("Evento cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar evento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        //-------------------------------------------

        // 🔄 Atualizar lista de eventos
        private void AtualizarListaEventos()
        {
            lstEventos.Items.Clear();

            foreach (var evento in gestor.Eventos)
            {
                string alunos = string.Join(", ", evento.AlunosIds
                    .Select(id => gestor.Alunos.FirstOrDefault(a => a.Id == id)?.Nome)
                    .Where(nome => !string.IsNullOrEmpty(nome)));

                string professores = string.Join(", ", evento.ProfessoresIds
                    .Select(id => gestor.Professores.FirstOrDefault(p => p.Id == id)?.Nome)
                    .Where(nome => !string.IsNullOrEmpty(nome)));

                string participantes = $"Alunos: {alunos} | Professores: {professores}";
                lstEventos.Items.Add($"{evento.Data:dd/MM/yyyy} - {evento.Nome} ({participantes})");
            }
        }

        // 🔄 Atualizar lista de alunos e professores participantes
        private void AtualizarListaParticipantes()
        {
            lstAlunos.Items.Clear();
            lstProfessores.Items.Clear();

            if (eventoSelecionado == null) return;

            foreach (var alunoId in eventoSelecionado.AlunosIds)
            {
                Aluno aluno = gestor.Alunos.FirstOrDefault(a => a.Id == alunoId);
                if (aluno != null)
                    lstAlunos.Items.Add(aluno.Nome);
            }

            foreach (var professorId in eventoSelecionado.ProfessoresIds)
            {
                Professor professor = gestor.Professores.FirstOrDefault(p => p.Id == professorId);
                if (professor != null)
                    lstProfessores.Items.Add(professor.Nome);
            }
        }
        // 🔄 Carregar Alunos e Professores disponíveis
        private void CarregarDados()
        { // Preencher a ComboBox com alunos
            cmbAluno.DataSource = gestor.Alunos;
            cmbAluno.DisplayMember = "Nome";
            cmbAluno.ValueMember = "Id";

            // Preencher a ComboBox com professores
            cmbProfessor.DataSource = gestor.Professores;
            cmbProfessor.DisplayMember = "Nome";
            cmbProfessor.ValueMember = "Id";

            // Adicionar alunos na ListBox (disponíveis)
            lstAlunos.Items.Clear();
            foreach (var aluno in gestor.Alunos)
            {
                lstAlunos.Items.Add(aluno.Nome);
            }

            // Adicionar professores na ListBox (disponíveis)
            lstProfessores.Items.Clear();
            foreach (var professor in gestor.Professores)
            {
                lstProfessores.Items.Add(professor.Nome);
            }
        }

        // 🗑 Limpar campos
        private void LimparCampos()
        {
            txtNomeEvento.Text = "";
            txtDescricaoEvento.Text = "";
            dtpDataEvento.Value = DateTime.Now;
            lstAlunos.Items.Clear();
            lstProfessores.Items.Clear();
            eventoSelecionado = null;
        }

        private void lstAlunos_DoubleClick(object sender, EventArgs e)
        {
            if (lstAlunos.SelectedItem == null || eventoSelecionado == null)
                return;

            string nomeAluno = lstAlunos.SelectedItem.ToString();
            Aluno alunoSelecionado = gestor.Alunos.FirstOrDefault(a => a.Nome == nomeAluno);

            if (alunoSelecionado != null)
            {
                if (eventoSelecionado.AlunosIds.Contains(alunoSelecionado.Id))
                {
                    // Remove o aluno se já estiver no evento
                    eventoSelecionado.AlunosIds.Remove(alunoSelecionado.Id);
                    MessageBox.Show($"Aluno {alunoSelecionado.Nome} removido do evento!", "Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Adiciona o aluno se ainda não estiver no evento
                    eventoSelecionado.AlunosIds.Add(alunoSelecionado.Id);
                    MessageBox.Show($"Aluno {alunoSelecionado.Nome} adicionado ao evento!", "Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            gestor.SalvarDados();
            AtualizarListaParticipantes();
            AtualizarListaEventos();
        }

        private void lstProfessores_DoubleClick(object sender, EventArgs e)
        {
            if (lstProfessores.SelectedItem == null || eventoSelecionado == null)
                return;

            string nomeProfessor = lstProfessores.SelectedItem.ToString();
            Professor professorSelecionado = gestor.Professores.FirstOrDefault(p => p.Nome == nomeProfessor);

            if (professorSelecionado != null)
            {
                if (eventoSelecionado.ProfessoresIds.Contains(professorSelecionado.Id))
                {
                    // Remove o professor se já estiver no evento
                    eventoSelecionado.ProfessoresIds.Remove(professorSelecionado.Id);
                    MessageBox.Show($"Professor {professorSelecionado.Nome} removido do evento!", "Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Adiciona o professor se ainda não estiver no evento
                    eventoSelecionado.ProfessoresIds.Add(professorSelecionado.Id);
                    MessageBox.Show($"Professor {professorSelecionado.Nome} adicionado ao evento!", "Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            gestor.SalvarDados();
            AtualizarListaParticipantes();
            AtualizarListaEventos();
        }
    }
}
