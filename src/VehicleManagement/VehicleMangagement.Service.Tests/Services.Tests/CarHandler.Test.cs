using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleManagement.Data.Repository.Implementation;
using VehicleManagement.Data.Repository.Interface;
using VehicleManagement.Domain.Commands.CarCommands;
using VehicleManagement.Domain.Dtos;
using VehicleManagement.Domain.Entities;
using VehicleManagement.Domain.Enum;
using VehicleManagement.Service.Services;
using VehicleManagement.Util.Mapper;
using Xunit;

namespace VehicleMangagement.Service.Tests.Services.Tests
{
    public class CarHandlerTests
    {
        public CarHandlerTests()
        {

        }
        [Fact]
        public async void Handle_AddCar_IdShouldSame()
        {
            var id = Guid.NewGuid();
            var mediator = new Mock<IMediator>();
            var carRepository = new Mock<ICarRepository>();
            var mapper = new AutoMapperConfiguration().GetMapper();

            var car = new Mock<Car>();
            car.SetupGet(s => s.Id).Returns(Guid.Parse(id.ToString()));

            var carCommand = new AddCarCommand(2000, "Kia211", 100.00m, "Kia", 2, CarBodyType.HatchBack);
            var carInput = mapper.Map<Car>(carCommand);
           
            carRepository.Setup(cr => cr.AddAsync(It.IsAny<Car>())).ReturnsAsync(car.Object);
            
            var handler = new AddCarHandler(mapper, mediator.Object, carRepository.Object);
            var addedCar = await handler.Handle(carCommand, new CancellationTokenSource().Token);
            addedCar.Id.Should().Be(id);
        }


        [Fact]
        public async void Handle_DeleteCar_IfCarNotFound()
        {
            var mediator = new Mock<IMediator>();
            var carRepository = new Mock<ICarRepository>();
            var mapper = new AutoMapperConfiguration().GetMapper();

            var carCommand = new DeleteCarCommand(Guid.NewGuid());
            
            carRepository.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Car)null);
            
            var handler = new DeleteCarHandler(mapper, mediator.Object, carRepository.Object);
            var deletedCar = await handler.Handle(carCommand, new CancellationTokenSource().Token);
            deletedCar.Should().BeNull();
        }

        [Fact]
        public async void Handle_DeleteCar_IfCarFound()
        {
            var id = Guid.NewGuid();
            var mediator = new Mock<IMediator>();
            var carRepository = new Mock<ICarRepository>();
            var mapper = new AutoMapperConfiguration().GetMapper();

            var car = new Mock<Car>();
            car.SetupGet(s => s.Id).Returns(Guid.Parse(id.ToString()));


            var carCommand = new DeleteCarCommand(Guid.NewGuid());

            carRepository.Setup(cr => cr.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(car.Object);
            carRepository.Setup(cr => cr.Delete(It.IsAny<Car>())).Returns(car.Object);

            var handler = new DeleteCarHandler(mapper, mediator.Object, carRepository.Object);
            var deletedCar = await handler.Handle(carCommand, new CancellationTokenSource().Token);
            deletedCar.Id.Should().Be(id);
        }
    }
}
