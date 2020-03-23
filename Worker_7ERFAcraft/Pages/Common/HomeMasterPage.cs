using Worker_7ERFAcraft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Worker_7ERFAcraft.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace Worker_7ERFAcraft.Pages
{
    public class HomeMasterPage : MasterDetailPage
    {
        public static wsHomeData result;
        public static HomeMasterPage _masterPage;

        public static MasterPageItem previousItem;

        MenuPage MasterPage;

        public static MasterPageItem selectedItem;
        public HomeMasterPage()
        {
            Common.GetLocation();
            App.IsHomeScreen = true;
            _masterPage = this;
            MessagingCenter.Subscribe<LoginViewModel>(this, "Guestlogin", (sender) =>
            {
                MasterPage = new MenuPage();
                this.Master = MasterPage;
                MessagingCenter.Unsubscribe<LoginViewModel>(sender, "Guestlogin");
            });

            MasterPage = new MenuPage();
            // Icon = "menu.png";
            //  Title = "";
            if (LoginUserDetails.userId == 0)
            {
                Master = MasterPage;
                {
                    Detail = new NavigationPage(new CustomerHomePage());
                }
            }
            else
            {
                if (LoginUserDetails.userType == (int)UserType.Customer)
                {
                    Master = MasterPage;
                    {
                        Detail = new NavigationPage(new CustomerHomePage());
                    }
                }
                else if (LoginUserDetails.userType == (int)UserType.Worker)
                {
                    Master = MasterPage;
                    {
                        Detail = new NavigationPage(new WorkerHomePage());
                    }
                }
                else
                {
                    Master = MasterPage;
                    {
                        Detail = new NavigationPage(new DriverHomePage());
                    }
                }
            }


            Padding = new Thickness(0);
            NavigationPage.SetHasNavigationBar(this, false);



        }




    }
}
