using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Worker_7ERFAcraft.Resx;
using System.Linq;

namespace Worker_7ERFAcraft.ViewModels
{
    public class WorkerHomeViewModel : INotifyPropertyChanged
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
                        _navigation.PushAsync(new WorkerWorkDetailPage(_selectedOrderItem.OrderId.ToString()));
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
        private bool _isPending = false;
        public bool IsPending
        {
            get
            {
                return _isPending;
            }
            set
            {
                _isPending = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsPending"));
            }
        }
        private bool _isNotPending = true;
        public bool IsNotPending
        {
            get
            {
                return _isNotPending;
            }
            set
            {
                _isNotPending = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsNotPending"));
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

        public static bool isFirstTime = false; 
        public event PropertyChangedEventHandler PropertyChanged;
        Lng objLng;
        public WorkerHomeViewModel(INavigation navigation)
        { 
            _navigation = navigation;
            objLng = App.Database.GetLng();
            isFirstTime = true;
            Device.BeginInvokeOnMainThread(() => {
                getOrdersList();
            });
           
             
        }

        public async void getOrdersList(bool isPullToRefresh=false)
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
                   if(!isPullToRefresh)
                    {
                        _isLoading = true;
                        IsLoading = true;
                    }
                    //await _navigation.PushPopupAsync(new Loader());

                    HttpClientBase cbase = new HttpClientBase();
                    string Url = string.Empty;
                    if (selectedOrderType == OrderType.Pending)
                    {
                        Url = ApiUrl.WorkerBookingPending;
                    }
                    else if (selectedOrderType == OrderType.OnGoing)
                    {
                        Url = ApiUrl.WorkerBookingOngoing;
                    }
                    else if (selectedOrderType == OrderType.History)
                    {
                        Url = ApiUrl.WorkerBookingHistory;
                    }
                    Url += LoginUserDetails.userId;
                      var result = await cbase.GetWorkerOrders(Url);
                    if (result != null)
                    {
                        if (result.status)
                        {
                            _isRefreshingData = false;
                            IsRefreshingData = false;

                            InternetVisible = false;
                            MainVisible = true;
                            NoDataVisible = false;

                            _list = new ObservableCollection<Order>();
                            foreach (var item in result.customerBookingData)
                            {
                                if (selectedOrderType == OrderType.Pending)
                                {
                                    lastPendingLst = result.customerBookingData;
                                }
                                else
                                {
                                    lastPendingLst = new System.Collections.Generic.List<Order>();
                                }
                                _list.Add(new Order
                                {
                                    CustomerImage = item.CustomerImage == null ? "user.png" : item.CustomerImage,
                                    CustomerName = Common.FirstCharToUpper(item.CustomerName),
                                    CustomerRegistrationNumber = item.CustomerRegistrationNumber,
                                    WorkerRegistrationNumber = item.WorkerRegistrationNumber,
                                    WorkerImage = item.WorkerImage == null ? "user.png" : item.WorkerImage,
                                    WorkerName = Common.FirstCharToUpper(item.WorkerName),
                                    CategoryName = objLng.Language == (CultureLanguage.Arabic) ? item.CategoryName_Arabic
                                    : objLng.Language == (CultureLanguage.Bangali) ? item.CategoryName_Bangali
                                    : objLng.Language == (CultureLanguage.English) ? item.CategoryName
                                    : objLng.Language == (CultureLanguage.Hindi) ? item.CategoryName_Hindi
                                    : objLng.Language == (CultureLanguage.Urdu) ? item.CategoryName_Urdu
                                    : item.CategoryName,
                                    OrderId = item.OrderId,
                                    WorkDateFrom = item.WorkDateFrom,
                                    StatusText = (int)OrderStatus.Pending == item.Status ? AppResources.Pending.ToUpper() :
                                    (int)OrderStatus.Canceled == item.Status ? AppResources.Canceled.ToUpper() :
                                    (int)OrderStatus.Completed == item.Status ? AppResources.Completed.ToUpper() :
                                    (int)OrderStatus.InProgress == item.Status ? AppResources.OnGoing.ToUpper() : item.StatusText.ToUpper(),
                                    WorkDateTo = item.WorkDateTo,
                                    WorkHoursFrom = item.WorkHoursFrom,
                                    WorkHoursTo = item.WorkHoursTo,
                                    IsHistory=selectedOrderType== OrderType.History ,
                                    IsOnGoing = selectedOrderType == OrderType.OnGoing,
                                    IsPending = selectedOrderType == OrderType.Pending,
                                });
                            }
                            OrderList = _list;
                            //  Loader.CloseAllPopup(); 
                            if (!isPullToRefresh)
                            {
                                _isLoading = false;
                                IsLoading = false;
                            }

                        }
                        else
                        {
                            InternetVisible = false;
                            MainVisible = false;
                            NoDataVisible = true;
                            if (!isPullToRefresh)
                            {
                                _isLoading = false ;
                                IsLoading = false ;
                            }
                            //Loader.CloseAllPopup();
                        }
                    }
                    else
                    {
                        // Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, Resx.AppResources.Ok);
                    }
                    if (isFirstTime)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                        {
                            if (Device.RuntimePlatform == Device.iOS)
                            {
                                Task.Run(async () =>
                                {
                                //await Task.Delay(300);
                                Device.BeginInvokeOnMainThread(() =>
                                    {
                                        if (selectedOrderType == OrderType.Pending)
                                        {
                                            updatePendingOrders();
                                        }
                                    });
                                });
                            }
                            else
                            {
                                Task.Run(() =>
                          {
                                    if (selectedOrderType == OrderType.Pending)
                                    {
                                        updatePendingOrders();
                                    }
                                });
                            }
                            //if (WorkerHomePage.IsDismissed)
                            //{
                            //    return false;
                            //}
                            //else
                            //{
                                return true;
                            //}

                        });
                    }
                    else
                    {
                        isFirstTime = false;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        public static System.Collections.Generic.List<Order> lastPendingLst;
        public async void updatePendingOrders()
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
                    string Url = ApiUrl.WorkerBookingPending;
                    Url += LoginUserDetails.userId;

                    var result = await cbase.GetWorkerOrders(Url);
                    if (result != null)
                    {
                        if (result.status)
                        {
                            _isRefreshingData = false;
                            IsRefreshingData = false;

                            InternetVisible = false;
                            MainVisible = true;
                            NoDataVisible = false;


                            if (result.customerBookingData.Count > 0)
                            {
                                if (lastPendingLst.Count < result.customerBookingData.Count)
                                {
                                    _list = OrderList;
                                    for (int i = 0; i < result.customerBookingData.Count - lastPendingLst.Count; i++)
                                    {
                                        var item = result.customerBookingData[i];

                                        _list.Add(new Order
                                        {
                                            CustomerImage = item.CustomerImage == null ? "user.png" : item.CustomerImage,
                                            CustomerName = Common.FirstCharToUpper(item.CustomerName),
                                            CustomerRegistrationNumber = item.CustomerRegistrationNumber,
                                            WorkerRegistrationNumber = item.WorkerRegistrationNumber,
                                            WorkerImage = item.WorkerImage == null ? "user.png" : item.WorkerImage,
                                            WorkerName = Common.FirstCharToUpper(item.WorkerName),
                                            CategoryName = objLng.Language == (CultureLanguage.Arabic) ? item.CategoryName_Arabic
                                                                            : objLng.Language == (CultureLanguage.Bangali) ? item.CategoryName_Bangali
                                                                            : objLng.Language == (CultureLanguage.English) ? item.CategoryName
                                                                            : objLng.Language == (CultureLanguage.Hindi) ? item.CategoryName_Hindi
                                                                            : objLng.Language == (CultureLanguage.Urdu) ? item.CategoryName_Urdu
                                                                            : item.CategoryName,
                                            OrderId = item.OrderId,
                                            WorkDateFrom = item.WorkDateFrom,
                                            StatusText = (int)OrderStatus.Pending == item.Status ? AppResources.Pending.ToUpper() :
                                                                            (int)OrderStatus.Canceled == item.Status ? AppResources.Canceled.ToUpper() :
                                                                            (int)OrderStatus.Completed == item.Status ? AppResources.Completed.ToUpper() :
                                                                            (int)OrderStatus.InProgress == item.Status ? AppResources.OnGoing.ToUpper() : item.StatusText.ToUpper(),
                                            WorkDateTo = item.WorkDateTo,
                                            WorkHoursFrom = item.WorkHoursFrom,
                                            WorkHoursTo = item.WorkHoursTo,
                                            IsHistory = selectedOrderType == OrderType.History,
                                            IsOnGoing = selectedOrderType == OrderType.OnGoing,
                                            IsPending = selectedOrderType == OrderType.Pending,
                                        });
                                    }
                                    lastPendingLst = result.customerBookingData;
                                    OrderList = _list;
                                }
                                if (lastPendingLst.Count > result.customerBookingData.Count)
                                {
                                    foreach (var item in OrderList.ToList())
                                    {
                                        var orderDetails = result.customerBookingData.Where(x=>x.OrderId == item.OrderId)
                                            .FirstOrDefault();
                                        if (orderDetails == null)
                                        {
                                            OrderList.Remove(item);
                                        }
                                    }
                                    lastPendingLst = result.customerBookingData;
                                }
                            }
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
        public Command OnGoingCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if(selectedOrderType != OrderType.OnGoing)
                    {
                        IsPending = false;
                        PendingBoxBgColor = deselectedColor;

                        IsOnGoing = true;
                        OnGoingBoxBgColor = selectedColor;

                        IsHistory = false;
                        HistoryBoxBgColor = deselectedColor;

                        IsNotPending = !IsPending;
                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;

                        selectedOrderType = OrderType.OnGoing;
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
                    if (selectedOrderType != OrderType.Pending)
                    {
                        IsPending = true;
                        IsOnGoing = false;
                        IsHistory = false;
                        IsNotPending = !IsPending;
                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;

                        PendingBoxBgColor = selectedColor;
                        OnGoingBoxBgColor = deselectedColor;
                        HistoryBoxBgColor = deselectedColor;

                        selectedOrderType = OrderType.Pending;
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
                        IsPending = false;
                        PendingBoxBgColor = deselectedColor;

                        IsOnGoing = false;
                        OnGoingBoxBgColor = deselectedColor;

                        IsHistory = true;
                        HistoryBoxBgColor = selectedColor;

                        IsNotPending = !IsPending;
                        IsNotOnGoing = !IsOnGoing;
                        IsNotHistory = !IsHistory;

                        selectedOrderType = OrderType.History;
                        getOrdersList();
                    }
                });
            }
        }

