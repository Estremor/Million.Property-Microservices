using Properties.Application.Commands;
using Properties.Domain.Entities;
using Properties.Domain.Services.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Base;
using Shared.Infrastructure.Errors;
using System.Net;

namespace Properties.Application.Handlers
{
    public class UpdatePropertyPriceHandler : CommandHandlerBase, IRequestHandler<UpdatePropertyPriceCommand>
    {
        private readonly IPropertyDomainService _propertyDomainServ;

        public UpdatePropertyPriceHandler(DbContext context) : base(context) 
        {
            _propertyDomainServ = Context.GetDomainService<IPropertyDomainService>() ?? throw new ArgumentNullException();
        }


        async Task IRequestHandler<UpdatePropertyPriceCommand>.Handle(UpdatePropertyPriceCommand priceDto, CancellationToken cancellationToken)
        {
            var result = await _propertyDomainServ.UpdatePriceAsync(new Property { CodeInternal = priceDto.CodeInternal, Price = priceDto.Price });
            if (!result.IsSuccessful)
                throw new RestException(HttpStatusCode.NotFound, new { Messages = result.ErrorMessage });
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}