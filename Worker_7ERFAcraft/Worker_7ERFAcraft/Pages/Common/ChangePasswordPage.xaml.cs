using Worker_7ERFAcraft.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Pages
{
    public partial class ChangePasswordPage :ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
            Title = Resx.AppResources.ChangePassword;
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
            BindingContext = new ChangePasswordViewModel(Navigation);
        }
    }
}
