namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public string AreaEnsino { get; set; }
        public List<int> DisciplinasIds { get; set; } = new List<int>(); // IDs das disciplinas

        public Professor(int id, string nome, string contato, string email, string areaEnsino)
        {
            Id = id;
            Nome = nome;
            Contato = contato;
            Email = email;
            AreaEnsino = areaEnsino;
        }
    }

}
