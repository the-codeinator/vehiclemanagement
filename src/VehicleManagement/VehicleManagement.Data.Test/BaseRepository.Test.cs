using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.AppConfig;
using VehicleManagement.Data.Repository.Implementation;
using VehicleManagement.Data.Repository.Interface;
using VehicleManagement.Domain.Entities;
using VehicleManagement.Domain.Enum;
using Xunit;

namespace VehicleManagement.Data.Test
{
    public class CarRepositoryTests
    {
        private readonly IRepository<Car> _carRepository;
        private readonly VehicleDbContext _vehicleDbContext;
        public CarRepositoryTests()
        {
            var config = new Config("devtest");
            var dbContextFactory = new DbContextFactory();
            _vehicleDbContext = dbContextFactory.CreateContext(config.ConnectionString);
            _carRepository = new CarRepository(_vehicleDbContext);
        }

        [Fact]
        public async Task CarRepository_CheckAdd()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();
            var count = _vehicleDbContext.Cars.Count();
            count.Should().Be(1);
            car.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CarRepository_CheckListAll()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            car = new Car(2012, "Bmwv2qw01", 200000, "BMW", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            car = new Car(2016, "FerraiAa324", 300000, "FerraiAa324", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            var cars = await _carRepository.GetAllAsync();
            cars.Count().Should().Be(3);
        }

        [Fact]
        public async Task CarRepository_Delete()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            var car1 = new Car(2012, "Bmwv2qw01", 200000, "BMW", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car1);
            var car2 = new Car(2016, "FerraiAa324", 300000, "FerraiAa324", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car2);
            await _carRepository.SaveChangesAsync();

            var deletedCar = _carRepository.Remove(car2);
            await _carRepository.SaveChangesAsync();

            var cars = await _vehicleDbContext.Cars.ToListAsync();
            Assert.DoesNotContain(cars, a => a.Id == deletedCar.Id);
        }

        [Fact]
        public async Task CarRepository_CheckIfMakeUpdates()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            var savedCar = await _carRepository.GetByIdAsync(car.Id);
            savedCar.SetMake(2012);
            await _carRepository.SaveChangesAsync();

            var updatedCar = await _carRepository.GetByIdAsync(car.Id);
            updatedCar.Make.Should().Be(2012);
        }

        [Fact]
        public async Task CarRepository_CheckIfModelUpdates()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            var savedCar = await _carRepository.GetByIdAsync(car.Id);
            savedCar.SetModel("Toyota123");
            await _carRepository.SaveChangesAsync();

            var updatedCar = await _carRepository.GetByIdAsync(car.Id);
            updatedCar.Model.Should().Be("Toyota123");
        }
        [Fact]
        public async Task CarRepository_CheckIfPriceUpdates()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            var savedCar = await _carRepository.GetByIdAsync(car.Id);
            savedCar.SetPrice(20000);
            await _carRepository.SaveChangesAsync();

            var updatedCar = await _carRepository.GetByIdAsync(car.Id);
            updatedCar.Price.Should().Be(20000);
        }
        [Fact]
        public async Task CarRepository_CheckIfBrandUpdates()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            var savedCar = await _carRepository.GetByIdAsync(car.Id);
            savedCar.SetBrand("Toyota");
            await _carRepository.SaveChangesAsync();

            var updatedCar = await _carRepository.GetByIdAsync(car.Id);
            updatedCar.Brand.Should().Be("Toyota");
        }
        [Fact]
        public async Task CarRepository_CheckIfDoorUpdates()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            var savedCar = await _carRepository.GetByIdAsync(car.Id);
            savedCar.SetDoor(2);
            var count = await _carRepository.SaveChangesAsync();

            var updatedCar = await _vehicleDbContext.Cars.
                FirstOrDefaultAsync(f => f.Id == car.Id);
            updatedCar.Doors.Should().Be(2);
        }
        [Fact]
        public async Task CarRepository_CheckIfBodyTypeUpdates()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            var savedCar = await _carRepository.GetByIdAsync(car.Id);
            savedCar.SetCarBodyType(CarBodyType.HatchBack);
            await _carRepository.SaveChangesAsync();

            var updatedCar = await _carRepository.GetByIdAsync(car.Id);
            updatedCar.BodyType.Should().Be(CarBodyType.HatchBack);
        }

        [Fact]
        public async Task CarRepository_CheckIfUpdatedAtUpdates()
        {
            var car = new Car(2006, "Brv2001", 100000, "Honda", 4, CarBodyType.Sedan);
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();
            var updatedAt = car.UpdatedAt;

            var savedCar = await _carRepository.GetByIdAsync(car.Id);
            savedCar.SetCarBodyType(CarBodyType.HatchBack);
            await _carRepository.SaveChangesAsync();

            var updatedCar = await _carRepository.GetByIdAsync(car.Id);
            updatedCar.UpdatedAt.Should().BeAfter(updatedAt);
        }

    }

}
