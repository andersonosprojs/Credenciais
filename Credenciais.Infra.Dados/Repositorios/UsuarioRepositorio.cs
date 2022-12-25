using Credenciais.Dominio.Entidades;
using Credenciais.Dominio.Interfaces;
using Credenciais.Infra.Dados.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Credenciais.Infra.Dados.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private ApplicationDbContext _usuarioContexto;

        public UsuarioRepositorio(ApplicationDbContext contexto)
            => _usuarioContexto = contexto;

        public async Task<IEnumerable<UsuarioDominio>> ListarAsync()
            => await _usuarioContexto.Usuarios.OrderBy(c => c.Login).ToListAsync();

        public async Task<UsuarioDominio> SelecionarAsync(long id)
            => await _usuarioContexto.Usuarios.FindAsync(id);

        public async Task<UsuarioDominio> SelecionarPorLoginAsync(string login)
            => await _usuarioContexto.Usuarios.FirstOrDefaultAsync(u => u.Login == login);

        public async Task<UsuarioDominio> SalvarAsync(UsuarioDominio usuario)
        {
            if (usuario.Id == 0)
                _usuarioContexto.Add(usuario);
            else
                _usuarioContexto.Update(usuario);

            await _usuarioContexto.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioDominio> ExcluirAsync(UsuarioDominio usuario)
        {
            _usuarioContexto.Remove(usuario);
            await _usuarioContexto.SaveChangesAsync();
            return usuario;
        }
    }
}
