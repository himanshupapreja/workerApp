using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels
{
    public class TermsConditionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _internetVisible=false;
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

        private bool _noDataVisible=false;
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

        private bool _mainVisible=true;
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

        private string _termsConditionsText;
        public string TermsConditionsText
        {
            get
            {
                return _termsConditionsText;
            }
            set
            {
                _termsConditionsText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TermsConditionsText"));
            }
        }
        INavigation _navigation;
        public TermsConditionsViewModel(INavigation navigation)
        {
            _navigation = navigation;
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
                TermsData();
            } 
        }


        public async void TermsData()
        {
            try
            {
                await _navigation.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();

                var result = await cbase.GetTermsData(ApiUrl.GetTerms);
                if (result != null)
                {
                    if (result.status)
                    {
                        InternetVisible = false;
                        MainVisible = true;
                        NoDataVisible = false;
                        TermsConditionsText = result.termsData.Text; 

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
                
            }
        }

    }
}
