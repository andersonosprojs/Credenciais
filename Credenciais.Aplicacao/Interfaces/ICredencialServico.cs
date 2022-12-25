using Credenciais.Aplicacao.DTOs;

namespace Credenciais.Aplicacao.Interfaces
{
    public interface ICredencialServico
    {
        Task<IEnumerable<CredencialDTO>> ListarAsync(long idUsuario);
        Task<CredencialDTO> SelecionarAsync(long id);
        Task SalvarAsync(CredencialDTO credencial);
        Task ExcluirAsync(long id);
    }
}
