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
	public partial class ContactUsPage : ContentPage
	{
		public ContactUsPage ()
		{
			InitializeComponent ();
            Title = Resx.AppResources.ContactUs;
            BindingContext = new ContactUsViewModel(Navigation);

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
        void Try_Again_Button_Clicked(object sender, EventArgs e)
        {
            BindingContext = new ContactUsViewModel(Navigation);
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
                    HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(WorkerHomePage)));
                }
            }
            return true;
        }

        private void Email_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(String.Format("mailto:{0}", lblEmail.Text)));
        }
        private void Phone1_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(String.Format("tel:{0}", lblPhone1.Text)));
        }
        private void Phone2_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(String.Format("tel:{0}", lblPhone2.Text)));
        }
        private void Phone3_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(String.Format("tel:{0}", lblPhone3.Text)));
        }
        private void Phone4_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(String.Format("tel:{0}", lblPhone4.Text)));
        }
    }
}