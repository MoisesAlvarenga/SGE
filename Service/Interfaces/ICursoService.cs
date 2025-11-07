using SGE.Model;

namespace SGE.Service.Interfaces;

public interface ICursoService
{
    Task<CursoDto> CreateAsync(CreateCursoDto dto);
    Task<IEnumerable<CursoDto>> GetAllAsync();
    Task<TurmaDto> CreateTurmaAsync(Guid cursoId, CreateTurmaDto dto);
    Task<TurmaDto> AdicionarAlunoTurmaAsync(Guid cursoId, Guid turmaId, Guid alunoId);
    Task<IEnumerable<TurmaDto>> GetTurmasAsync(Guid cursoId);
    Task<IEnumerable<string>> GerarRelatoriosAsync();
}
