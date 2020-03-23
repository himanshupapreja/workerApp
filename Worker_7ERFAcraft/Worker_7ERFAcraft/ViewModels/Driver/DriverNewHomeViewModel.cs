using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Pages.Driver;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Resx;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels
{
    public class DriverNewHomeViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public static bool isFirstTime = false;
        public event PropertyChangedEventHandler PropertyChanged;
        Lng objLng;

        public DriverNewHomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
            objLng = App.Database.GetLng();
            isFirstTime = true;
        }

        public Command OnGoingCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new DriverHomePage(DriverOrderStatus.InProgress));
                });
            }
        }
        public Command PendingCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new DriverHomePage(DriverOrderStatus.Pending));
                });
            }
        }
        public Command HistoryCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new DriverHomePage(DriverOrderStatus.Completed));
                });
            }
        }
        public Command ChatCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new DriverHomePage(DriverOrderStatus.Chat));
                });
            }
        }
    }
}
