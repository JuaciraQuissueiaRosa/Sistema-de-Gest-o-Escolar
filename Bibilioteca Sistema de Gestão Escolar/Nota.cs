using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Nota
    {
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public double ValorNota { get; set; }
        public string PeriodoLetivo { get; set; } // Ex: "1º Trimestre"

        public Nota(int alunoId, int disciplinaId, double valorNota, string periodoLetivo)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
            ValorNota = valorNota;
            PeriodoLetivo = periodoLetivo;
        }
    }

}
