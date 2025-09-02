using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Properties.Application.Commands;
using Properties.Domain.Entities;
using Properties.Domain.Services.Contracts;
using Shared.Domain.Base;
using Shared.Domain.Base.IRepository;
using Shared.Infrastructure.Base;
using Shared.Infrastructure.Errors;
using System.Net;

namespace Properties.Application.Handlers
{
    public class CreateImageHandler : CommandHandlerBase, IRequestHandler<CreateImageCommand>
    {
        #region Fields
        private readonly IPropertyImageDomainService _propertyImageDomainServ;
        private readonly IRepository<Property> _propertyRepo;
        private readonly IMapper _mapper;
        #endregion

        #region C´tor
        public CreateImageHandler(DbContext context, IMapper mapper) : base(context)
        {
            _propertyImageDomainServ = Context.GetDomainService<IPropertyImageDomainService>();
            _propertyRepo = Context.GetRepository<IRepository<Property>>();
            _mapper = mapper;
        }

        public async Task Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            PropertyImage entity = _mapper.Map<PropertyImage>(request);
            entity.IdProperty = _propertyRepo.List(x => x.CodeInternal == request.InernalCode)?.FirstOrDefault()?.IdProperty ?? Guid.Empty;
            ActionResult result = await _propertyImageDomainServ.SaveImageAsync(entity);

            if (!result.IsSuccessful)
                throw new RestException(HttpStatusCode.InternalServerError, new { Messages = result.ErrorMessage });
            await Context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
