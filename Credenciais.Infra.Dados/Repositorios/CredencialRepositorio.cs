using Credenciais.Dominio.Entidades;
using Credenciais.Dominio.Interfaces;
using Credenciais.Infra.Dados.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Credenciais.Infra.Dados.Repositorios
{
    public class CredencialRepositorio : ICredencialRepositorio
    {
        private ApplicationDbContext _credencialContexto;

        public CredencialRepositorio(ApplicationDbContext contexto)
            => _credencialContexto = contexto;

        public async Task<IEnumerable<CredencialDominio>> ListarAsync(long idUsuario) 
            => await _credencialContexto.Credenciais
                        .Where(c => c.IdUsuario == idUsuario)
                        .OrderBy(c => c.Titulo)
                        .ToListAsync();

        public async Task<CredencialDominio> SelecionarAsync(long id)
            => await _credencialContexto.Credenciais.FindAsync(id);

        public async Task<CredencialDominio> SalvarAsync(CredencialDominio credencial)
        {
            if (credencial.Id == 0)
                _credencialContexto.Add(credencial);
            else
                _credencialContexto.Update(credencial);

            await _credencialContexto.SaveChangesAsync();

            return credencial;
        }

        public async Task<CredencialDominio> ExcluirAsync(CredencialDominio credencial)
        {
            _credencialContexto.Remove(credencial);
            await _credencialContexto.SaveChangesAsync();
            return credencial;
        }
    }
}
