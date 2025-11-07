using System.Text.Json.Serialization;

namespace SGE.Model;

public class CreateTurmaDto
{
    public string Codigo { get; set; } = string.Empty;
    [JsonIgnore]
    public Guid CursoId { get; set; }
    public Guid ProfessorId { get; set; }
}