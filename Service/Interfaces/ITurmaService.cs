using SGE.Model;

namespace SGE.Service.Interfaces;

public interface ITurmaService
{
    Task<TurmaDto> CreateAsync(CreateTurmaDto dto);
    Task<IEnumerable<TurmaDto>> GetAllAsync();
    Task<IEnumerable<TurmaDto>> GetByCursoIdAsync(Guid cursoId);
    Task<TurmaDto> AdicionarAlunoAsync(Guid turmaId, Guid alunoId);
    Task<TurmaDto> RemoverAlunoAsync(Guid turmaId, Guid alunoId);
    Task<TurmaDto> AtribuirProfessorAsync(Guid turmaId, Guid professorId);
}