using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Base.Contract;
using System.Diagnostics.Contracts;

namespace Shared.Infrastructure.Base
{

    /// <summary>
    /// Métodos extendidos para .NET 9
    /// </summary>
    public static class DBExtensions
    {
        #region DbContext

        /// <summary>
        /// Se debe asignar en la resolución de dependencias
        /// </summary>
        public static IServiceCollection? services;

        /// <summary>
        /// Obtiene un repositorio
        /// </summary>
        /// <typeparam name="TRepository">Tipo del repositorio a obtener</typeparam>
        /// <param name="context">Contexto quien extiende el método</param>
        /// <returns>Repositorio</returns>
        public static TRepository GetRepository<TRepository>(this DbContext context) where TRepository : IRepository
        {
            if (services == null)
                return default;

            TRepository rep = services.BuildServiceProvider().GetService<TRepository>();

            if (rep != null)
                rep.Initialize(context);

            return rep;
        }

        /// <summary>
        /// Obtiene un servicio de dominio
        /// </summary>
        /// <typeparam name="TDomainService">Tipo del servicio a obtener</typeparam>
        /// <param name="context">Contexto quien extiende el método</param>
        /// <returns>Servicio</returns>
        public static TDomainService GetDomainService<TDomainService>(this DbContext context) where TDomainService : IDomainService
        {
            if (services == null)
                return default;

            TDomainService rep = services.BuildServiceProvider().GetService<TDomainService>();

            rep?.SetContext(context);

            return rep;
        }

        #endregion
    }
}