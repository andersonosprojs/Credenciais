using Credenciais.Aplicacao.Interfaces;
using Credenciais.Aplicacao.Servicos;
using Credenciais.Dominio.Interfaces;
using Credenciais.Infra.Dados.Contexto;
using Credenciais.Infra.Dados.Repositorios;
using Credenciais.Aplicacao.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Credenciais.Utils.Servicos;
using Credenciais.Utils.Interfaces;

namespace CleanArchMvc.Infra.IoC;

public static class InjecaoDependencia
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"
        ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        services.AddScoped<IUsuarioServico, UsuarioServico>();

        services.AddScoped<ICredencialRepositorio, CredencialRepositorio>();
        services.AddScoped<ICredencialServico, CredencialServico>();
        
        services.AddScoped<ICriptografiaServico, CriptografiaServico>();

        services.AddScoped<ISessaoServico, SessaoServico>();
        services.AddScoped<ISessaoRepositorio, SessaoRepositorio>();

        services.AddAutoMapper(typeof(DominioDTOProfile));

        var myhandlers = AppDomain.CurrentDomain.Load("Credenciais.Aplicacao");
        services.AddMediatR(myhandlers);

        return services;
    }
}
