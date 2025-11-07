namespace SGE.Model
{
    public class AvaliacaoDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public double Nota { get; set; }
        public string AlunoNome { get; set; } = string.Empty;
        public string TurmaCodigo { get; set; } = string.Empty;
    }
}
