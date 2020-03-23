using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace Worker_7ERFAcraft.ViewModels.Driver
{
    public class DriverChatUserViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ChatUserData> _Items = new ObservableCollection<ChatUserData>();
        public ObservableCollection<ChatUserData> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }

        private bool _internetVisible = false;
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

        private bool _noDataVisible = false;
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

        private bool _mainVisible = false;
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


        public DriverChatUserViewModel()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                getChatUsers();
            });
        }

        public async void getChatUsers()
        {
            if (!Common.CheckConnection())
            {
                InternetVisible = true;
                MainVisible = false;
                NoDataVisible = false;
            }
            else
            {
                try
                {
                    HttpClientBase cbase = new HttpClientBase();
                    var result = await cbase.GetChatUsers(ApiUrl.ChatUserUrl + "?userId=" + LoginUserDetails.userId);
                    if (result != null)
                    {
                        if (result.data.Count > 0)
                        {
                            if (result.status)
                            {
                                ObservableCollection<ChatUserData> lst = new ObservableCollection<ChatUserData>();

                                foreach (var item in result.data)
                                {
                                    lst.Add(new ChatUserData
                                    {
                                        CreatedDate = item.CreatedDate,
                                        Name = item.Name,
                                        PicturePath = string.IsNullOrEmpty(item.PicturePath) ? "user.png" : item.PicturePath,
                                        UserId = item.UserId
                                    });
                                }
                                Items = lst;
                            }

                            InternetVisible = false;
                            MainVisible = true;
                            NoDataVisible = false;
                        }

                        else
                        {
                            InternetVisible = false;
                            MainVisible = false;
                            NoDataVisible = true;

                        }
                    }
                    else
                    {
                        // Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, Resx.AppResources.Ok);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        //private async Task DownloadDataAsync()
        //{
        //    var items = await _dataService.GetItemsAsync(pageIndex: 0, pageSize: PageSize);

        //    Items.AddRange(items);
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }





    }
}
