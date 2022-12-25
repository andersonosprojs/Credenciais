using AutoMapper;
using Credenciais.Aplicacao.DTOs;
using Credenciais.Aplicacao.Interfaces;
using Credenciais.Dominio.Entidades;
using Credenciais.Dominio.Interfaces;

namespace Credenciais.Aplicacao.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioServico(
            IUsuarioRepositorio usuarioRepositorio,
            IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDTO>> ListarAsync()
        {
            var usuarioDominio = await _usuarioRepositorio.ListarAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarioDominio);
        }

        public async Task<UsuarioDTO> SelecionarAsync(long id)
        {
            var usuarioDominio = await _usuarioRepositorio.SelecionarAsync(id);
            return _mapper.Map<UsuarioDTO>(usuarioDominio);
        }
        public async Task<UsuarioDTO> SelecionarPorLoginAsync(string login)
        {
            var usuarioDominio = await _usuarioRepositorio.SelecionarPorLoginAsync(login);
            return _mapper.Map<UsuarioDTO>(usuarioDominio);
        }

        public async Task SalvarAsync(UsuarioDTO usuario)
        {
            var usuarioDominio = _mapper.Map<UsuarioDominio>(usuario);
            await _usuarioRepositorio.SalvarAsync(usuarioDominio);
            usuario.Id = usuarioDominio.Id;
        }

        public async Task RedefinirSenhaAsync(UsuarioDTO usuario)
        {
            var usuarioDominio = _usuarioRepositorio.SelecionarPorLoginAsync(usuario.Login).Result;
            if (usuarioDominio != null)
            {
                usuarioDominio.setSenha(usuario.Senha);
                await _usuarioRepositorio.SalvarAsync(usuarioDominio);
                usuario.Id = usuarioDominio.Id;
            }
        }

        public async Task ExcluirAsync(long id)
        {
            var usuarioDominio = _usuarioRepositorio.SelecionarAsync(id).Result;
            await _usuarioRepositorio.ExcluirAsync(usuarioDominio);
        }

    }
}
