using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Properties.Application.Dto;
using Properties.Application.Queries;
using Properties.Domain.Entities;
using Shared.Domain.Base.IRepository;
using Shared.Infrastructure.Base;

namespace Properties.Application.Handlers
{
    public class GetPropertiesHandler : CommandHandlerBase, IRequestHandler<GetPropertiesQuery, IQueryable<PropertyReadDto>>
    {
        private readonly IRepository<Property> _propertyRepo;
        private readonly IMapper _mapper;

        public GetPropertiesHandler(DbContext context, IMapper mapper) : base(context)
        {
            _propertyRepo = Context.GetRepository<IRepository<Property>>();
            _mapper = mapper;
        }

        public async Task<IQueryable<PropertyReadDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = _propertyRepo.ListByQuery();
            return _mapper.ProjectTo<PropertyReadDto>(properties);
        }
    }
}