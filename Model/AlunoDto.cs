using System.Security.Cryptography.X509Certificates;
using SGE.Entity;

namespace SGE.Model;

public class AlunoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public CursoDto? Curso { get; set; }
    public List<AvaliacaoDto> Avaliacoes { get; set; } = new();
}
