using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleManagement.Domain.Commands.CarCommands;
using VehicleManagement.Domain.Queries;

namespace VehicleManagement.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CarController> _logger;
        public CarController(IMediator mediator, ILogger<CarController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Add a new car
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/car/add
        ///     {
        ///        "make": 2001,
        ///        "model": "Tesla",
        ///        "price": 120000,
        ///        "doors":4,
        ///        "carBodyType":0,
        ///        "brand":"Tesla"
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>A new car </returns>
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
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets all the cars in the system
        /// </summary>
        /// <returns></returns>
        
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
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a car entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates a car
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /api/v1/car/update
        ///     {
        ///        "id":"1234-12312-1231-12313",
        ///        "make": 2001,
        ///        "model": "Tesla",
        ///        "price": 120000,
        ///        "doors":4,
        ///        "carBodyType":0,
        ///        "brand":"Tesla"
        ///     }
        ///
        /// </remarks>
        /// <param name="updateCarCommand"></param>
        /// <returns></returns>
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
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest();
            }
        }
    }
}
