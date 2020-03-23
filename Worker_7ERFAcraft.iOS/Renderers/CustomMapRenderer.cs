using System;
using System.Collections.Generic;
using CoreGraphics;
using HalaApp;
using HalaApp.iOS;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using Worker_7ERFAcraft.CustomControls;
using Foundation;
using System.Linq;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace HalaApp.iOS
{
    public class CustomMapRenderer : MapRenderer
    {
        UIView customPinView;
        //List<CustomPin> customPins;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = null;
                nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView; 
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
               // customPins = formsMap.CustomPins;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
            {
                annotationView = mapView.DequeueReusableAnnotation("1");
                if (annotationView == null)
                {
                    annotationView = new CustomMKAnnotationView(annotation, "1");
                    annotationView.Image = UIImage.FromFile("ic_map_pin.png");
                    annotationView.CalloutOffset = new CGPoint(0, 0);
                    //annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("monkey.png"));
                   // annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                   // ((CustomMKAnnotationView)annotationView).Id = customPin.rId.ToString();
                    //((CustomMKAnnotationView)annotationView).Url = customPin.Url;
                }
            }
            else{
                annotationView = mapView.DequeueReusableAnnotation(customPin.Id.ToString());
                if (annotationView == null)
                {
                    annotationView = new CustomMKAnnotationView(annotation, customPin.Id.ToString());
                    annotationView.Image = UIImage.FromFile("ic_map_pin.png");
                    annotationView.CalloutOffset = new CGPoint(0, 0);
                    //annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("monkey.png"));
                    annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                    ((CustomMKAnnotationView)annotationView).Id = customPin.rId.ToString();
                    //((CustomMKAnnotationView)annotationView).Url = customPin.Url;
                }
            }
           
            annotationView.CanShowCallout = true;

            return annotationView;
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            if (!string.IsNullOrWhiteSpace(customView.Id))
            {
                CustomerHomePage.navigation.PushAsync(new WorkerFullProfilePage(customView.Id, 0));
            }
        }

        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            customPinView = new UIView();

            if (customView.Id == "Xamarin")
            {
                customPinView.Frame = new CGRect(0, 0, 200, 84);
                var image = new UIImageView(new CGRect(0, 0, 200, 84));
                image.Image = UIImage.FromFile("call_icon.png");
                customPinView.AddSubview(image);
                customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
                e.View.AddSubview(customPinView);
            }
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                customPinView.RemoveFromSuperview();
                customPinView.Dispose();
                customPinView = null;
            }
        }

        CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            if (annotation != null)
            {
                var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
                foreach (var pin in CustomerHomePage.customPins)
                {
                    if (pin.Position == position)
                    {
                        return pin;
                    }
                }
            }
            return null;
        }
    }
    //public class CustomMapRenderer : MapRenderer
    //{
    //    public CustomMapRenderer()
    //    {

    //    }



    //    UIView customPinView;

    //    protected override void OnElementChanged(ElementChangedEventArgs<View> e)
    //    {
    //        base.OnElementChanged(e); 

    //        base.OnElementChanged(e);

    //        if (e.OldElement != null)
    //        {
    //            var nativeMap = Control as MKMapView;

    //            nativeMap.GetViewForAnnotation = null;
    //            nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
    //            nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
    //            nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView; 
    //        }

    //        if (e.NewElement != null)
    //        {
    //            var formsMap = (CustomMap)e.NewElement;
    //            var nativeMap = Control as MKMapView;
    //            CustomerHomePage.customPins = formsMap.CustomPins;

    //            nativeMap.GetViewForAnnotation = GetViewForAnnotation;
    //            nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
    //            nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
    //            nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
    //        }
    //    }

    //    protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
    //    {
    //        CustomMKAnnotationView annotationView = null;
    //        var kk= annotation.GetTitle();
    //        var kk1 = annotation.GetSubtitle();
    //        if (annotation is MKUserLocation)
    //            return null;

    //        var customPin = GetCustomPin(annotation as MKPointAnnotation);
    //        if (customPin == null)
    //        {
    //            //throw new Exception("Custom pin not found");
    //        }

    //        //annotationView = mapView.DequeueReusableAnnotation("1");
    //        if (annotationView == null)
    //        {
    //            annotationView = new CustomMKAnnotationView(annotation, 
    //                customPin != null? customPin.rId.ToString(): "");
    //            annotationView.Image = UIImage.FromFile("ic_map_pin.png"); 
    //            annotationView.CalloutOffset = new CGPoint(0, 0); 

    //            if (customPin != null)
    //            {
    //                ((CustomMKAnnotationView)annotationView).Id = customPin.Id.ToString();
    //                ((CustomMKAnnotationView)annotationView).Url = customPin.Id.ToString();
    //            }

    //           UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(Tap);
    //           tapGesture.NumberOfTapsRequired = 1;
    //           annotationView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
    //           ViewDetails(annotationView)
    //           ));
    //        }
    //        annotationView.CanShowCallout = true;

    //        return annotationView;
    //    }
    //    static void ViewDetails(CustomMKAnnotationView view)
    //    {
    //        try
    //        {
    //            var userId = view.ReuseIdentifier;
    //            if (!string.IsNullOrEmpty(userId))
    //            {
    //                CustomerHomePage.navigation.PushAsync(new WorkerFullProfilePage(userId, 0));
    //            }
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }
    //    void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
    //    {
    //        var customView = e.View as CustomMKAnnotationView;
    //        if (!string.IsNullOrWhiteSpace(customView.Url))
    //        {
    //            UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
    //        }
    //    }

    //    void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
    //    {
    //        var customView = e.View as CustomMKAnnotationView;
    //        if(customPinView ==null)
    //        {
    //            customPinView = new UIView();
    //        }

    //        if (customView.Id != null)
    //        {
    //            UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(Tap);
    //            tapGesture.NumberOfTapsRequired = 1;
    //            customView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
    //           ViewDetails(customView)
    //            ));
    //        }
    //        //if (customView.Id !=null)
    //        //{
    //        //    customPinView.Frame = new CGRect(0, 0, 50, 50);
    //        //    var lbl = new UILabel(new CGRect(0, 0, 30, 30));
    //        //    lbl.Text = "fsdfdfsfs";

    //        //    customPinView.AddSubview(lbl);
    //        //    customPinView.Center = new CGPoint(0, 0);
    //        //    e.View.AddSubview(customPinView);
    //        //}
    //    }
    //    void Tap(UITapGestureRecognizer tap1)
    //    {
    //        var gg = tap1.ValueForKey((NSString)"Id"); ;
    //        //lblDisplay.Text = "You Touched Me..";
    //    }
    //    void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
    //    {
    //        if (!e.View.Selected)
    //        {
    //            customPinView.RemoveFromSuperview();
    //            customPinView.Dispose();
    //            customPinView = null;
    //        }
    //    }

    //    CustomPin GetCustomPin(MKPointAnnotation annotation)
    //    {
    //        try{


    //        var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
    //            foreach (var pin in CustomerHomePage.customPins)
    //        {
    //            if (pin.Position == position)
    //            {
    //                return pin;
    //            }
    //        }
    //        }
    //        catch
    //        {

    //        }
    //        return null;
    //    }
    //}
}
