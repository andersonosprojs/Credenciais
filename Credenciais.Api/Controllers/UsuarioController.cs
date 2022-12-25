using Credenciais.Aplicacao.DTOs;
using Credenciais.Aplicacao.Interfaces;
using Credenciais.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Credenciais.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly ISessaoServico _sessaoServico;
        private readonly IConfiguration _configuracao;
        private readonly ICriptografiaServico _criptografiaServico;

        public UsuarioController(
            IUsuarioServico usuarioServico,
            ISessaoServico sessaoServico,
            IConfiguration configuracao,
            ICriptografiaServico criptografiaServico
        )
        {
            _usuarioServico = usuarioServico;
            _sessaoServico = sessaoServico ??
                throw new ArgumentNullException(nameof(sessaoServico));
            _configuracao = configuracao;
            _criptografiaServico = criptografiaServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> ListarAsync()
        {
            var usuarios = await _usuarioServico.ListarAsync();
            if (usuarios == null)
                return NotFound("Não existe usuários");

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> SelecionarAsync(long id)
        {
            var usuario = await _usuarioServico.SelecionarAsync(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return Ok(usuario);
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<UsuarioDTO>> SelecionarPorLoginAsync(string login)
        {
            var usuario = await _usuarioServico.SelecionarPorLoginAsync(login);
            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> SalvarAsync([FromBody] UsuarioDTO usuario)
        {
            if (usuario == null)
                return BadRequest("Dados inválidos");            

            usuario.Senha = _criptografiaServico.Criptografar(usuario.Senha);

            await _usuarioServico.SalvarAsync(usuario);

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDTO>> ExcluirAsync(long id)
        {
            var usuario = await _usuarioServico.SelecionarAsync(id);
            if (usuario == null)
                return NotFound("Usuário não existe");

            await _usuarioServico.ExcluirAsync(id);

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> AlterarSenhaAsync([FromBody] UsuarioDTO usuario)
        {
            if (usuario == null)
                return BadRequest("Dados inválidos");

            usuario.Senha = _criptografiaServico.Criptografar(usuario.Senha);

            await _usuarioServico.RedefinirSenhaAsync(usuario);

            return Ok(usuario);
        }

    }
}
