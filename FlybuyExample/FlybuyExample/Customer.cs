using System;

namespace FlybuyExample
{
    public class Customer
    {
        public string Name, CarType, CarColor, CarLicense, Phone;

        public Customer(string name, string carType, string carColor, string carLicense, string phone)
        {
            Name = name;
            CarType = carType;
            CarColor = carColor;
            CarLicense = carLicense;
            Phone = phone;
        }
    }
}
