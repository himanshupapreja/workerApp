using Worker_7ERFAcraft.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Models;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsConditionsPage : ContentPage
    {
        public TermsConditionsPage()
        {
            InitializeComponent();
            Title = Resx.AppResources.TermsConditions;


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
            BindingContext = new TermsConditionsViewModel(Navigation);
        }
        void Try_Again_Button_Clicked(object sender, EventArgs e)
        {
            BindingContext = new TermsConditionsViewModel(Navigation);
        }
        protected override bool OnBackButtonPressed()
        {
            if (LoginUserDetails.userId == 0)
            {
                HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CustomerHomePage)));
            }
            else
            {
                if (LoginUserDetails.userType == (int)UserType.Customer)
                {
                    HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CustomerHomePage)));
                }
                else
                {
                    HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(WorkerNewHomePage)));
                }
            }
            return true;
        }
    }
}
