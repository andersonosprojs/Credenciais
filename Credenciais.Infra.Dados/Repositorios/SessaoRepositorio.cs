using Credenciais.Dominio.Entidades;
using Credenciais.Dominio.Interfaces;
using Credenciais.Infra.Dados.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Credenciais.Infra.Dados.Repositorios
{
    public class SessaoRepositorio: ISessaoRepositorio
    {
        private ApplicationDbContext _usuarioContexto;

        public SessaoRepositorio(ApplicationDbContext contexto)
            => _usuarioContexto = contexto;

        public async Task<UsuarioDominio> Autenticar(string login, string senha)
            => await _usuarioContexto.Usuarios.FirstOrDefaultAsync(u => u.Login == login && u.Senha == senha);

        //public Task Logout()
        //    => null;
    }
}
