namespace SGE.Model
{
    public class CreateCursoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int CargaHoraria { get; set; }
    }
}