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
        public List<Aluno> Alunos { get; set; } // Agora pode armazenar vários alunos
        public List<Disciplina> Disciplinas { get; set; } // Agora pode armazenar várias disciplinas
        public DateTime Data { get; set; }
        public bool Compareceu { get; set; }

        public Presenca(int id, List<Aluno> alunos, List<Disciplina> disciplinas, DateTime data, bool compareceu)
        {
            Id = id;
            Alunos = alunos;
            Disciplinas = disciplinas;
            Data = data;
            Compareceu = compareceu;
        }
    }
}
