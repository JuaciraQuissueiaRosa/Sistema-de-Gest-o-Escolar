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
                // Validar seleção de turma, professor e disciplina
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

                // ✅ 1. Validar se o dia da semana NÃO é sábado ou domingo
                if (diaSemana == DayOfWeek.Saturday || diaSemana == DayOfWeek.Sunday)
                {
                    MessageBox.Show("As aulas só podem ser marcadas de segunda a sexta-feira.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 2. Validar horário permitido (08h30 às 22h30)
                TimeSpan horarioMinimo = new TimeSpan(8, 30, 0);
                TimeSpan horarioMaximo = new TimeSpan(22, 30, 0);

                if (horaInicio < horarioMinimo || horaFim > horarioMaximo)
                {
                    MessageBox.Show("As aulas devem ser marcadas entre 08h30 e 22h30.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 3. Validar se a hora de início é menor que a hora de fim
                if (horaInicio >= horaFim)
                {
                    MessageBox.Show("A hora de início deve ser menor que a hora de fim.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Criar objeto Horário
                Horario novoHorario = new Horario(gestor.Horarios.Count + 1, disciplinaId, professorId, turmaId, diaSemana, horaInicio, horaFim);

                // ✅ 4. Verificar conflitos de horários
                if (gestor.VerificarConflitoHorario(novoHorario))
                {
                    MessageBox.Show("Conflito de horário detectado! O professor ou a turma já tem uma aula nesse horário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Adicionar horário
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
            if (lstHorarios.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um horário para remover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)lstHorarios.SelectedItems[0].Tag;
            gestor.RemoverHorario(id);
            gestor.SalvarDados();
            AtualizarListaHorarios();
        }


        private void AtualizarListaHorarios()
        {
            lstHorarios.Items.Clear();

            foreach (var horario in gestor.Horarios)
            {
                var turma = gestor.Turmas.FirstOrDefault(t => t.Id == horario.TurmaId)?.Curso ?? "Turma não encontrada";
                var professor = gestor.Professores.FirstOrDefault(p => p.Id == horario.ProfessorId)?.Nome ?? "Professor não encontrado";
                var disciplina = gestor.Disciplinas.FirstOrDefault(d => d.Id == horario.DisciplinaId)?.Nome ?? "Disciplina não encontrada";

                ListViewItem item = new ListViewItem(new[] {
                horario.DiaSemana.ToString(),
                horario.HoraInicio.ToString(@"hh\:mm"),
                horario.HoraFim.ToString(@"hh\:mm"),
                disciplina,
                professor,
                turma
            });

                item.Tag = horario.Id;
                lstHorarios.Items.Add(item);
            }
        }

        private void btnEditarHorario_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ 1. Validar se um horário foi selecionado
                if (lstHorarios.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Selecione um horário para editar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 2. Obter o ID do horário selecionado
                int id = (int)lstHorarios.SelectedItems[0].Tag;
                Horario horarioSelecionado = gestor.Horarios.FirstOrDefault(h => h.Id == id);

                if (horarioSelecionado == null)
                {
                    MessageBox.Show("Erro ao encontrar o horário selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ 3. Obter os novos valores do formulário
                int turmaId = (int)cmbTurma.SelectedValue;
                int professorId = (int)cmbProfessor.SelectedValue;
                int disciplinaId = (int)cmbDisciplina.SelectedValue;
                DayOfWeek diaSemana = (DayOfWeek)cmbDiaSemana.SelectedItem;
                TimeSpan horaInicio = dtpHoraInicio.Value.TimeOfDay;
                TimeSpan horaFim = dtpHoraFim.Value.TimeOfDay;

                // ✅ 4. Validar Finais de Semana
                if (diaSemana == DayOfWeek.Saturday || diaSemana == DayOfWeek.Sunday)
                {
                    MessageBox.Show("As aulas só podem ser marcadas de segunda a sexta-feira.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 5. Validar Horário Permitido
                TimeSpan horarioMinimo = new TimeSpan(8, 30, 0);
                TimeSpan horarioMaximo = new TimeSpan(22, 30, 0);

                if (horaInicio < horarioMinimo || horaFim > horarioMaximo)
                {
                    MessageBox.Show("As aulas devem ser marcadas entre 08h30 e 22h30.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (horaInicio >= horaFim)
                {
                    MessageBox.Show("A hora de início deve ser menor que a hora de fim.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 6. Criar objeto atualizado
                Horario horarioAtualizado = new Horario(id, disciplinaId, professorId, turmaId, diaSemana, horaInicio, horaFim);

                // ✅ 7. Verificar Conflito de Horário
                if (gestor.VerificarConflitoHorario(horarioAtualizado))
                {
                    MessageBox.Show("Conflito de horário detectado! O professor ou a turma já tem uma aula nesse horário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 8. Atualizar o horário no sistema
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
