using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkerHomePage : ContentPage
    {
        public static bool IsDismissed = false;
        int SlidePosition;
        public WorkerHomePage()
        {
            InitializeComponent();
            Title = Resx.AppResources.Home;
            IsDismissed = false;




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


            BindingContext = new WorkerHomeViewModel(Navigation);
            getHomePageAdsData();
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
        private void sliderImage_Tapped(object sender, EventArgs e)
        {
            var data = (HomeAdsData)((FFImageLoading.Forms.CachedImage)sender).BindingContext;
            if (data != null && !string.IsNullOrEmpty(data.MediaName))
            {
                Device.OpenUri(new Uri(data.AdUrl));
            }
        }
        void Try_Again_Button_Clicked(object sender, EventArgs e)
        {
            BindingContext = new WorkerHomeViewModel(Navigation);
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsDismissed = false;
        }
        protected override void OnDisappearing()
        {
            IsDismissed = true;
        }

    }
}