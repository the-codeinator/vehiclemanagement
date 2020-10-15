using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Domain.Queries
{
    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {

    }
}
