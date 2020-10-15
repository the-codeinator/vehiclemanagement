using System;
using System.Collections.Generic;
using System.Text;
using VehicleManagement.Domain.Dtos;
using VehicleManagement.Domain.Enum;

namespace VehicleManagement.Domain.Entities
{
    public class Car: VehicleBase
    {
        public int Doors { get; private set; } 
        public CarBodyType BodyType { get; private set; }

        protected Car()
        {

        }

        public Car(int make, string model, decimal price, string brand,
            int doors, CarBodyType bodyType): base(make, model, price, brand)
        {
            Doors = doors;
            BodyType = bodyType;
        }

        public void Update(int make, string model, decimal price, string brand,
            int doors, CarBodyType bodyType)
        {
            //base class
            SetMake(make);
            SetModel(model);
            SetPrice(price);
            SetBrand(brand);
            SetDoor(doors);
            SetCarBodyType(bodyType);
        }

        public void SetDoor(int doors)
        {
            Doors = doors;
        }
        public void SetCarBodyType(CarBodyType bodyType)
        {
            BodyType = bodyType;
        }
    }
}
