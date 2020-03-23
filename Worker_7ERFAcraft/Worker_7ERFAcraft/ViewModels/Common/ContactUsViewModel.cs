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
    public class ContactUsViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        
        
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
        private string _phoneNumber1;
        public string PhoneNumber1
        {
            get
            {
                return _phoneNumber1;

            }
            set
            {
                _phoneNumber1 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber1"));
            }
        }
        private string _phoneNumber2;
        public string PhoneNumber2
        {
            get
            {
                return _phoneNumber2;

            }
            set
            {
                _phoneNumber2 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber2"));
            }
        }

        private string _phoneNumber3;
        public string PhoneNumber3
        {
            get
            {
                return _phoneNumber3;

            }
            set
            {
                _phoneNumber3 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber3"));
            }
        }

        private string _phoneNumber4;
        public string PhoneNumber4
        {
            get
            {
                return _phoneNumber4;

            }
            set
            {
                _phoneNumber4 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber4"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged; 
        public ContactUsViewModel(INavigation navigation)
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
                GetContactUsData();
            }
        }


        public async void GetContactUsData()
        {
            try
            {
                await _navigation.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();

                var result = await cbase.GetAboutUsData(ApiUrl.GetAboutUs);
                if (result != null)
                {
                    if (result.status)
                    {
                        InternetVisible = false;
                        MainVisible = true;
                        NoDataVisible = false;
                        Email = result.aboutUsData.Email;
                        PhoneNumber1 = result.aboutUsData.PhoneNumber1;
                        PhoneNumber2 = result.aboutUsData.PhoneNumber2;
                        PhoneNumber3 = result.aboutUsData.PhoneNumber3;
                        PhoneNumber4 = result.aboutUsData.PhoneNumber4;

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

