using Credenciais.Dominio.Entidades;

namespace Credenciais.Dominio.Interfaces;

public interface ISessaoRepositorio
{
    Task<UsuarioDominio> Autenticar(string email, string password);
    //Task Logout();
}
