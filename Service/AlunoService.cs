using AutoMapper;
using SGE.Entity;
using SGE.Model;
using SGE.Repository;
using SGE.Service.Interfaces;

namespace SGE.Service
{
    public class AlunoService : IAlunoService
    {
        private readonly IRepository<Aluno> _repo;
        private readonly IMapper _mapper;
        private readonly IAvaliacaoService _avaliacaoService;
        private readonly ITurmaService _turmaService;
        private readonly ICursoService _cursoService;

        public AlunoService(IRepository<Aluno> repo, IMapper mapper, IAvaliacaoService avaliacaoService, ITurmaService turmaService, ICursoService cursoService)
        {
            _repo = repo;
            _mapper = mapper;
            _avaliacaoService = avaliacaoService;
            _turmaService = turmaService;
            _cursoService = cursoService;
        }

        public async Task<AlunoDto> CreateAsync(CreateAlunoDto dto)
        {
            var aluno = _mapper.Map<Aluno>(dto);
            await _repo.AddAsync(aluno);
            return _mapper.Map<AlunoDto>(aluno);
        }

        public async Task<IEnumerable<AlunoDto>> GetAllAsync()
        {
            var alunos = await _repo.FindAllAsync();
            return _mapper.Map<IEnumerable<AlunoDto>>(alunos);
        }

        public async Task<IEnumerable<string>> GerarRelatoriosAsync()
        {
            var alunos = await _repo.FindAllAsync();
            return alunos.Select(a =>
                $"Aluno: {a.Nome}, Matrícula: {a.Matricula}, Curso: {a.Curso}"
            );
        }

        public async Task<AlunoDto> MatricularAsync(Guid alunoId, Guid cursoId)
        {
            var aluno = await _repo.FindByIdAsync(alunoId);
            if (aluno == null)
                throw new KeyNotFoundException($"Aluno {alunoId} não encontrado.");

            aluno.CursoId = cursoId;
            await _repo.UpdateAsync(aluno);

            return _mapper.Map<AlunoDto>(aluno);
        }

        public async Task<AvaliacaoDto> AddAvaliacaoAsync(CreateAvaliacaoDto dto)
        {
            // Valida se aluno existe
            var aluno = await _repo.FindByIdAsync(dto.AlunoId);
            if (aluno == null)
                throw new KeyNotFoundException($"Aluno {dto.AlunoId} não encontrado.");

            if (aluno.CursoId == null)
                throw new InvalidOperationException($"Aluno {dto.AlunoId} não está matriculado em nenhum curso.");
            if (aluno.TurmaId == null)
                throw new InvalidOperationException($"Aluno {dto.AlunoId} não está matriculado em nenhuma turma.");

            var turma = await _turmaService.GetByCursoIdAsync(aluno.TurmaId.Value);
            var curso = await _cursoService.GetTurmasAsync(aluno.CursoId.Value);

            if (turma == null)
                throw new KeyNotFoundException($"Turma {aluno.TurmaId} não encontrada para o aluno {dto.AlunoId}.");
            if (curso == null)
                throw new KeyNotFoundException($"Curso {aluno.CursoId} não encontrado para o aluno {dto.AlunoId}.");

            CreateAvaliacaoDto avaliacaoDto = _mapper.Map<CreateAvaliacaoDto>(dto);

            // Delega criação para o serviço de avaliação
            return await _avaliacaoService.CreateAsync(dto);
        }

    }
}
