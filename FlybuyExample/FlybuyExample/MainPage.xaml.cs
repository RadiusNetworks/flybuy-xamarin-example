using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;

namespace FlybuyExample
{
    public partial class MainPage : ContentPage
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public ObservableCollection<Order> Orders
        {
            get { return DependencyService.Get<IFlybuyService>().GetOrders(); }
        }
        public ObservableCollection<Site> Sites
        {
            get { return DependencyService.Get<IFlybuyService>().GetSites(); }
        }
        public Site Site { get; set; }
        public IList<string> PickupTypes { get; }

        public MainPage()
        {
            InitializeComponent();

            Order = new Order();
            PickupTypes = new List<string> { "pickup", "curbside" };

            var flybuyService = DependencyService.Get<IFlybuyService>();
            if (flybuyService != null)
            {
                Customer = flybuyService.CurrentCustomer();
                if (Customer == null)
                {
                    Console.WriteLine("No Customer");
                    Customer = new Customer();
                }
                else
                {
                    Console.WriteLine("Customer " + Customer.Name + " Exists");
                }
            }

            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += async (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received: " + p.Data["body"]);

                //var flybuyService = DependencyService.Get<IFlybuyService>();
                if (flybuyService != null)
                {
                    flybuyService.OnMessageReceived(p.Data);
                }
            };

            // Push message open event
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
            };

            BindingContext = this;
        }

        private void Redeem_Order(object sender, EventArgs e)
        {
            string redemptionCode = Order.Code;

            var FlybuyService = DependencyService.Get<IFlybuyService>();
            if (FlybuyService != null)
            {
                if (redemptionCode != null)
                {
                    FlybuyService.ClaimOrder(Order, Customer);
                }
                
                FlybuyService.FetchOrders();
            }

            Console.WriteLine("Fetched order count: " + Orders.Count);
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
                if (Customer != null && Customer.Token != null)
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

        private void Enroute(object sender, EventArgs e)
        {
            UpdateCustomerState("en_route");
        }

        private void Arrived(object sender, EventArgs e)
        {
            UpdateCustomerState("waiting");
        }

        private void Done(object sender, EventArgs e)
        {
            UpdateCustomerState("completed");
        }

        private void UpdateCustomerState(string state)
        {
            if (Orders.Count > 0)
            {
                Order = Orders[0];
                var FlybuyService = DependencyService.Get<IFlybuyService>();
                if (FlybuyService != null && Order != null)
                {
                    FlybuyService.UpdateOrder(Order, state);
                }
            }
        }
    }
}