using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehicleManagement.Domain.Dtos;

namespace VehicleManagement.Domain.Queries
{
    public class GetCarQuery: QueryBase<CarDto>
    {
        public GetCarQuery(Guid id)
        {
            Id = id;
        }

        [Required]
        public Guid Id { get; set; }
    }
}
