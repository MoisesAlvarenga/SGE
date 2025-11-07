namespace SGE.Model
{
    public class ProfessorDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public string Registro { get; set; } = string.Empty;
    }
}
