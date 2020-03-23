using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Plugin.Permissions;
using HockeyApp.Android;
using System.Linq;
using Plugin.FirebasePushNotification;

namespace Worker_7ERFAcraft.Droid
{
    [Activity(Label = "موجود", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string permissionCam = Manifest.Permission.Camera;
        const string permissionRS = Manifest.Permission.ReadExternalStorage;
        const string permissionWS = Manifest.Permission.WriteExternalStorage;
        const string ReadPhoneState = Manifest.Permission.ReadPhoneState;

        const string AccessFineLocation = Manifest.Permission.AccessFineLocation;
        const string AccessCoarseLocation = Manifest.Permission.AccessCoarseLocation;

        const string ModifyAudioSettings = Manifest.Permission.ModifyAudioSettings;

        const string RecordAudio = Manifest.Permission.RecordAudio;
        const int RequestLocationId = 0;

        readonly string[] permissions =
   {
       Manifest.Permission.Camera,
         Manifest.Permission.ReadExternalStorage,
           Manifest.Permission.WriteExternalStorage,
             Manifest.Permission.ReadPhoneState,
               Manifest.Permission.AccessFineLocation,
                 Manifest.Permission.AccessCoarseLocation,
                 Manifest.Permission.ModifyAudioSettings,
                 Manifest.Permission.RecordAudio
    };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (width - 0.5f) / density;
            App.ScreenHeight = (height - 0.5f) / density;
            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != (int)Permission.Granted)
            //{
            //    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, 0);
            //} 

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            LoadApplication(new App());
            GetPermissions();
            App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().
             UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            FirebasePushNotificationManager.Initialize(this, true);

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }
        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(intent);
        }
        string HOCKEYAPP_APPID = "59e0d5f32a5b4a4ab444c4d42372bf0b";
        protected override void OnResume()
        {
            base.OnResume();
            CrashManager.Register(this, HOCKEYAPP_APPID);
        }
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        //{
        //    Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public void GetPermissions()
        {
            //if ((int)Build.VERSION.SdkInt >= 23 &&
            //    ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.Camera) != PackageManager.gras)
            //{
            //    return;
            //}

            if (CheckSelfPermission(permissionCam) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if (CheckSelfPermission(permissionRS) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if (CheckSelfPermission(permissionWS) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if (CheckSelfPermission(ReadPhoneState) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if ((int)Build.VERSION.SdkInt >= 23 && CheckSelfPermission(AccessCoarseLocation) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if ((int)Build.VERSION.SdkInt >= 23 && CheckSelfPermission(AccessFineLocation) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if ((int)Build.VERSION.SdkInt >= 23 && CheckSelfPermission(ModifyAudioSettings) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if ((int)Build.VERSION.SdkInt >= 23 && CheckSelfPermission(RecordAudio) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
        }



        //public override void OnBackPressed()
        //{
        //    if (App.IsHomeScreen)
        //    {
        //        var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
        //        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        //        AlertDialog alert = dialog.Create();
        //        //alert.SetTitle("Title");
        //        alert.SetMessage("Are you want to exit?");
        //        // alert.SetIcon(Resource.Drawable.alert);
        //        alert.SetButton("Yes", (c, ev) =>
        //        {
        //            base.OnBackPressed();
        //        });
        //        alert.SetButton2("No", (c, ev) => { });
        //        alert.Show();
        //    }
        //    else
        //    {
        //        base.OnBackPressed();
        //    }

        //}
    }
}