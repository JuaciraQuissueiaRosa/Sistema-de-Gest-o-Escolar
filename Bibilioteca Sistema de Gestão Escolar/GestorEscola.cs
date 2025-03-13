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
        for (int i = 0; i < Alunos.Count; i++)
        {
            if (Alunos[i].Id == id)
            {
                Alunos[i].Nome = novoNome;
                Alunos[i].Contato = novoContato;
                return;
            }
        }
    }

    /// <summary>
    /// Permite mudar um aluno de turma sem perder seu histórico de notas.
    /// </summary>
    public void MudarAlunoDeTurma(int alunoId, int novaTurmaId)
    {
        for (int i = 0; i < Alunos.Count; i++)
        {
            if (Alunos[i].Id == alunoId)
            {
                Alunos[i].TurmaId = novaTurmaId;
                return;
            }
        }
    }

    /// <summary>
    /// Remove um aluno apenas se ele não tiver notas registradas.
    /// </summary>
    public void RemoverAluno(int id)
    {
        for (int i = 0; i < Alunos.Count; i++)
        {
            if (Alunos[i].Id == id)
            {
                for (int j = 0; j < Notas.Count; j++)
                {
                    if (Notas[j].AlunoId == id)
                    {
                        Console.WriteLine("O aluno não pode ser removido pois tem notas registradas.");
                        return;
                    }
                }
                Alunos.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// Permite buscar alunos por nome, número de estudante ou turma.
    /// </summary>
    public List<Aluno> BuscarAlunos(string termoBusca)
    {
        List<Aluno> resultado = new List<Aluno>();
        for (int i = 0; i < Alunos.Count; i++)
        {
            if (Alunos[i].Nome.Contains(termoBusca) || Alunos[i].Id.ToString() == termoBusca || Alunos[i].TurmaId.ToString() == termoBusca)
            {
                resultado.Add(Alunos[i]);
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
        for (int i = 0; i < Professores.Count; i++)
        {
            if (Professores[i].Id == id)
            {
                for (int j = 0; j < Disciplinas.Count; j++)
                {
                    for (int k = 0; k < Disciplinas[j].ProfessoresIds.Count; k++)
                    {
                        if (Disciplinas[j].ProfessoresIds[k] == id)
                        {
                            Console.WriteLine("O professor não pode ser removido pois está associado a disciplinas.");
                            return;
                        }
                    }
                }
                Professores.RemoveAt(i);
                return;
            }
        }
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
        for (int i = 0; i < Disciplinas.Count; i++)
        {
            if (Disciplinas[i].Id == disciplinaId)
            {
                for (int j = 0; j < Disciplinas[i].ProfessoresIds.Count; j++)
                {
                    if (Disciplinas[i].ProfessoresIds[j] == professorId)
                    {
                        return;
                    }
                }
                Disciplinas[i].ProfessoresIds.Add(professorId);
                return;
            }
        }
    }

    // ----------------- CRUD PARA NOTAS ----------------- 

    /// <summary>
    /// Adiciona uma nota ao sistema, verificando se o período letivo ainda está ativo e se o professor pode lançar a nota.
    /// </summary>
    public void AdicionarNota(Nota nota)
    {
        if (VerificarSePeriodoEncerrado(nota.PeriodoLetivo))
        {
            Console.WriteLine("Notas não podem ser adicionadas após o término do período letivo.");
            return;
        }

        if (!VerificarSeProfessorPodeLancarNota(nota.DisciplinaId, nota.AlunoId))
        {
            Console.WriteLine("Apenas professores da disciplina podem lançar notas.");
            return;
        }

        Notas.Add(nota);
    }

    // ----------------- MÉTODOS AUXILIARES ----------------- 

    /// <summary>
    /// Verifica se o período letivo já foi encerrado.
    /// </summary>
    private bool VerificarSePeriodoEncerrado(string periodoLetivo)
    {
        return periodoLetivo != "Ativo";
    }

    /// <summary>
    /// Verifica se o professor pode lançar notas para a disciplina.
    /// </summary>
    private bool VerificarSeProfessorPodeLancarNota(int disciplinaId, int alunoId)
    {
        for (int i = 0; i < Disciplinas.Count; i++)
        {
            if (Disciplinas[i].Id == disciplinaId)
            {
                return true;
            }
        }
        return false;
    }
}

