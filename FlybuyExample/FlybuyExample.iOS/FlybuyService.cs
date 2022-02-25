using System;

using FlyBuy;
using FlybuyExample.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(FlybuyService))]
namespace FlybuyExample.iOS
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
            FlyBuyCustomer flybuyCustomer = FlyBuyCore.Customer.Current;
            if (flybuyCustomer == null)
            {
                return null;
            }
            else
            {
                FlyBuyCustomerInfo info = flybuyCustomer.Info;
                return new Customer(
                    info.Name,
                    info.CarType,
                    info.CarColor,
                    info.LicensePlate,
                    info.Phone);
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
