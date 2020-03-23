using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels.Worker;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkerAcceptPopup : PopupPage
	{
		public WorkerAcceptPopup (int orderId, UpdateOrderStatus updateOrderStatus)
		{
			InitializeComponent ();

            BindingContext = new WorkerAcceptViewModel(Navigation,orderId, updateOrderStatus);
		}
	}
}