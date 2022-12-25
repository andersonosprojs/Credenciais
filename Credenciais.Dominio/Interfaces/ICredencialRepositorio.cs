using Credenciais.Dominio.Entidades;

namespace Credenciais.Dominio.Interfaces
{
    public interface ICredencialRepositorio
    {
        Task<IEnumerable<CredencialDominio>> ListarAsync(long idUsuario);
        Task<CredencialDominio> SelecionarAsync(long id);
        Task<CredencialDominio> SalvarAsync(CredencialDominio credencial);
        Task<CredencialDominio> ExcluirAsync(CredencialDominio credencial);
    }
}
