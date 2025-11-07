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

        public AlunoService(IRepository<Aluno> repo, IMapper mapper, IAvaliacaoService avaliacaoService)
        {
            _repo = repo;
            _mapper = mapper;
            _avaliacaoService = avaliacaoService;
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

            // Delega criação para o serviço de avaliação
            return await _avaliacaoService.CreateAsync(dto);
        }

    }
}
