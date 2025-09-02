using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Base.Contract
{
    #region DomainBase
    /// <summary>
    /// Define los atributos y métodos de un servicio de dominio
    /// </summary>
    public interface IDomainService : IDisposable
    {
        /// <summary>
        /// Asigna el contexto de datos a todos los repositorios del servicio
        /// </summary>
        /// <param name="context">Contexto de datos</param>
        void SetContext(DbContext context);
    }
    #endregion

    #region RepositoryBase
    /// <summary>
    /// Define los atributos y comportamientos de un repositorio
    /// </summary>
    public interface IRepository : IDisposable
    {
        #region Properties

        /// <summary>
        /// Obtiene el contexto de datos
        /// </summary>
        DbContext Context { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Inicializa el repositorio con el contexto
        /// </summary>
        /// <param name="context">Contexto de datos al que pertenece el repositorio</param>
        void Initialize(DbContext context);

        #endregion
    } 
    #endregion
}
