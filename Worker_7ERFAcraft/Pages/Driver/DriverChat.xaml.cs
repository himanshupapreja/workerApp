using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.ViewModels.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages.Driver
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DriverChat : ContentPage
	{
		public DriverChat ()
		{
			InitializeComponent ();
            Title = Resx.AppResources.ChatUsers;
            DriverChatUserViewModel model = new DriverChatUserViewModel();
            chatList.BindingContext = model;

           
        }
        protected override void OnAppearing()
        {
            DriverChatDetailPage.timer = false;
        }
        private  void ChatList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (chatList.SelectedItem != null)
                {
                    var data = chatList.SelectedItem as ChatUserData;
                    DriverChatDetailPage.ReciverId = data.UserId;
                    Navigation.PushAsync(new DriverChatDetailPage());
                    chatList.SelectedItem = null;
                }
            }
            catch (Exception)
            {
            }
            
        }
    }
}