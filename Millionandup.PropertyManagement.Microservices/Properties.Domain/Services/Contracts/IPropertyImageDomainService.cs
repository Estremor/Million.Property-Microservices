using Properties.Domain.Entities;
using Shared.Domain.Base.Contract;
using Shared.Domain.Base;

namespace Properties.Domain.Services.Contracts;

public interface IPropertyImageDomainService : IDomainService
{
    #region Contract
    Task<ActionResult> SaveImageAsync(PropertyImage image);
    #endregion
}
