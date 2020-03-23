using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.CustomControls;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerHomePage : ContentPage
	{
        public static INavigation navigation;
        public static List<CustomPin> customPins = new List<CustomPin>();

        int SlidePosition;
        public static string latitude = "0"; 
        public static string longitude = "0"; 
        public static List<Worker> lstWorkers;
        static bool isLoaded= false;

        public CustomerHomePage ()
		{
			InitializeComponent ();
            Title =Resx.AppResources.Home;
            isLoaded = false;
            navigation = Navigation;

            customPins = new List<CustomPin>();

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
            CustomerHomePage.latitude = App.latitude;
            CustomerHomePage.longitude = App.longitude;

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(App.latitude),
               Convert.ToDouble(App.longitude)), Distance.FromKilometers(1)));



            getHomePageAdsData();

            MessagingCenter.Subscribe<CustomerHomeViewModel>(this, "ShowWorkersLocation", (sender) =>
            {
                
                if (map.CustomPins != null)
                {
                    foreach (var item in map.CustomPins)
                    {
                        map.Pins.Remove(item);
                    }
                }
                if (lstWorkers != null)
                {
                    customPins = new List<CustomPin>();
                    foreach (var item in lstWorkers)
                    {
                        if (!string.IsNullOrEmpty(item.Latitude) && !string.IsNullOrEmpty(item.Longitude))
                        {
                            var pin = new CustomPin
                            {
                                Type = PinType.Place,
                                Position = new Position(Convert.ToDouble(item.Latitude)
                       , Convert.ToDouble(item.Longitude)),//(item.geometry.location.lat, item.geometry.location.lng),
                                Label = Resx.AppResources.ViewDetails,// item.Name,
                                //Address = item.Distance.ToString(),// "Show my Details",//item.Location,
                                Id = "Xamarin", 
                                rId = item.UserId.ToString(),
                            };
                            pin.Clicked += Pin_Clicked;
                            map.CustomPins = new List<CustomPin> { pin };
                            map.Pins.Add(pin); 
                            customPins.Add(pin); 
                        }
                    }
                }
                MessagingCenter.Unsubscribe<CustomerHomeViewModel>(sender, "ShowWorkersLocation");
            });

            MessagingCenter.Subscribe<CustomerHomeViewModel>(this, "UpdateMapLocation", (sender) =>
            {
                if (latitude != "0" && longitude != "0")
                {
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(latitude),
                Convert.ToDouble(longitude)), Distance.FromKilometers(1)));
                }
                else
                {
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(App.latitude),
                Convert.ToDouble(App.longitude)), Distance.FromKilometers(1)));
                }
                MessagingCenter.Unsubscribe<CustomerHomeViewModel>(sender, "UpdateMapLocation");
            });
        }
        public async void getHomePageAdsData()
        {
            try
            {
                var DeviceId = Device.RuntimePlatform == Device.Android ? 1 : 2;
                HttpClientBase cbase = new HttpClientBase();
                var result = await cbase.GetHomeVideo(ApiUrl.HomeScreenAdsUrl + "?userId=" + LoginUserDetails.userId);
                if (result != null)
                {
                    App.lstHomeAdsData = result.homeAdsData;
                    if (App.lstHomeAdsData != null && App.lstHomeAdsData.Count > 0)
                    {
                        CarouselView.ItemsSource = App.lstHomeAdsData;

                        Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                        {
                            try
                            {
                                SlidePosition++;
                                if (SlidePosition == App.lstHomeAdsData.Count) SlidePosition = 0;
                                CarouselView.Position = SlidePosition;
                                return true;
                            }
                            catch
                            {
                                return false;
                            }

                        });
                    }
                    else
                    {
                        CarouselView.HeightRequest = 0;
                    }

                }
            }
            catch
            {
            }
        }
        private void Pin_Clicked(object sender, EventArgs e)
        {
            var pin = (CustomPin)sender;
            string wId = pin.rId;// string.Empty;
            Navigation.PushAsync(new WorkerFullProfilePage(wId,0));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtSearch.Text = string.Empty;
            if (!isLoaded)
            {
                BindingContext = new CustomerHomeViewModel(Navigation);
                isLoaded = true;
            } 
        }
       


        protected override void OnDisappearing()
        {
            App.IsCustomerHomeScreen = false; 
            base.OnDisappearing();
        }

        private void sliderImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                var data = (HomeAdsData)((FFImageLoading.Forms.CachedImage)sender).BindingContext;
                if (data != null && !string.IsNullOrEmpty(data.MediaName))
                {
                    Device.OpenUri(new Uri(data.AdUrl));
                }
            }
            catch
            { 
            }
        }
        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await DisplayAlert("", Resx.AppResources.ExitApp, Resx.AppResources.Yes, Resx.AppResources.No);
                    if (result)
                    { 
                        DependencyService.Get<DependencyInterface.IAndroidMethods>().CloseApp(); 
                    }
                });
            }
            return true;
        }
    }
}