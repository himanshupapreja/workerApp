﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels;
using Worker_7ERFAcraft.ViewModels.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverHomePage : ContentPage
    {
        public static bool IsDismissed = false;
        int SlidePosition;
        DriverOrderStatus DriverOrderStatus;
        public DriverHomePage(DriverOrderStatus driverOrderStatus)
                  {
            InitializeComponent();
            DriverOrderStatus = driverOrderStatus;
            if (DriverOrderStatus == DriverOrderStatus.Pending)
            {
                Title = Resx.AppResources.New; 
            }
            else if (DriverOrderStatus == DriverOrderStatus.InProgress)
            {
                Title = Resx.AppResources.OnGoing; 
            }
            else if (DriverOrderStatus == DriverOrderStatus.Completed)
            {
                Title = Resx.AppResources.History; 
            }
            else if (DriverOrderStatus == DriverOrderStatus.Chat)
            {
                Title = Resx.AppResources.Chat; 
            }
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


            BindingContext = new DriverHomeViewModel(Navigation, DriverOrderStatus);
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
                        //CarouselView.ItemsSource = App.lstHomeAdsData;

                        Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                        {
                            try
                            {
                                SlidePosition++;
                                if (SlidePosition == App.lstHomeAdsData.Count) SlidePosition = 0;
                                //CarouselView.Position = SlidePosition;
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
                      // CarouselView.HeightRequest = 0;
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
            BindingContext = new DriverHomeViewModel(Navigation, DriverOrderStatus);
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
            DriverChatDetailPage.timer = false;
        }
        protected override void OnDisappearing()
        {
            IsDismissed = true;
        }


        private void ChatList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (chatList.SelectedItem != null)
                {
                    var data = chatList.SelectedItem as ChatUserData;
                    DriverChatDetailPage.ReciverId = data.UserId;
                    Navigation.PushAsync(new DriverChatDetailPage());
                    chatList.SelectedItem = null;
                }
            }
            catch (Exception)
            {
            }

        }
    }
}