using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Horario
    {
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public int ProfessorId { get; set; }
        public int TurmaId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public Horario(int id, int disciplinaId, int professorId, int turmaId, DayOfWeek diaSemana, TimeSpan horaInicio, TimeSpan horaFim)
        {
            Id = id;
            DisciplinaId = disciplinaId;
            ProfessorId = professorId;
            TurmaId = turmaId;
            DiaSemana = diaSemana;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }
    }
}
