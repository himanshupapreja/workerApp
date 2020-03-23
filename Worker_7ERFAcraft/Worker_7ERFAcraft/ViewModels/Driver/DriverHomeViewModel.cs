using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Pages.Driver;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Resx;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels.Driver
{

    public class DriverHomeViewModel : INotifyPropertyChanged
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

        static Color selectedColor = Color.FromHex("#fdc99b");
        static Color deselectedColor = (Color)App.Current.Resources["MainColor"];// Color.FromHex("#d2d2d2"); 

        public INavigation _navigation;


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
        private bool _isRefreshingData;
        public bool IsRefreshingData
        {
            get
            {
                return _isRefreshingData;
            }
            set
            {
                _isRefreshingData = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsRefreshingData"));
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

        private bool _ChatVisible;
        public bool ChatVisible
        {
            get
            {
                return _ChatVisible;
            }
            set
            {
                _ChatVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ChatVisible"));
            }
        }

        Order _selectedOrderItem;
        public Order SelectedOrderItem
        {
            get
            {
                return _selectedOrderItem;
            }
            set
            {
                if (value != null)
                {
                    _selectedOrderItem = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedOrderItem"));
                    if (_selectedOrderItem != null)
                    {
                        // _navigation.PushAsync(new WorkerWorkDetailPage(_selectedOrderItem.OrderId.ToString()));
                    }
                    _selectedOrderItem = null;
                }

            }
        }
        ObservableCollection<DriversOrder> _list;

        private ObservableCollection<DriversOrder> _orderList;
        public ObservableCollection<DriversOrder> OrderList
        {
            get
            {
                return _orderList;

            }
            set
            {
                _orderList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OrderList"));
            }
        }
        DriverOrderStatus selectedOrderType = DriverOrderStatus.Pending;
        #region Style
        private bool _isOnGoing = false;
        public bool IsOnGoing
        {
            get
            {
                return _isOnGoing;
            }
            set
            {
                _isOnGoing = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsOnGoing"));
            }
        }
        private bool _isNotOnGoing = true;
        public bool IsNotOnGoing
        {
            get
            {
                return _isNotOnGoing;
            }
            set
            {
                _isNotOnGoing = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsNotOnGoing"));
            }
        }

        private bool _isNew = true;
        public bool IsNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                _isNew = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsNew"));
            }
        }
        private bool _isNotNew = false;
        public bool IsNotNew
        {
            get
            {
                return _isNotNew;
            }
            set
            {
                _isNotNew = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsNotNew"));
            }
        }


        private bool _isHistory = false;
        public bool IsHistory
        {
            get
            {
                return _isHistory;
            }
            set
            {
                _isHistory = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsHistory"));
            }
        }
        private bool _isNotHistory = true;
        public bool IsNotHistory
        {
            get
            {
                return _isNotHistory;
            }
            set
            {
                _isNotHistory = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsNotHistory"));
            }
        }


        private bool _IsChat = false;
        public bool IsChat
        {
            get
            {
                return _IsChat;
            }
            set
            {
                _IsChat = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsChat"));
            }
        }
        private bool _IsNotChat = true;
        public bool IsNotChat
        {
            get
            {
                return _IsNotChat;
            }
            set
            {
                _IsNotChat = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsNotChat"));
            }
        }
        #endregion

        #region boxBgColor
        private Color _onNewBoxBgColor = selectedColor;
        public Color OnNewBgColor
        {
            get
            {
                return _onNewBoxBgColor;
            }
            set
            {
                _onNewBoxBgColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OnNewBgColor"));
            }
        }


        private Color _onGoingBoxBgColor = deselectedColor;
        public Color OnGoingBoxBgColor
        {
            get
            {
                return _onGoingBoxBgColor;
            }
            set
            {
                _onGoingBoxBgColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OnGoingBoxBgColor"));
            }
        }
        private Color _historyBoxBgColor = deselectedColor;
        public Color HistoryBoxBgColor
        {
            get
            {
                return _historyBoxBgColor;
            }
            set
            {
                _historyBoxBgColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HistoryBoxBgColor"));
            }
        }

        private Color _ChatBoxBgColor = deselectedColor;
        public Color ChatBoxBgColor
        {
            get
            {
                return _ChatBoxBgColor;
            }
            set
            {
                _ChatBoxBgColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ChatBoxBgColor"));
            }
        }
        #endregion

        public static bool isFirstTime = false;
        public event PropertyChangedEventHandler PropertyChanged;
        Lng objLng;
        public DriverHomeViewModel(INavigation navigation, DriverOrderStatus driverOrderStatus)
        {
            selectedOrderType = driverOrderStatus;
            _navigation = navigation;
            objLng = App.Database.GetLng();
            isFirstTime = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                if (selectedOrderType != DriverOrderStatus.Chat)
                {
                    getOrdersList();
                }
                else
                {
                    getChatUsers();
                }
            });


        }

        public async void getOrdersList(bool isPullToRefresh = false)
        {
            if (!Common.CheckConnection())
            {
                InternetVisible = true;
                MainVisible = false;
                ChatVisible = false;
                NoDataVisible = false;
            }
            else
            {
                try
                {
                    OrderList = new ObservableCollection<DriversOrder>();
                    if (!isPullToRefresh)
                    {
                        _isLoading = true;
                        IsLoading = true;
                    }
                    //await _navigation.PushPopupAsync(new Loader());

                    HttpClientBase cbase = new HttpClientBase();
                    string Url = ApiUrl.GetDriverRequestsUrl + LoginUserDetails.userId;
                    var result = await cbase.GetDriverOrders(Url);
                    if (result != null)
                    {
                        if (result.data.Count > 0)
                        {
                            if (result.status)
                            {
                                _isRefreshingData = false;
                                IsRefreshingData = false;

                                InternetVisible = false;
                                MainVisible = true;
                                ChatVisible = false;
                                NoDataVisible = false;

                                _list = new ObservableCollection<DriversOrder>();
                                foreach (var item in result.data)
                                {
                                    _list.Add(new DriversOrder
                                    {
                                        Id = item.Id,
                                        CustomerName = Common.FirstCharToUpper(item.CustomerName),
                                        CustomerRegNumber = "(" + item.CustomerRegNumber + ")",
                                        WorkerRegNumber = "(" + item.WorkerRegNumber + ")",
                                        WorkerName = Common.FirstCharToUpper(item.WorkerName),
                                        OrderId = item.OrderId,
                                        WorkerId = item.WorkerId,
                                        RequestId = item.RequestId,
                                        SendQuoteText = item.Price == null ? AppResources.SendQuote : AppResources.AlreadySent,
                                        IsHistory = selectedOrderType == DriverOrderStatus.Completed,
                                        IsOnGoing = selectedOrderType == DriverOrderStatus.InProgress,
                                        IsPending = selectedOrderType == DriverOrderStatus.Pending,
                                        IsHistoryVisible = item.RequestStatus == DriverOrderStatus.Completed.GetHashCode(),
                                        IsNewVisible = item.RequestStatus == DriverOrderStatus.Pending.GetHashCode(),
                                        IsOnGoingVisible = item.RequestStatus == DriverOrderStatus.InProgress.GetHashCode()
                                        || item.RequestStatus == DriverOrderStatus.StartRide.GetHashCode()
                                        || item.RequestStatus == DriverOrderStatus.EndRide.GetHashCode(),
                                        CustomerLocation = item.CustomerLocation,
                                        WorkerLocation = item.WorkerLocation,
                                        RequestStatus = item.RequestStatus,
                                        RideStatusText = item.RequestStatus == DriverOrderStatus.InProgress.GetHashCode() ? AppResources.start_ride : item.RequestStatus == DriverOrderStatus.StartRide.GetHashCode() ? AppResources.end_ride : string.Empty
                                    });
                                }
                                var _newList = _list.Where(A => A.RequestStatus == selectedOrderType.GetHashCode()
                                || A.RequestStatus == DriverOrderStatus.StartRide.GetHashCode());
                                var myObservableCollection = new ObservableCollection<DriversOrder>(_newList);
                                OrderList = myObservableCollection;

                                if(OrderList.Count>0)
                                {
                                    InternetVisible = false;
                                    MainVisible = true;
                                    ChatVisible = false;
                                    NoDataVisible = false;
                                }
                                else
                                {
                                    InternetVisible = false;
                                    MainVisible = false;
                                    ChatVisible = false;
                                    NoDataVisible = true;
                                }

                                //  Loader.CloseAllPopup(); 
                                if (!isPullToRefresh)
                                {
                                    _isLoading = false;
                                    IsLoading = false;
                                }

                            }
                        }

                        else
                        {
                            InternetVisible = false;
                            MainVisible = false;
                            ChatVisible = false;
                            NoDataVisible = true;
                            if (!isPullToRefresh)
                            {
                                _isLoading = false;
                                IsLoading = false;
                            }
                            //Loader.CloseAllPopup();
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

        public Command OnGoingCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (selectedOrderType != DriverOrderStatus.InProgress)
                    {
                        IsOnGoing = true;
                        OnGoingBoxBgColor = selectedColor;

                        IsNew = false;
                        OnNewBgColor = deselectedColor;

                        IsHistory = false;
                        HistoryBoxBgColor = deselectedColor;

                        IsChat = false;
                        ChatBoxBgColor = deselectedColor;

                        IsNotNew = !IsNew;
                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;
                        IsNotChat = !IsChat;

                        selectedOrderType = DriverOrderStatus.InProgress;
                        getOrdersList();
                    }


                });
            }
        }

        public Command HistoryCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (selectedOrderType != DriverOrderStatus.Completed)
                    {
                        IsOnGoing = false;
                        OnGoingBoxBgColor = deselectedColor;

                        IsHistory = true;
                        HistoryBoxBgColor = selectedColor;

                        IsNew = false;
                        OnNewBgColor = deselectedColor;

                        IsChat = false;
                        ChatBoxBgColor = deselectedColor;

                        IsNotNew = !IsNew;
                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;
                        IsNotChat = !IsChat;

                        selectedOrderType = DriverOrderStatus.Completed;
                        getOrdersList();
                    }
                });
            }
        }

        public Command PendingCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (selectedOrderType != DriverOrderStatus.Pending)
                    {
                        IsNew = true;
                        IsOnGoing = false;
                        IsHistory = false;
                        IsChat = false;
                        IsNotNew = !IsNew;
                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;
                        IsNotChat = !IsChat;

                        OnNewBgColor = selectedColor;
                        OnGoingBoxBgColor = deselectedColor;
                        HistoryBoxBgColor = deselectedColor;
                        ChatBoxBgColor = deselectedColor;

                        selectedOrderType = DriverOrderStatus.Pending;
                        getOrdersList();
                    }
                });
            }
        }

        public Command ChatTabCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (selectedOrderType != DriverOrderStatus.Chat)
                    {
                        IsNew = false;
                        IsOnGoing = false;
                        IsHistory = false;
                        IsChat = true;
                        IsNotNew = !IsNew;
                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;
                        IsNotChat = !IsChat;

                        OnNewBgColor = deselectedColor;
                        OnGoingBoxBgColor = deselectedColor;
                        HistoryBoxBgColor = deselectedColor;
                        ChatBoxBgColor = selectedColor;

                        selectedOrderType = DriverOrderStatus.Chat;

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            getChatUsers();
                        });
                    }
                });
            }
        }

        public async void getChatUsers()
        {
            if (!Common.CheckConnection())
            {
                InternetVisible = true;
                MainVisible = false;
                ChatVisible = false;
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
                            MainVisible = false;
                            ChatVisible = true;
                            NoDataVisible = false;
                        }

                        else
                        {
                            InternetVisible = false;
                            MainVisible = false;
                            ChatVisible = false;
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

        public Command ChatCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var getData = e as DriversOrder;
                    DriverChatDetailPage.ReciverId = getData.WorkerId;
                    _navigation.PushAsync(new DriverChatDetailPage());
                });
            }
        }

        public Command SendQuoteCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var getData = e as DriversOrder;
                    if (getData.SendQuoteText == AppResources.SendQuote)
                    {
                        _navigation.PushPopupAsync(new SendQuotePage(getData.RequestId.ToString()));
                    }
                });
            }
        }

        public Command RideStatusCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var getData = e as DriversOrder;
                    int status = getData.RideStatusText == AppResources.start_ride ? 1 : 2;
                    StartEndRideMethod(getData.RequestId,status);
                });
            }
        }

        public async void StartEndRideMethod(long id,int status)
        {
            try
            {
                await _navigation.PushPopupAsync(new Loader());
                HttpClientBase cbase = new HttpClientBase();
                StartStopRideModel lData = new StartStopRideModel()
                {
                    DriverResponseId=id,
                    Status= status,
                };
                string postData = Newtonsoft.Json.JsonConvert.SerializeObject(lData);
                string url = ApiUrl.StartEndRideUrl;
                var result = await cbase.SendQuote(url, postData);
                if (result != null)
                {
                    if (result.status)
                    {
                        Loader.CloseAllPopup();
                        getOrdersList();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);

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

        public Command RefreshDataCommand
        {
            get
            {
                return new Command((e) =>
                {
                    IsRefreshingData = true;
                    getOrdersList(true);
                    IsRefreshingData = false;
                });
            }
        }

        public Command MapOpenCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    var item = (DriversOrder)e;

                    //if (Device.RuntimePlatform == Device.iOS)
                    //{
                    //    var request = string.Format("http://maps.apple.com/?daddr=" + MyBooking.PropertyLatitude.Value + "," + MyBooking.PropertyLongitude.Value + "");
                    //    await Launcher.OpenAsync(new Uri(request));
                    //}
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        var request = string.Format("https://www.google.com/maps/dir/" + item.WorkerLocation+ "/" + item.CustomerLocation + "/");
                        await Launcher.OpenAsync(new Uri(request));
                    }
                });
            }
        }
    }

    public class StartStopRideModel
    {
        public long DriverResponseId { get; set; }
        public int Status { get; set; }
    }

}
