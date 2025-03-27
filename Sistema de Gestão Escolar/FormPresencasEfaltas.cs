using Bibilioteca_Sistema_de_Gestão_Escolar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormPresencasEfaltas : Form
    {
        private List<Aluno> alunos;
        private List<Disciplina> disciplinas;
        private List<Presenca> presencas;
        private GestorPersistencia gestorPersistencia;

        public FormPresencasEfaltas(List<Aluno> alunos, List<Disciplina> disciplinas, List<Presenca> presencas)
        {
            InitializeComponent();
            this.alunos = alunos;
            this.disciplinas = disciplinas;
            this.presencas = presencas;
            CarregarDados();
            ConfigurarDataGridView();
        }

        private void CarregarDados()
        {
            // Carregar os dados do gestor de persistência
            var dados = gestorPersistencia.CarregarDados();
            alunos = dados.Item1;
            disciplinas = dados.Item3;
            presencas = dados.Item8;

            // Preencher ComboBox de Alunos
            cbAlunos.DataSource = alunos;
            cbAlunos.DisplayMember = "Nome";
            cbAlunos.ValueMember = "Id";

            // Preencher ComboBox de Disciplinas
            cbDisciplinas.DataSource = disciplinas;
            cbDisciplinas.DisplayMember = "Nome";
            cbDisciplinas.ValueMember = "Id";

            // Atualizar DataGridView
            AtualizarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvPresencas.AutoGenerateColumns = false;
            dgvPresencas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Aluno",
                DataPropertyName = "NomeAluno",
                Width = 150
            });
            dgvPresencas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Disciplina",
                DataPropertyName = "NomeDisciplina",
                Width = 150
            });
            dgvPresencas.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Data",
                DataPropertyName = "Data",
                Width = 100
            });
            dgvPresencas.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Compareceu",
                DataPropertyName = "Compareceu",
                Width = 80
            });
        }

        private void AtualizarDataGridView()
        {
            var listaPresencas = presencas.Select(p => new
            {
                NomeAluno = alunos.FirstOrDefault(a => a.Id == p.AlunoId)?.Nome,
                NomeDisciplina = disciplinas.FirstOrDefault(d => d.Id == p.DisciplinaId)?.Nome,
                Data = p.Data.ToString("dd/MM/yyyy"),
                p.Compareceu
            }).ToList();

            dgvPresencas.DataSource = listaPresencas;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cbAlunos.SelectedValue == null || cbDisciplinas.SelectedValue == null)
            {
                MessageBox.Show("Selecione um aluno e uma disciplina.");
                return;
            }

            int alunoId = (int)cbAlunos.SelectedValue;
            int disciplinaId = (int)cbDisciplinas.SelectedValue;
            DateTime data = dtpData.Value.Date;
            bool compareceu = chkCompareceu.Checked;

            // Verificar se já existe um registro para este aluno, disciplina e data
            var presencaExistente = presencas.FirstOrDefault(p =>
                p.AlunoId == alunoId && p.DisciplinaId == disciplinaId && p.Data == data);

            if (presencaExistente != null)
            {
                MessageBox.Show("Já existe um registro de presença para este aluno nesta disciplina e data.");
                return;
            }

            // Criar nova presença
            int novoId = presencas.Any() ? presencas.Max(p => p.Id) + 1 : 1;
            var novaPresenca = new Presenca(novoId, alunoId, disciplinaId, data, compareceu);
            presencas.Add(novaPresenca);

            // Salvar dados
            gestorPersistencia.SalvarDados(alunos, new List<Professor>(), disciplinas, new List<Turma>(), new List<Nota>(), new List<Evento>(), new List<Horario>(), presencas);

            // Atualizar DataGridView
            AtualizarDataGridView();

            MessageBox.Show("Presença registrada com sucesso.");
        }

        private void FormPresencasEfaltas_Load(object sender, EventArgs e)
        {

            // Definir botões redondos

            SetRoundButton(btnRegistrar);
  
     
         


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

    }
}


