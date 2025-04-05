using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
     
        public DateTime Data { get; set; }

        // Armazena apenas os IDs dos alunos e professores para facilitar a persistência
        public List<int> AlunosIds { get; set; } = new List<int>();
        public List<int> ProfessoresIds { get; set; } = new List<int>();

        public Evento(int id, string nome, DateTime data)
        {
            Id = id;
            Nome = nome;
          
            Data = data;
        }

    }
}
