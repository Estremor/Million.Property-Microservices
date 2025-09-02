using Properties.Domain.Entities;
using Shared.Domain.Base.Contract;
using Shared.Domain.Base;

namespace Properties.Domain.Services.Contracts;

public interface IPropertyDomainService : IDomainService
{
    #region Contract
    Task<ActionResult> SaveAsync(Property property);
    Task<ActionResult> UpdatePropertyAsync(Property property);
    Task<ActionResult> UpdatePriceAsync(Property property);
    #endregion
}
