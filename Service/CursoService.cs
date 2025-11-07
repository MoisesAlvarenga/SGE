using AutoMapper;
using SGE.Entity;
using SGE.Model;
using SGE.Repository;
using SGE.Service.Interfaces;

namespace SGE.Service
{
    public class CursoService : ICursoService
    {
        private readonly IRepository<Curso> _repo;
        private readonly IMapper _mapper;
        private readonly ITurmaService _turmaService;
        private readonly IProfessorService _professorService;

        public CursoService(
            IRepository<Curso> repo,
            IMapper mapper,
            ITurmaService turmaService,
            IProfessorService professorService)
        {
            _repo = repo;
            _mapper = mapper;
            _turmaService = turmaService;
            _professorService = professorService;
        }

        public async Task<CursoDto> CreateAsync(CreateCursoDto dto)
        {
            var curso = _mapper.Map<Curso>(dto);
            await _repo.AddAsync(curso);
            return _mapper.Map<CursoDto>(curso);
        }

        public async Task<IEnumerable<CursoDto>> GetAllAsync()
        {
            var cursos = await _repo.FindAllAsync();
            return _mapper.Map<IEnumerable<CursoDto>>(cursos);
        }

        public async Task<TurmaDto> CreateTurmaAsync(Guid cursoId, CreateTurmaDto dto)
        {
            var curso = await _repo.FindByIdAsync(cursoId);
            var professor = await _professorService.getBiIdAsync(dto.ProfessorId);
            if (curso == null)
                throw new KeyNotFoundException($"Curso {cursoId} não encontrado");

            dto.CursoId = cursoId;
           TurmaDto turmaCreated = await _turmaService.CreateAsync(dto);

            turmaCreated.ProfessorNome = professor.Nome;
           
           return turmaCreated;
        }

        public async Task<TurmaDto> AdicionarAlunoTurmaAsync(Guid cursoId, Guid turmaId, Guid alunoId)
        {
            var curso = await _repo.FindByIdAsync(cursoId);
            if (curso == null)
                throw new KeyNotFoundException($"Curso {cursoId} não encontrado");

            return await _turmaService.AdicionarAlunoAsync(turmaId, alunoId);
        }

        public async Task<IEnumerable<TurmaDto>> GetTurmasAsync(Guid cursoId)
        {
            var curso = await _repo.FindByIdAsync(cursoId);
            if (curso == null)
                throw new KeyNotFoundException($"Curso {cursoId} não encontrado");

            var turmas = await _turmaService.GetByCursoIdAsync(curso.Id);
            return turmas;
        }

        public async Task<IEnumerable<string>> GerarRelatoriosAsync()
        {
            var cursos = await _repo.FindAllAsync();
            return cursos.Select(c => $"Curso: {c.Nome}, Carga Horária: {c.CargaHoraria}");
        }
    }
}
