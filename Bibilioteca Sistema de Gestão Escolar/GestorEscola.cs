using Bibilioteca_Sistema_de_Gestão_Escolar;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class GestorEscola
{
    // Listas para armazenar os dados das entidades
    public List<Aluno> Alunos { get; set; } = new List<Aluno>();
    public List<Professor> Professores { get; set; } = new List<Professor>();
    public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    public List<Turma> Turmas { get; set; } = new List<Turma>();
    public List<Nota> Notas { get; set; } = new List<Nota>();

    // ----------------- CRUD PARA ALUNOS ----------------- 

    /// <summary>
    /// Adiciona um aluno à lista de alunos.
    /// </summary>
    public void AdicionarAluno(Aluno aluno)
    {
        Alunos.Add(aluno);
    }

    /// <summary>
    /// Retorna a lista de alunos cadastrados.
    /// </summary>
    public List<Aluno> ListarAlunos()
    {
        return Alunos;
    }

    /// <summary>
    /// Atualiza os dados de um aluno pelo ID.
    /// </summary>
    public void AtualizarAluno(int id, string novoNome, string novoContato)
    {
        foreach (var aluno in Alunos)
        {
            if (aluno.Id == id)
            {
                aluno.Nome = novoNome;
                aluno.Contato = novoContato;
                return;
            }
        }
        throw new Exception("Aluno não encontrado.");
    }

    /// <summary>
    /// Permite mudar um aluno de turma sem perder seu histórico de notas.
    /// </summary>
    public void MudarAlunoDeTurma(int alunoId, int novaTurmaId)
    {
        foreach (var aluno in Alunos)
        {
            if (aluno.Id == alunoId)
            {
                aluno.TurmaId = novaTurmaId;
                return;
            }
        }
        throw new Exception("Aluno não encontrado.");
    }

    /// <summary>
    /// Remove um aluno apenas se ele não tiver notas registradas.
    /// </summary>
    public bool RemoverAluno(int id)
    {
        for (int i = 0; i < Alunos.Count; i++)
        {
            if (Alunos[i].Id == id)
            {
                // Verificar se o aluno tem notas registradas
                for (int j = 0; j < Notas.Count; j++)
                {
                    if (Notas[j].AlunoId == id)
                    {
                        return false; // Não pode remover se houver notas registradas
                    }
                }

                Alunos.RemoveAt(i);
                return true; // Aluno removido com sucesso
            }
        }

        return false; // Aluno não encontrado
    }

    /// <summary>
    /// Permite buscar alunos por nome, número de estudante ou turma.
    /// </summary>
    public List<Aluno> BuscarAlunos(string termoBusca)
    {
        List<Aluno> resultado = new List<Aluno>();
        foreach (var aluno in Alunos)
        {
            if (aluno.Nome.Contains(termoBusca) || aluno.Id.ToString() == termoBusca || aluno.TurmaId.ToString() == termoBusca)
            {
                resultado.Add(aluno);
            }
        }
        return resultado;
    }

    // ----------------- CRUD PARA PROFESSORES ----------------- 

    /// <summary>
    /// Adiciona um professor à lista de professores.
    /// </summary>
    public void AdicionarProfessor(Professor professor)
    {
        Professores.Add(professor);
    }

    /// <summary>
    /// Remove um professor apenas se ele não estiver associado a disciplinas.
    /// </summary>
    public bool RemoverProfessor(int id)
    {
        // Verificar se o professor existe
        for (int i = 0; i < Professores.Count; i++)
        {
            if (Professores[i].Id == id)
            {
                // Verificar se o professor está associado a alguma disciplina
                for (int j = 0; j < Disciplinas.Count; j++)
                {
                    for (int k = 0; k < Disciplinas[j].ProfessoresIds.Count; k++)
                    {
                        if (Disciplinas[j].ProfessoresIds[k] == id)
                        {
                            return false; // O professor não pode ser removido
                        }
                    }
                }

                // Remover o professor da lista
                Professores.RemoveAt(i);
                return true; // Professor removido com sucesso
            }
        }

        return false; // Professor não encontrado
    }

    // ----------------- CRUD PARA DISCIPLINAS ----------------- 

    /// <summary>
    /// Adiciona uma disciplina à lista de disciplinas.
    /// </summary>
    public void AdicionarDisciplina(Disciplina disciplina)
    {
        Disciplinas.Add(disciplina);
    }

    /// <summary>
    /// Associa um professor a uma disciplina.
    /// </summary>
    public void AssociarProfessorADisciplina(int disciplinaId, int professorId)
    {
        foreach (var disciplina in Disciplinas)
        {
            if (disciplina.Id == disciplinaId)
            {
                if (!disciplina.ProfessoresIds.Contains(professorId))
                {
                    disciplina.ProfessoresIds.Add(professorId);
                }
                return;
            }
        }
        throw new Exception("Disciplina não encontrada.");
    }

    // ----------------- CRUD PARA NOTAS ----------------- 

    /// <summary>
    /// Adiciona uma nota ao sistema, verificando se o período letivo ainda está ativo e se o professor pode lançar a nota.
    /// </summary>
    public void AdicionarNota(Nota nota)
    {
        if (VerificarSePeriodoEncerrado(nota.PeriodoLetivo))
        {
            throw new Exception("Notas não podem ser adicionadas após o término do período letivo.");
        }

        if (!VerificarSeProfessorPodeLancarNota(nota.DisciplinaId, nota.AlunoId))
        {
            throw new Exception("Apenas professores da disciplina podem lançar notas.");
        }

        Notas.Add(nota);
    }

    /// <summary>
    /// Remove uma nota, verificando se o período letivo está encerrado.
    /// </summary>
    public bool RemoverNota(int alunoId, int disciplinaId, string periodoLetivo)
    {
        for (int i = 0; i < Notas.Count; i++)
        {
            if (Notas[i].AlunoId == alunoId &&
                Notas[i].DisciplinaId == disciplinaId &&
                Notas[i].PeriodoLetivo.Equals(periodoLetivo, StringComparison.OrdinalIgnoreCase))
            {
                Notas.RemoveAt(i);
                return true; // Nota removida com sucesso
            }
        }

        return false; // Nota não encontrada
    }

    // ----------------- MÉTODOS AUXILIARES ----------------- 

    /// <summary>
    /// Verifica se o período letivo já foi encerrado.
    /// </summary>
    public bool VerificarSePeriodoEncerrado(string periodoLetivo)
    {
        // Extrair o ano do período letivo (se for no formato "2023/2024", pega "2024")
        string[] partes = periodoLetivo.Split('/');
        int anoFinal;

        if (partes.Length > 1 && int.TryParse(partes[1], out anoFinal))
        {
            int anoAtual = DateTime.Now.Year;
            return anoFinal < anoAtual; // Se o período terminou antes do ano atual, está encerrado
        }

        return false; // Se não conseguiu identificar um ano, assume que está ativo
    }

    /// <summary>
    /// Verifica se o professor pode lançar notas para a disciplina.
    /// </summary>
    public bool VerificarSeProfessorPodeLancarNota(int disciplinaId, int alunoId)
    {
        foreach (var disciplina in Disciplinas)
        {
            if (disciplina.Id == disciplinaId)
            {
                foreach (var professor in Professores)
                {
                    if (disciplina.ProfessoresIds.Contains(professor.Id))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Verifica se a área de ensino do professor é compatível com a disciplina.
    /// </summary>
    public bool ValidarAreaDeEnsino(string areaEnsino, string disciplina)
    {
        // Mapeamento das áreas de ensino e disciplinas correspondentes
        Dictionary<string, List<string>> areaParaDisciplinas = new Dictionary<string, List<string>>()
    {
        { "Línguas e Humanidades", new List<string> { "Português", "Inglês", "Francês", "Espanhol", "Filosofia", "História" } },
        { "Ciências e Tecnologias", new List<string> { "Matemática", "Física e Química", "Biologia e Geologia", "Geometria Descritiva" } },
        { "Ciências Socioeconómicas", new List<string> { "Economia", "Geografia", "Sociologia", "Direito" } },
        { "Artes Visuais", new List<string> { "Educação Visual", "Desenho", "História da Cultura e das Artes" } },
        { "Educação Física e Desporto", new List<string> { "Educação Física", "Ciências do Desporto" } },
        { "Informática e Tecnologias", new List<string> { "Tecnologias de Informação e Comunicação (TIC)", "Programação", "Robótica" } }
    };

        // Verifica se a área de ensino existe e se a disciplina pertence a ela
        if (areaParaDisciplinas.ContainsKey(areaEnsino))
        {
            return areaParaDisciplinas[areaEnsino].Contains(disciplina);
        }

        return false; // Se a área de ensino não for encontrada, assume que não é válida
    }



    /// <summary>
    /// Adiciona uma nova turma ao sistema.
    /// </summary>
    public void AdicionarTurma(Turma turma)
    {
        // Verificar se já existe uma turma com o mesmo ID
        foreach (var t in Turmas)
        {
            if (t.Id == turma.Id)
            {
                throw new Exception("Já existe uma turma com esse ID.");
            }
        }

        // Inicializar listas se estiverem nulas
        if (turma.AlunosIds == null)
            turma.AlunosIds = new List<int>();

        if (turma.DisciplinasIds == null)
            turma.DisciplinasIds = new List<int>();

        // Adicionar a turma à lista de turmas
        Turmas.Add(turma);
    }

    /// <summary>
    /// Remove uma turma do sistema, verificando se há alunos matriculados.
    /// </summary>
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
                return true; // Retorna verdadeiro se a remoção for bem-sucedida
            }
        }

        return false; // Retorna falso se a turma não for encontrada
    }

    /// <summary>
    /// Tenta remover uma disciplina do sistema, verificando se ela está associada a turmas ou notas.
    /// Retorna true se a remoção for bem-sucedida e false se não puder ser removida.
    /// </summary>
    public bool RemoverDisciplina(int id)
    {
        for (int i = 0; i < Disciplinas.Count; i++)
        {
            if (Disciplinas[i].Id == id)
            {
                // Verificar se a disciplina está associada a turmas
                for (int j = 0; j < Turmas.Count; j++)
                {
                    for (int k = 0; k < Turmas[j].DisciplinasIds.Count; k++)
                    {
                        if (Turmas[j].DisciplinasIds[k] == id)
                        {
                            return false; // A disciplina não pode ser removida pois está associada a uma turma
                        }
                    }
                }

                // Verificar se existem notas registradas para a disciplina
                for (int j = 0; j < Notas.Count; j++)
                {
                    if (Notas[j].DisciplinaId == id)
                    {
                        return false; // A disciplina não pode ser removida pois há notas registradas
                    }
                }

                // Remover a disciplina da lista
                Disciplinas.RemoveAt(i);
                return true; // Disciplina removida com sucesso
            }
        }

        return false; // Disciplina não encontrada
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
            if (anoInicio >= anoFim)
            {
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            return false; // Em caso de erro, retorna falso sem quebrar o sistema
        }
    }




}
