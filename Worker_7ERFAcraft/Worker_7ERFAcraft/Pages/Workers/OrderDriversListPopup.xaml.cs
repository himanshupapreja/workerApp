using System;
using System.Collections.Generic;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.ViewModels.Workers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages.Workers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderDriversListPopup : ContentPage
	{
		public OrderDriversListPopup (List<DriverList> driverLists)
		{
			InitializeComponent ();

            Title = Resx.AppResources.DriversList;

            BindingContext = new OrderDriverListViewModel(Navigation,driverLists);
		}
    }
}