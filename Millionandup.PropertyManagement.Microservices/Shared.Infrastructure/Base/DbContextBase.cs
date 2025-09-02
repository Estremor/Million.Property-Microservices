using Microsoft.EntityFrameworkCore;
using Shared.Domain.Base.Enum;

namespace Shared.Infrastructure.Base
{
    /// <summary>
    /// Context Base
    /// </summary>
    public abstract class DbContextBase : DbContext
    {
        #region Properties
        /// <summary>
        /// Obtiene el objeto de configuración del contexto
        /// </summary>
        protected DbSettings DbSettings { get; private set; }

        #endregion

        #region Builders

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// </summary>
        /// <param name="dbSettings">Objeto de configuración del contexto</param>
        public DbContextBase(DbSettings dbSettings) : base()
        {
            DbSettings = dbSettings;
            DbSettings.ConnectionString = DbSettings.ConnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Aqui se usar el proveedor de conexion que esté configurado
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                switch (DbSettings.Provider)
                {
                    case SupportedProvider.SqlServer:
                        optionsBuilder.UseSqlServer(DbSettings.ConnectionString);
                        break;
                    default:
                        optionsBuilder.UseSqlServer(DbSettings.ConnectionString);
                        break;
                }
            }
        }

        #endregion
    }


    #region Settings
    /// <summary>
    /// Encapsula las propiedades de configuración de una
    /// conexión a una base de datos Sql
    /// </summary>
    public sealed class DbSettings
    {
        #region Properties

        /// <summary>
        /// Obtiene o asigna la cadena de conexión a la base de datos Sql
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o asigna el tipo de proveedor Sql
        /// </summary>
        public SupportedProvider Provider { get; set; }

        #endregion
    }
    #endregion
}
