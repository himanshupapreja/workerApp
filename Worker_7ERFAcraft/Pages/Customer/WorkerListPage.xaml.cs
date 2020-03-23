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
	public partial class WorkerListPage : ContentPage
	{
        int _categoryId; 
        public static List<Worker> lstWorkers;
        public static string latitude = "0";
        public static string longitude = "0";
        public WorkerListPage (int categoryId)
		{
			InitializeComponent ();
            _categoryId = categoryId;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(App.latitude),
              Convert.ToDouble(App.longitude)), Distance.FromKilometers(5)));

            Title =Resx.AppResources.WorkerList;// "Worker List";


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



            BindingContext = new WorkerListViewModel(Navigation, _categoryId);
            MessagingCenter.Subscribe<WorkerListViewModel>(this, "ShowWorkersLocationOnList", (sender) =>
            { 
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(latitude),
               Convert.ToDouble(longitude)), Distance.FromKilometers(5)));


                foreach (var item in lstWorkers)
                {
                    if (!string.IsNullOrEmpty(item.Latitude) && !string.IsNullOrEmpty(item.Longitude))
                    {
                        var pin = new CustomPin
                        {
                            Type = PinType.Place,
                            Position = new Position(Convert.ToDouble(item.Latitude)
                   , Convert.ToDouble(item.Longitude)),//(item.geometry.location.lat, item.geometry.location.lng),
                            Label = "View Details",//item.Name,
                            Address = "",//item.Location,
                            Id = "Xamarin",
                            rId = item.UserId.ToString(),
                        };
                        pin.Clicked += Pin_Clicked;
                        map.CustomPins = new List<CustomPin> { pin };
                        map.Pins.Add(pin);
                    }
                }
                MessagingCenter.Unsubscribe<WorkerListViewModel>(sender, "ShowWorkersLocationOnList");
            });
            MessagingCenter.Subscribe<WorkerListViewModel>(this, "UpdateMapLocationOnList", (sender) =>
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(latitude),
               Convert.ToDouble(longitude)), Distance.FromKilometers(5)));
                MessagingCenter.Unsubscribe<WorkerListViewModel>(sender, "UpdateMapLocationOnList");


            });
        }
        private void Pin_Clicked(object sender, EventArgs e)
        {
            var pin = (CustomPin)sender;
            string wId = pin.rId;// string.Empty;
            Navigation.PushAsync(new WorkerFullProfilePage(wId, 0));
        }
        protected override void OnAppearing()
        {
            
            base.OnAppearing();
        }
        void Try_Again_Button_Clicked(object sender, EventArgs e)
        {
            BindingContext = new WorkerListViewModel(Navigation,_categoryId);
        }
        protected override void OnDisappearing()
        { 
            App.IsCustomerWorkerListScreen = false;
            base.OnDisappearing();
        }
    }
}