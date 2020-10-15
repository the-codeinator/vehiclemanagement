using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.Domain.Commands.CarCommands;
using VehicleManagement.Domain.Dtos;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Util.Mapper
{
    public class AutoMapperConfiguration
    {
        public IMapper GetMapper()
        {
            return GetMapperConfiguration().CreateMapper();
        }

        private MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Car, CarDto>().ReverseMap();
                cfg.CreateMap<AddCarCommand, Car>();
            });
         }
    }
}
