using Bibilioteca_Sistema_de_Gestão_Escolar;

using System;
using System.Collections.Generic;

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
    public void RemoverAluno(int id)
    {
        foreach (var aluno in Alunos)
        {
            if (aluno.Id == id)
            {
                foreach (var nota in Notas)
                {
                    if (nota.AlunoId == id)
                    {
                        throw new Exception("O aluno não pode ser removido pois tem notas registradas.");
                    }
                }
                Alunos.Remove(aluno);
                return;
            }
        }
        throw new Exception("Aluno não encontrado.");
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
    public void RemoverProfessor(int id)
    {
        foreach (var professor in Professores)
        {
            if (professor.Id == id)
            {
                foreach (var disciplina in Disciplinas)
                {
                    if (disciplina.ProfessoresIds.Contains(id))
                    {
                        throw new Exception("O professor não pode ser removido pois está associado a disciplinas.");
                    }
                }
                Professores.Remove(professor);
                return;
            }
        }
        throw new Exception("Professor não encontrado.");
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
    public void RemoverNota(int alunoId, int disciplinaId, string periodoLetivo)
    {
        for (int i = 0; i < Notas.Count; i++)
        {
            if (Notas[i].AlunoId == alunoId &&
                Notas[i].DisciplinaId == disciplinaId &&
                Notas[i].PeriodoLetivo.Equals(periodoLetivo, StringComparison.OrdinalIgnoreCase))
            {
                Notas.RemoveAt(i);
                return;
            }
        }
        throw new Exception("Nota não encontrada.");
    }

    // ----------------- MÉTODOS AUXILIARES ----------------- 

    /// <summary>
    /// Verifica se o período letivo já foi encerrado.
    /// </summary>
    public bool VerificarSePeriodoEncerrado(string periodoLetivo)
    {
        return periodoLetivo != "Ativo";
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
    public void RemoverTurma(int id)
    {
        for (int i = 0; i < Turmas.Count; i++)
        {
            if (Turmas[i].Id == id)
            {
                // Verificar se há alunos matriculados antes de remover a turma
                if (Turmas[i].AlunosIds.Count > 0)
                {
                    throw new Exception("A turma não pode ser removida pois ainda tem alunos matriculados.");
                }

                // Remover a turma da lista
                Turmas.RemoveAt(i);
                return;
            }
        }

        throw new Exception("Turma não encontrada.");
    }

    /// <summary>
    /// Remove uma disciplina do sistema, verificando se ela está associada a turmas ou notas.
    /// </summary>
    public void RemoverDisciplina(int id)
    {
        for (int i = 0; i < Disciplinas.Count; i++)
        {
            if (Disciplinas[i].Id == id)
            {
                // Verificar se a disciplina está associada a turmas
                foreach (var turma in Turmas)
                {
                    if (turma.DisciplinasIds.Contains(id))
                    {
                        throw new Exception("A disciplina não pode ser removida pois está associada a uma ou mais turmas.");
                    }
                }

                // Verificar se existem notas registradas para a disciplina
                foreach (var nota in Notas)
                {
                    if (nota.DisciplinaId == id)
                    {
                        throw new Exception("A disciplina não pode ser removida pois há notas registradas para ela.");
                    }
                }

                // Remover a disciplina da lista
                Disciplinas.RemoveAt(i);
                return;
            }
        }

        // Se a disciplina não for encontrada, lança um erro
        throw new Exception("Disciplina não encontrada.");
    }


}
