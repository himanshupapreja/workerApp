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
    public class RateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
         
        private decimal _rating = 0;
        public decimal Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                _rating = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Rating"));
            }
        }

        private ImageSource _imgRate1 = "ic_star_fill_2.png";
        public ImageSource ImgRate1
        {
            get
            {
                return _imgRate1;
            }
            set
            {
                _imgRate1 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate1"));
            }
        }
        private ImageSource _imgRate2 = "ic_star_fill_2.png";
        public ImageSource ImgRate2
        {
            get
            {
                return _imgRate2;
            }
            set
            {
                _imgRate2 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate2"));
            }
        }
        private ImageSource _imgRate3 = "ic_star_fill_2.png";
        public ImageSource ImgRate3
        {
            get
            {
                return _imgRate3;
            }
            set
            {
                _imgRate3 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate3"));
            }
        }
        private ImageSource _imgRate4 = "ic_star_fill_2.png";
        public ImageSource ImgRate4
        {
            get
            {
                return _imgRate4;
            }
            set
            {
                _imgRate4 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate4"));
            }
        }
        private ImageSource _imgRate5 = "ic_star_fill_2.png";
        public ImageSource ImgRate5
        {
            get
            {
                return _imgRate5;
            }
            set
            {
                _imgRate5 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate5"));
            }
        }

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
        public RateViewModel(INavigation navigation,string orderId)
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
                    string Url =    ApiUrl.AddRatingUrl;
                    Url += "?orderId="+_orderId+"&rating="+Rating+ "&comment="+Comment;
                    var result = await cbase.RateWorker(Url);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                        if (result.status)
                        {
                            RatePopup.Rating = Rating;
                            RatePopup.ClosePopup();
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
                    if (Rating == 0)
                    {
                        msg += AppResources.Giveatleastonestar + Environment.NewLine;
                    }
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


        public Command ImgRate1Command
        {
            get
            {
                return new Command((e) =>
                {
                    Rating = 1;
                       ImgRate1 = "ic_star_fill.png";
                    ImgRate2 = "ic_star_fill_2.png";
                    ImgRate3 = "ic_star_fill_2.png";
                    ImgRate4 = "ic_star_fill_2.png";
                    ImgRate5 = "ic_star_fill_2.png";
                });
            }
        }
        public Command ImgRate2Command
        {
            get
            {
                return new Command((e) =>
                {
                    Rating = 2;
                       ImgRate1 = "ic_star_fill.png";
                    ImgRate2 = "ic_star_fill.png";
                    ImgRate3 = "ic_star_fill_2.png";
                    ImgRate4 = "ic_star_fill_2.png";
                    ImgRate5 = "ic_star_fill_2.png";
                });
            }
        }
        public Command ImgRate3Command
        {
            get
            {
                return new Command((e) =>
                {
                    Rating = 3;
                    ImgRate1 = "ic_star_fill.png";
                    ImgRate2 = "ic_star_fill.png";
                    ImgRate3 = "ic_star_fill.png";
                    ImgRate4 = "ic_star_fill_2.png";
                    ImgRate5 = "ic_star_fill_2.png";
                });
            }
        }
        public Command ImgRate4Command
        {
            get
            {
                return new Command((e) =>
                {
                    Rating = 4;
                    ImgRate1 = "ic_star_fill.png";
                    ImgRate2 = "ic_star_fill.png";
                    ImgRate3 = "ic_star_fill.png";
                    ImgRate4 = "ic_star_fill.png";
                    ImgRate5 = "ic_star_fill_2.png";
                });
            }
        }
        public Command ImgRate5Command
        {
            get
            {
                return new Command((e) =>
                {
                    Rating = 5;
                    ImgRate1 = "ic_star_fill.png";
                    ImgRate2 = "ic_star_fill.png";
                    ImgRate3 = "ic_star_fill.png";
                    ImgRate4 = "ic_star_fill.png";
                    ImgRate5 = "ic_star_fill.png";
                });
            }
        }





    }
}
