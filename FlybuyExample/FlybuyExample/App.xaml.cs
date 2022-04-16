using System;
using System.Collections.Generic;
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
                flybuyService.GetSites();
                flybuyService.GetOrders();
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
