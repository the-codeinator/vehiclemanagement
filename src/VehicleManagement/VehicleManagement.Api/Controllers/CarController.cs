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

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddCarCommand command)
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
        [HttpGet]
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

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var car = await _mediator.Send(new DeleteCarCommand(id));
                if(car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarCommand updateCarCommand)
        {
            try
            {
                var car = await _mediator.Send(updateCarCommand);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
