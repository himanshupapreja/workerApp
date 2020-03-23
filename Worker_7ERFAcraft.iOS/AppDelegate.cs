using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Platform;
using Foundation;
using HockeyApp.iOS;
using Plugin.FirebasePushNotification;
using Plugin.FirebasePushNotification.Abstractions;
using Syncfusion.SfRating.XForms.iOS;
using UIKit;
using UserNotifications;
using Xamarin.Forms.Platform.iOS;

namespace Worker_7ERFAcraft.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            App.ScreenWidth = UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = UIScreen.MainScreen.Bounds.Height;

            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            Rg.Plugins.Popup.Popup.Init();
            InTheHand.Forms.Platform.iOS.InTheHandForms.Init();
            CachedImageRenderer.Init();
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            // Add the following line if you are using SfLinearProgressBar. 
            Syncfusion.XForms.iOS.ProgressBar.SfLinearProgressBarRenderer.Init();
            // Add the following line if you are using SfCircularProgressBar. 
            Syncfusion.XForms.iOS.ProgressBar.SfCircularProgressBarRenderer.Init();
            new SfRatingRenderer();
            LoadApplication(new App());


            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure("$ca829fc33d6247738b9d46777366b113");
            manager.DisableMetricsManager = true;
            manager.StartManager();


            FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]
        {
                new NotificationUserCategory("message",new List<NotificationUserAction> {
                    new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground)
                }),
                new NotificationUserCategory("request",new List<NotificationUserAction> {
                    new NotificationUserAction("Accept","Accept"),
                    new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)
                })

        });
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });
                UNUserNotificationCenter.Current.Delegate = new MyNotificationCenterDelegate();
            }
            var attribute = new UITextAttributes();
            attribute.TextColor = UIColor.Clear;
            UIBarButtonItem.Appearance.SetTitleTextAttributes(attribute, UIControlState.Normal);
            UIBarButtonItem.Appearance.SetTitleTextAttributes(attribute, UIControlState.Highlighted);

            UINavigationBar.Appearance.BarTintColor = Xamarin.Forms.Color.FromHex("#fb9237").ToUIColor(); // UIColor.Black;
            UINavigationBar.Appearance.TintColor = UIColor.White;


            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });
            return base.FinishedLaunching(app, options);
        }
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
#if DEBUG
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken, FirebaseTokenType.Sandbox);
#endif
#if RELEASE
                  FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken,FirebaseTokenType.Production);
#endif

        }
    }
    public class MyNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            completionHandler();
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Alert);

        }
    }
}
