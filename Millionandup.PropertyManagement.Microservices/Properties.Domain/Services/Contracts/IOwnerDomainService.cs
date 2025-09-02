using Properties.Domain.Entities;
using Shared.Domain.Base;
using Shared.Domain.Base.Contract;

namespace Properties.Domain.Services.Contracts;

public interface IOwnerDomainService : IDomainService
{
    #region Contract
    Task<ActionResult> SaveAsync(Owner owner);
    #endregion
}
