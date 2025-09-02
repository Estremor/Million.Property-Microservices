using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.Base
{
    /// <summary>
    /// Clase base de los servicios de aplicación
    /// </summary>
    public abstract class CommandHandlerBase 
    {
        #region Properties

        /// <summary>
        /// Contexto de datos
        /// </summary>
        public DbContext Context { get; private set; }

        #endregion

        #region Builders

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// </summary>
        /// <param name="context">Contexto de datos</param>
        public CommandHandlerBase(DbContext context) => Context = context;

        #endregion
    }
}
