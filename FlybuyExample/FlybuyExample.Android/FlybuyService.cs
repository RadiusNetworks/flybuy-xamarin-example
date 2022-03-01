using System;

using FlyBuy;
using FlybuyExample.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FlybuyService))]
namespace FlybuyExample.Droid
{
    public class FlybuyService : IFlybuyService
    {
        private customerCallback customerCallback;

        public FlybuyService()
        {
            customerCallback = new customerCallback();
        }

        public Order ClaimOrder(string redemptionCode, string pickupType)
        {
            throw new NotImplementedException();
        }

        public void CreateCustomer(Customer customer)
        {
            var customerInfo = new FlyBuy.Data.CustomerInfo(
                customer.Name,
                customer.Phone,
                customer.CarType,
                customer.CarColor,
                customer.CarLicense);
            FlyBuy.Core.customer.Create(
                customerInfo,
                true, true,
                null, null,
                customerCallback);
        }

        public void UpdateCustomer(Customer customer)
        {
            var customerInfo = new FlyBuy.Data.CustomerInfo(
                customer.Name,
                customer.Phone,
                customer.CarType,
                customer.CarColor,
                customer.CarLicense);
            FlyBuy.Core.customer.Update(customerInfo, customerCallback);
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
                    flybuyCustomer.Phone,
                    flybuyCustomer.CarType,
                    flybuyCustomer.CarColor,
                    flybuyCustomer.LicensePlate,
                    flybuyCustomer.ApiToken);
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

    class customerCallback : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction2
    {
        public Java.Lang.Object Invoke(Java.Lang.Object p0, Java.Lang.Object p1)
        {
            var customer = p0 as FlyBuy.Data.Customer;
            var error = p1 as FlyBuy.Data.SdkError;

            if (error != null)
            {
                Console.WriteLine("Customer callback error: " + error.UserError());
            }
            else
            {
                Console.WriteLine("Customer callback success");
            }

            return null;
        }
    }
}
