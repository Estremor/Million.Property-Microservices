using Properties.Application.Dto;
using MediatR;

namespace Properties.Application.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse> { }

    public record GetPropertiesQuery() : IQuery<IQueryable<PropertyReadDto>>;
}