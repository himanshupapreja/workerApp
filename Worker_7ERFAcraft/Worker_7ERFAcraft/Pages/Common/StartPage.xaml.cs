using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
    {   
        public StartPage ()
        {   
            InitializeComponent(); 
            NavigationPage.SetHasNavigationBar(this, false);
             
            var objLng = App.Database.GetLng();
            try
            { 

                if (objLng != null && !string.IsNullOrEmpty(objLng.Language))
                {  
                    if (objLng.Language == Models.CultureLanguage.Arabic || objLng.Language == Models.CultureLanguage.Urdu)
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
            catch (Exception ex)
            { 
            }

        }
        private void LoginAsCustomer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomerSignupPage());
        }
        private void LoginAsWorkerTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignupPage());
        }
        private void LoginAsDriverTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DriverSignupPage());
        } 
        private void Close_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        
    }
}