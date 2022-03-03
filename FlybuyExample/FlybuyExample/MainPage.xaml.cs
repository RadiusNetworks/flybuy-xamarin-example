﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using DatePicker = Xamarin.Forms.DatePicker;

namespace FlybuyExample
{
    public partial class MainPage : ContentPage
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Site> Sites { get; set; }
        public Site Site { get; set; }
        public IList<string> PickupTypes { get; }

        public MainPage()
        {
            InitializeComponent();

            var FlybuyService = DependencyService.Get<IFlybuyService>();
            if (FlybuyService != null)
            {
                Sites = FlybuyService.GetSites();
                Customer = FlybuyService.CurrentCustomer();
            }

            if (null == Customer)
            {
                Customer = new Customer();
            }

            Order = new Order();
            PickupTypes = new List<string> { "pickup", "curbside" };

            BindingContext = this;
        }

        private void Redeem_Order(object sender, EventArgs e)
        {
            string redemptionCode = Order.Code;

            var FlybuyService = DependencyService.Get<IFlybuyService>();
            if (FlybuyService != null && redemptionCode != null)
            {
                FlybuyService.ClaimOrder(Order, Customer);
                FlybuyService.FetchOrders();
            }

            Console.WriteLine("ORDERS: " + FlybuyService.GetOrders().Count);
        }

        private void Create_Order(object sender, EventArgs e)
        {
            Order.PickupStart = datePicker.Date.Add(timePicker.Time).ToUniversalTime();

            var FlybuyService = DependencyService.Get<IFlybuyService>();
            if (FlybuyService != null)
            {
                FlybuyService.CreateOrder(Order, Customer);
            }
        }

        private void Update_Customer(object sender, EventArgs e)
        {
            var FlybuyService = DependencyService.Get<IFlybuyService>();
            if (FlybuyService != null)
            {
                if (Customer.Token != null)
                {
                    FlybuyService.UpdateCustomer(Customer);
                }
                else
                {
                    FlybuyService.CreateCustomer(Customer);
                }
            }
            //((Button)sender).Text = $"You clicked {count} times.";
        }
    }
}
