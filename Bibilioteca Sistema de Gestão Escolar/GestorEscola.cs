using Bibilioteca_Sistema_de_Gestão_Escolar;
using System.Net.Mail;
using System.Net;
using System.Numerics;

public class GestorEscola
{
    private GestorPersistencia persistencia = new GestorPersistencia();

    // Listas para armazenar os dados das entidades
    public List<Horario> Horarios { get; set; } = new List<Horario>();
    public List<Aluno> Alunos { get; set; } = new List<Aluno>();
    public List<Professor> Professores { get; set; } = new List<Professor>();
    public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    public List<Turma> Turmas { get; set; } = new List<Turma>();
    public List<Nota> Notas { get; set; } = new List<Nota>();
    public List<Evento> Eventos { get; set; } = new List<Evento>();

    public List<Presenca> Presencas { get; set; } = new List<Presenca>();


    // 🔹 Carregar dados ao iniciar o programa
    public GestorEscola()
    {
        var dados = persistencia.CarregarDados();
        Alunos = dados.Item1;
        Professores = dados.Item2;
        Disciplinas = dados.Item3;
        Turmas = dados.Item4;
        Notas = dados.Item5;
        Eventos = dados.Item6;
        Horarios = dados.Item7;
        Presencas = dados.Item8;
    }

    public void SalvarDados()
    {
        persistencia.SalvarDados(Alunos, Professores, Disciplinas, Turmas, Notas, Eventos, Horarios, Presencas);
    }

    //------------- Dados para gerir faltas e presenças--------------------------

    public void RegistarPresenca(int alunoId, int disciplinaId, DateTime data, bool compareceu)
    {
        int id = Presencas.Count + 1;
        Presenca novaPresenca = new Presenca(id, alunoId, disciplinaId, data, compareceu);
        Presencas.Add(novaPresenca);
    }

    public void VerificarFaltasExcessivas()
    {
        int limiteFaltas = 5; // Limite de faltas para envio de alerta

        foreach (var aluno in Alunos)
        {
            foreach (var disciplina in Disciplinas)
            {
                int faltas = Presencas.Count(p => p.AlunoId == aluno.Id && p.DisciplinaId == disciplina.Id && !p.Compareceu);

                if (faltas >= limiteFaltas)
                {
                    EnviarEmailAlerta(aluno.Email, aluno.Nome, disciplina.Nome, faltas);
                }
            }
        }
    }

    public static void EnviarEmailAlerta(string emailAluno, string nomeAluno, string nomeDisciplina, int totalFaltas)
    {
        try
        {
            string remetente = "seuemail@gmail.com"; // Seu e-mail
            string senha = "suaSenhaGerada"; // Senha gerada no Google
            string assunto = "⚠ Alerta de Faltas Excessivas ⚠";
            string corpo = $"Prezado(a) {nomeAluno},\n\n" +
                           $"Você atingiu {totalFaltas} faltas na disciplina {nomeDisciplina}. " +
                           "Entre em contato com a coordenação.\n\nAtenciosamente,\nCoordenação Escolar";

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(remetente, senha);
                smtp.EnableSsl = true;

                using (MailMessage mensagem = new MailMessage(remetente, emailAluno, assunto, corpo))
                {
                    smtp.Send(mensagem);
                }
            }

            Console.WriteLine($"✅ E-mail enviado para {emailAluno}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao enviar e-mail: {ex.Message}");
        }
    }
    //--------------------------metodo para gerir horarios


    public void AdicionarHorario(Horario horario)
    {
        if (VerificarConflitoHorario(horario))
            throw new Exception("Conflito de horário detectado!");

        Horarios.Add(horario);
    }

    public void RemoverHorario(int id)
    {
        Horarios.RemoveAll(h => h.Id == id);
    }

    public bool VerificarConflitoHorario(Horario novoHorario)
    {
        return Horarios.Any(h =>
       h.DiaSemana == novoHorario.DiaSemana &&
       (h.TurmaId == novoHorario.TurmaId || h.ProfessorId == novoHorario.ProfessorId) &&
       h.HoraInicio < novoHorario.HoraFim &&
       novoHorario.HoraInicio < h.HoraFim);
    }

    public List<Horario> ListarHorariosPorTurma(int turmaId)
    {
        return Horarios.Where(h => h.TurmaId == turmaId).ToList();
    }

    public List<Horario> ListarHorariosPorProfessor(int professorId)
    {
        return Horarios.Where(h => h.ProfessorId == professorId).ToList();
    }

    public void EditarHorario(Horario horarioAtualizado)
    {
        for (int i = 0; i < Horarios.Count; i++)
        {
            if (Horarios[i].Id == horarioAtualizado.Id)
            {
                Horarios[i] = horarioAtualizado; // Substitui pelo novo horário
                return;
            }
        }
        throw new Exception("Horário não encontrado.");
    }
    //-------------------Metodo para Criar métodos para gerar pautas de notas por aluno e turma,  Calcular médias e estatísticas de desempenho

    // 📌 Gera a pauta de notas de um aluno
    public string GerarPautaAluno(int alunoId)
    {
        var aluno = Alunos.FirstOrDefault(a => a.Id == alunoId);
        if (aluno == null) return "Aluno não encontrado.";

        var notasAluno = Notas.Where(n => n.AlunoId == alunoId).ToList();
        if (!notasAluno.Any()) return "Nenhuma nota registrada para este aluno.";

        string pauta = $"Pauta de Notas - {aluno.Nome}\n";
        foreach (var nota in notasAluno)
        {
            var disciplina = Disciplinas.FirstOrDefault(d => d.Id == nota.DisciplinaId);
            string nomeDisciplina = disciplina != null ? disciplina.Nome : "Desconhecida";
            pauta += $"{nomeDisciplina}: {nota.ValorNota} ({nota.TipoAvaliacao})\n";
        }

        return pauta;
    }


    // 📌 Gera um relatório de desempenho da turma
    public string GerarRelatorioTurma(int turmaId)
    {
        var turma = Turmas.FirstOrDefault(t => t.Id == turmaId);
        if (turma == null) return "Turma não encontrada.";

        var alunosTurma = Alunos.Where(a => a.TurmaId == turmaId).ToList();
        if (!alunosTurma.Any()) return "Nenhum aluno nesta turma.";

        string relatorio = $"Relatório de Desempenho - {turma.Curso}\n";

        foreach (var aluno in alunosTurma)
        {
            var notasAluno = Notas.Where(n => n.AlunoId == aluno.Id).ToList();
            if (!notasAluno.Any())
            {
                relatorio += $"{aluno.Nome}: Sem notas registradas\n";
                continue;
            }

            double media = notasAluno.Average(n => n.ValorNota);
            relatorio += $"{aluno.Nome}: Média {media:F2}\n";
        }

        return relatorio;
    }



