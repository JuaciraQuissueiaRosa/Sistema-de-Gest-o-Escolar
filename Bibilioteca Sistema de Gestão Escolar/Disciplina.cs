namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public List<int> ProfessoresIds { get; set; } = new List<int>(); // IDs dos professores
        public List<int> TurmasIds { get; set; } = new List<int>(); // IDs das turmas

        public Disciplina(int id, string nome, int cargaHoraria)
        {
            Id = id;
            Nome = nome;
            CargaHoraria = cargaHoraria;
        }
    }

}
