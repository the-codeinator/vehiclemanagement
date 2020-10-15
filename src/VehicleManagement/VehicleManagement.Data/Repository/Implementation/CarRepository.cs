using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.Data.Repository.Interface;
using VehicleManagement.Domain.Entities;

namespace VehicleManagement.Data.Repository.Implementation
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(VehicleDbContext dbContext) : base(dbContext)
        {
        }
    }
}
