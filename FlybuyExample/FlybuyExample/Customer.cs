using System;
using Xamarin.Forms;

namespace FlybuyExample
{
    public class Customer : BindableObject
    {
        public Customer()
        {
            Name = "";
            CarType = "";
            CarColor = "";
            CarLicense = "";
            Phone = "";
        }

        public Customer(string name, string phone, string carType, string carColor, string carLicense, string token)
        {
            Name = name;
            Phone = phone;
            CarType = carType;
            CarColor = carColor;
            CarLicense = carLicense;
            Token = token;
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string CarType { get; set; }
        public string CarColor { get; set; }
        public string CarLicense { get; set; }
        public string Token { get; set; }
    }
}
