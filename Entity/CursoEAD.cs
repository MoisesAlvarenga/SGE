namespace SGE.Entity;

public class CursoEAD : Curso
{
    public string Plataforma { get; set; } = string.Empty;

    public override string DetalharCurso() =>
        base.DetalharCurso() + $" | EAD - Plataforma: {Plataforma}";
}
