using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehicleManagement.Domain.Dtos;

namespace VehicleManagement.Domain.Commands.CarCommands
{
    public class DeleteCarCommand: CommandBase<CarDto>
    {
        protected DeleteCarCommand()
        {

        }
        [Required]
        public Guid Id { get; }
        public DeleteCarCommand(Guid id)
        {
            Id = id;
        }
    }
}
