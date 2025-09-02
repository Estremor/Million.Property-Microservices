using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Properties.API.Filter;
using Properties.Application.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Properties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OwnerController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region C'tor
        public OwnerController(IMediator mediator) => _mediator = mediator;
        #endregion

        #region Methods

        // POST api/<OwnerController>
        [HttpPost]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> Post(CreateOwnerCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        #endregion
    }
}
