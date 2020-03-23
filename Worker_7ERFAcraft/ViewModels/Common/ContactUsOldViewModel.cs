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
    public class ContactUsOldViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;

        private string _email;
        public string Email
        {
            get
            {
                return _email;

            }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string _subject;
        public string Subject
        {
            get
            {
                return _subject;

            }
            set
            {
                _subject = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Subject"));
            }
        }
        private string _message;
        public string Message
        {
            get
            {
                return _message;

            }
            set
            {
                _message = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Message"));
            }
        }   

        public event PropertyChangedEventHandler PropertyChanged; 
        public ContactUsOldViewModel(INavigation navigation)
        { 
            _navigation = navigation; 
        }


        public Command sendBtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    if (!Common.CheckConnection())
                    {
                        await _navigation.PushPopupAsync(new NoInternetPopup());
                        return;
                    }
                    string msg = string.Empty;
                    if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Email.Trim()))
                    {
                        msg += AppResources.EnterEmail + System.Environment.NewLine;
                    }
                    else if(!Common.CheckValidEmail(Email))
                    {
                        msg += AppResources.EntervalidEmail + System.Environment.NewLine;
                    }
                    if (string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Subject.Trim()))
                    {
                        msg += AppResources.EnterSubject + System.Environment.NewLine;
                    }
                    if (string.IsNullOrEmpty(Message)
                            || string.IsNullOrEmpty(Message.Trim()))
                    {
                        msg += AppResources.EnterDetails + System.Environment.NewLine;
                    } 
                    if (!string.IsNullOrEmpty(msg))
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(msg));
                        await Task.Delay(1000);
                        await _navigation.PopPopupAsync();
                        return;
                    }
                    else
                    {
                        try
                        {
                            await _navigation.PushPopupAsync(new Loader());
                            HttpClientBase cbase = new HttpClientBase();

                            ContactUsModel m = new ContactUsModel()
                            {
                                UserName=Email,
                                Subject=Subject,
                                Message=Message
                            };

                            var result = await cbase.ContactUs(ApiUrl.ContactUs,m);
                            if (result != null)
                            {
                                Loader.CloseAllPopup();
                                await App.Current.MainPage.DisplayAlert("",Common.getMsg( result.notificationMessage), AppResources.Ok);
                                if (result.status)
                                {
                                    Email = string.Empty;
                                    Subject = string.Empty;
                                    Message = string.Empty;
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

                });
            }
        }



    }
}

