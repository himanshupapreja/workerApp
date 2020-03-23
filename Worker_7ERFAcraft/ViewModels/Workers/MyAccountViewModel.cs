using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Plugin.Media;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
    public class MyAccountViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;
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
        bool IsPageLoaded = false;

        private string _availableText;
        public string AvailableText
        {
            get
            {
                return _availableText;
            }
            set
            {
                _availableText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AvailableText")); 
            }
        }
        private DateTime _mDate = DateTime.Now;
        public DateTime MDate
        {
            get
            {
                return _mDate; 
            }
            set
            {
                _mDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MDate"));
                if (IsPageLoaded)
                {
                    getAccountData(false, true);
                }
            }
        }
        //TotalJobs=Total Jobs: 10
        private string _totalJobs;
        public string TotalJobs
        {
            get
            {
                return _totalJobs; 
            }
            set
            {
                _totalJobs = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalJobs"));
            }
        } 
        private string _totalBalance;
        public string TotalBalance
        {
            get
            {
                return _totalBalance; 
            }
            set
            {
                _totalBalance = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalBalance"));
            }
        }
        private string _registrationNumber;
        public string RegistrationNumber
        {
            get
            {
                return _registrationNumber; 
            }
            set
            {
                _registrationNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RegistrationNumber"));
            }
        }
        private double? _rating=0;
        public double? Rating
        {
            get
            {
                return _rating; 
            }
            set
            {
                _rating = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Rating"));
            }
        }
        private ImageSource _imgRate1 = "ic_star_fill_2.png";
        public ImageSource ImgRate1
        {
            get
            {
                return _imgRate1; 
            }
            set
            {
                _imgRate1 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate1"));
            }
        }
        private ImageSource _imgRate2 = "ic_star_fill_2.png";
        public ImageSource ImgRate2
        {
            get
            {
                return _imgRate2; 
            }
            set
            {
                _imgRate2 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate2"));
            }
        }
        private ImageSource _imgRate3 = "ic_star_fill_2.png";
        public ImageSource ImgRate3
        {
            get
            {
                return _imgRate3; 
            }
            set
            {
                _imgRate3 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate3"));
            }
        }
        private ImageSource _imgRate4 = "ic_star_fill_2.png";
        public ImageSource ImgRate4
        {
            get
            {
                return _imgRate4; 
            }
            set
            {
                _imgRate4 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate4"));
            }
        }
        private ImageSource _imgRate5 = "ic_star_fill_2.png";
        public ImageSource ImgRate5
        {
            get
            {
                return _imgRate5; 
            }
            set
            {
                _imgRate5 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgRate5"));
            }
        }
        private string _totalReviews;
        public string TotalReviews
        {
            get
            {
                return _totalReviews;

            }
            set
            {
                _totalReviews = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalReviews"));
            }
        }

        private string _userImage;
        public string UserImage
        {
            get
            {
                return _userImage;

            }
            set
            {
                _userImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserImage"));
            }
        }

        private string _fullName;
        public string FullName
        {
            get
            {
                return _fullName;

            }
            set
            {
                _fullName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FullName"));
            }
        }
        //int count = -1;
        //bool _isOwned=false;
        //public bool IsOwned
        //{
        //    get
        //    {
        //        return _isOwned;
        //    }
        //    set
        //    {
        //        _isOwned = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs("IsOwned"));
        //        if( count !=0)
        //        {
        //            updateAvailability(_isOwned);
        //        } 
        //    }
        //} 
        private string _yourDebt;
        public string YourDebt
        {
            get
            {
                return _yourDebt;

            }
            set
            {
                _yourDebt = value;
                PropertyChanged(this, new PropertyChangedEventArgs("YourDebt"));
            }
        }
        //PerLongProjects=10%
        private string _perLongProjects;
        public string PerLongProjects
        {
            get
            {
                return _perLongProjects;

            }
            set
            {
                _perLongProjects = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PerLongProjects"));
            }
        }

 
        private ImageSource _imageReceipt = "ic_add_image.png";
        public ImageSource ImageReceipt
        {
            get
            {
                return _imageReceipt;

            }
            set
            {
                _imageReceipt = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageReceipt"));
            }
        }
    
    /// <summary>
    /// property for the first ReceiptNumber field
    /// </summary>
    private string _receiptNumber;
    public string ReceiptNumber
    {
        get
        {
            return _receiptNumber;
        }
        set
        {
            _receiptNumber = value;
            PropertyChanged(this, new PropertyChangedEventArgs("ReceiptNumber"));
        }
    }
    /// <summary>
    /// property for the amount
    /// </summary>
    private string _balance;
    public string Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Balance"));
            }
        }
        public bool isJobAvailabe = false;
        private ImageSource _imageJobAvailable= "ic_off.png";
        public ImageSource ImageJobAvailable
        {
            get
            {
                return _imageJobAvailable;
            }
            set
            {
                _imageJobAvailable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageJobAvailable"));
            }
        }

        LongOrder _selectedOrderItem;
        public LongOrder SelectedOrderItem
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
        private ObservableCollection<Grouping<string, LongOrder>>  _orderListGrouped;
        public ObservableCollection<Grouping<string, LongOrder>> OrderListGrouped
        {
            get
            {
                return _orderListGrouped;

            }
            set
            {
                _orderListGrouped = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OrderListGrouped"));
            }
        }
        
        ObservableCollection<LongOrder> _list;

        private ObservableCollection<LongOrder> _orderList;
        public ObservableCollection<LongOrder> OrderList
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
        private decimal _listViewHeight = 0;
        public decimal ListViewHeight
        {
            get
            {
                return _listViewHeight;

            }
            set
            {
                _listViewHeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ListViewHeight"));
            }
        }

        public MyAccountViewModel(INavigation navigation)
        { 
            _navigation = navigation;
            getAccountData();
        }
        public async void getAccountData(bool isPullToRefresh=false,bool isDateChanged=false)
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
                    var cbase = new HttpClientBase();
                    if (!isPullToRefresh)
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                    }
                    OrderListGrouped = new ObservableCollection<Grouping<string, LongOrder>>();
                   var result = await cbase.GetAccountData(ApiUrl.GetAccountDataUrl+LoginUserDetails.userId+ "&month=" + MDate.Month
                       + "&year=" + MDate.Year);
                    if (result != null)
                    {
                        IsPageLoaded = true;
                        if (!isPullToRefresh)
                        {
                            Loader.CloseAllPopup();
                        }
                        if (result.status)
                        {
                            if (!isDateChanged)
                            {
                                FullName = result.myAccountData.FullName;
                                RegistrationNumber = result.myAccountData.RegistrationNumber;
                                UserImage = result.myAccountData.UserImage;
                                Rating = result.myAccountData.Rating??0;

                                showRating(result.myAccountData.Rating ?? 0);
                                
                                TotalReviews = "("+result.myAccountData.TotalReviews+")";

                                TotalBalance =   (string.IsNullOrEmpty(result.myAccountData.TotalBalance) ? "0"
                                    :Decimal.Round(Convert.ToDecimal( result.myAccountData.TotalBalance)).ToString()) + " "+ result.myAccountData.CurrencyCode;

                                TotalJobs =   (string.IsNullOrEmpty(result.myAccountData.TotalJobs) ? "0"
                                    : result.myAccountData.TotalJobs);

                                //count = 0;
                                //IsOwned = result.myAccountData.IsAvailable ?? false;
                                AvailableText = result.myAccountData.IsAvailable ==true ?Resx.AppResources.Available
                                    : Resx.AppResources.Unavailable;
                                isJobAvailabe = result.myAccountData.IsAvailable ?? false;
                                ImageJobAvailable = isJobAvailabe ? "ic_on.png" : "ic_off.png";
                                YourDebt = (string.IsNullOrEmpty(result.myAccountData.YourDebt) ? "0" 
                                    :Decimal.Round(Convert.ToDecimal(result.myAccountData.YourDebt)).ToString()) + " " + result.myAccountData.CurrencyCode;

                                PerLongProjects = (string.IsNullOrEmpty(result.myAccountData.LongProjectPercentage) ? "0" :
                                    result.myAccountData.LongProjectPercentage) + "%";
                            }

                            if (result.myAccountData != null && result.myAccountData.LongOrders != null && result.myAccountData.LongOrders.Count > 0)
                            {
                                ListViewHeight = result.myAccountData.LongOrders.Count * 130; 
                                var sorted = from orders in result.myAccountData.LongOrders
                                             orderby orders.WorkDateFrom descending
                                             group orders by orders.MonthYearName into caserefGroup
                                             select new Grouping<string, LongOrder>(caserefGroup.Key, caserefGroup);

                                OrderListGrouped = new ObservableCollection<Grouping<string, LongOrder>>(sorted);

                            } 
                        }
                    }
                    else
                    {
                        if (!isPullToRefresh)
                        {
                            Loader.CloseAllPopup();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        void showRating(double rating)
        {
            if (rating >= 5)
            {
                ImgRate1 = "ic_star_fill.png";
                ImgRate2 = "ic_star_fill.png";
                ImgRate3 = "ic_star_fill.png";
                ImgRate4 = "ic_star_fill.png";
                ImgRate5 = "ic_star_fill.png";
            }
            else if (rating >= 4)
            {
                ImgRate1 = "ic_star_fill.png";
                ImgRate2 = "ic_star_fill.png";
                ImgRate3 = "ic_star_fill.png";
                ImgRate4 = "ic_star_fill.png"; 
            }
            else if (rating >= 3)
            {
                ImgRate1 = "ic_star_fill.png";
                ImgRate2 = "ic_star_fill.png";
                ImgRate3 = "ic_star_fill.png"; 
            }
            else if (rating >= 2)
            {
                ImgRate1 = "ic_star_fill.png";
                ImgRate2 = "ic_star_fill.png"; 
            }
            else if (rating >= 1)
            {
                ImgRate1 = "ic_star_fill.png"; 
            }
        }
        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public async void updateAvailability( )
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
                    var cbase = new HttpClientBase();
                    await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                    var result = await cbase.UpdateAvailability(ApiUrl.UpdateAvailabilityUrl + LoginUserDetails.userId+ "&isAvailable="+ isJobAvailabe, "");
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok );
                        if (result.status)
                        { 
                        }
                    }
                    else
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

       
        public Command ImageJobAvailableCommand
        {
            get
            {
                return new Command((e) =>
                {
                     if(isJobAvailabe)
                    {
                        ImageJobAvailable = "ic_off.png";
                        isJobAvailabe = false;
                        AvailableText = Resx.AppResources.Unavailable;
                    }
                    else
                    {
                        ImageJobAvailable = "ic_on.png";
                        isJobAvailabe = true;
                        AvailableText = Resx.AppResources.Available;
                    }
                    updateAvailability();
                });
            }
        }
        public Command addbtnCommand
        {
            get
            {
                return new Command((e) =>
                {
                     App.Current.MainPage.Navigation.PushPopupAsync(new AddBalancePopup());
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
                    getAccountData(true);
                    IsRefreshingData = false;
                });
            }
        }

        public Command workerDetailCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var data = (LongOrder)e;
                    if (data != null)
                    {
                        //_navigation.PushAsync(new WorkerProfilePage(data.UserId.ToString()));
                        _navigation.PushAsync(new WorkerWorkDetailPage(data.OrderId.ToString()));
                    }
                });
            }
        }
         
    }
}

