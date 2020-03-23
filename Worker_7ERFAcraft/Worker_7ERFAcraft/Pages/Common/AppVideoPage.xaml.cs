using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppVideoPage : ContentPage
	{
        public AppVideoPage()
        {
            InitializeComponent();
            Title = Resx.AppResources.AppVideo;
            var tb_share = new ToolbarItem()
            {
                Icon = Device.RuntimePlatform==Device.iOS? "ic_share_black_small.png" : "ic_share_white.png",
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => shareVideo())
            };
            ToolbarItems.Add(tb_share);

            video.HeightRequest = App.ScreenHeight;
            BindingContext = new AppVideoViewModel(Navigation);

            var lng = App.Database.GetLng();
            if (lng != null && !string.IsNullOrEmpty(lng.Language))
            {
                if (lng.Language == Models.CultureLanguage.Arabic || lng.Language == Models.CultureLanguage.Urdu)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                }
                else
                {
                    this.FlowDirection = FlowDirection.LeftToRight;
                } 
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        void shareVideo()
        {
            DependencyService.Get<IShare>().Share(  "http://appmantechnologies.com:8033/Images/AppVideo_nature.mp4");
        }
        protected override bool OnBackButtonPressed()
        {
            if (LoginUserDetails.userId == 0)
            {
                HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CustomerHomePage)));
            }
            else
            {
                if (LoginUserDetails.userType == (int)UserType.Customer)
                {
                    HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CustomerHomePage)));
                }
                else
                {
                    HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(WorkerNewHomePage)));
                }
            }
            return true;
        }
    }
}