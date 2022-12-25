using Credenciais.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credenciais.Infra.Dados.ConfiguracaoEntidades
{
    public class CredencialConfiguracao : IEntityTypeConfiguration<CredencialDominio>
    {
        public void Configure(EntityTypeBuilder<CredencialDominio> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Titulo)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Login)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Senha)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(p => p.Assinatura)
                .HasMaxLength(50);

            builder.Property(p => p.Url)
                .HasMaxLength(1000);

            builder.Property(p => p.Agencia)
                .HasMaxLength(50);

            builder.Property(p => p.Conta)
                .HasMaxLength(50);

            builder.Property(p => p.Pix)
                .HasMaxLength(1000);

            builder.Property(p => p.UsuarioAplicativo)
                .HasMaxLength(300);

            builder.Property(p => p.SenhaAplicativo)
                .HasMaxLength(300);

            builder.Property(p => p.SenhaCartao)
                .HasMaxLength(300);

            builder.Property(p => p.Observacao)
                .HasMaxLength(1000);

            builder.HasOne(e => e.Usuario).WithMany(e => e.Credenciais)
                .HasForeignKey(e => e.IdUsuario);
        }
    }
}
