using Credenciais.Aplicacao.Interfaces;
using Credenciais.Dominio.Entidades;
using Credenciais.Dominio.Interfaces;
using Credenciais.Utils.Interfaces;

namespace Credenciais.Aplicacao.Servicos
{
    public class SessaoServico : ISessaoServico
    {
        private readonly ISessaoRepositorio _sessaoRepositorio;
        private readonly ICriptografiaServico _criptografiaServico;

        public SessaoServico(ISessaoRepositorio sessaoRepositorio, ICriptografiaServico criptografiaServico)
        {
            _sessaoRepositorio = sessaoRepositorio;
            _criptografiaServico = criptografiaServico;
        }            

        public async Task<UsuarioDominio> Autenticar(string login, string senha)
        {
            var senhaCriptografada = _criptografiaServico.Criptografar(senha);
            return await _sessaoRepositorio.Autenticar(login, senhaCriptografada);
        }
            
    }
}
