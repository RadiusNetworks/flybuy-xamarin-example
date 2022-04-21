# FlyBuy Xamarin.forms Example App

Example implimentation of the FlyBuy SDK native Xamarin bindings within a Xamain.forms app. Replace the default tokens to link to your FlyBuy project.

## Android

Replace the string `101.token` in `MainActivity.cs` with an app token set up in the FlyBuy project.

 * `FlybuyExample/FlybuyExample.Android/MainActivity.cs`

```
            string appToken = "101.token";

            Core.Configure(this, appToken);

            var Pickup = PickupManager.Manager.GetInstance(null) as PickupManager;
            Pickup.Configure(this);
```

## iOS

Replace the string `102.token` in `AppDelegate.cs` with an app token set up in the FlyBuy project.

 * `FlybuyExample/FlybuyExample.iOS/AppDelegate.cs`

```
            var opts = new NSDictionary<NSString, NSObject>(
                new NSString("token"), new NSString("102.token")
            );

            FlyBuyCore.Configure(opts);
            FlyBuyPickupManager.Shared.Configure();
```
