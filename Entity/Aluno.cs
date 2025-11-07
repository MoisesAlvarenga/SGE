using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entity;

public class Aluno : Usuario, IAutenticacao
{
    public string Matricula { get; set; } = string.Empty;
    public Guid? CursoId { get; set; }

    [ForeignKey("CursoId")]
    public Curso? Curso { get; set; }
    public Guid? TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public List<Avaliacao> Avaliacoes { get; set; } = new();

    public override bool Autenticar(string login, string senha) => Login == login && Senha == senha;

    // Relatorio polimórfico
    public string GerarRelatorio()
    {
        var avg = Avaliacoes.Any() ? Avaliacoes.Average(a => a.Nota) : 0.0;
        return $"Aluno: {Nome} | Matrícula: {Matricula} | Curso: {Curso?.Nome ?? "-"} | Média: {avg:F2}";
    }
}