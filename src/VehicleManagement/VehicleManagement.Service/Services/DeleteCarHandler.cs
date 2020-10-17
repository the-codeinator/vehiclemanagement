using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleManagement.Data.Repository.Interface;
using VehicleManagement.Domain.Commands.CarCommands;
using VehicleManagement.Domain.Dtos;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Service.Services
{
    public class DeleteCarHandler : IRequestHandler<DeleteCarCommand, CarDto>
    {
        private readonly IMediator _mediator;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public DeleteCarHandler(IMapper mapper, IMediator mediator, ICarRepository carRepository)
        {
            _carRepository = carRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<CarDto> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByIdAsync(request.Id);
            if(car == null)
            {
                return null;
            }
            car = _carRepository.Delete(car);
            await _carRepository.SaveChangesAsync();
            return _mapper.Map<CarDto>(car);
        }
    }
}
