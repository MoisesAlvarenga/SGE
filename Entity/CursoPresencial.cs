namespace SGE.Entity;

public class CursoPresencial : Curso
{
    public string SalaAula { get; set; } = string.Empty;

    public override string DetalharCurso() =>
        base.DetalharCurso() + $" | Presencial - Sala: {SalaAula}";
}
