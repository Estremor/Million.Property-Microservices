using Properties.Domain.Services.Contracts;
using Properties.Application.Commands;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Errors;
using Properties.Domain.Entities;
using Shared.Infrastructure.Base;
using AutoMapper;
using MediatR;

namespace Properties.Application.Handlers;

public class CreateOwnerHandler : CommandHandlerBase, IRequestHandler<CreateOwnerCommand>
{
    #region Fields
    private readonly IOwnerDomainService _ownerDomainService;
    private readonly IMapper _mapper;
    #endregion

    #region C'tor
    public CreateOwnerHandler(DbContext context, IMapper mapper) : base(context)
    {
        _ownerDomainService = Context.GetDomainService<IOwnerDomainService>();
        _mapper = mapper;
    }

    public async Task Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        Owner owner = _mapper.Map<Owner>(request);
        var result = await _ownerDomainService.SaveAsync(owner);
        if (!result.IsSuccessful)
            throw new RestException(System.Net.HttpStatusCode.AlreadyReported, new { Messages = result.ErrorMessage });

        await Context.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
