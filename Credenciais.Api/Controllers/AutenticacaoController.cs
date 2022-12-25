using Credenciais.Aplicacao.DTOs;
using Credenciais.Aplicacao.Interfaces;
using Credenciais.Dominio.Entidades;
using Credenciais.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Credenciais.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ISessaoServico _sessaoServico;
        private readonly IConfiguration _configuracao;
        private readonly IUsuarioServico _usuarioServico;
        private readonly ICriptografiaServico _criptografiaServico;

        public AutenticacaoController(ISessaoServico sessaoServico, IConfiguration configuracao, IUsuarioServico usuarioServico, ICriptografiaServico criptografiaServico)
        {
            _sessaoServico = sessaoServico;
            _configuracao = configuracao;
            _usuarioServico = usuarioServico;
            _criptografiaServico = criptografiaServico;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CadastrarUsuarioAsync([FromBody] UsuarioDTO usuario)
        {
            if (usuario == null)
                return BadRequest("Dados inválidos");

            if (ExisteUsuario(usuario.Login))
                return BadRequest("Usuário já existe");

            usuario.Senha = _criptografiaServico.Criptografar(usuario.Senha);

            await _usuarioServico.SalvarAsync(usuario);

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<TokenDTO>> Login([FromBody] LoginDTO login)
        {
            var usuarioDominio = await _sessaoServico.Autenticar(login.Login, login.Senha);

            if (usuarioDominio != null)
            {
                return Ok(GerarToken(usuarioDominio));
            }                
            else
            {
                ModelState.AddModelError("errors", "E-mail ou senha estão inválidos");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> RedefinirSenhaAsync([FromBody] UsuarioDTO usuario)
        {
            if (usuario == null)
                return BadRequest("Dados inválidos");

            usuario.Senha = _criptografiaServico.Criptografar(usuario.Senha);

            await _usuarioServico.RedefinirSenhaAsync(usuario);

            return Ok(usuario);
        }

        private bool ExisteUsuario(string login)
            => _usuarioServico.SelecionarPorLoginAsync(login).Result != null;

        private TokenDTO GerarToken(UsuarioDominio usuario)
        {
            //declarações do usuário
            var claims = new[]
            {
                new Claim("login", usuario.Login),
                new Claim("meuvalor", "andersonos"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave privada para assinar o token
            var chavePrivada = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuracao["Jwt:ChavePrivada"]));

            //gerar a assinatura digital
            var credenciais = new SigningCredentials(chavePrivada, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            var expiracao = DateTime.UtcNow.AddHours(8);

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: _configuracao["Jwt:Emissor"],
                //audiencia
                audience: _configuracao["Jwt:Publico"],
                //claims
                claims: claims,
                //data de expiracao
                expires: expiracao,
                //assinatura digital
                signingCredentials: credenciais
                );

            return new TokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Login = usuario.Login,
                Expiracao = expiracao,
                IdUsuario = usuario.Id
            };
        }
    }
}
