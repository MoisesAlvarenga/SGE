using SGE.Model;

namespace SGE.Service.Interfaces
{
    public interface IProfessorService
    {
        Task<ProfessorDto> CreateAsync(CreateProfessorDto dto);
        Task<ProfessorDto> getBiIdAsync(Guid professorId);
        Task<IEnumerable<ProfessorDto>> GetAllAsync();
    }
}
