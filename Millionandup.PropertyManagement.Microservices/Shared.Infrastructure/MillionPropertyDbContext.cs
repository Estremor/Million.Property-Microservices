using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Base;
using System.Reflection;
namespace Shared.Infrastructure
{
    public partial class MillionPropertyDbContext : DbContextBase
    {
        private readonly Assembly _configurationAssembly;
        #region C'tor
        /// <summary>
        /// Inicia el contexto de Datos
        /// </summary>
        /// <param name="dbSettings"></param>
        public MillionPropertyDbContext(DbSettings dbSettings, Assembly configurationAssembly = null) : base(dbSettings)
        {
            _configurationAssembly = configurationAssembly;
            Database.EnsureCreated();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Aplica la configuracion
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar configuraciones automáticamente
            ApplyConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            // Si se proporcionó un assembly específico, usarlo
            if (_configurationAssembly != null)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(_configurationAssembly);
            }

            // Buscar en todos los assemblies que contengan ".Infrastructure" por convención
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && a.FullName.Contains(".Infrastructure"))
                .Distinct();

            foreach (var assembly in assemblies)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }
        }
        #endregion
    }
}