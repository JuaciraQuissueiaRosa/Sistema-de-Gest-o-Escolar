using Bibilioteca_Sistema_de_Gestão_Escolar;

public class GestorPersistencia
{
    private const string PastaDados = "Dados";

    private string CaminhoArquivo(string nomeArquivo) => Path.Combine(PastaDados, nomeArquivo);

    public (List<Aluno>, List<Professor>, List<Disciplina>, List<Turma>, List<Nota>, List<Evento>, List<Horario> ) CarregarDados()
    {
        List<Aluno> alunos = new List<Aluno>();
        List<Professor> professores = new List<Professor>();
        List<Disciplina> disciplinas = new List<Disciplina>();
        List<Turma> turmas = new List<Turma>();
        List<Nota> notas = new List<Nota>();
        List<Evento> eventos = new List<Evento>();
        List<Horario> horarios = new List<Horario>();
      

        // Criar diretório caso não exista
        if (!Directory.Exists(PastaDados))
            Directory.CreateDirectory(PastaDados);

        // Carregar alunos
        if (File.Exists(CaminhoArquivo("alunos.txt")))
        {
            foreach (var linha in File.ReadAllLines(CaminhoArquivo("alunos.txt")))
            {
                var partes = linha.Split(';');
                if (partes.Length >= 7)
                {
                    alunos.Add(new Aluno(
                        int.Parse(partes[0]), partes[1], DateTime.Parse(partes[2]),
                        partes[3], partes[4], partes[5], int.Parse(partes[6])
                    ));
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

        // Carregar notas
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

        // Carregar eventos
        if (File.Exists(CaminhoArquivo("eventos.txt")))
        {
            foreach (var linha in File.ReadAllLines(CaminhoArquivo("eventos.txt")))
            {
                var partes = linha.Split(';');
                if (partes.Length >= 4)
                {
                    var evento = new Evento(
                        int.Parse(partes[0]), partes[1], partes[2], DateTime.Parse(partes[3])
                    );

                    if (partes.Length > 4)
                    {
                        evento.AlunosIds = partes[4].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    }

                    if (partes.Length > 5)
                    {
                        evento.ProfessoresIds = partes[5].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    }

                    eventos.Add(evento);
                }
            }
        }

        if (File.Exists("Dados/horarios.txt"))
        {
            foreach (var linha in File.ReadAllLines("Dados/horarios.txt"))
            {
                var partes = linha.Split(';');
                if (partes.Length == 7)
                {
                    horarios.Add(new Horario(
                        int.Parse(partes[0]), int.Parse(partes[1]), int.Parse(partes[2]), int.Parse(partes[3]),
                        (DayOfWeek)int.Parse(partes[4]), TimeSpan.Parse(partes[5]), TimeSpan.Parse(partes[6])
                    ));
                }
            }
        }
   
        return (alunos, professores, disciplinas, turmas, notas, eventos, horarios);
    }

    public void SalvarDados(List<Aluno> alunos, List<Professor> professores, List<Disciplina> disciplinas, List<Turma> turmas, List<Nota> notas, List<Evento> eventos,
         List<Horario> horarios)
    {
        if (!Directory.Exists(PastaDados))
            Directory.CreateDirectory(PastaDados);

        // Salvar alunos
        using (StreamWriter sw = new StreamWriter(CaminhoArquivo("alunos.txt")))
        {
            foreach (var aluno in alunos)
            {
                sw.WriteLine($"{aluno.Id};{aluno.Nome};{aluno.DataNascimento:yyyy-MM-dd};{aluno.Contato};{aluno.Morada};{aluno.Email};{aluno.TurmaId}");
            }
        }

        // Salvar professores
        using (StreamWriter sw = new StreamWriter(CaminhoArquivo("professores.txt")))
        {
            foreach (var professor in professores)
            {
                sw.WriteLine($"{professor.Id};{professor.Nome};{professor.Contato};{professor.Email};{professor.AreaEnsino}");
            }
        }

        // Salvar disciplinas
        using (StreamWriter sw = new StreamWriter(CaminhoArquivo("disciplinas.txt")))
        {
            foreach (var disciplina in disciplinas)
            {
                sw.WriteLine($"{disciplina.Id};{disciplina.Nome};{disciplina.CargaHoraria};{string.Join(",", disciplina.ProfessoresIds)};{string.Join(",", disciplina.TurmasIds)}");
            }
        }

        // Salvar turmas
        using (StreamWriter sw = new StreamWriter(CaminhoArquivo("turmas.txt")))
        {
            foreach (var turma in turmas)
            {
                sw.WriteLine($"{turma.Id};{turma.Curso};{turma.AnoLetivo};{turma.Turno};{string.Join(",", turma.AlunosIds)};{string.Join(",", turma.DisciplinasIds)}");
            }
        }

        // Salvar notas
        using (StreamWriter sw = new StreamWriter(CaminhoArquivo("notas.txt")))
        {
            foreach (var nota in notas)
            {
                sw.WriteLine($"{nota.AlunoId};{nota.DisciplinaId};{nota.ValorNota};{nota.PeriodoLetivo};{nota.TipoAvaliacao}");
            }
        }

        // Salvar eventos
        using (StreamWriter sw = new StreamWriter(CaminhoArquivo("eventos.txt")))
        {
            foreach (var evento in eventos)
            {
                string alunosIds = string.Join(",", evento.AlunosIds);
                string professoresIds = string.Join(",", evento.ProfessoresIds);
                sw.WriteLine($"{evento.Id};{evento.Nome};{evento.Descricao};{evento.Data:yyyy-MM-dd};{alunosIds};{professoresIds}");
            }
        }

        // Salvar dados em arquivos .txt
        using (StreamWriter sw = new StreamWriter(CaminhoArquivo("pautas.txt")))
        {
            foreach (var aluno in alunos)
            {
                sw.WriteLine($"{aluno.Id};{aluno.Nome};{aluno.TurmaId}");
            }
        }

        using (StreamWriter sw = new StreamWriter(CaminhoArquivo("notas.txt")))
        {
            foreach (var nota in notas)
            {
                sw.WriteLine($"{nota.AlunoId};{nota.DisciplinaId};{nota.ValorNota};{nota.PeriodoLetivo};{nota.TipoAvaliacao}");
            }
        }

        using (StreamWriter sw = new StreamWriter("Dados/horarios.txt"))
        {
            foreach (var horario in horarios)
            {
                sw.WriteLine($"{horario.Id};{horario.DisciplinaId};{horario.ProfessorId};{horario.TurmaId};{(int)horario.DiaSemana};{horario.HoraInicio};{horario.HoraFim}");
            }
        }
   


    }
}

