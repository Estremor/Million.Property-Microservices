using Properties.Domain.Entities;
using Properties.Domain.Services.Contracts;
using Shared.Domain.Base;
using Shared.Domain.Base.IRepository;
using Shared.Infrastructure.Base;

namespace Properties.Domain.Services;

public class PropertyDomainService : DomainService, IPropertyDomainService
{
    #region Fileds
    private readonly IRepository<Property> _propertyRepo;
    private readonly IRepository<Owner> _ownerRepo;
    #endregion

    #region C'tor
    public PropertyDomainService(IRepository<Property> propertyRepo, IRepository<Owner> ownerRepo)
    {
        _propertyRepo = propertyRepo;
        _ownerRepo = ownerRepo;
    }
    #endregion

    #region Method
    public async Task<ActionResult> SaveAsync(Property property)
    {
        ICollection<Property> entityResult = _propertyRepo.List(p => p.CodeInternal.Equals(property.CodeInternal));
        if (entityResult is not null && entityResult.Count > 0)
        {
            return new ActionResult { IsSuccessful = false, ErrorMessage = "Ya se encuentra una propiedad registrada con el cogigo interno enviado" };
        }
        ICollection<Owner> ownerResult = _ownerRepo.List(o => o.IdOwner == property.IdOwner);
        if (ownerResult is null || ownerResult.Count == 0)
        {
            return new ActionResult { IsSuccessful = false, ErrorMessage = "No existe un owner con el identificador enviado" };
        }
        property.IdOwner = ownerResult.First().IdOwner;
        Property resultSave = await _propertyRepo.InsertAsync(property);
        return new ActionResult { IsSuccessful = true, Result = resultSave };
    }

    public async Task<ActionResult> UpdatePropertyAsync(Property property)
    {
        var propertyEnity = _propertyRepo.List(x => x.CodeInternal == property.CodeInternal);
        if (propertyEnity is not null && propertyEnity.Count > 0)
        {
            if (_ownerRepo.Entity.Find(property.IdOwner) != null)
            {
                Property propertyUp = propertyEnity.First();
                propertyUp.Address = property.Address;
                propertyUp.IdOwner = property.IdOwner;
                propertyUp.Name = property.Name;
                propertyUp.Price = property.Price;
                propertyUp.Year = property.Year;
                propertyUp = await _propertyRepo.UpdateAsync(propertyUp);
                return new ActionResult { IsSuccessful = true, Result = propertyUp };
            }
            return new ActionResult { IsSuccessful = false, ErrorMessage = "No existe un owner con el identificador enviado" };
        }
        return new ActionResult { IsSuccessful = false, ErrorMessage = "Ya se encuentra una propiedad registrada con el cogigo interno enviado" };
    }
    public async Task<ActionResult> UpdatePriceAsync(Property property)
    {
        var prop = _propertyRepo.List(x => x.CodeInternal == property.CodeInternal);
        if (prop == null || prop.Count == 0)
            return new ActionResult { Result = false, ErrorMessage = "No existe una propiedad con el identificador enviado" };

        Property propertyForUpdate = prop.FirstOrDefault();
        propertyForUpdate.Price = property.Price;
        await _propertyRepo.UpdateAsync(propertyForUpdate);

        return new ActionResult { IsSuccessful = true };
    }
    #endregion
}
