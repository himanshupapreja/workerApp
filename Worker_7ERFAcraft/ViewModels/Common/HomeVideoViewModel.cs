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
    public class HomeVideoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private bool _isShownHeader=false;
        public bool IsShownHeader
        {
            get
            {
                return _isShownHeader;
            }
            set
            {
                _isShownHeader = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsShownHeader"));
            }
        }
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
        private bool _isCountShown = false;
        public bool IsCountShown
        {
            get
            {
                return _isCountShown;
            }
            set
            {
                _isCountShown = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsCountShown"));
            }
        }

        private int _count = 0;
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Count"));
            }
        }
        private string _time = string.Empty;
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Time"));
            }
        }
        private string _min = string.Empty;
        public string Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Min"));
            }
        }
        private string _sec = string.Empty;
        public string Sec
        {
            get
            {
                return _sec;
            }
            set
            {
                _sec = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Sec"));
            }
        }
        /// <summary>
        /// property for IsImage
        /// </summary>
        private bool _isImage=true;
        public bool  IsImage
        {
            get
            {
                return _isImage;
            }
            set
            {
                _isImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsImage"));
            }
        }

        /// <summary>
        /// property for the IsVideo
        /// </summary>
        private bool _isVideo=false;
        public bool IsVideo
        {
            get
            {
                return _isVideo;
            }
            set
            {
                _isVideo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsVideo"));
            }
        }

        /// <summary>
        /// property for the MediaName
        /// </summary>
        private ImageSource _mediaName= "splash.png";
        public ImageSource MediaName
        {
            get
            {
                return _mediaName;
            }
            set
            {
                _mediaName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MediaName"));
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
        static bool isClose = false;
        INavigation _navigation;
        public HomeVideoViewModel(INavigation navigation)
        {
            _navigation = navigation;
            getHomeData();
             
        } 
        public async void getHomeData()
        {
            try
            {
                //await _navigation.PushPopupAsync(new Loader());
                _isLoading = true;
                var DeviceId = Device.RuntimePlatform == Device.Android ? 1 : 2;
                HttpClientBase cbase = new HttpClientBase();
                var result = await cbase.GetHomeVideo(ApiUrl.PreHomeScreenMediaUrl + "?userId=" + LoginUserDetails.userId );
                //Loader.CloseAllPopup();
                IsLoading = false;
                if (result != null)
                { 
                    if (result.screenMediaData != null)
                    {
                        IsShownHeader = true;
                           MediaName = result.screenMediaData.MediaName;
                        MediaNameUrl = !string.IsNullOrEmpty(result.screenMediaData.MediaName)
                            ? new Uri(result.screenMediaData.MediaName) : null;

                        IsVideo = result.screenMediaData.MediaType.ToLower() == "video";
                        IsImage = result.screenMediaData.MediaType.ToLower() == "image";

                        // run task in 5 seconds
                        IsCountShown = true;
                         Count = 0;

                        //get current time
                        //int hh = DateTime.Now.Hour;
                        int mm = DateTime.Now.Minute;
                        int ss = DateTime.Now.Second;
                       // hh = 0;
                        mm = 0;
                        ss = 6;
                        string time = "";
                        time = "00:00";
                        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                        {
                            //if (ss == 0)
                            //{
                            //    if (!isClose)
                            //    { 
                            //        App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                            //    }
                            //    return false;
                            //}
                            //time

                            time = string.Empty;
                            if (mm < 10)
                            {
                                Min = "0" + mm;
                                time += "0" + mm;
                            }
                            else
                            {
                                time += mm;
                            }
                            time += ":";
                            ss--;
                            if (ss < 10)
                            {
                                Sec = "0" + ss;
                                time += "0" + ss;
                                Time = time;
                                if (ss == 0)
                                {
                                    close();
                                    return false;
                                }
                            }
                            else
                            {
                                if (ss == 60)
                                {
                                    ss = 0;
                                    mm++;
                                }
                                time += ss;
                                Time = time;
                            }

                            //update label
                            //label1.Text = time;

                           
                            //Count++; 

                            //if (Count <=5)
                            //{
                            //    // returning true will fire task again in 5 seconds.
                            //    if (Count == 5)
                            //    {
                            //        if (!isClose)
                            //        {
                            //            App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                            //        }
                            //        return false;

                            //    }
                            //    return true;
                               
                            //}
                            //if (!isClose)
                            //{
                            //    App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                            //}
                            return true;
                        });
                    }
                    else
                    {
                       App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                        //_navigation.PushAsync(new HomeMasterPage());
                    } 
                }
            }
            catch
            {
            }
        }
        
        async void close()
        {
            if (!isClose)
            {
                await Task.Delay(5);
                App.Current.MainPage = new NavigationPage(new HomeMasterPage());
            }
        }
        public Command btnCloseCommand
        {
            get
            {
                return new Command((e) =>
                {
                    isClose = true;
                    App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                });
            }
        }




    }
}
