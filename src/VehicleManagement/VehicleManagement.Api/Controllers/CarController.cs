using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Domain.Commands.CarCommands;
using VehicleManagement.Domain.Queries;

namespace VehicleManagement.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("AddCar")]
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] AddCarCommand command)
        {
            try
            {
                var car = await _mediator.Send(command);
                return Ok(car);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        
        [Route("Cars")]
        [HttpPost]
        public async Task<IActionResult> Cars()
        {
            try
            {
                var car = await _mediator.Send(new GetCarsQuery());
                return Ok(car);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
