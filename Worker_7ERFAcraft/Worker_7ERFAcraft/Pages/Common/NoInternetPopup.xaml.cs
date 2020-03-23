using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoInternetPopup : PopupPage
        {
            public NoInternetPopup()
            {
                InitializeComponent();
            }

            private void Button_Clicked(object sender, EventArgs e)
            {
                Navigation.PopPopupAsync();
            }
        }
}
