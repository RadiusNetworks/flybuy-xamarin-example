using System;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;

namespace FlybuyExample
{
    public partial class App : Application
    {
        public Customer customer;

        public App()
        {
            InitializeComponent();

            // Token event
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");

            };
            //Push message received event
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }
            };

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            var flybuyService = DependencyService.Get<IFlybuyService>();
            if (flybuyService != null)
            {
                customer = flybuyService.CurrentCustomer();
                if (customer == null)
                {
                    Console.WriteLine("No Customer");
                }
                else
                {
                    Console.WriteLine("Customer " + customer.Name + " Exists");
                }
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
