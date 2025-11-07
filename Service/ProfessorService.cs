using AutoMapper;
using SGE.Entity;
using SGE.Model;
using SGE.Repository;
using SGE.Service.Interfaces;

namespace SGE.Service
{
    public class ProfessorService : IProfessorService
    {
        private readonly IRepository<Professor> _repo;
        private readonly IMapper _mapper;

        public ProfessorService(IRepository<Professor> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProfessorDto> CreateAsync(CreateProfessorDto dto)
        {
            var professor = _mapper.Map<Professor>(dto);
            await _repo.AddAsync(professor);
            return _mapper.Map<ProfessorDto>(professor);
        }

        public async Task<IEnumerable<ProfessorDto>> GetAllAsync()
        {
            var professores = await _repo.FindAllAsync();
            return _mapper.Map<IEnumerable<ProfessorDto>>(professores);
        }

        public async Task<ProfessorDto> getBiIdAsync(Guid professorId)
        {
            var professor = await _repo.FindByIdAsync(professorId);
            if (professor == null)
                throw new KeyNotFoundException($"Professor {professorId} não encontrado");

            return _mapper.Map<ProfessorDto>(professor);
        }
    }
}
