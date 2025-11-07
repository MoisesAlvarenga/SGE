namespace SGE.Model;

public class CreateAvaliacaoDto
{
    public Guid AlunoId { get; set; }
    public double Nota { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string TurmaCodigo { get; set; } = string.Empty;
}