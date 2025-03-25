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
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

        // Armazena apenas os IDs dos alunos e professores para facilitar a persistência
        public List<int> AlunosIds { get; set; } = new List<int>();
        public List<int> ProfessoresIds { get; set; } = new List<int>();

        public Evento(int id, string nome, string descricao, DateTime data)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Data = data;
        }

        // Método para associar aluno ao evento (evita duplicação)
        public void AdicionarAluno(int alunoId)
        {
            if (!AlunosIds.Contains(alunoId))
            {
                AlunosIds.Add(alunoId);
            }
        }

        // Método para associar professor ao evento (evita duplicação)
        public void AdicionarProfessor(int professorId)
        {
            if (!ProfessoresIds.Contains(professorId))
            {
                ProfessoresIds.Add(professorId);
            }
        }
    }
}
