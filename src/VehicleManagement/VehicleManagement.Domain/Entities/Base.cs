using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Domain.Entities
{
    public class Base: IEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
