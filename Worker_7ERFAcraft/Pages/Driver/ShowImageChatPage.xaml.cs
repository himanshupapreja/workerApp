using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowImageChatPage : ContentPage
	{
		public ShowImageChatPage (string ImageUrl)
		{
			InitializeComponent ();

            image.Source = ImageUrl;
        }
	}
}