using AutoMapper;
using Credenciais.Aplicacao.DTOs;
using Credenciais.Aplicacao.Interfaces;
using Credenciais.Dominio.Entidades;
using Credenciais.Dominio.Interfaces;

namespace Credenciais.Aplicacao.Servicos
{
    public class CredencialServico : ICredencialServico
    {
        private readonly ICredencialRepositorio _credencialRepositorio;
        private readonly IMapper _mapper;

        public CredencialServico(
            ICredencialRepositorio credencialRepositorio,
            IMapper mapper)
        {
            _credencialRepositorio = credencialRepositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CredencialDTO>> ListarAsync(long idUsuario)
        {
            var credencialDominio = await _credencialRepositorio.ListarAsync(idUsuario);
            return _mapper.Map<IEnumerable<CredencialDTO>>(credencialDominio);
        }

        public async Task<CredencialDTO> SelecionarAsync(long id)
        {
            var credencialDominio = await _credencialRepositorio.SelecionarAsync(id);
            return _mapper.Map<CredencialDTO>(credencialDominio);
        }

        public async Task SalvarAsync(CredencialDTO credencial)
        {
            var credencialDominio = _mapper.Map<CredencialDominio>(credencial);
            await _credencialRepositorio.SalvarAsync(credencialDominio);
            credencial.Id = credencialDominio.Id;
        }

        public async Task ExcluirAsync(long id)
        {
            var credencialDominio = _credencialRepositorio.SelecionarAsync(id).Result;
            await _credencialRepositorio.ExcluirAsync(credencialDominio);
        }

    }
}
