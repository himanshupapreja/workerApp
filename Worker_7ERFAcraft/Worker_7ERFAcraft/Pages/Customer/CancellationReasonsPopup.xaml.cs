using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CancellationReasonsPopup : PopupPage
    {
        public static bool isCancelDone = false;
        string _orderId = string.Empty;
		public CancellationReasonsPopup(string orderId)
		{
			InitializeComponent ();
            _orderId = orderId;
            isCancelDone = false;
            NavigationPage.SetHasNavigationBar(this, false);
            
            FrameContainer.WidthRequest = App.ScreenWidth - 50;

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
            BindingContext = new CancellationReasonsViewModel(Navigation, _orderId);
        }
        private  void Closed_Tapped(object sender, EventArgs e)
        {
            ClosePopup();
        }
        public static async void ClosePopup()
        { 
            await App.Current.MainPage.Navigation.PopPopupAsync(true);
        }

        private void imgReasons_Tapped(object sender, EventArgs e)
        {
           // pkReason.Focus();
        }
    }
}