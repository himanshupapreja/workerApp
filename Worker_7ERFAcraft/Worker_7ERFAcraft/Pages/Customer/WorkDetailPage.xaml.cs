using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.CustomControls;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkDetailPage : ContentPage
    {
        public static string latitude;
        public static string longitude;
        public static string address;

        public WorkDetailPage (Order order)
		{
			InitializeComponent ();
            Title = Resx.AppResources.WorkDetails;// "Work Details";
            lblPhotosVideos.Text = Resx.AppResources.Photos_Videos;// "PHOTOS & VIDEOS";
            BindingContext = new WorkDetailViewModel(Navigation, order);

            var lng = App.Database.GetLng();
            if (lng != null && !string.IsNullOrEmpty(lng.Language))
            {
                if (lng.Language == Models.CultureLanguage.Arabic || lng.Language == Models.CultureLanguage.Urdu)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                }
                else
                {
                    this.FlowDirection = FlowDirection.LeftToRight;
                }
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }


            MessagingCenter.Subscribe<WorkDetailViewModel>(this, "Hi", (sender) =>
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(latitude),
               Convert.ToDouble(longitude)), Distance.FromMiles(5)));


                var pin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(Convert.ToDouble(App.latitude)
                    , Convert.ToDouble(App.longitude)),//(item.geometry.location.lat, item.geometry.location.lng),
                    Label = address,
                    Address = "",
                    Id = "Xamarin",
                    rId = "",
                };

                map.CustomPins = new List<CustomPin> { pin };
                map.Pins.Add(pin);

            });
        }
	}
}