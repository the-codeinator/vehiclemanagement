using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Domain.Entities
{
    public class VehicleBase: Base
    {
        public virtual int Make { get; private set; }
        public virtual string Model { get; private set; }
        public virtual decimal Price { get; private set; }
        public virtual string Brand { get; private set; }

        protected VehicleBase()
        {

        }

        public VehicleBase(int make, string model, decimal price, string brand)
        {
            Make = make;
            Model = model;
            Price = price;
            Brand = brand;
        }

        public void SetMake(int make)
        {
            Make = make;
        }
        public void SetModel(string model)
        {
            Model = model;
        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }
        public void SetBrand(string brand)
        {
            Brand = brand;
        }

    }
}
