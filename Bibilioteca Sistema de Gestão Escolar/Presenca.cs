using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Presenca
    {
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public DateTime Data { get; set; }
        public bool Presente { get; set; }

        public Presenca(int alunoId, int disciplinaId, DateTime data, bool presente)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
            Data = data;
            Presente = presente;
        }
    }
}
