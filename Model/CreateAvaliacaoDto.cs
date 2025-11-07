namespace SGE.Model;

public class CreateAvaliacaoDto
{
    public Guid AlunoId { get; set; }
    public double Nota { get; set; }
    public string Descricao { get; set; } = string.Empty;
}