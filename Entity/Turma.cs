namespace SGE.Entity;

public class Turma
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Codigo { get; set; } = string.Empty;
    public Guid ProfessorId { get; set; }
    public Professor? Professor { get; set; }
    public Guid? CursoId { get; set; }
    public Curso? Curso { get; set; }
    public List<Aluno> Alunos { get; set; } = new();
    public string Resumo() =>
        $"Turma {Codigo} | Curso: {Curso?.Nome ?? "-"} | Professor: {Professor?.Nome ?? "-"} | Alunos: {Alunos.Count}";
}