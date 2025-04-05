using Bibilioteca_Sistema_de_Gestão_Escolar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            ConfigurarListView();
            AtualizarListaEventos();
            CarregarDados();
        }

     

        /// <summary>
        /// Configura a ListView para eventos
        /// </summary>
        private void ConfigurarListView()
        {
            lstEventos.View = View.Details;
            lstEventos.FullRowSelect = true;
            lstEventos.Columns.Add("Data", 100);
            lstEventos.Columns.Add("Nome", 150);
            lstEventos.Columns.Add("Participantes", 250);
        }
        private void btnRemoverEvento_Click(object sender, EventArgs e)
        {

            if (eventoSelecionado == null)
            {
                MessageBox.Show("Selecione um evento!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            gestor.RemoverEvento(eventoSelecionado.Id);
            AtualizarListaEventos();
            LimparCampos();
            MessageBox.Show("Evento removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    



        private void btnSelecionarEvento_Click(object sender, EventArgs e)
        {
            if (lstEventos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um evento!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            eventoSelecionado = (Evento)lstEventos.SelectedItems[0].Tag;
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

            // Coletar os novos valores do formulário
            string novoNome = txtNomeEvento.Text.Trim();
            string novaDescricao = txtDescricaoEvento.Text.Trim();
            DateTime novaData = dtpDataEvento.Value;

            // Chamar o método EditarEvento
            bool sucesso = gestor.EditarEvento(eventoSelecionado.Id, novoNome, novaDescricao, novaData);

            if (sucesso)
            {
                AtualizarListaEventos(); // Atualizar a interface gráfica
                MessageBox.Show("Evento atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao editar o evento. Verifique se ele ainda existe.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionarEvento_Click(object sender, EventArgs e)
        {
            int id = gestor.Eventos.Count + 1;
            string nome = txtNomeEvento.Text.Trim();
            string descricao = txtDescricaoEvento.Text.Trim();
            DateTime data = dtpDataEvento.Value;

            gestor.AdicionarEvento(id, nome, descricao, data);
            AtualizarListaEventos();
            MessageBox.Show("Evento cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




    
        /// <summary>
        ///  Atualiza a ListView de eventos
        /// </summary>
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

                ListViewItem item = new ListViewItem(evento.Data.ToString("dd/MM/yyyy"));
                item.SubItems.Add(evento.Nome);
                item.SubItems.Add(participantes);
                item.Tag = evento;

                lstEventos.Items.Add(item);
            }
        }


        /// <summary>
        /// Atualiza List view Alunos
        /// </summary>

        private void AtualizarListaAlunos()
        {
            lstAlunos.Items.Clear();
            foreach (var aluno in gestor.Alunos)
            {
                if (eventoSelecionado == null || !eventoSelecionado.AlunosIds.Contains(aluno.Id))
                {
                    lstAlunos.Items.Add(aluno.Nome);
                }
            }
        }


        /// <summary>
        /// Atualiza list view professores
        /// </summary>
        private void AtualizarListaProfessores()
        {
            lstProfessores.Items.Clear();
            foreach (var professor in gestor.Professores)
            {
                if (eventoSelecionado == null || !eventoSelecionado.ProfessoresIds.Contains(professor.Id))
                {
                    lstProfessores.Items.Add(professor.Nome);
                }
            }
        }
        /// <summary>
        /// 📥 Carrega os dados iniciais na lista de alunos e professores
        /// </summary>
        private void CarregarDados()
        {
            AtualizarListaAlunos();
            AtualizarListaProfessores();
        }

        /// <summary>
        ///  Limpa os campos do formulário
        /// </summary>
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
                    eventoSelecionado.AlunosIds.Remove(alunoSelecionado.Id);
                }
                else
                {
                    eventoSelecionado.AlunosIds.Add(alunoSelecionado.Id);
                }
            }

            gestor.SalvarDados();
            AtualizarListaAlunos();
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
                    eventoSelecionado.ProfessoresIds.Remove(professorSelecionado.Id);
                }
                else
                {
                    eventoSelecionado.ProfessoresIds.Add(professorSelecionado.Id);
                }
            }

            gestor.SalvarDados();
            AtualizarListaProfessores();
            AtualizarListaEventos();
        }

        private void lblListaEventos_Click(object sender, EventArgs e)
        {

        }

        private void FormEvento_Load(object sender, EventArgs e)
        {
            // Definir botões redondos

            SetRoundButton(btnSelecionarEvento);
            SetRoundButton(btnAdicionarEvento);
            SetRoundButton(btnRemoverEvento);
            SetRoundButton(btnRemoverEvento);
            SetRoundButton(btnEditarEvento);



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

        private void lstEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEventos.SelectedItems.Count == 0) return;

            eventoSelecionado = (Evento)lstEventos.SelectedItems[0].Tag;
            txtNomeEvento.Text = eventoSelecionado.Nome;
            txtDescricaoEvento.Text = eventoSelecionado.Descricao;
            dtpDataEvento.Value = eventoSelecionado.Data;

            AtualizarListaAlunos();
            AtualizarListaProfessores();
        }
    }
    
}

