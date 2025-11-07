namespace SGE.Entity;

public class Curso
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public List<Aluno> Alunos { get; set; } = new();

    public virtual string DetalharCurso() => $"Curso: {Nome} ({Codigo}) - {CargaHoraria}h";

    // Polimórfico
    public string GerarRelatorio() => DetalharCurso();
}
