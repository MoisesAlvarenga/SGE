namespace SGE.Model
{
    public class CursoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int CargaHoraria { get; set; }
    }
}