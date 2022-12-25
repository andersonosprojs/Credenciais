using Credenciais.Aplicacao.DTOs;

namespace Credenciais.Aplicacao.Interfaces
{
    public interface IUsuarioServico
    {
        Task<IEnumerable<UsuarioDTO>> ListarAsync();
        Task<UsuarioDTO> SelecionarAsync(long id);
        Task<UsuarioDTO> SelecionarPorLoginAsync(string login);
        Task SalvarAsync(UsuarioDTO usuario);
        Task RedefinirSenhaAsync(UsuarioDTO usuario);
        Task ExcluirAsync(long id);
    }
}
