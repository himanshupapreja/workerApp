using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class ForgotPasswordViewModel: INotifyPropertyChanged
    {
        public INavigation _navigation;

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// property for the PhoneNumber
        /// </summary>
        private string _phoneNumber;


        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }
        /// <summary>
        /// property for the email 
        /// </summary>
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

        /// <summary>
        /// property for the email 
        /// </summary>
        private string _contactNumber=AppResources.ForgotPasswordContactText+ " 0555383908";
        public string ContactNumber
        {
            get
            {
                return _contactNumber;
            }
            set
            {
                _contactNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ContactNumber"));
            }
        }
        public ForgotPasswordViewModel(INavigation navigation)
        {
            _navigation = navigation;
            GetForgotPasswordContactDeatils();
        }

        /// <summary>
        /// This event will be used for sign up functionality.
        /// </summary>
        public Command forgotPasswordCommand
        {
            get
            {
                return new Command(async (e) =>
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
                            await ForgotPassword();
                        }
                    }
                });
            }
        }

        public string CheckValidations()
        {
            string msg = string.Empty;
 
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                msg += AppResources.PleaseEnterPhoneNumber;
            } 
            return msg;
        }

        /// <summary>
        /// This tapped event will be used for opening the login page.
        /// </summary>
        public Command loginCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PopAsync(); ;

                });
            }
        }

        public   async void GetForgotPasswordContactDeatils()
        {
            try
            {
                HttpClientBase cbase = new HttpClientBase();
                var result = await cbase.GetForgotPasswordContactDeatils(ApiUrl.GetForgotPassContDeatilsUrl);
                if (result != null && result.status)
                {
                    ContactNumber = AppResources.ForgotPasswordContactText +" " +result.contactNumber;
                }
            }
            catch
            {
            }
        }
        public async Task ForgotPassword()
        {
            try
            {
                await _navigation.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();

                var result = await cbase.ForgotPassword(ApiUrl.ForgotPasswordUrl + "?phoneNumber=" + PhoneNumber+
                    "&email="+Email);
                if (result != null)
                {
                    await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                    Loader.CloseAllPopup();
                    if (result.status)
                    { 
                        _navigation.PopAsync();
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
    }
}
