using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Presenca
    {
        public int Id { get; set; }
        public List<int> AlunosIds { get; set; }
        public List<int> DisciplinasIds { get; set; }
        public DateTime Data { get; set; }
        public bool Compareceu { get; set; } // true = presente, false = falta

        public Presenca(int id, List<int> alunosIds, List<int> disciplinasIds, DateTime data, bool compareceu)
        {
            Id = id;
            AlunosIds = alunosIds;
            DisciplinasIds = disciplinasIds;
            Data = data;
            Compareceu = compareceu;
        }
    }
}

