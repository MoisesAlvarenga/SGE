using AutoMapper;
using SGE.Entity;
using SGE.Model;
using SGE.Repository;
using SGE.Service.Interfaces;

namespace SGE.Service;

public class TurmaService : ITurmaService
{
    private readonly IRepository<Turma> _repo;
    private readonly IRepository<Aluno> _alunoRepo;
    private readonly IRepository<Professor> _profRepo;
    private readonly IMapper _mapper;

    public TurmaService(
        IRepository<Turma> repo,
        IRepository<Aluno> alunoRepo,
        IRepository<Professor> profRepo,
        IMapper mapper)
    {
        _repo = repo;
        _alunoRepo = alunoRepo;
        _profRepo = profRepo;
        _mapper = mapper;
    }

    public async Task<TurmaDto> CreateAsync(CreateTurmaDto dto)
    {
        var turma = _mapper.Map<Turma>(dto);
        await _repo.AddAsync(turma);
        return _mapper.Map<TurmaDto>(turma);
    }

    public async Task<IEnumerable<TurmaDto>> GetAllAsync()
    {
        var turmas = await _repo.FindAllAsync();
        return _mapper.Map<IEnumerable<TurmaDto>>(turmas);
    }

    public async Task<IEnumerable<TurmaDto>> GetByCursoIdAsync(Guid cursoId)
    {
        var turmas = await _repo.FindByAsync( t => t.CursoId == cursoId);
        if (turmas == null)
            throw new KeyNotFoundException($"Turma para o curso {cursoId} não encontrada");

        IList<TurmaDto> listTurmaDtos = new List<TurmaDto>();
        foreach (var turma in turmas)
        {
            var professor = await _profRepo.FindByIdAsync(turma.ProfessorId);
            if (professor != null)
            {
                // Assuming TurmaDto has a ProfessorNome property
                var turmaDto = _mapper.Map<TurmaDto>(turma);
                turmaDto.ProfessorNome = professor.Nome;
                listTurmaDtos.Add(turmaDto);
            }

            var alunos = await _alunoRepo.FindByAsync(a => a.TurmaId == turma.Id);
            foreach (var aluno in alunos)
            {
                var turmaDto = listTurmaDtos.FirstOrDefault(t => t.Id == turma.Id);
                if (turmaDto != null)
                {
                    turmaDto.Alunos.Add(_mapper.Map<AlunoTurmaDto>(aluno));
                }
            }
        }
            return listTurmaDtos;
        
    }

    public async Task<TurmaDto> AdicionarAlunoAsync(Guid turmaId, Guid alunoId)
    {
        var turma = await _repo.FindByIdAsync(turmaId);
        if (turma == null)
            throw new KeyNotFoundException($"Turma {turmaId} não encontrada");

        var aluno = await _alunoRepo.FindByIdAsync(alunoId);
        if (aluno == null)
            throw new KeyNotFoundException($"Aluno {alunoId} não encontrado");

        aluno.TurmaId = turmaId;
        aluno.CursoId = turma.CursoId;
        await _alunoRepo.UpdateAsync(aluno);

        return _mapper.Map<TurmaDto>(turma);
    }

    public async Task<TurmaDto> RemoverAlunoAsync(Guid turmaId, Guid alunoId)
    {
        var turma = await _repo.FindByIdAsync(turmaId);
        if (turma == null)
            throw new KeyNotFoundException($"Turma {turmaId} não encontrada");

        var aluno = await _alunoRepo.FindByIdAsync(alunoId);
        if (aluno == null)
            throw new KeyNotFoundException($"Aluno {alunoId} não encontrado");

        aluno.TurmaId = null;
        await _alunoRepo.UpdateAsync(aluno);

        return _mapper.Map<TurmaDto>(turma);
    }

    public async Task<TurmaDto> AtribuirProfessorAsync(Guid turmaId, Guid professorId)
    {
        var turma = await _repo.FindByIdAsync(turmaId);
        if (turma == null)
            throw new KeyNotFoundException($"Turma {turmaId} não encontrada");

        var professor = await _profRepo.FindByIdAsync(professorId);
        if (professor == null)
            throw new KeyNotFoundException($"Professor {professorId} não encontrado");

        turma.ProfessorId = professorId;
        await _repo.UpdateAsync(turma);

        return _mapper.Map<TurmaDto>(turma);
    }
}