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
    public partial class AddBalancePopup : PopupPage
    { 
        public AddBalancePopup()
        {
            InitializeComponent();
            FrameContainer.WidthRequest = App.ScreenWidth;
            BindingContext = new AddBalanceViewModel(Navigation);

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
        public static async void CloseAllPopup()
        { 
            await App.Current.MainPage.Navigation.PopPopupAsync(true);
        }

        private void Cancelbtn_Clicked(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
    }
}