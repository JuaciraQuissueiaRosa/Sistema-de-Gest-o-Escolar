namespace Bibilioteca_Sistema_de_Gestão_Escolar
{
    public class Nota
    {
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public double ValorNota { get; set; }
        public string PeriodoLetivo { get; set; } // Ex: "1º Trimestre"

        public string TipoAvaliacao { get; set; } // Novo campo

        public Nota(int alunoId, int disciplinaId, double valorNota, string periodoLetivo, string tipoAvaliacao)
        {
            AlunoId = alunoId;
            DisciplinaId = disciplinaId;
            ValorNota = valorNota;
            PeriodoLetivo = periodoLetivo;
            TipoAvaliacao = tipoAvaliacao;
        }
    }

}
