using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FlybuyExample
{
    public partial class MainPage : ContentPage
    {
        public Customer Customer { get; set; }

        public MainPage()
        {
            InitializeComponent();
            // This is optional, but provides better layout for the iPhone X 
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            var FlybuyService = DependencyService.Get<IFlybuyService>();
            if (FlybuyService != null)
            {
                Customer = FlybuyService.CurrentCustomer();
            }

            if (null == Customer)
            {
                Customer = new Customer();
            }

            BindingContext = this;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var FlybuyService = DependencyService.Get<IFlybuyService>();
            if (FlybuyService != null)
            {
                if (Customer.Token != null)
                {
                    FlybuyService.UpdateCustomer(Customer);
                } else
                {
                    FlybuyService.CreateCustomer(Customer);
                }
            }
            //((Button)sender).Text = $"You clicked {count} times.";
        }
    }
}
