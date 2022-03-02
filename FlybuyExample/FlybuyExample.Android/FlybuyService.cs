using System;
using System.Collections.Generic;

using FlyBuy;
using FlybuyExample.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FlybuyService))]
namespace FlybuyExample.Droid
{
    public class FlybuyService : IFlybuyService
    {
        private CustomerCallback CustomerCallback;
        private OrderCallback OrderCallback;
        public static Java.Util.ISortedSet Sites;

        public FlybuyService()
        {
            CustomerCallback = new CustomerCallback();
            OrderCallback = new OrderCallback();
            Core.sites.FetchAll("1", new SitesCallback());
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
                CustomerCallback);
        }

        public void UpdateCustomer(Customer customer)
        {
            var customerInfo = new FlyBuy.Data.CustomerInfo(
                customer.Name,
                customer.Phone,
                customer.CarType,
                customer.CarColor,
                customer.CarLicense);
            FlyBuy.Core.customer.Update(customerInfo, CustomerCallback);
        }

        public void CreateOrder(Order order, Customer customer)
        {
            var customerInfo = new FlyBuy.Data.CustomerInfo(
                customer.Name,
                customer.Phone,
                customer.CarType,
                customer.CarColor,
                customer.CarLicense);

            DateTime epoch = DateTime.UnixEpoch;
            TimeSpan ts = order.PickupStart.Subtract(epoch);
            ThreeTen.BP.Instant x = ThreeTen.BP.Instant.OfEpochMilli((long)ts.TotalMilliseconds);
            var pickupWindow = new FlyBuy.Data.PickupWindow(x);

            FlyBuy.Core.orders.Create(
                order.SiteId(),
                order.Number,
                customerInfo,
                pickupWindow,
                "created",
                order.PickupType,
                OrderCallback);
        }

        public Order ClaimOrder(string redemptionCode, string pickupType)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order, string customerState)
        {
            throw new NotImplementedException();
        }

        public Customer CurrentCustomer()
        {
            FlyBuy.Data.Customer customer = Core.customer.Current;
            if (customer == null)
            {
                return null;
            }
            else
            {
                return new Customer(
                    customer.Name,
                    customer.Phone,
                    customer.CarType,
                    customer.CarColor,
                    customer.LicensePlate,
                    customer.ApiToken);
            }
        }

        public Order[] GetOrders()
        {
            throw new NotImplementedException();
        }

        public IList<Site> GetSites()
        {
            IList<Site> sites = new List<Site>();

            foreach (FlyBuy.Data.Site site in Core.sites.All)
            {
                sites.Add(new Site(site.Id, site.PartnerIdentifier, site.Name, site.Description));
            }

            return sites;
        }
    }

    class CustomerCallback : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction2
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
                Console.WriteLine("Customer [" + customer.Name + "] callback success");
            }

            return null;
        }
    }

    class OrderCallback : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction2
    {
        public Java.Lang.Object Invoke(Java.Lang.Object p0, Java.Lang.Object p1)
        {
            var order = p0 as FlyBuy.Data.Order;
            var error = p1 as FlyBuy.Data.SdkError;

            if (error != null)
            {
                Console.WriteLine("Order callback error: " + error.UserError());
            }
            else
            {
                Console.WriteLine("Order [" + order.PartnerIdentifier + "] callback success");
            }

            return null;
        }
    }

    class SitesCallback : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction2
    {
        public Java.Lang.Object Invoke(Java.Lang.Object p0, Java.Lang.Object p1)
        {
            var error = p1 as FlyBuy.Data.SdkError;

            if (error != null)
            {
                Console.WriteLine("Sites callback error: " + error.UserError());
            }

            return null;
        }
    }
}
