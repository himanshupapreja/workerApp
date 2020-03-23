using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMessage : PopupPage
    {
        public ShowMessage(string message)
        {
            InitializeComponent();

            msgTxt.Text = message;

            CloseWhenBackgroundIsClicked = false;
        }
    }
}
