using Credenciais.Aplicacao.DTOs;

namespace Credenciais.Aplicacao.Interfaces
{
    public interface IEmailEnvioServico
    {
        RetornoEnviarEmail EnviarEmailAsync(string email, string assunto, string mensagem);
    }
}
