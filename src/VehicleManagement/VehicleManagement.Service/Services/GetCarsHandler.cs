using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VehicleManagement.Data.Repository.Interface;
using VehicleManagement.Domain.Commands.CarCommands;
using VehicleManagement.Domain.Dtos;
using VehicleManagement.Domain.Entities;
using VehicleManagement.Domain.Queries;

namespace VehicleManagement.Service.Services
{
    public class GetCarsHandler: IRequestHandler<GetCarsQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
       
        public GetCarsHandler(IMapper mapper, IMediator mediator, ICarRepository carRepository )
        {
            _carRepository = carRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<List<CarDto>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carRepository.GetAllAsync();
            return _mapper.Map<List<CarDto>>(cars);
        }
    }
}