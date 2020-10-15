using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Domain.Dtos
{
    public class VehicleBaseDto
    {
        public Guid Id { get;  set; }
        public int Make { get;  set; }
        public string Model { get;  set; }
        public decimal Price { get;  set; }
        public string Brand { get;  set; }
    }
}
