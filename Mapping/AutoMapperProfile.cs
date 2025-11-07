using AutoMapper;
using SGE.Entity;
using SGE.Model;

namespace SGE.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Aluno
            CreateMap<Aluno, AlunoDto>()
                .ForMember(dest => dest.Curso, opt => opt.MapFrom(src => src.Curso));

            CreateMap<CreateAlunoDto, Aluno>()
                .ForMember(dest => dest.Curso, opt => opt.Ignore());

            CreateMap<Aluno, AlunoTurmaDto>().ReverseMap();

            // Professor
            CreateMap<Professor, ProfessorDto>();
            CreateMap<CreateProfessorDto, Professor>();

            // Curso
            CreateMap<Curso, CursoDto>().ReverseMap();
            CreateMap<Curso, CreateCursoDto>().ReverseMap();
            CreateMap<CursoPresencial, CursoPresencialDto>().ReverseMap();
            CreateMap<CursoEAD, CursoEADDto>().ReverseMap();

            // Turma
            CreateMap<Turma, TurmaDto>().ReverseMap();
            CreateMap<Turma, CreateTurmaDto>().ReverseMap();

            // Avaliacao
            CreateMap<Avaliacao, AvaliacaoDto>().ReverseMap();
            CreateMap<CreateAvaliacaoDto, Avaliacao>();
        }
    }
}
