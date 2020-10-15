using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehicleManagement.Domain.Dtos;
using VehicleManagement.Domain.Enum;

namespace VehicleManagement.Domain.Commands.CarCommands
{
    public class AddCarCommand: CommandBase<CarDto>
    {
        public AddCarCommand(int make, string model, decimal price, string brand,
            int doors, CarBodyType carBodyType)
        {
            Make = make;
            Model = model;
            Price = price;
            Brand = brand;
            Doors = doors;
            CarBodyType = carBodyType;
        }

        [Required]
        public decimal Price { get; }
        [Required]
        public int Make { get;  }
        [Required]
        public int Doors { get; }

        [Required]
        [MaxLength(40)]
        public string Model { get; }

        [Required]
        [MaxLength(40)]
        public string Brand { get;  }

        [Required]
        public CarBodyType CarBodyType { get;  }
    }
}
