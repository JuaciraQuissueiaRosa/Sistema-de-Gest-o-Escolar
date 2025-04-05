using Bibilioteca_Sistema_de_Gestão_Escolar;
using System.Net.Mail;
using System.Net;
using System.Numerics;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

public class GestorEscola
{

    // instãncia do gestor persistência para que se carregue e salve os dados introduzidos
    private GestorPersistencia persistencia = new GestorPersistencia();

    // Listas para armazenar os dados das entidades
    public List<Horario> Horarios { get; set; } = new List<Horario>();
    public List<Aluno> Alunos { get; set; } = new List<Aluno>();
    public List<Professor> Professores { get; set; } = new List<Professor>();
    public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    public List<Turma> Turmas { get; set; } = new List<Turma>();
    public List<Nota> Notas { get; set; } = new List<Nota>();
    public List<Evento> Eventos { get; set; } = new List<Evento>();



    /// <summary>
    ///  // 🔹 Carregar dados 
    /// </summary>
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
      
    }

    /// <summary>
    ///Método para salvar os dados
    /// </summary>
    public void SalvarDados()
    {
        persistencia.SalvarDados(Alunos, Professores, Disciplinas, Turmas, Notas, Eventos, Horarios);
    }

    


    //--------------------------metodo para gerir horarios


    /// <summary>
    /// Metodo que adiciona horário
    /// </summary>
    /// <param name="horario"></param>
    /// <exception cref="Exception"></exception>
    public void AdicionarHorario(Horario horario)
    {
        if (VerificarConflitoHorario(horario))
            throw new Exception("Conflito de horário detectado!");

        Horarios.Add(horario);
    }

    /// <summary>
    /// Metodo que remove horário
    /// </summary>
    /// <param name="id"></param>
    public void RemoverHorario(int id)
    {
        Horarios.RemoveAll(h => h.Id == id);
    }

    /// <summary>
    /// Metodo que verifica conflito de horários
    /// </summary>
    /// <param name="novoHorario"></param>
    /// <returns></returns>
    public bool VerificarConflitoHorario(Horario novoHorario)
    {
        return Horarios.Any(h =>
       h.DiaSemana == novoHorario.DiaSemana &&
       (h.TurmaId == novoHorario.TurmaId || h.ProfessorId == novoHorario.ProfessorId) &&
       h.HoraInicio < novoHorario.HoraFim &&
       novoHorario.HoraInicio < h.HoraFim);
    }

   
    /// <summary>
    /// Metodo para editar horario
    /// </summary>
    /// <param name="horarioAtualizado"></param>
    /// <exception cref="Exception"></exception>
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


    //-------------------Metodo para Criar métodos para gerar pautas de notas por aluno e turma, calcular médias e estatísticas de desempenho

 

    /// <summary>
    ///  Gera a pauta de notas de um aluno
    /// </summary>
    /// <param name="alunoId"></param>
    /// <returns></returns>
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

    /// <summary>
    ///   Gera um relatório de desempenho da turma
    /// </summary>
    /// <param name="turmaId"></param>
    /// <returns></returns>


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

    /// <summary>
    /// METODO PARA ADICIONAR ALUNO
    /// </summary>
    /// <param name="aluno"></param>
     public void AdicionarAluno(Aluno aluno)
     {
        Alunos.Add(aluno);
        SalvarDados();
     }


    /// <summary>
    /// METODO PARA REMOVER ALUNO
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// MÉTODO PARA ATUALIZAR ALUNO
    /// </summary>
    /// <param name="id"></param>
    /// <param name="novoNome"></param>
    /// <param name="novoContato"></param>
    /// <param name="dataDeNascimento"></param>
    /// <param name="email"></param>
    /// <param name="morada"></param>
    /// <exception cref="Exception"></exception>
    public void AtualizarAluno(int id, string novoNome, string novoContato, DateTime dataDeNascimento, string email, string morada)
    {
        Aluno aluno = Alunos.FirstOrDefault(a => a.Id == id);
        if (aluno != null)
        {
            aluno.Nome = novoNome;
            aluno.Contato = novoContato;
            aluno.DataNascimento = dataDeNascimento;
            aluno.Email = email;
            aluno.Morada = morada;
            SalvarDados();
        }
        else
        {
            throw new Exception("Aluno não encontrado.");
        }
    }

    /// <summary>
    /// METODO PARA ADICIONAR ALUNO A TURMA DISPONIVEL PELO FORM ALUNO: botão 
    /// </summary>
    /// <param name="novaTurma"></param>
    /// <returns></returns>

    // Método para adicionar uma nova turma
    public bool AdicionarTurma(Turma novaTurma)
    {
        // Verifica se já existe uma turma com o mesmo ID
        if (Turmas.Any(t => t.Id == novaTurma.Id))
        {
            return false; // Retorna falso se a turma já existir
        }

        // Adiciona a turma na lista
        Turmas.Add(novaTurma);
        SalvarDados(); // Salva os dados após adicionar a turma
        return true;
    }

    /// <summary>
    /// Método para editar uma turma existente
    /// </summary>
    /// <param name="id"></param>
    /// <param name="novoCurso"></param>
    /// <param name="novoAnoLetivo"></param>
    /// <param name="novoTurno"></param>
    /// <returns></returns>
   
    public bool EditarTurma(int id, string novoCurso, string novoAnoLetivo, string novoTurno)
    {
        // Encontrar a turma com o ID especificado
        Turma turma = Turmas.FirstOrDefault(t => t.Id == id);
        if (turma != null)
        {
            // Atualiza as informações da turma
            turma.Curso = novoCurso;
            turma.AnoLetivo = novoAnoLetivo;
            turma.Turno = novoTurno;

            SalvarDados(); // Salva os dados após editar a turma
            return true; // Retorna verdadeiro se a edição for bem-sucedida
        }
        return false; // Retorna falso se a turma não for encontrada
    }


    /// <summary>
    /// MÉTODO PARA ADICIONAR ALUNO A TURMA
    /// </summary>
    /// <param name="alunoId"></param>
    /// <param name="turmaId"></param>
    /// <returns></returns>
    public string AdicionarAlunoATurma(int alunoId, int turmaId)
    {
        var aluno = Alunos.FirstOrDefault(a => a.Id == alunoId);
        var turma = Turmas.FirstOrDefault(t => t.Id == turmaId);

        if (aluno == null)
            return "Erro: Aluno não encontrado!";

        if (turma == null)
            return "Erro: Turma não encontrada!";

        if (turma.AlunosIds.Contains(alunoId))
            return "Erro: O aluno já está nesta turma!";

        // Adicionar o aluno à turma
        turma.AlunosIds.Add(alunoId);

        // Associar o aluno à turma
        aluno.TurmaId = turmaId;

        // Salvar os dados
        SalvarDados();

      



        return "Aluno adicionado com sucesso!";
    }

    /// <summary>
    /// METODO PARA REMOVER TURMA
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

  
    public bool RemoverTurma(int id)
    {
        {
            Turma turma = Turmas.FirstOrDefault(t => t.Id == id);
            if (turma == null)
            {
                return false; // Turma não encontrada
            }

            // 🚨 Verifica se há alunos matriculados
            if (Alunos.Any(a => a.TurmaId == id))
            {
                return false; // Há alunos ainda vinculados à turma
            }

            // 🚨 Remove a turma da lista
            bool removida = Turmas.Remove(turma);

            if (removida)
            {
                SalvarDados(); // Garante que a remoção foi salva
            }

            return removida;
          
        }
    }

    // ----------------- CRUD PARA PROFESSORES -----------------


    /// <summary>
    /// Metodo para adicionar professor
    /// </summary>
    /// <param name="professor"></param>
    public void AdicionarProfessor(Professor professor)
    {
        Professores.Add(professor);
        SalvarDados();
    }


    /// <summary>
    /// Metodo para remover professor
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Metodo para editar professor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="novoNome"></param>
    /// <param name="novaAreaEnsino"></param>
    /// <param name="novoContato"></param>
    /// <param name="novoEmail"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Metodo para adicionar disciplina
    /// </summary>
    /// <param name="disciplina"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Metodo para editar disciplina
    /// </summary>
    /// <param name="id"></param>
    /// <param name="novoNome"></param>
    /// <param name="novosProfessoresIds"></param>
    /// <returns></returns>

    public bool EditarDisciplina(int id, string novoNome, List<int> novosProfessoresIds)
    {
        // Verificar se o nome da disciplina já existe
        if (Disciplinas.Any(d => d.Nome.Equals(novoNome, StringComparison.OrdinalIgnoreCase) && d.Id != id))
        {
            return false; // Retorna false se já existir uma disciplina com esse nome
        }

        // Encontrar a disciplina a ser editada
        Disciplina disciplina = Disciplinas.FirstOrDefault(d => d.Id == id);
        if (disciplina != null)
        {
            // Atualizar o nome da disciplina
            disciplina.Nome = novoNome;

            // Atualizar os professores associados à disciplina
            disciplina.ProfessoresIds = novosProfessoresIds;

            // Salvar as alterações
            SalvarDados();

            return true; // Retorna true se a edição for bem-sucedida
        }

        return false; // Retorna false se a disciplina não for encontrada
    }

    /// <summary>
    /// Metodo para remover disciplina
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Metodo para vincular professor a uma disciplina
    /// </summary>
    /// <param name="disciplinaId"></param>
    /// <param name="professorId"></param>
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

    /// <summary>
    /// Método para adicionar notas
    /// </summary>
    /// <param name="nota"></param>
    /// <exception cref="Exception"></exception>
    public void AdicionarNota(Nota nota)
    {
        if (VerificarSePeriodoEncerrado(nota.PeriodoLetivo))
            throw new Exception("Notas não podem ser adicionadas após o término do período letivo.");

        Notas.Add(nota);

        // Salvar os dados antes de calcular a média
        SalvarDados();

        // Calcular a média e salvar na nota
        double mediaFinal = CalcularMedia(nota.AlunoId, nota.DisciplinaId);

        // Atualizar todas as notas do aluno na disciplina com a nova média
        foreach (var n in Notas.Where(n => n.AlunoId == nota.AlunoId && n.DisciplinaId == nota.DisciplinaId))
        {
            n.Media = mediaFinal;
        }

        // Salvar novamente para persistir a média
        SalvarDados();
    }

    /// <summary>
    /// Método para remover nota
    /// </summary>
    /// <param name="alunoId"></param>
    /// <param name="disciplinaId"></param>
    /// <param name="periodoLetivo"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Método para editar nota
    /// </summary>
    /// <param name="alunoId"></param>
    /// <param name="disciplinaId"></param>
    /// <param name="periodoLetivo"></param>
    /// <param name="novaNota"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool EditarNota(int alunoId, int disciplinaId, string periodoLetivo, double novaNota)
    {
        if (VerificarSePeriodoEncerrado(periodoLetivo))
        {
            throw new Exception("Notas não podem ser editadas após o término do período letivo.");
        }

        Nota notaExistente = Notas.FirstOrDefault(n => n.AlunoId == alunoId &&
                                                       n.DisciplinaId == disciplinaId &&
                                                       n.PeriodoLetivo == periodoLetivo);

        if (notaExistente != null)
        {
            // Atualizar a nota
            notaExistente.ValorNota = novaNota;

            // Recalcular a média final dessa disciplina para o aluno
            double novaMedia = CalcularMedia(alunoId, disciplinaId);

            // Atualizar TODAS as notas desse aluno na disciplina com a nova média
            foreach (var nota in Notas.Where(n => n.AlunoId == alunoId && n.DisciplinaId == disciplinaId))
            {
                nota.Media = novaMedia;
            }

            // Salvar os dados atualizados
            SalvarDados();

            return true; // Retorna true se a edição foi bem-sucedida
        }

        return false; // Retorna false se a nota não foi encontrada
    }

    /// <summary>
    /// Método para verificar se o professor disponível a lançar nota leciona à disciplina/// </summary>
    /// <param name="professorId"></param>
    /// <param name="disciplinaId"></param>
    /// <returns></returns>
    public bool ProfessorPodeGerirNota(int professorId, int disciplinaId)
    {
        // Verifica se a disciplina existe
        Disciplina disciplina = Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);
        if (disciplina == null)
        {
            return false;
        }

        // Verifica se o professor está associado à disciplina
        return disciplina.ProfessoresIds.Contains(professorId);
    }

    /// <summary>
    /// Método criado para calcular média da nota
    /// </summary>
    /// <param name="alunoId"></param>
    /// <param name="disciplinaId"></param>
    /// <returns></returns>
    public double CalcularMedia(int alunoId, int disciplinaId)
    {
        var notas = Notas.Where(n => n.AlunoId == alunoId && n.DisciplinaId == disciplinaId).ToList();

        if (!notas.Any())
            return 0.0; // Se não houver notas, a média é zero

        double mediaFinal = notas.Average(n => n.ValorNota);

        return Math.Round(mediaFinal, 2); // Arredonda para duas casas decimais

    }
    /// <summary>
    /// Método para verificar se o ano letivo foi encerrado 
    /// </summary>
    /// <param name="periodoLetivo"></param>
    /// <returns></returns>

    public bool VerificarSePeriodoEncerrado(string periodoLetivo)
    {
        string[] partes = periodoLetivo.Split('/');
        if (partes.Length == 2 && int.TryParse(partes[1], out int anoFinal))
        {
            return anoFinal < DateTime.Now.Year;
        }
        return false;
    }
    // ----------------- CRUD PARA EVENTOS -----------------


    /// <summary>
    /// Metodo para adicionar evento
    /// </summary>
    /// <param name="id"></param>
    /// <param name="nome"></param>
    /// <param name="descricao"></param>
    /// <param name="data"></param>
    /// <exception cref="Exception"></exception>
    // 📌 Adicionar Evento
    public void AdicionarEvento(int id, string nome, string descricao, DateTime data)
    {
        if (Eventos.Any(e => e.Id == id))
            throw new Exception("Já existe um evento com este ID.");

        Evento novoEvento = new Evento(id, nome, descricao, data);
        Eventos.Add(novoEvento);
        SalvarDados();
    }


    /// <summary>
    ///  Metodo para editar evento
    /// </summary>
    /// <param name="id"></param>
    /// <param name="novoNome"></param>
    /// <param name="novaDescricao"></param>
    /// <param name="novaData"></param>
    /// <returns></returns>

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

    /// <summary>
    /// Metodo para remover evento
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

  


    //---------------------------------METODOS AUXILIARES----------------------------
    /// <summary>
    /// Método para verificar se o professor pode lecionar uma disciplina
    /// </summary>
    /// <param name="professorId"></param>
    /// <param name="disciplinaId"></param>
    /// <param name="mensagemErro"></param>
    /// <returns></returns>

    public bool PodeLecionarDisciplina(int professorId, int disciplinaId, out string mensagemErro)
    {
        mensagemErro = string.Empty; // Inicializar a mensagem de erro como vazia

        // Procurar professor e disciplina pelo ID
        var professor = Professores.FirstOrDefault(p => p.Id == professorId);
        var disciplina = Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);

        // Se o professor ou a disciplina não existirem, retornar false e a mensagem de erro
        if (professor == null || disciplina == null)
        {
            mensagemErro = "Erro: Professor ou disciplina não encontrados.";
            return false;
        }

        // Mapeamento das áreas de ensino e as disciplinas que podem ser lecionadas
        Dictionary<string, List<string>> mapeamentoAreaDisciplinas = new Dictionary<string, List<string>>
    {
        { "Línguas e Humanidades", new List<string> { "Português", "Inglês", "Francês", "Espanhol", "Filosofia", "História" } },
        { "Ciências e Tecnologias", new List<string> { "Matemática", "Física e Química", "Biologia e Geologia", "Geometria Descritiva", "Programação", "Robótica" } },
        { "Ciências Socioeconómicas", new List<string> { "Economia", "Geografia", "Sociologia", "Direito" } },
        { "Artes Visuais", new List<string> { "Educação Visual", "Desenho", "História da Cultura e das Artes" } },
        { "Educação Física e Desporto", new List<string> { "Educação Física", "Ciências do Desporto" } },
        { "Informática e Tecnologias", new List<string> { "Tecnologias de Informação e Comunicação (TIC)", "Programação", "Robótica" } }
    };

        // Verificar se a área do professor está no mapeamento
        if (mapeamentoAreaDisciplinas.ContainsKey(professor.AreaEnsino))
        {
            // Verificar se a disciplina está na lista de disciplinas compatíveis com a área do professor
            if (mapeamentoAreaDisciplinas[professor.AreaEnsino].Contains(disciplina.Nome))
            {
                return true; // O professor pode lecionar a disciplina
            }
            else
            {
                mensagemErro = $"Erro: O professor {professor.Nome} não pode lecionar a disciplina {disciplina.Nome} porque ela não corresponde à sua área de ensino ({professor.AreaEnsino}).";
                return false;
            }
        }
        else
        {
            mensagemErro = $"Erro: A área de ensino '{professor.AreaEnsino}' do professor {professor.Nome} não está mapeada para nenhuma disciplina válida.";
            return false;
        }
    }

    /// <summary>
    /// Metodo para validar ano letivo 
    /// </summary>
    /// <param name="anoLetivo"></param>
    /// <returns></returns>
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
