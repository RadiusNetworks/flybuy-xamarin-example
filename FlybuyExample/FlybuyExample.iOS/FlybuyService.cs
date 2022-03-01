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

        public void CreateCustomer(Customer customer)
        {
            FlyBuyCustomerInfo customerInfo = new FlyBuyCustomerInfo(
                customer.Name,
                customer.Phone,
                customer.CarType,
                customer.CarColor,
                customer.CarLicense);
            FlyBuyCore.Customer.Create(
                customerInfo,
                true, true,
                (FlyBuyCustomer customer, Foundation.NSError error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine("Create customer error: " + error.LocalizedDescription);
                    }
                    else
                    {
                        Console.WriteLine("Create customer success");
                    }
                });
        }

        public void UpdateCustomer(Customer customer)
        {
            FlyBuyCustomerInfo customerInfo = new FlyBuyCustomerInfo(
                customer.Name,
                customer.Phone,
                customer.CarType,
                customer.CarColor,
                customer.CarLicense);
            FlyBuyCore.Customer.Update(
                customerInfo,
                (FlyBuyCustomer customer, Foundation.NSError error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine("Update customer error: " + error.LocalizedDescription);
                    }
                    else
                    {
                        Console.WriteLine("Update customer success");
                    }
                });
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
