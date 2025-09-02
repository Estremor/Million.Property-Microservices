using Auth.Application.Commands;
using Auth.Application.Dto;
using Auth.Domain.Entities;
using Auth.Domain.Services.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Base;
using Shared.Infrastructure.Base;
using Shared.Infrastructure.Errors;
using System.Net;

namespace Auth.Application.Handlers
{
    public class LoginCommandHandler : CommandHandlerBase, IRequestHandler<LoginCommand, UserDto>
    {
        #region Fileds
        private readonly ILoginDomainService _loginDomainService;
        #endregion

        #region C'tor
        public LoginCommandHandler(DbContext context) : base(context) 
        {
            _loginDomainService = Context.GetDomainService<ILoginDomainService>() ?? throw new ArgumentNullException();
        } 
        #endregion



        async Task<UserDto> IRequestHandler<LoginCommand, UserDto>.Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ActionResult userResult = await _loginDomainService.FindUserAsync(new User { UserName = request.UserName, Password = request.Password });

            if (userResult.IsSuccessful)
            {
                var user = (User)userResult.Result;

                return  new UserDto
                {
                    UserName = user?.UserName,
                    Token = _loginDomainService.CreateToken(user)
                };
            }
            throw new RestException(HttpStatusCode.NotFound, new { Messages = userResult.ErrorMessage });
        }
    }
}
