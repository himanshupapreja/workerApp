using System;
using System.Collections.Generic;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    { 
        public LoginPage(bool isBackToAddWork=false)
        { 
            InitializeComponent();

            App.IsHomeScreen = false;

            NavigationPage.SetHasNavigationBar(this, false);
             
            BindingContext = new LoginViewModel(Navigation, isBackToAddWork);
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
        }
        private void Closed_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
