using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace Worker_7ERFAcraft.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true  , NoHistory = true , 
        Label = "موجود", Icon = "@drawable/logo",
          ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        private static int SPLASH_TIME = 1 * 1000;// 1 seconds
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

          
            try
            {
                new Handler().PostDelayed(() =>
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish();

                }, SPLASH_TIME);

            }
            catch (Exception e) { }

        }
    }
}