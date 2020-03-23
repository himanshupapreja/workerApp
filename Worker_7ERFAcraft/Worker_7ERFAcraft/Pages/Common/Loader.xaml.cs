using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loader : PopupPage
    {
        public static bool isLoading=false;
        public Loader()
        {
            InitializeComponent();
            isLoading = true;
        }
        public static async void CloseAllPopup()
        {
            isLoading = false;
            await App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
        public static async void CloseAllPopups()
        {
            isLoading = false;
            await App.Current.MainPage.Navigation.PopAllPopupAsync(true);
        }
    }
}