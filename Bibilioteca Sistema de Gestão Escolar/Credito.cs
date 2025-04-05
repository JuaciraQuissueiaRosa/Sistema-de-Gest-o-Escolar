namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Credito
    {
        public string Autor { get; private set; }
        public string DataCriacao { get; private set; }
        public string Versao { get; private set; }

        public Credito()
        {
            Autor = "Juacira Rosa";
            DataCriacao = "06/04/2024";
            Versao = "1.0.0";
        }
    }
}
