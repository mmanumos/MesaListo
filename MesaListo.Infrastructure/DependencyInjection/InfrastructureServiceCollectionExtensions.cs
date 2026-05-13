using MesaListo.Application.Interfaces;
using MesaListo.Infrastructure.Persistence;
using MesaListo.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MesaListo.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<SqlConnectionFactory>();

            services.AddScoped<SqlStoredProcedureExecutor>();

            services.AddScoped<IDatabaseConnectionTester, DatabaseConnectionTester>();

            services.AddScoped<IJuegoRepository, JuegoRepository>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IComunidadRepository, ComunidadRepository>();

            services.AddScoped<INoticiaRepository, NoticiaRepository>();

            services.AddScoped<IReplicaRepository, ReplicaRepository>();

            services.AddScoped<IEventoRepository, EventoRepository>();

            return services;
        }
    }
}