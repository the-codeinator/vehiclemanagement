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

namespace VehicleManagement.Service.Services
{
    public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, CarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UpdateCarHandler(IMediator mediator, ICarRepository carRepository, IMapper  mapper)
        {
            _carRepository = carRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<CarDto> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByIdAsync(request.Id);
            if (car == null)
            {
                return null;
            }

            if(request.Brand != car.Brand)
                car.SetBrand(request.Brand);
            if(request.CarBodyType != car.BodyType)
                car.SetCarBodyType(request.CarBodyType);
            if(request.Model != car.Model)
                car.SetModel(request.Model);
            if (request.Price != car.Price)
                car.SetPrice(request.Price);
            if(request.Make != car.Make)
                car.SetMake(request.Make);
            if(request.Doors != car.Doors)
                car.SetDoor(request.Doors);

            await _carRepository.SaveChangesAsync();
            return _mapper.Map<CarDto>(car);
        }
    }
}
