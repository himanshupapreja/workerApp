using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.ViewModels.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages.Driver
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SendQuotePage : PopupPage
	{
		public SendQuotePage (string RequestId)
		{
			InitializeComponent ();
            Title = Resx.AppResources.SendQuote;

            BindingContext = new DriverSendQuoteViewModel(Navigation,RequestId);
        }


        void OnCloseButtonTapped(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}