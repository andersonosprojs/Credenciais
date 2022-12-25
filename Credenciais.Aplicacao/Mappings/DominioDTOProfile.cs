using AutoMapper;
using Credenciais.Aplicacao.DTOs;
using Credenciais.Dominio.Entidades;

namespace Credenciais.Aplicacao.Mappings
{
    public class DominioDTOProfile: Profile
    {
        public DominioDTOProfile()
        {
            CreateMap<UsuarioDominio, UsuarioDTO>().ReverseMap();
            CreateMap<CredencialDominio, CredencialDTO>().ReverseMap();
        }
    }
}
