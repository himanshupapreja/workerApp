using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.ViewModels;
using Worker_7ERFAcraft.ViewModels.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkerChatDetailPage : ContentPage
	{
        public static bool timer = false;
        public static int ReciverId = 0;
		public WorkerChatDetailPage()
		{
			InitializeComponent ();
            timer = true;
            Title = Resx.AppResources.Chat;
            //WorkerChatingViewModel model = new WorkerChatingViewModel();
            //BindingContext = model;
            try
            {
                MessagingCenter.Subscribe<string>(this, "TextFocus", (sender) =>
                {
                    
                    txt_msg.Text = "";
                    cursor = true;
                });
            }
            catch (Exception ex)
            {
            }
            cursor = false;
             
            txt_msg.Unfocused += Txt_Msg_Unfocused;
        }

        public static bool cursor = false;

      
        void Txt_Msg_Unfocused(object sender, FocusEventArgs e)
        {
            if (cursor == true)
            {

                txt_msg.Focus();

            }
            cursor = false;
        }


    }
}