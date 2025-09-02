using Properties.Domain.Entities;
using Properties.Domain.Services.Contracts;
using Shared.Domain.Base;
using Shared.Domain.Base.IRepository;
using Shared.Infrastructure.Base;

namespace Properties.Domain.Services;

public class PropertyTraceDomainService : DomainService, IPropertyTraceDomainService
{
    #region Fields
    private readonly IRepository<PropertyTrace> _repository;
    private readonly IRepository<Property> _propertyRepo;
    #endregion

    #region C'tor
    public PropertyTraceDomainService(IRepository<PropertyTrace> repository, IRepository<Property> propertyRepo)
    {
        _repository = repository;
        _propertyRepo = propertyRepo;
    }
    #endregion

    #region Method
    public ActionResult RegisterTrace(PropertyTrace trace)
    {
        if (_propertyRepo.Entity.Find(trace.IdProperty) != null)
        {
            if (!string.IsNullOrWhiteSpace(trace.Name))
            {
                _repository.Insert(trace);
                return new ActionResult { IsSuccessful = true };
            }
        }
        return new ActionResult { IsSuccessful = false, ErrorMessage = "No existe una propiedad con el identificador enviado" };
    }
    #endregion
}
