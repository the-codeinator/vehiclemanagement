using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.Domain.Enum;

namespace VehicleManagement.Domain.Dtos
{
    public class CarDto : VehicleBaseDto
    {
        public int Doors { get; set; }
        public CarBodyType BodyType { get; set; }

    }
}
