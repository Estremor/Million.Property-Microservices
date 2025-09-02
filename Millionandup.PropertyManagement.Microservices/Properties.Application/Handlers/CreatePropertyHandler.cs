using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Properties.Application.Commands;
using Properties.Application.Dto;
using Properties.Domain.Entities;
using Properties.Domain.Services.Contracts;
using Shared.Domain.Base;
using Shared.Domain.Base.IRepository;
using Shared.Infrastructure.Base;
using Shared.Infrastructure.Errors;
using System.Net;

namespace Properties.Application.Handlers
{
    public class CreatePropertyHandler : CommandHandlerBase, IRequestHandler<CreatePropertyCommand>
    {
        #region Fields

        #region servicios de dominio
        private readonly IPropertyDomainService _propertyDomainServ;
        private readonly IPropertyImageDomainService _propertyImageDomainServ;
        #endregion

        #region Repository
        private readonly IRepository<Owner> _ownerRepo;
        #endregion

        private readonly IMapper _mapper;
        #endregion

        public CreatePropertyHandler(DbContext context, IMapper mapper) : base(context)
        {
            _propertyDomainServ = Context.GetDomainService<IPropertyDomainService>();
            _propertyImageDomainServ = Context.GetDomainService<IPropertyImageDomainService>();
            _ownerRepo = Context.GetRepository<IRepository<Owner>>();
            _mapper = mapper;
        }

        async Task IRequestHandler<CreatePropertyCommand>.Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            Property property = _mapper.Map<Property>(request);
            property.IdOwner = _ownerRepo.Entity.FirstOrDefault(o => o.Document == request.OwnerDocument)?.IdOwner ?? Guid.Empty;
            ActionResult propertyResult = await _propertyDomainServ.SaveAsync(property);
            if (!propertyResult.IsSuccessful)
                throw new RestException(HttpStatusCode.AlreadyReported, new { Messages = propertyResult.ErrorMessage });

            //registramos las imagenes
            List<PropertyImage> images = _mapper.Map<List<PropertyImage>>(request.PropertyImages);
            foreach (PropertyImage item in images)
            {
                item.IdProperty = property.IdProperty;
                ActionResult result = await _propertyImageDomainServ.SaveImageAsync(item);
                if (!result.IsSuccessful)
                {
                    throw new RestException(HttpStatusCode.InternalServerError, new { Messages = result.ErrorMessage });
                }
            }
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}