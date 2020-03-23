using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
    {
        public INavigation _navigation;
        /// <summary>
        /// property for the email field
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
        /// property for the password field
        /// </summary>
        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        bool _isBackToAddWork;
        public LoginViewModel(INavigation navigation ,bool isBackToAddWork)
        {
            _navigation = navigation;
            _isBackToAddWork = isBackToAddWork;
        }

        /// <summary>
        /// This tapped event will be used for opening the login page.
        /// </summary>
        public Command backCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PopAsync(); ;

                });
            }
        }

       
        /// <summary>
        /// This tapped event will be used for opening the signup page.
        /// </summary>
        public Command signUpCommand
        {
            get
            {
                return new Command((e) =>
                { 
                        _navigation.PushAsync(new StartPage());
                     
                });
            }
        }
       
        /// <summary>
        /// This tapped event will be used for opening the forgot password page.
        /// </summary>
        public Command forgotPasswordCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new ForgotPasswordPage());

                });
            }
        }

        /// <summary>
        /// This event will be used for login button functionality.
        /// </summary>
        public Command loginBtnCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    var returnMessage = CheckLoginValidations();
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
                           await Login();
                        }
                    }
               
                });
            }
        }
        public Command guestloginBtnCommand
        {
            get
            {
                return new Command((e) =>
                {
                _navigation.PushAsync(new HomeMasterPage());
                });
            }
        }

        public string CheckLoginValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                msg +=  AppResources.PleaseEnterPhoneNumber + Environment.NewLine;
            } 
            if (string.IsNullOrEmpty(Password))
            {
                msg +=  AppResources.PleaseEnterpassword;
            }
            return msg;
        }


        public async Task Login()
        {
            try
            {
                var DeviceId = Device.RuntimePlatform == Device.Android ? 1 : 2;
                await _navigation.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();
                postLoginData lData = new postLoginData() { PhoneNumber = PhoneNumber, Password = Password,
                    DeviceId =DeviceId,DeviceToken= App.DeviceToken};
                string postData = Newtonsoft.Json.JsonConvert.SerializeObject(lData);
                string url = ApiUrl.LoginUrl;
                var result = await cbase.Login(url, postData);
                if (result != null)
                {
                    if (result.status)
                    {
                        LoggedInUser objLoggedInUser = new LoggedInUser();
                        objLoggedInUser.userId = result.userData.UserId;
                        objLoggedInUser.phone = result.userData.PhoneNumber;
                        objLoggedInUser.name = result.userData.Name;
                        objLoggedInUser.userType = result.userData.UserType??0;
                        objLoggedInUser.image = result.userData.Image;

                        App.Database.SaveUpdateLoggedInUser(objLoggedInUser);
                        LoginUserDetails.userId = result.userData.UserId;
                        LoginUserDetails.name = result.userData.Name;
                        LoginUserDetails.phone = result.userData.PhoneNumber;
                        LoginUserDetails.userType = result.userData.UserType??0;
                        LoginUserDetails.image = result.userData.Image;

                        Loader.CloseAllPopup();
                        if(App.latitude =="0" && App.longitude =="0")
                        {
                            Common.GetLocation();
                        }
                        if(_isBackToAddWork && (result.userData.UserType == (int)UserType.Customer))
                        {
                            MessagingCenter.Send<LoginViewModel>(this, "Guestlogin");
                            _navigation.PopAsync();
                        }
                        else
                        {
                            App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                        }



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
    }
}
