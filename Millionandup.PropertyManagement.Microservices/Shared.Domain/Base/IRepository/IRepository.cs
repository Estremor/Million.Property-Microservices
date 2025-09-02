using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shared.Domain.Base.IRepository
{
    /// <summary>
    /// Define los atributos y comportamientos de un repositorio genérico
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad de negocio</typeparam>
    public interface IRepository<TEntity> : Contract.IRepository where TEntity : class, new()
    {
        #region Properties

        /// <summary>
        /// Obtiene la entidad que da acceso al contexto
        /// </summary>
        DbSet<TEntity> Entity { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserta una nueva entidad en el repositorio de datos
        /// </summary>
        /// <param name="entity">Entidad a insertar</param>
        /// <returns>Entidad insertada</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Actualiza una entidad en el repositorio de datos
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Elimina una entidad en el repositorio de datos
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        /// <returns>Entidad eliminada</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Lista las entidades que cumplen con la expresión
        /// </summary>
        /// <param name="expression">Expresión de búsqueda</param>
        /// <returns>Enumeración de entidades resultado</returns>
        ICollection<TEntity> List(Expression<Func<TEntity, bool>> expression = null);

        /// <summary>
        /// Lista las entidades que cumplen con la expresión incluyendo sus agregados
        /// </summary>
        /// <param name="expression">Expresión de búsqueda</param>
        /// <returns>Enumeración de entidades resultado</returns>
        /// <param name="properties">Rutas a las propiedades a incluir como agregados</param>
        ICollection<TEntity> ListInclude(Expression<Func<TEntity, bool>> expression, params string[] properties);

        IQueryable<TEntity> ListByQuery();

        /// <summary>
        /// Inserta una nueva entidad en el repositorio de datos de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a insertar</param>
        /// <returns>Entidad insertada</returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Actualiza una entidad en el repositorio de datos de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <returns>Entidad actualizada</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Elimina una entidad en el repositorio de datos de forma asíncrona
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        /// <returns>Entidad eliminada</returns>
        Task<TEntity> DeleteAsync(TEntity entity);

        /// <summary>
        /// Lista las entidades que cumplen con la expresión de forma asíncrona
        /// </summary>
        /// <param name="expression">Expresión de búsqueda</param>
        /// <returns>Enumeración de entidades resultado</returns>
        Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression);

        #endregion
    }
}
