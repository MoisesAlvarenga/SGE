namespace SGE.Entity;

public abstract class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

    public abstract bool Autenticar(string login, string senha);
}

public interface IAutenticacao
{
    bool Autenticar(string login, string senha);
}
