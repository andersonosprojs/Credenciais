using Credenciais.Aplicacao.DTOs;
using Credenciais.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Credenciais.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailEnvioServico _emailEnvioSerico;

        public EmailController(IEmailEnvioServico emailEnvioServico)
            => _emailEnvioSerico = emailEnvioServico;

        [HttpPost]
        public ActionResult<RetornoEnviarEmail> EnviarEmail(EmailDTO email)
        {
            if (!ModelState.IsValid)
                BadRequest();

            try
            {
                var result = _emailEnvioSerico.EnviarEmailAsync(email.EmailDestino, email.Assunto, email.Mensagem);
                return Ok(result);
            }
            catch (Exception)
            {
                ModelState.AddModelError("errors", "Falha ao enviar e-mail");
                return BadRequest(ModelState);
            }
        }
    }
}
