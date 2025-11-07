namespace SGE.Entity;

public class Professor : Usuario, IAutenticacao
{
    public string Especialidade { get; set; } = string.Empty;
    public string Registro { get; set; } = string.Empty;

    public override bool Autenticar(string login, string senha) => Login == login && Senha == senha;

    public string GerarRelatorio()
    {
        return $"Professor: {Nome} | Registro: {Registro} | Especialidade: {Especialidade}";
    }
}
