using System.Collections.Generic;

using Android;
using Android.OS;
using Android.App;
using Android.Content.PM;
using AndroidX.Core.App;
using AndroidX.Core.Content;

using FlyBuy;
using FlyBuy.Pickup;
using Plugin.FirebasePushNotification;

namespace FlybuyExample.Droid
{
    [Activity(Label = "FlybuyExample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int IRIS_PERMISSIONS_REQUEST = 42;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        protected override void OnStart()
        {
            base.OnStart();
            RequestPermissions();
        }

        private void RequestPermissions()
        {
            var permissions = CheckForMissingPermissions();
            if (permissions.Count > 0)
            {
                ActivityCompat.RequestPermissions(this, permissions.ToArray(), IRIS_PERMISSIONS_REQUEST);
            }
            else
            {
                var Pickup = PickupManager.Manager.GetInstance(null) as PickupManager;
                Pickup.OnPermissionChanged();
            }
        }

        private List<string> CheckForMissingPermissions()
        {
            var permissionsToRequest = new List<string>();

            if (ContextCompat.CheckSelfPermission(this,
                Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
            {
                permissionsToRequest.Add(Manifest.Permission.AccessCoarseLocation);
            }
            if (ContextCompat.CheckSelfPermission(this,
                Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted)
            {
                permissionsToRequest.Add(Manifest.Permission.AccessFineLocation);
            }

            return permissionsToRequest;
        }
    }
}
