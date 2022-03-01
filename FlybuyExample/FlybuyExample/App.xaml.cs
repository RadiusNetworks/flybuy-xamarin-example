using System;
using Xamarin.Forms;

namespace FlybuyExample
{
    public partial class App : Application
    {
        public Customer customer;

        public App()
        {
            InitializeComponent();

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
