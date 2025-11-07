namespace SGE.Entity;

public class Administrador : Usuario, IAutenticacao
{
    public override bool Autenticar(string login, string senha) => Login == login && Senha == senha;
}
