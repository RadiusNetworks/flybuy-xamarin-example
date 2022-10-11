using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Runtime;

using Firebase;
using Plugin.FirebasePushNotification;

using FlyBuy;
using FlyBuy.Pickup;

[Application]
public class MainApplication : Application
{
    public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();

        string appToken = "427.83r3299CtMi8H2LdNy4ZxAFr";
        Core.Configure(this, appToken);

        var Pickup = PickupManager.Manager.GetInstance(null) as PickupManager;
        Pickup.Configure(this);

        //Set the default notification channel for your app when running Android Oreo
        if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
        {
            //Change for your default notification channel id here
            FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

            //Change for your default notification channel name here
            FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
        }

        //If debug you should reset the token each time.
#if DEBUG
        //FirebasePushNotificationManager.Initialize(this, true);
#else
        FirebasePushNotificationManager.Initialize(this, false);
#endif

        CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
        {
            System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");

            Core.OnNewPushToken(p.Token);
        };
    }

    class Callback : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction1
    {
        public Java.Lang.Object Invoke(Java.Lang.Object p0)
        {
            var error = p0 as FlyBuy.Data.SdkError;

            if (error != null)
            {
                Console.WriteLine("Callback error: " + error.UserError());
            }

            return null;
        }
    }
}
