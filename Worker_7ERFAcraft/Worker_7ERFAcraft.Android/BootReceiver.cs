//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;

//namespace Worker_7ERFAcraft.Droid
//{
//    [BroadcastReceiver]
//    [Service(Exported = false), IntentFilter(new string[] { Intent.ActionBootCompleted })]
//    public class BootReceiver : BroadcastReceiver
//    {
//        public override void OnReceive(Context context, Intent intent)
//        {
//            if (intent.Action.Equals(Intent.ActionBootCompleted))
//            {
//                var serviceIntent = new Intent(context, typeof(MainActivity));
//                serviceIntent.AddFlags(ActivityFlags.ClearTop);
//                context.StartActivity(serviceIntent);
//            }
//        }
//    }
//}