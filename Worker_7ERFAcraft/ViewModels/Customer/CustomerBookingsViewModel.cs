using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class CustomerBookingsViewModel : INotifyPropertyChanged
    {
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
                        _navigation.PushAsync(new WorkDetailPage(_selectedOrderItem));
                    }
                    _selectedOrderItem = null;
                }

            }
        }
        ObservableCollection<Order> _list;

        private ObservableCollection<Order> _orderList;
        public ObservableCollection<Order> OrderList
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
        OrderType selectedOrderType = OrderType.OnGoing;
        #region Style
        private bool _isOnGoing = true;
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
        private bool _isNotOnGoing = false;
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
        
        
        #endregion

        #region boxBgColor
        private Color _onGoingBoxBgColor = selectedColor;
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
        private Color _pendingBoxBgColor = deselectedColor;
        public Color PendingBoxBgColor
        {
            get
            {
                return _pendingBoxBgColor;
            }
            set
            {
                _pendingBoxBgColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PendingBoxBgColor"));
            }
        }
        #endregion

        Lng objLng;
        public event PropertyChangedEventHandler PropertyChanged;
        public CustomerBookingsViewModel(INavigation navigation)
        {
            _navigation = navigation;
            objLng = App.Database.GetLng();
            Device.BeginInvokeOnMainThread(() => {
                getOrdersList();
            });


        }

        public async void getOrdersList(bool isPullToRefresh = false)
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
                    OrderList = new ObservableCollection<Order>();
                    if (!isPullToRefresh)
                    {
                        _isLoading = true; 
                    }
                  //  await _navigation.PushPopupAsync(new Loader());
                    HttpClientBase cbase = new HttpClientBase();
                    string Url = string.Empty;
                    if (selectedOrderType == OrderType.OnGoing)
                    {
                        Url = ApiUrl.CustomerBookingOngoingUrl;
                    }
                    else if (selectedOrderType == OrderType.History)
                    {
                        Url = ApiUrl.CustomerBookingHistoryUrl;
                    }
                    Url +=   LoginUserDetails.userId;
                    var result = await cbase.GetCustomerBookings(Url);
                    if (result != null)
                    {
                        if (result.status)
                        {
                            InternetVisible = false;
                            MainVisible = true;
                            NoDataVisible = false;

                            _list = new ObservableCollection<Order>();
                            foreach (var item in result.customerBookingData)
                            {
                                _list.Add(new Order
                                {
                                    Location=item.Location,
                                    CategoryName = objLng.Language == (CultureLanguage.Arabic) ? item.CategoryName_Arabic
                                    : objLng.Language == (CultureLanguage.Bangali) ? item.CategoryName_Bangali
                                    : objLng.Language == (CultureLanguage.English) ? item.CategoryName
                                    : objLng.Language == (CultureLanguage.Hindi) ? item.CategoryName_Hindi
                                    : objLng.Language == (CultureLanguage.Urdu) ? item.CategoryName_Urdu
                                    : item.CategoryName,
                                    CustomerImage =item.CategoryImage,
                                    OrderId = item.OrderId,
                                    WorkDateFrom = item.WorkDateFrom,
                                    StatusText = (int)OrderStatus.Pending == item.Status ?AppResources.Pending.ToUpper():
                                    (int)OrderStatus.Canceled == item.Status ? AppResources.Canceled.ToUpper():
                                    (int)OrderStatus.Completed == item.Status ? AppResources.Completed.ToUpper():
                                    (int)OrderStatus.InProgress == item.Status ? AppResources.OnGoing.ToUpper():item.StatusText.ToUpper(),
                                    WorkDateTo = item.WorkDateTo,
                                    WorkHoursFrom = item.WorkHoursFrom,
                                    WorkHoursTo = item.WorkHoursTo,
                                    IsHistory = selectedOrderType == OrderType.History,
                                    IsOnGoing = selectedOrderType == OrderType.OnGoing,
                                    IsPending= (int)OrderStatus.Pending == item.Status,
                                });
                            }
                            OrderList = _list;
                            //Loader.CloseAllPopup();
                            if (!isPullToRefresh)
                            {
                                _isLoading = false ;
                                IsLoading = false ;
                            }
                        }
                        else
                        {
                            InternetVisible = false;
                            MainVisible = false;
                            NoDataVisible = true;

                            //Loader.CloseAllPopup();
                            if (!isPullToRefresh)
                            {
                                _isLoading = false ;
                                IsLoading = false ;
                            }
                        }
                    }
                    else
                    {
                        // Loader.CloseAllPopup();
                        if (!isPullToRefresh)
                        {
                            _isLoading = false ;
                            IsLoading = false ;
                        }
                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, "OK");
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
                    if (selectedOrderType != OrderType.OnGoing)
                    {  
                        OnGoingBoxBgColor = selectedColor; 
                        HistoryBoxBgColor = deselectedColor; 
                        selectedOrderType = OrderType.OnGoing;

                        IsOnGoing = true;
                        IsHistory = false;

                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;

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
                    if (selectedOrderType != OrderType.History)
                    { 
                         
                        OnGoingBoxBgColor = deselectedColor; 
                        HistoryBoxBgColor = selectedColor;

                        selectedOrderType = OrderType.History;

                        IsOnGoing = false;
                        IsHistory = true;

                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;

                        getOrdersList();
                    }
                });
            }
        }
        int index = -1;
        public Command cancelBtnCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    var order = (Order)e;
                   
                        index = OrderList.ToList().FindIndex(x => x.OrderId == order.OrderId);
                      
                        CancellationReasonsPopup popup = new CancellationReasonsPopup(order.OrderId.ToString());
                        popup.Disappearing += Popup_Disappearing;
                        await _navigation.PushPopupAsync(popup);
                });
            }
        }

        private void Popup_Disappearing(object sender, EventArgs e)
        {
            if (CancellationReasonsPopup.isCancelDone && index > 0)
            {
                OrderList.RemoveAt(index);
                index = -1;
            }
        }

        public Command bookingDetailCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var data = (Order)e;
                    _navigation.PushAsync(new WorkDetailPage(data));

                });
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

    }
}

