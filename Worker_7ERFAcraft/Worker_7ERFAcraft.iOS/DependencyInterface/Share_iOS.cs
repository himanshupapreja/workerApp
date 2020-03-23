using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Worker_7ERFAcraft.iOS.DependencyInterface;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Worker_7ERFAcraft.Repository;

[assembly: Dependency(typeof(Share_iOS))]
namespace Worker_7ERFAcraft.iOS.DependencyInterface
    {
    public class Share_iOS : Worker_7ERFAcraft.DependencyInterface.IShare
    {
        

        //// MUST BE CALLED FROM THE UI THREAD
        //public async Task Show(string title, string message, string filePath)
        //{
        //    var items = new NSObject[] { NSObject.FromObject(title), NSUrl.FromFilename(filePath) };
        //    var activityController = new UIActivityViewController(items, null);
        //    var vc = GetVisibleViewController();

        //    NSString[] excludedActivityTypes = null;

        //    if (excludedActivityTypes != null && excludedActivityTypes.Length > 0)
        //        activityController.ExcludedActivityTypes = excludedActivityTypes;

        //    if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
        //    {
        //        if (activityController.PopoverPresentationController != null)
        //        {
        //            activityController.PopoverPresentationController.SourceView = vc.View;
        //        }
        //    }
        //    await vc.PresentViewControllerAsync(activityController, true);
        //}

        //UIViewController GetVisibleViewController()
        //{
        //    var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        //    if (rootController.PresentedViewController == null)
        //        return rootController;

        //    if (rootController.PresentedViewController is UINavigationController)
        //    {
        //        return ((UINavigationController)rootController.PresentedViewController).TopViewController;
        //    }

        //    if (rootController.PresentedViewController is UITabBarController)
        //    {
        //        return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
        //    }

        //    return rootController.PresentedViewController;
        //}


        public async Task Share( string url)
        {
           // var imgSource = ImageSource.FromUri(new Uri(url));
            var handler = new ImageLoaderSourceHandler();
           // var uiImage = await handler.LoadImageAsync(imgSource);
            //var img = NSObject.FromObject(uiImage);
            var mess = NSObject.FromObject(Common.appName);
            var activityItems = new[] { mess };
            var activityController = new UIActivityViewController(activityItems, null);
            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }

            topController.PresentViewController(activityController, true, () => { });
            //return Task.FromResult(0);
        }
    }
    
}
