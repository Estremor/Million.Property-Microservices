using Auth.Application.Commands;
using Auth.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Auth.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    #region Fields
    private readonly IMediator _mediator;
    #endregion

    #region C'tor
    public AuthController(IMediator mediator) => _mediator = mediator;
    #endregion

    #region Methods
    // GET: api/<AuthController>
    [HttpGet]
    [Route(nameof(Get))]
    [AllowAnonymous]
    public async Task<UserDto> Get(string userName, string password)
    {
        var command = new LoginCommand(userName, password);
        return await _mediator.Send(command);
    }
    #endregion
}
