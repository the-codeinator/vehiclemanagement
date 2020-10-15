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
    public class AddCarHandler : IRequestHandler<AddCarCommand, CarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
       
        public AddCarHandler()
        {

        }
        public async Task<CarDto> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();
            return _mapper.Map<CarDto>(car);
        }
    }
}
