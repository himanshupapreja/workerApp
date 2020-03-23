using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Resx;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels.Driver
{
    public class DriverSendQuoteViewModel : INotifyPropertyChanged
    {
        public string _requestId = string.Empty;
        public INavigation _navigation;

        private string _price;
        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
            }
        }

        private string _estimatedTime;

        public event PropertyChangedEventHandler PropertyChanged;

        public string EstimatedTime
        {
            get
            {
                return _estimatedTime;
            }
            set
            {
                _estimatedTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EstimatedTime"));
            }
        }

        public DriverSendQuoteViewModel(INavigation navigation,string RequestId)
        {
            _navigation = navigation;
            _requestId = RequestId;

        }

        public Command SendQuoteCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    var returnMessage = CheckValidations();
                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                        await _navigation.PushPopupAsync(new ShowMessage(returnMessage));
                        await Task.Delay(1000);
                        await _navigation.PopPopupAsync();
                        return;
                    }
                    else
                    {
                        if (!Common.CheckConnection())
                        {
                            await _navigation.PushPopupAsync(new NoInternetPopup());
                            return;
                        }
                        else
                        {
                            await SendQuoteMethod();
                        }
                    }
                });
            }
        }

        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Price))
            {
                msg += AppResources.PleaseEnterPrice + Environment.NewLine;
            }
            //if (string.IsNullOrEmpty(EstimatedTime))
            //{
            //    msg += AppResources.PleaseEnterTime;
            //}
            return msg;
        }

        public async Task SendQuoteMethod()
        {
            try
            {
                await _navigation.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();
                SendQuoteModel lData = new SendQuoteModel()
                {
                    Price=Convert.ToInt32(Convert.ToDouble(Price)),
                    EstimatedTime=EstimatedTime,
                    RequestId=_requestId,
                    DriverId=Convert.ToInt32(LoginUserDetails.userId)
                };
                string postData = Newtonsoft.Json.JsonConvert.SerializeObject(lData);
                string url = ApiUrl.SendQuoteUrl;
                var result = await cbase.SendQuote(url, postData);
                if (result != null)
                {
                    if (result.status)
                    {
                        Loader.CloseAllPopups();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                        //await App.Current.MainPage.Navigation.PopPopupAsync(true);
                        MessagingCenter.Send("Driver", "RefreshHomePage");
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
                Loader.CloseAllPopup();
            }
        }

    }

    public class SendQuoteModel
    {
        public string RequestId { get; set; }
        public int Price { get; set; }
        public string EstimatedTime { get; set; }
        public int DriverId { get; set; }
    }
}
