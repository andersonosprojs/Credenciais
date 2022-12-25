using Credenciais.Aplicacao.DTOs;
using Credenciais.Aplicacao.Interfaces;
using Credenciais.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Credenciais.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class CredencialController : ControllerBase
    {
        private readonly ICredencialServico _credencialServico;
        private readonly ICriptografiaServico _criptografiaServico;

        public CredencialController(ICredencialServico credencialServico, ICriptografiaServico criptografiaServico)
        {
            _credencialServico = credencialServico;
            _criptografiaServico = criptografiaServico;
        }

        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<IEnumerable<CredencialDTO>>> ListarAsync(long idUsuario)
        {
            var credenciais = await _credencialServico.ListarAsync(idUsuario);
            if (credenciais == null)
                return NotFound("Não existe credenciais");

            foreach (var credencial in credenciais)
            {
                credencial.Senha = _criptografiaServico.Descriptografar(credencial.Senha);
                credencial.SenhaAplicativo = _criptografiaServico.Descriptografar(credencial.SenhaAplicativo);
                credencial.SenhaCartao = _criptografiaServico.Descriptografar(credencial.SenhaCartao);
            }

            return Ok(credenciais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CredencialDTO>> SelecionarAsync(long id)
        {
            var credencial = await _credencialServico.SelecionarAsync(id);
            if (credencial == null)
                return NotFound("Credencial não encontrada");


            credencial.Senha = _criptografiaServico.Descriptografar(credencial.Senha);
            credencial.SenhaAplicativo = _criptografiaServico.Descriptografar(credencial.SenhaAplicativo);
            credencial.SenhaCartao = _criptografiaServico.Descriptografar(credencial.SenhaCartao);

            return Ok(credencial);
        }

        [HttpPost]
        public async Task<ActionResult<CredencialDTO>> SalvarAsync([FromBody] CredencialDTO credencial)
        {
            if (credencial == null)
                return BadRequest("Dados inválidos");

            credencial.Senha = _criptografiaServico.Criptografar(credencial.Senha);
            credencial.SenhaAplicativo = _criptografiaServico.Criptografar(credencial.SenhaAplicativo);
            credencial.SenhaCartao = _criptografiaServico.Criptografar(credencial.SenhaCartao);

            await _credencialServico.SalvarAsync(credencial);

            credencial.Senha = _criptografiaServico.Descriptografar(credencial.Senha);
            credencial.SenhaAplicativo = _criptografiaServico.Descriptografar(credencial.SenhaAplicativo);
            credencial.SenhaCartao = _criptografiaServico.Descriptografar(credencial.SenhaCartao);

            return Ok(credencial);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CredencialDTO>> ExcluirAsync(long id)
        {
            var credencial = await _credencialServico.SelecionarAsync(id);
            if (credencial == null)
                return NotFound("Credencial não existe");

            await _credencialServico.ExcluirAsync(id);

            credencial.Senha = _criptografiaServico.Descriptografar(credencial.Senha);
            credencial.SenhaAplicativo = _criptografiaServico.Descriptografar(credencial.SenhaAplicativo);
            credencial.SenhaCartao = _criptografiaServico.Descriptografar(credencial.SenhaCartao);

            return Ok(credencial);
        }
    }
}
