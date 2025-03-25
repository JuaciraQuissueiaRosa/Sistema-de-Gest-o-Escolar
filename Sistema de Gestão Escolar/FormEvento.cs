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

        private void btnAssociarProfessor_Click(object sender, EventArgs e)
        {
            if (eventoSelecionado == null || cmbProfessor.SelectedItem == null)
            {
                MessageBox.Show("Selecione um evento e um professor!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int professorId = (int)cmbProfessor.SelectedValue;
            gestor.AssociarProfessorEvento(eventoSelecionado.Id, professorId);
            AtualizarListaParticipantes();
        }

        private void btnRemoverProfessor_Click(object sender, EventArgs e)
        {
            if (eventoSelecionado == null || lstProfessores.SelectedItem == null)
            {
                MessageBox.Show("Selecione um evento e um professor!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int professorId = (int)lstProfessores.SelectedValue;
            eventoSelecionado.ProfessoresIds.Remove(professorId);
            gestor.SalvarDados();
            AtualizarListaParticipantes();
        }

        private void btnRemoverAluno_Click(object sender, EventArgs e)
        {

            if (eventoSelecionado == null || lstAlunos.SelectedItem == null)
            {
                MessageBox.Show("Selecione um evento e um aluno!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int alunoId = (int)lstAlunos.SelectedValue;
            eventoSelecionado.AlunosIds.Remove(alunoId);
            gestor.SalvarDados();
            AtualizarListaParticipantes();
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

        private void btnAssociarAluno_Click(object sender, EventArgs e)
        {
            if (eventoSelecionado == null || cmbAluno.SelectedItem == null)
            {
                MessageBox.Show("Selecione um evento e um aluno!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int alunoId = (int)cmbAluno.SelectedValue;
            gestor.AssociarAlunoEvento(eventoSelecionado.Id, alunoId);
            AtualizarListaParticipantes();
        }



        //-------------------------------------------

        // 🔄 Atualizar lista de eventos
        private void AtualizarListaEventos()
        {
            lstEventos.Items.Clear();
            foreach (var evento in gestor.Eventos)
            {
                lstEventos.Items.Add($"{evento.Data:dd/MM/yyyy} - {evento.Nome}");
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
        {
            cmbAluno.DataSource = gestor.Alunos;
            cmbAluno.DisplayMember = "Nome";
            cmbAluno.ValueMember = "Id";

            cmbProfessor.DataSource = gestor.Professores;
            cmbProfessor.DisplayMember = "Nome";
            cmbProfessor.ValueMember = "Id";
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

    }
}
