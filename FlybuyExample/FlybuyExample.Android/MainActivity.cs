using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using FlyBuy;

namespace FlybuyExample.Droid
{
    [Activity(Label = "FlybuyExample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            string appToken = "426.otMfWCDb4vsYKyZZtociLpJs";

            Core.Configure(this, appToken);
            ((FlyBuy.Pickup.PickupManager)FlyBuy.Pickup.PickupManager.Manager.GetInstance(null)).Configure(this);
        }
    }
}
