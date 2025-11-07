using SGE.Model;

namespace SGE.Service.Interfaces
{
    public interface IAvaliacaoService
    {
        Task<AvaliacaoDto> CreateAsync(CreateAvaliacaoDto dto);
        Task<IEnumerable<AvaliacaoDto>> GetByAlunoIdAsync(Guid alunoId);
    }
}