using Credenciais.Dominio.Entidades;

namespace Credenciais.Dominio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<IEnumerable<UsuarioDominio>> ListarAsync();
        Task<UsuarioDominio> SelecionarAsync(long id);
        Task<UsuarioDominio> SelecionarPorLoginAsync(string login);
        Task<UsuarioDominio> SalvarAsync(UsuarioDominio usuario);
        Task<UsuarioDominio> ExcluirAsync(UsuarioDominio usuario);
    }
}
