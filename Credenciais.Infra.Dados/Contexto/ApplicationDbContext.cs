using Credenciais.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Credenciais.Infra.Dados.Contexto
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<UsuarioDominio> Usuarios { get; set; }
        public DbSet<CredencialDominio> Credenciais { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