        public Command jobDetailCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var data = (Order)e;
                    _navigation.PushAsync(new WorkerWorkDetailPage(data.OrderId.ToString()));

                });
            }
        }
        public Command rejectOrderCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    var data = (Order)e;
                    var answer = await App.Current.MainPage.DisplayAlert("", Resx.AppResources.RejectThisOrder,
                        Resx.AppResources.Yes, Resx.AppResources.No);
                    if (answer)
                    {
                        updateOrder(UpdateOrderStatus.Reject, data);
                    }
                   
                });
            }
        }
        public async void updateOrder(UpdateOrderStatus status, Order data)
        {
            if (!Common.CheckConnection())
            {
                await _navigation.PushPopupAsync(new NoInternetPopup());
                return;
            }
            else
            {
                try
                {
                    await _navigation.PushPopupAsync(new Loader());
                    HttpClientBase cbase = new HttpClientBase();
                    string Url = string.Empty;
                    if (status == UpdateOrderStatus.Accept)
                    {
                        Url = ApiUrl.AcceptOrderUrl;
                    }
                    else
                    {
                        Url = ApiUrl.RejectOrderUrl;
                    }
                    Url += data.OrderId;
                    var result = await cbase.AcceptRejectOrder(Url);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        if (status == UpdateOrderStatus.Accept)
                        {
                            await App.Current.MainPage.Navigation.PushPopupAsync(new WorkerAcceptPopup(data.OrderId));

                            if (result.status)
                            {
                                OrderList.Remove(data);
                            }
                        }

                    }
                    else
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, Resx.AppResources.Ok);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        public Command acceptOrderCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    var data =(Order)e;
                var answer = await App.Current.MainPage.DisplayAlert("", Resx.AppResources.AcceptThisOrder
                    , Resx.AppResources.Yes, Resx.AppResources.No);
                    if (answer)
                    {
                        updateOrder(UpdateOrderStatus.Accept, data);
                    }

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

