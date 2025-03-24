using Bibilioteca_Sistema_de_Gestão_Escolar;

public class GestorPersistencia
{
    private const string PastaDados = "Dados";

    private string CaminhoArquivo(string nomeArquivo) => Path.Combine(PastaDados, nomeArquivo);

    public GestorPersistencia()
    {
        // Criar diretório "Dados" caso não exista
        if (!Directory.Exists(PastaDados))
            Directory.CreateDirectory(PastaDados);
    }

    public (List<Aluno>, List<Professor>, List<Disciplina>, List<Turma>, List<Nota>) CarregarDados()
    {
        List<Aluno> alunos = new List<Aluno>();
        List<Professor> professores = new List<Professor>();
        List<Disciplina> disciplinas = new List<Disciplina>();
        List<Turma> turmas = new List<Turma>();
        List<Nota> notas = new List<Nota>();

        // Carregar notas primeiro para vincular corretamente aos alunos depois
        if (File.Exists(CaminhoArquivo("notas.txt")))
        {
            foreach (var linha in File.ReadAllLines(CaminhoArquivo("notas.txt")))
            {
                var partes = linha.Split(';');
                if (partes.Length == 5)
                {
                    notas.Add(new Nota(
                        int.Parse(partes[0]), int.Parse(partes[1]), double.Parse(partes[2]), partes[3], partes[4]
                    ));
                }
            }
        }

        // Carregar alunos
        if (File.Exists(CaminhoArquivo("alunos.txt")))
        {
            foreach (var linha in File.ReadAllLines(CaminhoArquivo("alunos.txt")))
            {
                var partes = linha.Split(';');
                if (partes.Length >= 7)
                {
                    var aluno = new Aluno(
                        int.Parse(partes[0]), partes[1], DateTime.Parse(partes[2]),
                        partes[3], partes[4], partes[5], int.Parse(partes[6])
                    );

                    // Associar notas ao aluno
                    aluno.Notas = notas.Where(n => n.AlunoId == aluno.Id).ToList();
                    alunos.Add(aluno);
                }
            }
        }

        // Carregar professores
        if (File.Exists(CaminhoArquivo("professores.txt")))
        {
            foreach (var linha in File.ReadAllLines(CaminhoArquivo("professores.txt")))
            {
                var partes = linha.Split(';');
                if (partes.Length == 6)
                {
                    var professor = new Professor(int.Parse(partes[0]), partes[1], partes[2], partes[3], partes[4]);
                    professor.DisciplinasIds = partes[5].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    professores.Add(professor);
                }
            }
        }

        // Carregar disciplinas
        if (File.Exists(CaminhoArquivo("disciplinas.txt")))
        {
            foreach (var linha in File.ReadAllLines(CaminhoArquivo("disciplinas.txt")))
            {
                var partes = linha.Split(';');
                if (partes.Length == 5)
                {
                    var disciplina = new Disciplina(int.Parse(partes[0]), partes[1], int.Parse(partes[2]));
                    disciplina.ProfessoresIds = partes[3].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    disciplina.TurmasIds = partes[4].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    disciplinas.Add(disciplina);
                }
            }
        }

        // Carregar turmas
        if (File.Exists(CaminhoArquivo("turmas.txt")))
        {
            foreach (var linha in File.ReadAllLines(CaminhoArquivo("turmas.txt")))
            {
                var partes = linha.Split(';');
                if (partes.Length == 6)
                {
                    var turma = new Turma(int.Parse(partes[0]), partes[1], partes[2], partes[3]);
                    turma.AlunosIds = partes[4].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    turma.DisciplinasIds = partes[5].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    turmas.Add(turma);
                }
            }
        }

        return (alunos, professores, disciplinas, turmas, notas);
    }
}
