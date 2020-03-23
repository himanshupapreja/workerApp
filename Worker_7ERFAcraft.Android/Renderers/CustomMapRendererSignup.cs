using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Worker_7ERFAcraft.CustomControls;
using Worker_7ERFAcraft.Droid.Renderers;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMapSignup), typeof(CustomMapRendererSignup))]
namespace Worker_7ERFAcraft.Droid.Renderers
{
    public class CustomMapRendererSignup : MapRenderer, IOnMapReadyCallback
    {
        List<CustomPin> customPins;

        public CustomMapRendererSignup(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.MapClick += NativeMap_MapClick;
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMapSignup)e.NewElement;
                customPins = formsMap.CustomPins;
                Control.GetMapAsync(this);
            }
        }
        
        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (App.IsCustomerHomeScreen || App.IsCustomerWorkerListScreen)
        //    {
        //        var map = (CustomMap)sender;
        //        var reg = map.VisibleRegion;
        //        if (reg != null)
        //        {
        //            var pos = reg.Center;
        //            if (pos.Latitude != 0 && App.oldPos.Latitude != 0)
        //            {
        //                var distance = Common.CalculateDistance(App.oldPos.Latitude, App.oldPos.Longitude,
        //                    pos.Latitude, pos.Longitude);
        //                if (distance > 50 && !App.IsSearch)
        //                {
        //                    App.oldPos = pos;
        //                    if (App.IsCustomerHomeScreen)
        //                    {
        //                        MessagingCenter.Send<CustomerHomeViewModel>(CustomerHomeViewModel.model, "OnHomeVisionChange");
        //                    }
        //                    else
        //                    {
        //                        MessagingCenter.Send<WorkerListViewModel>(WorkerListViewModel.model, "OnListVisionChange");
        //                    }
                            
        //                }
        //                //App.IsSearch = false;
        //            }
        //            base.OnElementPropertyChanged(sender, e);
        //        }
        //    }
        //}
          
        private void NativeMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            //LatLng clickPosition = e.Point;
            Position pos = new Position(e.Point.Latitude, e.Point.Longitude);
            Common.GetAddress(pos, Pages.MapLocationPage.page);
            
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            map.MapClick += NativeMap_MapClick;
            //NativeMap.InfoWindowClick += OnInfoWindowClick;
            //NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_map_pin)); 
            // marker.SetSnippet(pin.Address);
            //marker.SetIcon(BitmapDescriptorFactory.DefaultMarker());
            //if (!string.IsNullOrEmpty(pin.Address))
            //{
            //    var dis = Convert.ToDouble(pin.Address);
            //    if (dis > 50)
            //    {
            //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin_red_40));
            //    }
            //    else
            //    {
            //        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin_green_40));
            //    }
            //}
            //else
            //{
            //    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin_green_40));
            //}
            return marker;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                //throw new Exception("Custom pin not found");
            }

            if (!string.IsNullOrWhiteSpace(customPin.rId))
            {
                //var id = customPin.rId;
                //App.Current.MainPage.Navigation.PushAsync(new Pages.RestaurantDetailPage(id)); 
            }
        }
        /*
        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                 view = inflater.Inflate(Resource.Layout.XamarinMapInfoWindow, null);
               

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }
        */
        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}