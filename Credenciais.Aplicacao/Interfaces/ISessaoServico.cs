using Credenciais.Dominio.Entidades;

namespace Credenciais.Aplicacao.Interfaces;

public interface ISessaoServico
{
    Task<UsuarioDominio> Autenticar(string login, string senha);
    //Task Logout();
}
