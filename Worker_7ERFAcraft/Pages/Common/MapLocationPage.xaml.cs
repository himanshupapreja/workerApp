using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapLocationPage : ContentPage
	{
       public static MapLocationPage page;
        public MapLocationPage()
		{
			InitializeComponent ();
            page = this;
            Title = Resx.AppResources.Location;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(App.latitude),
                Convert.ToDouble(App.longitude)), Distance.FromMiles (2)));

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
        }
	}
}