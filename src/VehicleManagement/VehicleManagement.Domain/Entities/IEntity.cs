using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime UpdatedAt { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
