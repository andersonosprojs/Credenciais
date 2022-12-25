using Credenciais.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credenciais.Infra.Dados.ConfiguracaoEntidades
{
    public class UsuarioConfiguracao : IEntityTypeConfiguration<UsuarioDominio>
    {
        public void Configure(EntityTypeBuilder<UsuarioDominio> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Login)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Senha)
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}
