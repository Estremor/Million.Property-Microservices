using Auth.Domain.Entities;
using Shared.Domain.Base;
using Shared.Domain.Base.Contract;

namespace Auth.Domain.Services.Contracts
{
    public interface ILoginDomainService : IDomainService
    {
        #region Contarct
        /// <summary>
        /// verifica si existe un usuario con los datos proporcionados
        /// </summary>
        /// <param name="user"></param>
        /// <returns>usuario</returns>
        Task<ActionResult> FindUserAsync(User user);
        /// <summary>
        /// Crea token para autenticación
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateToken(User user);
        #endregion
    }
}
