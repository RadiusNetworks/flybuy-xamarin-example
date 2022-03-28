using System;
using System.Collections.Generic;
using FlyBuy;
using FlybuyExample.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(FlybuyService))]
namespace FlybuyExample.iOS
{
    public class FlybuyService : IFlybuyService
    {
        static private IList<Site> sites = new List<Site>();
        //private CustomerCallback CustomerCallback;
        //private OrderCallback OrderCallback;

        public FlybuyService()
        {
            //CustomerCallback = new CustomerCallback();
            //OrderCallback = new OrderCallback();
        }

        static FlyBuyCustomerInfo CustomerInfo(Customer customer) => new FlyBuyCustomerInfo(
                customer.Name,
                customer.Phone,
                customer.CarType,
                customer.CarColor,
                customer.CarLicense);

        public void CreateCustomer(Customer customer)
        {
            FlyBuyCore.Customer.Create(
                CustomerInfo(customer),
                true, true,
                (FlyBuyCustomer customer1, Foundation.NSError error) =>
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
            FlyBuyCore.Customer.Update(
                CustomerInfo(customer),
                (FlyBuyCustomer customer1, Foundation.NSError error) =>
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

        public void CreateOrder(Order order, Customer customer)
        {
            DateTime epoch = DateTime.UnixEpoch;
            TimeSpan ts = order.PickupStart.Subtract(epoch);
            Foundation.NSDate start = new Foundation.NSDate((long)ts.TotalMilliseconds);
            var pickupWindow = new FlyBuyPickupWindow(start, null);

            FlyBuyCore.Orders.CreateWithSiteID(
                order.SiteId(),
                order.Number,
                CustomerInfo(customer),
                pickupWindow,
                "created",
                order.PickupType,
                (FlyBuyOrder order1, Foundation.NSError error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine("Create order error: " + error.LocalizedDescription);
                    }
                    else
                    {
                        Console.WriteLine("Create order success");
                    }
                });
        }

        public void ClaimOrder(Order order, Customer customer)
        {
            FlyBuyCore.Orders.ClaimWithRedemptionCode(
                order.Code,
                CustomerInfo(customer),
                order.PickupType,
                (FlyBuyOrder order1, Foundation.NSError error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine("Claim order error: " + error.LocalizedDescription);
                    }
                    else
                    {
                        Console.WriteLine("Claim order success");
                    }
                });
        }

        public void UpdateOrder(Order order, string customerState)
        {
            FlyBuyCore.Orders.UpdateCustomerStateWithOrderID(
                order.Id,
                customerState,
                (FlyBuyOrder order1, Foundation.NSError error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine("Update order error: " + error.LocalizedDescription);
                    }
                    else
                    {
                        Console.WriteLine("Update order success");
                    }
                });
        }

        public void FetchOrders()
        {
            FlyBuyCore.Orders.FetchWithCallback(
                (Foundation.NSArray<FlyBuyOrder> order, Foundation.NSError error) =>
                {
                    if (error != null)
                    {
                        Console.WriteLine("Fetch orders error: " + error.LocalizedDescription);
                    }
                    else
                    {
                        Console.WriteLine("Fetch orders success");
                    }
                });
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
                    info.Phone,
                    info.CarType,
                    info.CarColor,
                    info.LicensePlate,
                    flybuyCustomer.Token);
            }
        }

        public IList<Order> GetOrders()
        {
            IList<Order> orders = new List<Order>();

            foreach (FlyBuyOrder order in FlyBuyCore.Orders.Open)
            {
                Site site = new Site(
                    (int)order.SiteID,
                    order.SitePartnerIdentifier,
                    order.SiteName,
                    order.SiteDescription);

                FlyBuyPickupWindow pickupWindow = order.PickupWindow;
                if (pickupWindow != null)
                {
                    double x = pickupWindow.Start.SecondsSince1970;
                    var startTime = new DateTime((long)x * 1000L);
                    orders.Add(
                        new Order(
                            site,
                            order.PartnerIdentifier,
                            order.PickupType,
                            startTime)
                        );
                }
                else
                {
                    orders.Add(
                        new Order(
                            site,
                            order.PartnerIdentifier,
                            order.PickupType)
                        );
                }
            }

            return orders;
        }

        public IList<Site> GetSites()
        {
            if (sites.Count == 0)
            {
                FlyBuyCore.Sites.FetchAllWithQuery("",
                    (Foundation.NSArray<FlyBuySite> sites1, Foundation.NSError error) =>
                    {
                        if (error != null)
                        {
                            Console.WriteLine("Site fetch error: " + error.LocalizedDescription);
                        }
                        else
                        {
                            foreach (FlyBuySite site in sites1)
                            {
                                sites.Add(new Site((int)site.Id, site.PartnerIdentifier, site.Name, site.Description));
                            }
                            Console.WriteLine("Site fetch success");
                        }
                    });
            }

            return sites;
        }

        public void OnMessageReceived(IDictionary<string, object> data)
        {
            Foundation.NSDictionary<Foundation.NSString, Foundation.NSString> q = new Foundation.NSDictionary<Foundation.NSString, Foundation.NSString>();
            foreach (var p in data)
            {
                System.Diagnostics.Debug.WriteLine($"{p.Key} : {p.Value}");
                q.TryAdd(new Foundation.NSString(p.Key), new Foundation.NSString(p.Value.ToString()));
            }
            FlyBuyCore.HandleRemoteNotification(q);
        }



    }
}
