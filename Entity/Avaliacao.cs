namespace SGE.Entity;

public class Avaliacao
{
    private double _nota;
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AlunoId { get; set; }
    public Guid CursoId { get; set; }
    public string Descricao { get; set; } = string.Empty;

    public double Nota
    {
        get => _nota;
        private set
        {
            if (value < 0 || value > 10)
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    "Nota deve estar entre 0 e 10"
                );
            _nota = value;
        }
    }

    public DateTime Data { get; set; } = DateTime.UtcNow;

    // Navegação para Aluno
    public Aluno? Aluno { get; set; }

    public Avaliacao() { }

    public Avaliacao(double nota, string descricao)
    {
        AtribuirNota(nota);
        Descricao = descricao;
    }

    public void AtribuirNota(double valor)
    {
        if (valor < 0 || valor > 10)
            throw new ArgumentOutOfRangeException(nameof(valor), "Nota deve estar entre 0 e 10");
        Nota = valor;
    }
}