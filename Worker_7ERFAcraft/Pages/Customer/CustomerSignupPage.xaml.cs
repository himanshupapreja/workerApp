﻿using System;
using System.Collections.Generic;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerSignupPage : ContentPage
    {
        static bool isLoaded = false;
        public CustomerSignupPage()
        {
            InitializeComponent();
            isLoaded = false;
            Title = Resx.AppResources.CreateNewAccount;
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
            if (!isLoaded)
            {
                BindingContext = new CustomerSignupViewModel(Navigation);
                isLoaded = true;
            } 
        }
        private void imgCountry_Tapped(object sender, EventArgs e)
        {
            //pkCountry.Focus();
        } 
    }
}