// ----------------- CRUD PARA ALUNOS -----------------

     public void AdicionarAluno(Aluno aluno)
     {
        Alunos.Add(aluno);
        SalvarDados();
     }

    public bool RemoverAluno(int id)
    {
        Aluno aluno = Alunos.FirstOrDefault(a => a.Id == id);
        if (aluno != null && !Notas.Any(n => n.AlunoId == id))
        {
            Alunos.Remove(aluno);
            SalvarDados();
            return true;
        }
        return false;
    }

    public List<Aluno> ListarAlunos()
    {
        return Alunos;
    }

    public void AtualizarAluno(int id, string novoNome, string novoContato)
    {
        Aluno aluno = Alunos.FirstOrDefault(a => a.Id == id);
        if (aluno != null)
        {
            aluno.Nome = novoNome;
            aluno.Contato = novoContato;
            SalvarDados();
        }
        else
        {
            throw new Exception("Aluno não encontrado.");
        }
    }

    public void MudarAlunoDeTurma(int alunoId, int novaTurmaId)
    {
        Aluno aluno = Alunos.FirstOrDefault(a => a.Id == alunoId);
        if (aluno != null)
        {
            aluno.TurmaId = novaTurmaId;
            SalvarDados();
        }
        else
        {
            throw new Exception("Aluno não encontrado.");
        }
    }

    public bool RemoverTurma(int id)
    {
        for (int i = 0; i < Turmas.Count; i++)
        {
            if (Turmas[i].Id == id)
            {
                // Verificar se há alunos matriculados antes de remover a turma
                if (Turmas[i].AlunosIds.Count > 0)
                {
                    return false; // Retorna falso se houver alunos matriculados
                }

                // Remover a turma da lista
                Turmas.RemoveAt(i);
                SalvarDados();
                return true; // Retorna verdadeiro se a remoção for bem-sucedida
            }
        }

        return false; // Retorna falso se a turma não for encontrada
    }

    // ----------------- CRUD PARA PROFESSORES -----------------

    public void AdicionarProfessor(Professor professor)
    {
        Professores.Add(professor);
        SalvarDados();
    }

    public bool RemoverProfessor(int id)
    {
        if (!Disciplinas.Any(d => d.ProfessoresIds.Contains(id)))
        {
            Professores.RemoveAll(p => p.Id == id);
            SalvarDados();
            return true;
        }
        return false;
    }

    public bool EditarProfessor(int id, string novoNome, string novaAreaEnsino, string novoContato, string novoEmail)
    {
        for (int i = 0; i < Professores.Count; i++)
        {
            if (Professores[i].Id == id)
            {
                if (string.IsNullOrEmpty(novoNome) || string.IsNullOrEmpty(novaAreaEnsino) ||
                    string.IsNullOrEmpty(novoContato) || string.IsNullOrEmpty(novoEmail))
                {
                    return false;
                }

                // Atualizar informações do professor
                Professores[i].Nome = novoNome;
                Professores[i].AreaEnsino = novaAreaEnsino;
                Professores[i].Contato = novoContato;
                Professores[i].Email = novoEmail;

                SalvarDados();
                return true;
            }
        }
        return false; // Retorna falso se o professor não for encontrado
    }

    // ----------------- CRUD PARA DISCIPLINAS -----------------

    public bool AdicionarDisciplina(Disciplina disciplina)
    {
        if (Disciplinas.Any(d => d.Nome.Equals(disciplina.Nome, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        Disciplinas.Add(disciplina);
        SalvarDados();
        return true;
    }

    public bool RemoverDisciplina(int id)
    {
        for (int i = 0; i < Disciplinas.Count; i++)
        {
            if (Disciplinas[i].Id == id)
            {
                // Verificar se a disciplina está associada a alguma turma
                foreach (var turma in Turmas)
                {
                    if (turma.DisciplinasIds.Contains(id))
                    {
                        return false; // A disciplina não pode ser removida pois está associada a uma turma
                    }
                }

                // Verificar se há notas registradas para a disciplina
                foreach (var nota in Notas)
                {
                    if (nota.DisciplinaId == id)
                    {
                        return false; // A disciplina não pode ser removida pois há notas registradas
                    }
                }

                // Se não houver restrições, remover a disciplina
                Disciplinas.RemoveAt(i);
                SalvarDados();
                return true; // Retorna verdadeiro indicando que a disciplina foi removida com sucesso
            }
        }

        return false; // Retorna falso se a disciplina não for encontrada
    }


    public void AssociarProfessorADisciplina(int disciplinaId, int professorId)
    {
        Disciplina disciplina = Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);
        if (disciplina != null && !disciplina.ProfessoresIds.Contains(professorId))
        {
            disciplina.ProfessoresIds.Add(professorId);
            SalvarDados();
        }
    }

    // ----------------- CRUD PARA NOTAS -----------------

    public void AdicionarNota(Nota nota)
    {
        if (VerificarSePeriodoEncerrado(nota.PeriodoLetivo))
            throw new Exception("Notas não podem ser adicionadas após o término do período letivo.");

        Notas.Add(nota);
        SalvarDados();
    }

    public bool RemoverNota(int alunoId, int disciplinaId, string periodoLetivo)
    {
        Nota nota = Notas.FirstOrDefault(n => n.AlunoId == alunoId && n.DisciplinaId == disciplinaId && n.PeriodoLetivo == periodoLetivo);
        if (nota != null)
        {
            Notas.Remove(nota);
            SalvarDados();
            return true;
        }
        return false;
    }

    // ----------------- CRUD PARA EVENTOS -----------------
    // --- Métodos para Eventos ---
    // 📌 Adicionar Evento
    public void AdicionarEvento(int id, string nome, string descricao, DateTime data)
    {
        if (Eventos.Any(e => e.Id == id))
            throw new Exception("Já existe um evento com este ID.");

        Evento novoEvento = new Evento(id, nome, descricao, data);
        Eventos.Add(novoEvento);
        SalvarDados();
    }

    // 📌 Editar Evento
    public bool EditarEvento(int id, string novoNome, string novaDescricao, DateTime novaData)
    {
        Evento evento = Eventos.FirstOrDefault(e => e.Id == id);
        if (evento == null)
            return false;

        evento.Nome = novoNome;
        evento.Descricao = novaDescricao;
        evento.Data = novaData;

        SalvarDados();
        return true;
    }

    // 📌 Remover Evento
    public bool RemoverEvento(int id)
    {
        Evento evento = Eventos.FirstOrDefault(e => e.Id == id);
        if (evento == null)
            return false;

        Eventos.Remove(evento);
        SalvarDados();
        return true;
    }

    // 📌 Associar Aluno ao Evento
    public void AssociarAlunoEvento(int eventoId, int alunoId)
    {
        Evento evento = Eventos.FirstOrDefault(e => e.Id == eventoId);
        if (evento != null && !evento.AlunosIds.Contains(alunoId))
        {
            evento.AlunosIds.Add(alunoId);
            SalvarDados();
        }
    }

    // 📌 Associar Professor ao Evento
    public void AssociarProfessorEvento(int eventoId, int professorId)
    {
        Evento evento = Eventos.FirstOrDefault(e => e.Id == eventoId);
        if (evento != null && !evento.ProfessoresIds.Contains(professorId))
        {
            evento.ProfessoresIds.Add(professorId);
            SalvarDados();
        }
    }

    // ----------------- MÉTODOS AUXILIARES -----------------

    public bool VerificarSePeriodoEncerrado(string periodoLetivo)
    {
        string[] partes = periodoLetivo.Split('/');
        if (partes.Length == 2 && int.TryParse(partes[1], out int anoFinal))
        {
            return anoFinal < DateTime.Now.Year;
        }
        return false;
    }

    public bool ValidarAnoLetivo(string anoLetivo)
    {
        try
        {
            // Verificar se o formato está correto: "AAAA/AAAA"
            string[] anos = anoLetivo.Split('/');

            if (anos.Length != 2)
            {
                return false; // Deve ter exatamente dois anos separados por "/"
            }

            // Verificar se ambos os anos são números inteiros
            if (!int.TryParse(anos[0], out int anoInicio) || !int.TryParse(anos[1], out int anoFim))
            {
                return false;
            }

            // O primeiro ano deve ser menor que o segundo (exemplo: 2023/2024)
            return anoInicio < anoFim;
        }
        catch
        {
            return false; // Em caso de erro, retorna falso sem quebrar o sistema
        }
    }
}
