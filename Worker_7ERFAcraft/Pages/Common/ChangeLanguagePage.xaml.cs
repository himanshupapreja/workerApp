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
	public partial class ChangeLanguagePage : ContentPage
	{
		public ChangeLanguagePage()
		{
			InitializeComponent ();
            Title = Resx.AppResources.ChangeLangauge;
            BindingContext = new ChangeLanguageViewModel(Navigation);
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
    }
}