﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Com.Radiusnetworks.Flybuy.Sdk.Manager.Builder;
using FlyBuy;
using FlyBuy.Data;
using FlybuyExample.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FlybuyService))]
namespace FlybuyExample.Droid
{
    public class FlybuyService : IFlybuyService
    {
        static private ObservableCollection<Site> Sites { get; set; }
        static private ObservableCollection<Order> Orders { get; set; }
        private CustomerCallback CustomerCallback;
        private OrderCallback OrderCallback;

        public FlybuyService()
        {
            CustomerCallback = new CustomerCallback();
            OrderCallback = new OrderCallback();
            Sites = new ObservableCollection<Site>();
            Orders = new ObservableCollection<Order>();
            Core.sites.FetchAll("", "live", new SitesCallback());
        }

        static FlyBuy.Data.CustomerInfo CustomerInfo(Customer customer) => new FlyBuy.Data.CustomerInfo(
                customer.Name,
                customer.Phone,
                customer.CarType,
                customer.CarColor,
                customer.CarLicense);

        public void CreateCustomer(Customer customer)
        {
            if (customer != null)
            {
                Core.customer.Create(
                    CustomerInfo(customer),
                    true, true,
                    null, null,
                    CustomerCallback);
            }
            else
            {
                Console.WriteLine("Create customer null");
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer != null)
            {
                Core.customer.Update(
                    CustomerInfo(customer),
                    CustomerCallback);
            }
            else
            {
                Console.WriteLine("Update customer null");
            }
        }

        public void CreateOrder(Order order, Customer customer)
        {
            DateTime epoch = DateTime.UnixEpoch;
            TimeSpan ts = order.PickupStart.Subtract(epoch);
            ThreeTen.BP.Instant x = ThreeTen.BP.Instant.OfEpochMilli((long)ts.TotalMilliseconds);
            var pickupWindow = new FlyBuy.Data.PickupWindow(x);

            if (customer != null)
            {
                OrderOptions orderOptions = new OrderOptions.Builder(customer.Name).
                    SetCustomerPhone(customer.Phone).
                    SetCustomerCarColor(customer.CarColor).
                    SetCustomerCarType(customer.CarType).
                    SetCustomerCarPlate(customer.CarLicense).
                    SetPartnerIdentifier(order.Number).
                    SetPickupWindow(pickupWindow).
                    SetState("ready").
                    SetPickupType(order.PickupType).
                    Build();

                Core.orders.Create(order.SiteId(), orderOptions, new OrderCallback());
            }
            else
            {
                Console.WriteLine("Create order customer null");
            }
        }

        public void FetchOrder(String code)
        {
            Core.orders.Fetch(
                code,
                OrderCallback);
        }

        public void ClaimOrder(Order order, Customer customer)
        {
            if (customer != null)
            {
                OrderOptions orderOptions = new OrderOptions.Builder(customer.Name).
                    SetCustomerPhone(customer.Phone).
                    SetCustomerCarColor(customer.CarColor).
                    SetCustomerCarType(customer.CarType).
                    SetCustomerCarPlate(customer.CarLicense).
                    SetPickupType(order.PickupType).
                    Build();

                Core.orders.Claim(order.Code, orderOptions, OrderCallback);
            }
            else
            {
                Console.WriteLine("Claim order customer null");
            }
        }

        public void UpdateOrder(Order order, string customerState)
        {
            Core.orders.UpdateCustomerState(
                order.Id,
                customerState,
                OrderCallback);
        }

        public void FetchOrders()
        {
            Core.sites.FetchByPartnerIdentifier("1116", "live", new SitesCallback());

            Core.orders.Fetch(OrderCallback);
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

        public ObservableCollection<Order> GetOrders()
        {
            Orders.Clear();
            foreach (FlyBuy.Data.Order order in Core.orders.Open)
            {
                FlyBuy.Data.Site flybuySite = order.Site;
                Site site = new Site(
                    flybuySite.Id,
                    flybuySite.PartnerIdentifier,
                    flybuySite.Name,
                    flybuySite.Description);

                FlyBuy.Data.PickupWindow pickupWindow = order.PickupWindow;
                if (pickupWindow != null)
                {
                    ThreeTen.BP.Instant x = pickupWindow.Start;
                    DateTime startTime = new DateTime(x.ToEpochMilli());
                    Orders.Add(
                        new Order(
                            (int)order.Id,
                            site,
                            order.PartnerIdentifier,
                            order.PickupType,
                            startTime)
                        );
                }
                else
                {
                    Orders.Add(
                        new Order(
                            (int)order.Id,
                            site,
                            order.PartnerIdentifier,
                            order.PickupType)
                        );
                }
            }

            return Orders;
        }

        public ObservableCollection<Site> GetSites()
        {
            if (Sites.Count == 0)
            {
                foreach (FlyBuy.Data.Site site in Core.sites.All)
                {
                    Sites.Add(new Site(site.Id, site.PartnerIdentifier, site.Name, site.Description));
                }
            }
            return Sites;
        }

        public void OnMessageReceived(IDictionary<string, object> data)
        {
            IDictionary<string, string> q = new Dictionary<string, string>();
            foreach (var p in data)
            {
                System.Diagnostics.Debug.WriteLine($"{p.Key} : {p.Value}");
                q.Add(p.Key, p.Value.ToString());
            }
            Core.OnMessageReceived(q, new Callback());
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
                if (order != null)
                {
                    string s = order.CustomerState;
                    string p = order.Customer.Phone;
                    string n = order.Customer.Name;
                    Console.WriteLine("Order Phone: " + p);
                    Console.WriteLine("Order Name: " + n);
                    Console.WriteLine("Order [" + order.PartnerIdentifier + "] callback success");
                }
                else
                {
                    //LiveData x = Core.orders.GetOrder(order.Id);
                    //x.Observe(this, null);

                    Console.WriteLine("Order callback success");
                }
            }

            return null;
        }
    }

    class SitesCallback : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction2
    {
        public Java.Lang.Object Invoke(Java.Lang.Object p0, Java.Lang.Object p1)
        {
            var site = p0 as FlyBuy.Data.Site;
            var error = p1 as FlyBuy.Data.SdkError;

            if (site != null)
            {
                Console.WriteLine("Getting back from sites --> " + site.Name);
            }

            if (error != null)
            {
                Console.WriteLine("Sites callback error: " + error.UserError());
            }

            return null;
        }
    }

    class Callback : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction1
    {
        public Java.Lang.Object Invoke(Java.Lang.Object p0)
        {
            var error = p0 as FlyBuy.Data.SdkError;

            if (error != null)
            {
                Console.WriteLine("Sites callback error: " + error.UserError());
            }

            return null;
        }
    }
}
