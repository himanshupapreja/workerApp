using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.ViewModels;
using Worker_7ERFAcraft.ViewModels.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DriverProfilePage : ContentPage
    {
        ToolbarItem tb_editpro;
        static bool isLoaded = false;
        public DriverProfilePage()
		{
			InitializeComponent ();
            Title = Resx.AppResources.Profile;

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

            isLoaded = false;
            tb_editpro = new ToolbarItem()
            {
                Icon = "profile_edit.png",
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => editProfile())
            };
            //ToolbarItems.Add(tb_editpro);

           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!isLoaded)
            {
                BindingContext = new DriverProfileViewModel(Navigation);
                isLoaded = true;
            } 
        }
        void editProfile()
        {

        }
        private void imgCountry_Tapped(object sender, EventArgs e)
        {
            //pkCountry.Focus();
        }
        protected override bool OnBackButtonPressed()
        {
            HomeMasterPage._masterPage.Detail = new
                NavigationPage((Page)Activator.CreateInstance(typeof(DriverHomePage)));
            return true;
        }
        private void Civil_Copy_Tapped(object sender, EventArgs e)
        {
            civilCopyPicker.Focus();
        }

        private void Vehicle_Form_Tapped(object sender, EventArgs e)
        {
            vehicleFormPicker.Focus();
        }

        private void Vehicle_Tapped(object sender, EventArgs e)
        {
            vehiclePicker.Focus();
        }

    }
}