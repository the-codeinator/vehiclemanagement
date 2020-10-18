using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Api.EndToEnd.Tests.Factory;
using VehicleManagement.Domain.Commands.CarCommands;
using VehicleManagement.Domain.Dtos;
using VehicleManagement.Domain.Enum;
using Xunit;

namespace VehicleManagement.Api.EndToEnd.Tests
{
    public class CarApiTests: BaseTest
    {
        private const string ADD_CAR_URL = "/api/v1/car/add";
        private const string DELETE_CAR_URL = "/api/v1/car";
        private const string UPDATE_CAR_URL = "/api/v1/car/update";
        private const string GET_CARS_URL = "/api/v1/car/cars";
        public CarApiTests(ApplicationFactory app): base(app)
        {
           
        }

        private async Task<HttpResponseMessage> AddCar(AddCarCommand addCarCommand, HttpClient client = null)
        {
            if(client == null)
            {
                client = _httpClient;
            }
            var carJson = JsonConvert.SerializeObject(addCarCommand);
            var content = new StringContent(carJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ADD_CAR_URL, content);
            return response;
        }

        [Fact]
        public async Task Car_CheckIfCarIsAdded()
        {
            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrari", 2, Domain.Enum.CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<CarDto>(result);

            car.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Car_AllGetCars()
        {
            //Add some default car

            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrari", 2, CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);

            addCarCommand = new AddCarCommand(2020, "Mercs", 121212.12m, "Mercedes", 4, CarBodyType.Sedan);
            response = await AddCar(addCarCommand);

            response = await _httpClient.GetAsync(GET_CARS_URL);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task Car_CheckIfCarIsDeleted()
        {
            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrari", 2, Domain.Enum.CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);

            var result = await response.Content.ReadAsStringAsync();
            var carIdTobeDeleted = JsonConvert.DeserializeObject<CarDto>(result).Id;

            addCarCommand = new AddCarCommand(2020, "Mercs", 121212.12m, "Mercedes", 4, Domain.Enum.CarBodyType.Sedan);
            response = await AddCar(addCarCommand);

            var deleted = await _httpClient.DeleteAsync($"{DELETE_CAR_URL}/{carIdTobeDeleted}");
            response = await _httpClient.GetAsync(GET_CARS_URL);
            result = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<List<CarDto>>(result);

            Assert.DoesNotContain(cars, c => c.Id == carIdTobeDeleted);
        }

        [Fact]
        public async Task Car_CheckIfMakeIsUpdated()
        {
            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrari", 
                2, Domain.Enum.CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);

            var result = await response.Content.ReadAsStringAsync();
            var addedCar = JsonConvert.DeserializeObject<CarDto>(result);

            var updateCarCommand = new UpdateCarCommand(addedCar.Id, 2012, addedCar.Model, 
                addedCar.Price, addedCar.Brand, addedCar.Doors, addedCar.BodyType);

            var content = new StringContent(JsonConvert.SerializeObject(updateCarCommand),
                Encoding.UTF8, "application/json");
            var updatedCarResponse = await _httpClient.PutAsync(UPDATE_CAR_URL, content);

            result = await updatedCarResponse.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<CarDto>(result);

            car.Make.Should().Be(2012);
        }

        [Fact]
        public async Task Car_CheckIfModelIsUpdated()
        {
            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrari",
                2, Domain.Enum.CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);

            var result = await response.Content.ReadAsStringAsync();
            var addedCar = JsonConvert.DeserializeObject<CarDto>(result);

            var updateCarCommand = new UpdateCarCommand(addedCar.Id, addedCar.Make, "Tesla123",
                addedCar.Price, addedCar.Brand, addedCar.Doors, addedCar.BodyType);

            var content = new StringContent(JsonConvert.SerializeObject(updateCarCommand),
                Encoding.UTF8, "application/json");
            var updatedCarResponse = await _httpClient.PutAsync(UPDATE_CAR_URL, content);

            result = await updatedCarResponse.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<CarDto>(result);

            car.Model.Should().Be("Tesla123");
        }

        [Fact]
        public async Task Car_CheckIfPriceIsUpdated()
        {
            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrari",
                2, Domain.Enum.CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);

            var result = await response.Content.ReadAsStringAsync();
            var addedCar = JsonConvert.DeserializeObject<CarDto>(result);

            var updateCarCommand = new UpdateCarCommand(addedCar.Id, addedCar.Make, addedCar.Model,
                10000m, addedCar.Brand, addedCar.Doors, addedCar.BodyType);

            var content = new StringContent(JsonConvert.SerializeObject(updateCarCommand),
                Encoding.UTF8, "application/json");
            var updatedCarResponse = await _httpClient.PutAsync(UPDATE_CAR_URL, content);

            result = await updatedCarResponse.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<CarDto>(result);

            car.Price.Should().Be(10000m);
        }

        [Fact]
        public async Task Car_CheckIfBrandIsUpdated()
        {
            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrai",
                2, Domain.Enum.CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);

            var result = await response.Content.ReadAsStringAsync();
            var addedCar = JsonConvert.DeserializeObject<CarDto>(result);

            var updateCarCommand = new UpdateCarCommand(addedCar.Id, addedCar.Make, addedCar.Model,
                addedCar.Price, "Tesla", addedCar.Doors, addedCar.BodyType);

            var content = new StringContent(JsonConvert.SerializeObject(updateCarCommand),
                Encoding.UTF8, "application/json");
            var updatedCarResponse = await _httpClient.PutAsync(UPDATE_CAR_URL, content);

            result = await updatedCarResponse.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<CarDto>(result);

            car.Brand.Should().Be("Tesla");
        }

        [Fact]
        public async Task Car_CheckIfDoorsIsUpdated()
        {
            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrai",
                2, Domain.Enum.CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);

            var result = await response.Content.ReadAsStringAsync();
            var addedCar = JsonConvert.DeserializeObject<CarDto>(result);

            var updateCarCommand = new UpdateCarCommand(addedCar.Id, addedCar.Make, addedCar.Model,
                addedCar.Price, addedCar.Brand, 4, addedCar.BodyType);

            var content = new StringContent(JsonConvert.SerializeObject(updateCarCommand),
                Encoding.UTF8, "application/json");
            var updatedCarResponse = await _httpClient.PutAsync(UPDATE_CAR_URL, content);

            result = await updatedCarResponse.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<CarDto>(result);

            car.Doors.Should().Be(4);
        }

        [Fact]
        public async Task Car_CheckIfBodyTypeIsUpdated()
        {
            var addCarCommand = new AddCarCommand(2000, "Ferrai123", 121212.12m, "Ferrai",
                2, Domain.Enum.CarBodyType.HatchBack);
            var response = await AddCar(addCarCommand);

            var result = await response.Content.ReadAsStringAsync();
            var addedCar = JsonConvert.DeserializeObject<CarDto>(result);

            var updateCarCommand = new UpdateCarCommand(addedCar.Id, addedCar.Make, addedCar.Model,
                addedCar.Price, addedCar.Brand, 2, Domain.Enum.CarBodyType.Sedan);

            var content = new StringContent(JsonConvert.SerializeObject(updateCarCommand),
                Encoding.UTF8, "application/json");
            var updatedCarResponse = await _httpClient.PutAsync(UPDATE_CAR_URL, content);

            result = await updatedCarResponse.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<CarDto>(result);

            car.BodyType.Should().Be(CarBodyType.Sedan);
        }

    }
}
