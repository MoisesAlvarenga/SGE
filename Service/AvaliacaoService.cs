using AutoMapper;
using SGE.Entity;
using SGE.Model;
using SGE.Repository;
using SGE.Service.Interfaces;

namespace SGE.Service
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IRepository<Avaliacao> _repo;
        private readonly IMapper _mapper;

        public AvaliacaoService(IRepository<Avaliacao> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AvaliacaoDto> CreateAsync(CreateAvaliacaoDto dto)
        {
            var avaliacao = _mapper.Map<Avaliacao>(dto);
            await _repo.AddAsync(avaliacao);
            return _mapper.Map<AvaliacaoDto>(avaliacao);
        }

        public async Task<IEnumerable<AvaliacaoDto>> GetByAlunoIdAsync(Guid alunoId)
        {
            var avaliacoes = await _repo.FindAllAsync();
            var filtradas = avaliacoes.Where(a => a.AlunoId == alunoId);
            return _mapper.Map<IEnumerable<AvaliacaoDto>>(filtradas);
        }
    }
}