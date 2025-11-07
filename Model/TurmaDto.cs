using System.Collections.Generic;

namespace SGE.Model
{
    public class TurmaDto
    {
        public Guid Id { get; set; }                 // mudado de int para Guid
        public string Codigo { get; set; } = string.Empty;
        public Guid? CursoId { get; set; }          // adicionado
        public string CursoNome { get; set; } = string.Empty;
        public Guid? ProfessorId { get; set; }      // adicionado
        public string ProfessorNome { get; set; } = string.Empty;
        public List<AlunoTurmaDto> Alunos { get; set; } = new();  // mudado para dto específico
    }

    public class AlunoTurmaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
    }
}
