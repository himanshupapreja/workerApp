using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Worker_7ERFAcraft.Resx;
using System.Linq;

namespace Worker_7ERFAcraft.ViewModels
{
    public class WorkerNewHomeViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public static bool isFirstTime = false;
        public event PropertyChangedEventHandler PropertyChanged;
        Lng objLng;

        public WorkerNewHomeViewModel(INavigation navigation)
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
                    _navigation.PushAsync(new WorkerHomePage(OrderType.OnGoing));
                });
            }
        }
        public Command PendingCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new WorkerHomePage(OrderType.Pending));
                });
            }
        }
        public Command HistoryCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new WorkerHomePage(OrderType.History));
                });
            }
        }
        public Command ChatCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new WorkerHomePage(OrderType.Chat));
                });
            }
        }
    }
}
