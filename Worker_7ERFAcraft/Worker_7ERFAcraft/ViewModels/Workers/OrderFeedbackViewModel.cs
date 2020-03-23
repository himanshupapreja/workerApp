using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class OrderFeedbackViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
          

        
        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Comment"));
            }
        }

         

         

        INavigation NavigationService;
        string _orderId;
        public OrderFeedbackViewModel(INavigation navigation,string orderId)
        {
            _orderId = orderId;
            NavigationService = navigation;  
        }

        public async void RateWorker()
        {
            if (!Common.CheckConnection())
            {
                await NavigationService.PushPopupAsync(new NoInternetPopup());
                return;
            }
            else
            {
                try
                {
                    await NavigationService.PushPopupAsync(new Loader());
                    HttpClientBase cbase = new HttpClientBase();
                    string Url =    ApiUrl.AddWorkerFeedbackUrl;
                    Url += "?orderId="+_orderId + "&comment="+Comment;
                    var result = await cbase.RateWorker(Url);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                        if (result.status)
                        {
                            OrderFeedbackPopup.ClosePopup();
                        }
                        
                    }
                    else
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }



        public Command submitBtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    string msg = string.Empty; 
                    if (string.IsNullOrEmpty(Comment) )
                    {
                        msg += AppResources.EnterComments + Environment.NewLine; 
                    }
                    if (!string.IsNullOrEmpty(msg))
                    { 
                        await NavigationService.PushPopupAsync(new ShowMessage(msg));
                        await Task.Delay(1000);
                        await NavigationService.PopPopupAsync();
                        return;
                    }
                    
                    RateWorker();
                });
            }
        }


     


       


    }
}
