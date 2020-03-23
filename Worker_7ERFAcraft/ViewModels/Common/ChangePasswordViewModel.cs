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
    public class ChangePasswordViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// property for the current password field
        /// </summary>
        private string _currentPassword;
        public string CurrentPassword
        {
            get
            {
                return _currentPassword;
            }
            set
            {
                _currentPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentPassword"));
            }
        }

        /// <summary>
        /// property for the new password field
        /// </summary>
        private string _newPassword;
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NewPassword"));
            }
        }

        /// <summary>
        /// property for the confirm new password field
        /// </summary>
        private string _confirmNewPassword;
        public string ConfirmNewPassword
        {
            get
            {
                return _confirmNewPassword;
            }
            set
            {
                _confirmNewPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmNewPassword"));
            }
        }
        INavigation NavigationService;
        Lng lng;
        public ChangePasswordViewModel(INavigation navigation)
        {
            NavigationService = navigation;
            lng = App.Database.GetLng();
        }
        string getMsg(NotificationMessage nMsg)
        {
            string msg = string.Empty;
            if (lng != null && !string.IsNullOrEmpty(lng.Language))
            {
                if (lng.Language == Models.CultureLanguage.Arabic)
                {
                    msg = nMsg.msgAr;
                }
                else if (lng.Language == Models.CultureLanguage.Bangali)
                {
                    msg = nMsg.msgBa;
                }
                else if (lng.Language == Models.CultureLanguage.Hindi)
                {
                    msg = nMsg.msgHi;
                }
                else if (lng.Language == Models.CultureLanguage.Urdu)
                {
                    msg = nMsg.msgUr;
                }
                else
                {
                    msg = nMsg.msgEng;
                }
            }
            else
            {
                return nMsg.msgEng;
            }
            return msg;
        }
        /// <summary>
        /// This event will be used for sign up functionality.
        /// </summary>
        public Command changePasswordCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var returnMessage = CheckValidations();
                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(returnMessage));
                        await Task.Delay(1000);
                        await App.Current.MainPage.Navigation.PopPopupAsync();
                        return;
                    }
                    else
                    {
                        if (!Common.CheckConnection())
                        {
                            await NavigationService.PushPopupAsync(new NoInternetPopup());
                            return;
                        }
                        else
                        {
                            await ChangePassword();
                        }
                    }
                });
            }
        }


        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                msg += AppResources.PleaseEnteroldpassword + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                msg += AppResources.PleaseEnternewpassword + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(CurrentPassword))
            {
                if (NewPassword!=ConfirmNewPassword)
                {
                    msg +=AppResources.PasswordsDoNotMatch + Environment.NewLine;
                }
            }
            if (!string.IsNullOrEmpty(CurrentPassword)&&!string.IsNullOrEmpty(NewPassword))
            {
                if (CurrentPassword == NewPassword)
                {
                    msg += AppResources.NewPasswordSameAsOldPassword + Environment.NewLine;
                }
            }
            return msg;
        }


        public async Task ChangePassword()
        {
            try
            {
                await NavigationService.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();

                string postData = "?userId=" + LoginUserDetails.userId + "&oldPassword=" + CurrentPassword + "&newPassword=" + NewPassword;

                var result = await cbase.ChangePassword(ApiUrl.ChangePasswordUrl + postData);
                if (result != null)
                {
                    Loader.CloseAllPopup();
                    await App.Current.MainPage.DisplayAlert("", getMsg(result.notificationMessage), AppResources.Ok);
                    if (result.status)
                    {  
                        App.Database.ClearLoginDetails();
                        App.Current.MainPage = new NavigationPage(new LoginPage());
                    } 
                }
                else
                {
                    Loader.CloseAllPopup();
                    await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg,AppResources.Ok);
                }
            }
            catch
            {
                
            }
        }
    }
}
