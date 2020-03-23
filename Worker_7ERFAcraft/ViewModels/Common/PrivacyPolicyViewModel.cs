using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels
{
    public class PrivacyPolicyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _privacyPolicyText;
        public string PrivacyPolicyText
        {
            get
            {
                return _privacyPolicyText;
            }
            set
            {
                _privacyPolicyText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PrivacyPolicyText"));
            }
        }

        private bool _internetVisible;
        public bool InternetVisible
        {
            get
            {
                return _internetVisible;
            }
            set
            {
                _internetVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("InternetVisible"));
            }
        }

        private bool _noDataVisible;
        public bool NoDataVisible
        {
            get
            {
                return _noDataVisible;
            }
            set
            {
                _noDataVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NoDataVisible"));
            }
        }

        private bool _mainVisible;
        public bool MainVisible
        {
            get
            {
                return _mainVisible;
            }
            set
            {
                _mainVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MainVisible"));
            }
        }
        INavigation NavigationService;
        public PrivacyPolicyViewModel(INavigation navigation)
        { 
            NavigationService = navigation;
            _internetVisible = false;
            _mainVisible = true;
            _noDataVisible = false;

            if (!Common.CheckConnection())
            {
                _internetVisible = true;
                _mainVisible = false;
                _noDataVisible = false;
            }
            else
            {
                PrivacyPolicyData();
            }
        }

        public async void PrivacyPolicyData()
        {
            try
            {
                await NavigationService.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();

                var result = await cbase.GetPrivacyPolicyData(ApiUrl.GetPrivacyPolicy);
                if (result != null)
                {
                    if (result.status)
                    {
                        InternetVisible = false;
                        MainVisible = true;
                        NoDataVisible = false;
                        PrivacyPolicyText = result.privacyPolicyData.Text; 

                        Loader.CloseAllPopup();
                    }
                    else
                    {
                        InternetVisible = false;
                        MainVisible = false;
                        NoDataVisible = true;

                        Loader.CloseAllPopup();
                    }
                }
                else
                {
                    Loader.CloseAllPopup();
                    await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, "OK");
                }
            }
            catch (Exception ex)
            {
                // Loader.CloseAllPopup();
            }
        }


    }
}


