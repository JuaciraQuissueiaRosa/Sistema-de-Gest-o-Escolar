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
    public partial class FormHorario : Form
    {
        private GestorEscola gestor;

        public FormHorario(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            CarregarDados();
        }

        private void CarregarDados()
        {
            // Carregar turmas
            cmbTurma.DataSource = gestor.Turmas;
            cmbTurma.DisplayMember = "Curso";
            cmbTurma.ValueMember = "Id";

            // Carregar professores
            cmbProfessor.DataSource = gestor.Professores;
            cmbProfessor.DisplayMember = "Nome";
            cmbProfessor.ValueMember = "Id";

            // Carregar disciplinas
            cmbDisciplina.DataSource = gestor.Disciplinas;
            cmbDisciplina.DisplayMember = "Nome";
            cmbDisciplina.ValueMember = "Id";

            // Carregar dias da semana
            cmbDiaSemana.DataSource = Enum.GetValues(typeof(DayOfWeek));

            // Configurar DateTimePicker para selecionar apenas a hora
            dtpHoraInicio.Format = DateTimePickerFormat.Time;
            dtpHoraInicio.ShowUpDown = true;
            dtpHoraFim.Format = DateTimePickerFormat.Time;
            dtpHoraFim.ShowUpDown = true;

            AtualizarListaHorarios();
        }

        private void btnAdicionarHorario_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTurma.SelectedValue == null || cmbProfessor.SelectedValue == null || cmbDisciplina.SelectedValue == null)
                {
                    MessageBox.Show("Selecione uma turma, professor e disciplina.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int turmaId = (int)cmbTurma.SelectedValue;
                int professorId = (int)cmbProfessor.SelectedValue;
                int disciplinaId = (int)cmbDisciplina.SelectedValue;
                DayOfWeek diaSemana = (DayOfWeek)cmbDiaSemana.SelectedItem;
                TimeSpan horaInicio = dtpHoraInicio.Value.TimeOfDay;
                TimeSpan horaFim = dtpHoraFim.Value.TimeOfDay;

                if (!ValidarHorario(diaSemana, horaInicio, horaFim)) return;

                Horario novoHorario = new Horario(gestor.Horarios.Count + 1, disciplinaId, professorId, turmaId, diaSemana, horaInicio, horaFim);

                gestor.AdicionarHorario(novoHorario);
                gestor.SalvarDados();

                MessageBox.Show("Horário adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarListaHorarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar horário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoverHorario_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstHorarios.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Selecione um horário para remover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = (int)lstHorarios.SelectedItems[0].Tag;

                gestor.RemoverHorario(id);
                gestor.SalvarDados();

                MessageBox.Show("Horário removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarListaHorarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover horário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AtualizarListaHorarios()
        {
            lstHorarios.Items.Clear();

            foreach (var horario in gestor.Horarios)
            {
                string turma = gestor.Turmas.FirstOrDefault(t => t.Id == horario.TurmaId)?.Curso ?? "Turma não encontrada";
                string professor = gestor.Professores.FirstOrDefault(p => p.Id == horario.ProfessorId)?.Nome ?? "Professor não encontrado";
                string disciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == horario.DisciplinaId)?.Nome ?? "Disciplina não encontrada";

                string descricaoHorario = $"{horario.DiaSemana} - {horario.HoraInicio:hh\\:mm} às {horario.HoraFim:hh\\:mm}";

                ListViewItem item = new ListViewItem(descricaoHorario); // Coluna 0: Horário
                item.SubItems.Add(disciplina);  // Coluna 1: Disciplina
                item.SubItems.Add(professor);   // Coluna 2: Professor
                item.SubItems.Add(turma);       // Coluna 3: Turma

                item.Tag = horario.Id;
                lstHorarios.Items.Add(item);
            }
        }

        private bool ValidarHorario(DayOfWeek diaSemana, TimeSpan horaInicio, TimeSpan horaFim)
        {
            if (diaSemana == DayOfWeek.Saturday || diaSemana == DayOfWeek.Sunday)
            {
                MessageBox.Show("As aulas só podem ser marcadas de segunda a sexta-feira.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            TimeSpan horarioMinimo = new TimeSpan(8, 30, 0);
            TimeSpan horarioMaximo = new TimeSpan(22, 30, 0);

            if (horaInicio < horarioMinimo || horaFim > horarioMaximo)
            {
                MessageBox.Show("As aulas devem ser marcadas entre 08h30 e 22h30.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (horaInicio >= horaFim)
            {
                MessageBox.Show("A hora de início deve ser menor que a hora de fim.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnEditarHorario_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstHorarios.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Selecione um horário para editar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = (int)lstHorarios.SelectedItems[0].Tag;
                Horario horarioSelecionado = gestor.Horarios.FirstOrDefault(h => h.Id == id);

                if (horarioSelecionado == null)
                {
                    MessageBox.Show("Erro ao encontrar o horário selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int turmaId = (int)cmbTurma.SelectedValue;
                int professorId = (int)cmbProfessor.SelectedValue;
                int disciplinaId = (int)cmbDisciplina.SelectedValue;
                DayOfWeek diaSemana = (DayOfWeek)cmbDiaSemana.SelectedItem;
                TimeSpan horaInicio = dtpHoraInicio.Value.TimeOfDay;
                TimeSpan horaFim = dtpHoraFim.Value.TimeOfDay;

                if (!ValidarHorario(diaSemana, horaInicio, horaFim)) return;

                Horario horarioAtualizado = new Horario(id, disciplinaId, professorId, turmaId, diaSemana, horaInicio, horaFim);

                gestor.EditarHorario(horarioAtualizado);
                gestor.SalvarDados();

                MessageBox.Show("Horário atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarListaHorarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar horário: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHorarios.SelectedItems.Count == 0)
                return;

            int id = (int)lstHorarios.SelectedItems[0].Tag;
            Horario horarioSelecionado = gestor.Horarios.FirstOrDefault(h => h.Id == id);

            if (horarioSelecionado != null)
            {
                cmbTurma.SelectedValue = horarioSelecionado.TurmaId;
                cmbProfessor.SelectedValue = horarioSelecionado.ProfessorId;
                cmbDisciplina.SelectedValue = horarioSelecionado.DisciplinaId;
                cmbDiaSemana.SelectedItem = horarioSelecionado.DiaSemana;
                dtpHoraInicio.Value = DateTime.Today.Add(horarioSelecionado.HoraInicio);
                dtpHoraFim.Value = DateTime.Today.Add(horarioSelecionado.HoraFim);
            }
        }

        private void FormHorario_Load(object sender, EventArgs e)
        {  
            // Configuração da ListView
            lstHorarios.View = View.Details; // Exibir detalhes com colunas
            lstHorarios.FullRowSelect = true; // Selecionar a linha toda
            lstHorarios.GridLines = true; // Exibir linhas de grade

            // Definir colunas
            lstHorarios.Columns.Clear();
            lstHorarios.Columns.Add("Horário", 300);
            lstHorarios.Columns.Add("Disciplina", 300);
            lstHorarios.Columns.Add("Professor", 300);
            lstHorarios.Columns.Add("Turma", 300);

            // Atualizar a lista
            AtualizarListaHorarios();

            // Definir botões redondos
            SetRoundButton(btnEditarHorario);
            SetRoundButton(btnRemoverHorario);
            SetRoundButton(btnAdicionarHorario);
           
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
