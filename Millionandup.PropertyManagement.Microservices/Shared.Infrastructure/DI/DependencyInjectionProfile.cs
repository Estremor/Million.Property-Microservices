using Shared.Domain.Base.IRepository;
using Shared.Infrastructure;
using Shared.Infrastructure.Base;
using Shared.Infrastructure.Base.Repository;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contiene la Configuracion de la injeccion de dependencias para dbContext y repositorys
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Registra Las dependencias, como se resuelven
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, Assembly assembly = null)
        {
            #region Context
            /*Registramos el contexto*/
            services.AddTransient<EntityFrameworkCore.DbContext, MillionPropertyDbContext>();
            #endregion

            #region Repositories
            /*Resolvemos los repositorios Genericos*/
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            DBExtensions.services = services;
            #endregion
            return services;
        }
    }
}
