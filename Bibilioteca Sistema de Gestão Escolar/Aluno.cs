using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Contato { get; set; }
        public string Morada { get; set; }
        public string Email { get; set; }
        public int TurmaId { get; set; } // ID da turma
        public List<Nota> Notas { get; set; } = new List<Nota>(); // Histórico de notas

        public Aluno(int id, string nome, DateTime dataNascimento, string contato, string morada, string email, int turmaId)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Contato = contato;
            Morada = morada;
            Email = email;
            TurmaId = turmaId;
        }
    }

}
