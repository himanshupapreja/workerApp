using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels
{
    public class AppVideoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsLoading"));
            }
        }
        
        
        /// <summary>
        /// property for the MediaNameUrl
        /// </summary>
        private Uri _mediaNameUrl=null;
        public Uri MediaNameUrl
        {
            get
            {
                return _mediaNameUrl;
            }
            set
            {
                _mediaNameUrl = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MediaNameUrl"));
            }
        }

        INavigation NavigationService;
        public AppVideoViewModel(INavigation navigation)
        {
            NavigationService = navigation;
            getVideo(); 

        } 
        public async void getVideo()
        {
            try
            {
                //_isLoading = true;
                await NavigationService.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();
                var result = await cbase.GetAppVideo(ApiUrl.AppVideoUrl);
                //IsLoading = false;
                Loader.CloseAllPopup();
                if (result != null)
                {
                    if (result.appVideoData != null)
                    {
                        MediaNameUrl = !string.IsNullOrEmpty(result.appVideoData)
                            ? new Uri(result.appVideoData) : null;
                    } 
                } 
            }
            catch
            {
            }
        }






    }
}
