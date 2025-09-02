using Auth.Application.Dto;
using MediatR;

namespace Auth.Application.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }

    public record LoginCommand(string UserName, string Password) : ICommand<UserDto>;
}
