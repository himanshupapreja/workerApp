using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;
using Worker_7ERFAcraft.Models;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages.Workers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderDriversListPopup : PopupPage
	{
		public OrderDriversListPopup (List<DriverList> driverLists)
		{
			InitializeComponent ();

            lstViewDriver.ItemsSource = driverLists;
		}
	}
}