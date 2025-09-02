using Properties.Domain.Entities;
using Shared.Domain.Base.Contract;
using Shared.Domain.Base;

namespace Properties.Domain.Services.Contracts;

public interface IPropertyTraceDomainService : IDomainService
{
    #region Contract
    ActionResult RegisterTrace(PropertyTrace trace);
    #endregion
}
