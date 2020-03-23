
using Worker_7ERFAcraft.Droid.DependencyInterface;
using System;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Content;
using System.Net;
using Worker_7ERFAcraft.Repository;

[assembly: Dependency(typeof(Share_Droid))]
namespace Worker_7ERFAcraft.Droid.DependencyInterface
{
    public class Share_Droid : Worker_7ERFAcraft.DependencyInterface.IShare
    {
        private readonly Context _context;
            public Share_Droid()
            {
                _context = Android.App.Application.Context;
            }

        public Task Share(string text)
        {
            var path = Android.OS.Environment.GetExternalStoragePublicDirectory("Temp");

            if (!File.Exists(path.Path))
            {
                Directory.CreateDirectory(path.Path);
            }
            var _context = Android.App.Application.Context;
            // for video
            //string absPath = path.Path + "appvideo.3gp";
            //File.WriteAllBytes(absPath, GetBytes(text));



            Intent sendIntent = new Intent(global::Android.Content.Intent.ActionSend);

            sendIntent.PutExtra(global::Android.Content.Intent.ExtraText, Common.appName);

            // for video
            //sendIntent.SetType("video/3gp");

            //sendIntent.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse("content://" + absPath));


            // for text
            sendIntent.SetType("text/plain");
            sendIntent.PutExtra(Intent.ExtraText, text);


            _context.StartActivity(Intent.CreateChooser(sendIntent, "Sharing"));
            return Task.FromResult(0);
        }
        public static byte[] GetBytes(string url)
        {
            byte[] arry;
            using (var webClient = new WebClient())
            {
                arry = webClient.DownloadData(url);
            }
            return arry;
        }
    }
    
}