using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HelpPage : ContentPage
	{
		public HelpPage ()
		{
			InitializeComponent ();
            Title = Resx.AppResources.Help;
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

        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrivacyPolicyPage());
        }
        private void TermsConditions_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermsConditionsPage());
        }
        private void ContactUs_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContactUsPage());
        }
        private void ChangeLangauge_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangeLanguagePage());
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