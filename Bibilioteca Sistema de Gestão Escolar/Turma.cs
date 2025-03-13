using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Turma
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public string AnoLetivo { get; set; }
        public string Turno { get; set; } // Diurno / Noturno
        public List<int> AlunosIds { get; set; } = new List<int>(); // IDs dos alunos
        public List<int> DisciplinasIds { get; set; } = new List<int>(); // IDs das disciplinas

        public Turma(int id, string curso, string anoLetivo, string turno)
        {
            Id = id;
            Curso = curso;
            AnoLetivo = anoLetivo;
            Turno = turno;
        }
    }

}
