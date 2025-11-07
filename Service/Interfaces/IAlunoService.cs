using SGE.Model;

namespace SGE.Service.Interfaces
{
    public interface IAlunoService
    {
        Task<AlunoDto> CreateAsync(CreateAlunoDto dto);
        Task<IEnumerable<AlunoDto>> GetAllAsync();
        Task<AvaliacaoDto> AddAvaliacaoAsync(CreateAvaliacaoDto dto);
        Task<IEnumerable<string>> GerarRelatoriosAsync();
        Task<AlunoDto> MatricularAsync(Guid alunoId, Guid cursoId);
    }
}
