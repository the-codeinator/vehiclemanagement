using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.Domain.Dtos;

namespace VehicleManagement.Domain.Queries
{
    public class GetCarsQuery: QueryBase<List<CarDto>>
    {
        public GetCarsQuery()
        {
        }
    }
}
