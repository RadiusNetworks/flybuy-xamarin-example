using System;

using FlyBuy;
using FlybuyExample.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FlybuyService))]
namespace FlybuyExample.Droid
{
    public class FlybuyService : IFlybuyService
    {
        public Order ClaimOrder(string redemptionCode, string pickupType)
        {
            throw new NotImplementedException();
        }

        public Customer CreateCustomer(string name, string carType, string carColor, string carLicense, string phone)
        {
            throw new NotImplementedException();
        }

        public Order CreateOrder(string siteNumber, string orderNumber, string pickupType, DateTime pickupStart, DateTime pickupEnd)
        {
            throw new NotImplementedException();
        }

        public Customer CurrentCustomer()
        {
            FlyBuy.Data.Customer flybuyCustomer = Core.customer.Current;
            if (flybuyCustomer == null)
            {
                return null;
            }
            else
            {
                return new Customer(
                    flybuyCustomer.Name,
                    flybuyCustomer.CarType,
                    flybuyCustomer.CarColor,
                    flybuyCustomer.LicensePlate,
                    flybuyCustomer.Phone);
            }
        }

        public Order[] GetOrders()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(string orderNumber, string customerState)
        {
            throw new NotImplementedException();
        }
    }
}
