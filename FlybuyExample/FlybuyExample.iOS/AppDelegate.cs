using Foundation;
using UIKit;

using CoreLocation;
using FlyBuy;

namespace FlybuyExample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        CLLocationManager locationManager;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();

            // Configure SDK
            var token = "102.token";
            var opts = FlyBuyConfigOptions.BuilderWithToken(token).SetDeferredLocationTracking(true).Build;
            FlyBuyCore.ConfigureWithOptions(opts);

            // Configure Pickup Module
            FlyBuyPickupManager.Shared.Configure();

            return base.FinishedLaunching(app, options);
        }
    }
}
