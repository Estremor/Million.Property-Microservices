using Properties.Domain.Entities;
using Properties.Domain.Services.Contracts;
using Shared.Domain.Base;
using Shared.Domain.Base.IRepository;
using Shared.Infrastructure.Base;

namespace Properties.Domain.Services;

public class PropertyImageDomainService : DomainService, IPropertyImageDomainService
{
    #region Fields
    private readonly IRepository<PropertyImage> _imageRepo;
    private readonly IRepository<Property> _propertyRepo;
    #endregion

    #region C´tor
    public PropertyImageDomainService(IRepository<PropertyImage> imageRepo, IRepository<Property> propertyRepo)
    {
        _imageRepo = imageRepo;
        _propertyRepo = propertyRepo;
    }
    #endregion

    #region Methods
    public async Task<ActionResult> SaveImageAsync(PropertyImage image)
    {
        var propertyResult = _propertyRepo.Entity.Find(image.IdProperty);
        if (propertyResult == null) return new ActionResult { IsSuccessful = false, ErrorMessage = "No existe una propiedad con el identificador enviado" };
        await _imageRepo.InsertAsync(image);
        return new ActionResult { IsSuccessful = true };
    }
    #endregion
}
