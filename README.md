# FlyBuy Xamarin.forms Example App

Example implimentation of the FlyBuy SDK native Xamarin bindings within a Xamain.forms app. Replace the default tokens to link to your FlyBuy project.

## Android

Replace the string `101.token` in `MainActivity.cs` with an app token set up in the FlyBuy project.

 * `FlybuyExample/FlybuyExample.Android/MainActivity.cs`

```
            // Init ThreeTen lib
            AndroidThreeTen.Init(this);

            // Configure SDK
            string appToken = "101.token";
            ConfigOptions opts = new ConfigOptions.Builder(appToken).SetDeferredLocationTrackingEnabled(true).Build();
            Core.Configure(this, opts);

            // Configure Pickup Module
            var Pickup = PickupManager.Manager.GetInstance(null) as PickupManager;
            Pickup.Configure(this);
```

## iOS

Replace the string `102.token` in `AppDelegate.cs` with an app token set up in the FlyBuy project.

 * `FlybuyExample/FlybuyExample.iOS/AppDelegate.cs`

```
            // Configure SDK
            var token = "102.token";
            var opts = FlyBuyConfigOptions.BuilderWithToken(token).SetDeferredLocationTracking(true).Build;
            FlyBuyCore.ConfigureWithOptions(opts);

            // Configure Pickup Module
            FlyBuyPickupManager.Shared.Configure();
```
