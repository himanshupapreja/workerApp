using Worker_7ERFAcraft.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Worker_7ERFAcraft.Repository;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();

            Title = Resx.AppResources.ForgotYourPassword;
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
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    this.Padding = new Thickness(0, 30, 0, 0);
            //    sendBtn.CornerRadius = 20;
            //    emailTxt.Margin = new Thickness(0, 15, 0, 15);
            //    emailLbl.Margin = new Thickness(0, 50, 0, 0);
            //}



        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new ForgotPasswordViewModel(Navigation);
        }
    }
}
