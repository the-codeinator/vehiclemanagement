using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Domain.Commands
{
    public abstract class CommandBase<T>: IRequest<T> where T: class
    {
    }
}
