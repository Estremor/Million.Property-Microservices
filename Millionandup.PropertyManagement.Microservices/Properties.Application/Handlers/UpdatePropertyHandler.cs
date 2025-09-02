using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Properties.Application.Commands;
using Properties.Domain.Entities;
using Properties.Domain.Services.Contracts;
using Shared.Domain.Base.IRepository;
using Shared.Infrastructure.Base;
using Shared.Infrastructure.Errors;
using System.Net;

namespace Properties.Application.Handlers
{
    public class UpdatePropertyHandler : CommandHandlerBase, IRequestHandler<UpdatePropertyCommand>
    {
        #region Fields

        #region servicios de dominio
        private readonly IPropertyDomainService _propertyDomainServ;
        private readonly IPropertyImageDomainService _propertyImageDomainServ;
        private readonly IPropertyTraceDomainService _traceDomain;
        #endregion

        #region Repository
        private readonly IRepository<Owner> _ownerRepo;

        private readonly IMapper _mapper;
        #endregion

        #endregion
        public UpdatePropertyHandler(DbContext context, IMapper mapper) : base(context)
        {
            _propertyDomainServ = Context.GetDomainService<IPropertyDomainService>();
            _propertyImageDomainServ = Context.GetDomainService<IPropertyImageDomainService>();
            _ownerRepo = Context.GetRepository<IRepository<Owner>>();
            _mapper = mapper;
            _traceDomain = Context.GetDomainService<IPropertyTraceDomainService>();
        }

        async Task IRequestHandler<UpdatePropertyCommand>.Handle(UpdatePropertyCommand traceDto, CancellationToken cancellationToken)
        {
            //mapeamos la entidad property y buscamos el id
            Property property = _mapper.Map<Property>(traceDto);
            property.IdOwner = _ownerRepo.List(x => x.Document == traceDto.OwnerDocument).FirstOrDefault()?.IdOwner ?? Guid.Empty;
            var propertyResult = await _propertyDomainServ.UpdatePropertyAsync(property);

            if (!propertyResult.IsSuccessful)
                throw new RestException(HttpStatusCode.InternalServerError, new { Messages = propertyResult.ErrorMessage });

            //aseguramos que el owner exista
            PropertyTrace propertyTrace = _mapper.Map<PropertyTrace>(traceDto);
            propertyTrace.IdProperty = ((Property)propertyResult.Result).IdProperty;
            propertyTrace.Name = _ownerRepo.Entity.Find(property.IdOwner)?.Name ?? string.Empty;

            if (string.IsNullOrWhiteSpace(propertyTrace.Name))
                throw new RestException(HttpStatusCode.NotFound, new { Messages = "No se encontro el Owner ingresado" });

            var result = _traceDomain.RegisterTrace(propertyTrace);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}