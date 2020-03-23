using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Resx;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels.Workers
{
    public class OrderDriverListViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;

        private ObservableCollection<DriverListVM> _driverList;
        public ObservableCollection<DriverListVM> DriverList
        {
            get
            {
                return _driverList;

            }
            set
            {
                _driverList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DriverList"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public OrderDriverListViewModel(INavigation navigation, List<DriverList> driverList)
        {
            _navigation = navigation;

            var _list = new ObservableCollection<DriverListVM>();
            
            if (driverList.Count > 0)
            {
                for (int i = 0; i < driverList.Count; i++)
                {
                    _list.Add(new DriverListVM
                    {
                        Id=driverList[i].Id,
                        IsAccepted=driverList[i].IsAccepted,
                        Name = driverList[i].Name,
                        EstimatedTime = driverList[i].EstimatedTime,
                        Distance = driverList[i].Distance,
                        Price = driverList[i].Price,
                        Image = driverList[i].Image,
                        Location = driverList[i].Location,
                        RegistrationNumber = driverList[i].RegistrationNumber,
                        UserId = driverList[i].UserId,
                        ButtonVisible=true,
                        EstimatedTimeVisible = !string.IsNullOrEmpty(driverList[i].EstimatedTime) ? true : false,
                        PriceVisible = driverList[i].Price != null && driverList[i].Price != 0 ? true : false,
                        AcceptEnable = !string.IsNullOrEmpty(driverList[i].EstimatedTime) ? true : false,
                        NoPriceVisible = driverList[i].Price != null && driverList[i].Price != 0 ? false : true,
                    });

                    var getAcceptedResult = _list.Any(a => a.IsAccepted.Value);
                    if (getAcceptedResult == true)
                    {
                        foreach (var item in _list)
                        {
                            item.NoPriceVisible = false;
                            item.ButtonVisible = false;
                        }
                    }
                }
            }

            _driverList = _list;


        }

        //private Command _AcceptCommand;

        //public Command AcceptCommand
        //{
        //    get
        //    {
        //        /*the false returned in second constructor parameter will mean that button bound to this command 
        //        will alwasy be disabled; please change to your logic eg IsBusy view model property*/

        //        return _AcceptCommand ?? (_AcceptCommand =
        //            new Command(async () => await ExecuteSaveCommand(),() => false)
        //            );
        //    }
        //}

        public Command AcceptCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var getData = e as DriverListVM;
                    if(getData.PriceVisible)
                    {
                        AcceptQuote(getData.Id);
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("", AppResources.WaitingForConfirmation, AppResources.Ok);
                    }
                });
            }
        }

        public async void AcceptQuote(long id)
        {
            try
            {
                await _navigation.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();
                QuoteAcceptModel lData = new QuoteAcceptModel()
                {
                    DriverResponseId = id.ToString()
                };
                string postData = Newtonsoft.Json.JsonConvert.SerializeObject(lData);
                string url = ApiUrl.ApproveQuoteUrl;
                var result = await cbase.SendQuote(url, postData);
                if (result != null)
                {
                    if (result.status)
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);

                    }
                    else
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                    }
                }
                else
                {
                    Loader.CloseAllPopup();
                    await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                }
            }
            catch
            {
            }
        }

        public Command ChatCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var getData = e as DriverListVM;
                    DriverChatDetailPage.ReciverId = getData.UserId;
                    _navigation.PushAsync(new DriverChatDetailPage());
                });
            }
        }

    }

    public class QuoteAcceptModel
    {
        public string DriverResponseId { get; set; }
    }
}
