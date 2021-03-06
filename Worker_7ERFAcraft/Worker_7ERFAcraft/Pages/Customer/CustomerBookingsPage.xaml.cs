﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerBookingsPage : ContentPage
    {
        public CustomerBookingsPage()
        {
            InitializeComponent();
            Title = Resx.AppResources.MyBookings;//"My Bookings";


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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new CustomerBookingsViewModel(Navigation);
        }
        void Try_Again_Button_Clicked(object sender, EventArgs e)
        {
            BindingContext = new CustomerBookingsViewModel(Navigation);
        }
        protected override bool OnBackButtonPressed()
        {

            HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CustomerHomePage)));

            return true;
        }
    }
}