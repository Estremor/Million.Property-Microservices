using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Properties.API.Filter;
using Properties.Application.Commands;
using Properties.Application.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Properties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ODataController
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region C'tor
        public PropertyController(IMediator mediator) => _mediator = mediator;
        #endregion


        #region Methods
        // GET: api/<PropertyController>
        //[HttpGet]
        //[EnableQuery]
        //public IEnumerable<PropertyReadDto> Get()
        //{
        //    return _propertyAppService.List();
        //}

        // POST api/<PropertyController>
        [HttpPost]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> Post(CreatePropertyCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        // PUT api/<PropertyController>/5
        [HttpPut]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> Put(UpdatePropertyCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPatch]
        [Authorize]
        [CustomValidation]
        public async Task<IActionResult> UpdatePrice(UpdatePropertyPriceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        #endregion
    }
}
