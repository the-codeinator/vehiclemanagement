using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Domain.Entities
{
    public class Base: IEntity
    {
        public virtual Guid Id { get; private set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
