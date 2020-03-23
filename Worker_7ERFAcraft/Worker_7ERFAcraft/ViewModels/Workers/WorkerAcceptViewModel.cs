using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels.Worker
{
    public class WorkerAcceptViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public int _orderId;
        public UpdateOrderStatus _updateOrderStatus;
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties

        private string _optionOne;
        public string OptionOne
        {
            get
            {
                return _optionOne;
            }
            set
            {
                _optionOne = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OptionOne"));
            }
        }

        private string _optionTwo;
        public string OptionTwo
        {
            get
            {
                return _optionTwo;
            }
            set
            {
                _optionTwo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OptionTwo"));
            }
        }

        #endregion


        public WorkerAcceptViewModel(INavigation navigation,int orderId, UpdateOrderStatus updateOrderStatus)
        {
            _navigation = navigation;
            _orderId = orderId;
            _optionOne = "app_select_2.png";
            _optionTwo = "app_select_2.png";
        }

        public Command OptionOneSelected
        {
            get
            {
                return new Command(async (e) =>
                {
                    OptionOne = "app_select.png";
                    OptionTwo = "app_select_2.png";
                    var answer = await App.Current.MainPage.DisplayAlert("", 
                        Resx.AppResources.DriverAcceptRequest,
                          Resx.AppResources.Yes, Resx.AppResources.No);
                    if(answer)
                    {
                        if (!Common.CheckConnection())
                        {
                            await _navigation.PushPopupAsync(new NoInternetPopup());
                            return;
                        }
                        else
                        {
                            try
                            {
                                await _navigation.PopPopupAsync();
                                await _navigation.PushPopupAsync(new Loader());
                                HttpClientBase cbase = new HttpClientBase();
                                AddRequestModel addRequestModel = new AddRequestModel();
                                addRequestModel.OrderId = _orderId;
                                if (_updateOrderStatus == UpdateOrderStatus.Accept)
                                {
                                    addRequestModel.RequestType = DriverRequestType.OnAccepted.GetHashCode();
                                }
                                else if (_updateOrderStatus == UpdateOrderStatus.Complete)
                                {
                                    addRequestModel.RequestType = DriverRequestType.OnCompleted.GetHashCode();
                                }
                                string Url = ApiUrl.AddDriverRequestUrl;
                                var result = await cbase.AddDriverRequest(Url, addRequestModel);
                                if (result != null)
                                {
                                    await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), Resx.AppResources.Ok);
                                    Loader.CloseAllPopup();
                                }
                                else
                                {
                                    Loader.CloseAllPopup();
                                    await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, Resx.AppResources.Ok);
                                }
                            }
                            catch (Exception ex)
                            {
                                Loader.CloseAllPopup();
                            }
                        }
                    }                 
                });
            }
        }


        public Command OptionTwoSelected
        {
            get
            {
                return new Command(async (e) =>
                {
                    OptionTwo = "app_select.png";
                    OptionOne = "app_select_2.png";
                    var answer = await App.Current.MainPage.DisplayAlert("",
                        Resx.AppResources.DriverRejectRequest,
                          Resx.AppResources.Yes, Resx.AppResources.No);
                    if(answer)
                    {
                        await _navigation.PopPopupAsync();
                    }
                });
            }
        }
    }

    public class AddRequestModel
    {
        public int OrderId { get; set; }
        public int? RequestType { get; set; }
    }
}
