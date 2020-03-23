using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.Generic;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.Resx;
using Worker_7ERFAcraft.Pages.Workers;

namespace Worker_7ERFAcraft.ViewModels
{
    public class WorkerWorkDetailViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public static List<DriverList> driversList=new List<DriverList>();
        private bool _isRatingShown = false;
        public bool IsRatingShown
        {
            get
            {
                return _isRatingShown;

            }
            set
            {
                _isRatingShown = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsRatingShown"));
            }
        }
        private decimal? _rating = 0;
        public decimal? Rating
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
        private bool _isVisibleComplete;
        public bool IsVisibleComplete
        {
            get
            {
                return _isVisibleComplete;

            }
            set
            {
                _isVisibleComplete = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsVisibleComplete"));
            }
        }
        //IsAcceptReject
        private bool _isAcceptReject;
        public bool IsAcceptReject
        {
            get
            {
                return _isAcceptReject;

            }
            set
            {
                _isAcceptReject = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsAcceptReject"));
            }
        }
        private string _customerImage;
        public string CustomerImage
        {
            get
            {
                return _customerImage;

            }
            set
            {
                _customerImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerImage"));
            }
        }
        private string _regNo;
        public string RegNo
        {
            get
            {
                return _regNo;

            }
            set
            {
                _regNo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RegNo"));
            }
        }
        private string _customerName;
        public string CustomerName
        {
            get
            {
                return _customerName;

            }
            set
            {
                _customerName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerName"));
            }
        }

        private string _location;
        public string Location
        {
            get
            {
                return _location;

            }
            set
            {
                _location = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            }
        }
        private bool _isNotPending = false;
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
        private string _workDate;
        public string WorkDate
        {
            get
            {
                return _workDate;

            }
            set
            {
                _workDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkDate"));
            }
        }

        private string _workHours;
        public string WorkHours
        {
            get
            {
                return _workHours;

            }
            set
            {
                _workHours = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkHours"));
            }
        }

        private string _workNotes;
        public string WorkNotes
        {
            get
            {
                return _workNotes;

            }
            set
            {
                _workNotes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkNotes"));
            }
        }
        private decimal _lstViewHeight = 0;
        public decimal LstViewHeight
        {
            get
            {
                return _lstViewHeight;

            }
            set
            {
                _lstViewHeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LstViewHeight"));
            }
        }


        private List<OrderMedia> _mediaList;
        public List<OrderMedia> MediaList
        {
            get
            {
                return _mediaList;

            }
            set
            {
                _mediaList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MediaList"));
            }
        }

        public List<DriverList> _driversList;
        public List<DriverList> DriversList
        {
            get
            {
                return _driversList;

            }
            set
            {
                _driversList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DriversList"));
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

        string _orderId;
        public event PropertyChangedEventHandler PropertyChanged;
        public WorkerWorkDetailViewModel(INavigation navigation, string orderId)
        {
            _navigation = navigation;
            _orderId = orderId;
            driversList = new List<DriverList>();
            getOrderDetail();
        }
        WorkDetailData objData = null;
        public async void getOrderDetail()
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
                    // _orderId = _orderId == "16" ? "9" : _orderId; ;
                    await _navigation.PushPopupAsync(new Loader());
                    HttpClientBase cbase = new HttpClientBase();
                    string Url = ApiUrl.WorkerWorkDetailUrl + _orderId;
                    var result = await cbase.GetOrderDetail(Url);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        if (result.status)
                        {
                            CustomerImage = string.IsNullOrEmpty(result.workDetailData.CustomerImage) ? "user.png"
                                : result.workDetailData.CustomerImage;
                            CustomerName = Common.FirstCharToUpper(result.workDetailData.CustomerName);
                            RegNo = result.workDetailData.RegistrationNumber;

                            WorkNotes = result.workDetailData.WorkNote;
                            WorkDate = result.workDetailData.WorkDateFrom == result.workDetailData.WorkDateTo ?
                                result.workDetailData.WorkDateFrom.ToString("dd-MM-yyyy") :
                                (result.workDetailData.WorkDateFrom.ToString("dd-MM-yyyy") + " - " + result.workDetailData.WorkDateTo.ToString("dd-MM-yyyy"));
                            WorkHours = result.workDetailData.WorkHoursFrom + "-" + result.workDetailData.WorkHoursTo;
                            objData = result.workDetailData;
                            Rating = result.workDetailData.Rating == null ? 0 : result.workDetailData.Rating;
                            showRating(Rating ?? 0);
                            MediaList = result.workDetailData.OrderMedias;
                            LstViewHeight = result.workDetailData.OrderMedias != null ? result.workDetailData.OrderMedias.Count * 140 : 0;

                            if (result.workDetailData.Status == (int)OrderStatus.Completed)
                            {
                                IsRatingShown = true;
                            }
                            if (result.workDetailData.Status == (int)OrderStatus.Pending)
                            {
                                IsAcceptReject = true;
                                IsVisibleComplete = false;
                                IsNotPending = false;
                            }
                            else if (result.workDetailData.Status == (int)OrderStatus.InProgress)
                            {
                                IsAcceptReject = false;
                                var curDate = DateTime.Now;
                                if (curDate >= result.workDetailData.WorkDateFrom)
                                {
                                    IsVisibleComplete = true;
                                }
                            }
                            if (result.workDetailData.Status != (int)OrderStatus.Pending
                                && result.workDetailData.Status != (int)OrderStatus.Canceled)
                            {
                                IsNotPending = true;
                                Location = result.workDetailData.Location;
                                if (!string.IsNullOrEmpty(result.workDetailData.Latitude)
                                    && !string.IsNullOrEmpty(result.workDetailData.Longitude))
                                {
                                    WorkerWorkDetailPage.latitude = result.workDetailData.Latitude.ToString();
                                    WorkerWorkDetailPage.longitude = result.workDetailData.Longitude.ToString();
                                }
                                else
                                {
                                    WorkerWorkDetailPage.latitude = "0";
                                    WorkerWorkDetailPage.longitude = "0";
                                }

                                if (result.drivers != null)
                                {
                                    if (result.drivers.Count > 0)
                                    {
                                        foreach (var item in result.drivers)
                                        {
                                            item.Image = string.IsNullOrEmpty(item.Image) ? "user.png" : item.Image;
                                        }
                                        DriversList = result.drivers;
                                        driversList = DriversList;
                                    }
                                }

                                WorkerWorkDetailPage.address = result.workDetailData.Location.ToString();
                                MessagingCenter.Send<WorkerWorkDetailViewModel>(this, "Hi");
                            }
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
        void showRating(decimal rating)
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
        public Command ViewImageCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var data = (OrderMedia)e;
                    _navigation.PushAsync(new ZoomImagePage(data.MediaName));
                });
            }
        }

        public Command showDriversLists
        {
            get
            {
                return new Command((e) =>
                {
                    if (driversList.Count > 0)
                    {
                        _navigation.PushPopupAsync(new OrderDriversListPopup(driversList));
                    }
                });
            }
        }

        public Command imgSharingCommand
        {
            get
            {
                return new Command((e) =>
                {
                    string applInk = string.Empty;
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        applInk = "https://play.google.com/store/apps/details?id=com.orem.Worker_7ERFAcraft";
                    }
                    else
                    {
                        applInk = "https://itunes.apple.com/us/app/%D8%AA%D8%B7%D8%A8%D9%8A%D9%82-%D9%85%D9%88%D8%AC%D9%88%D8%AF/id1462877713?ls=1&mt=8";
                    }
                    string msg = string.Empty;
                    var lng = App.Database.GetLng();
                    if (lng != null && !string.IsNullOrEmpty(lng.Language))
                    {
                        if (lng.Language != Models.CultureLanguage.English)
                        {
                            msg = "لقد عملت لدي تطبيق موجود عدة مرات وحصلت على معدل مرتفع ونفذت العديد من الوظائف مع عمولات صغيرة جدًا جرب معنا إذا كنت ترغب في تحسين عملك" +
                    " " + Common.appName + System.Environment.NewLine + applInk;
                        }
                        else
                        {
                            msg = "I worked for the many times and got high rate and made many jobs with very small commesions if you want to improve your work try " +
                          Common.appName + System.Environment.NewLine + applInk;

                        }
                    }
                    else
                    {
                        msg = "I worked for the many times and got high rate and made many jobs with very small commesions if you want to improve your work try " +
                   Common.appName + System.Environment.NewLine + applInk;
                    }

                    DependencyService.Get<IShare>().Share(msg);
                });
            }
        }
        public Command completeOrderCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var answer = await App.Current.MainPage.DisplayAlert("", AppResources.CompleteThisOrder,
                        AppResources.Yes, AppResources.No);
                    if (answer)
                    {
                        acceptOrder(UpdateOrderStatus.Complete);
                    }
                });
            }
        }
        public Command rejectOrderCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var answer = await App.Current.MainPage.DisplayAlert("", AppResources.RejectThisOrder,
                        AppResources.Yes, AppResources.No);
                    if (answer)
                    {
                        acceptOrder(UpdateOrderStatus.Reject);
                    }
                });
            }
        }
        public async void acceptOrder(UpdateOrderStatus status)
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
                    if (status == UpdateOrderStatus.Complete)
                    {
                        Url = ApiUrl.CompleteOrderUrl;
                    }
                    else if (status == UpdateOrderStatus.Accept)
                    {
                        Url = ApiUrl.AcceptOrderUrl;
                    }
                    else
                    {
                        Url = ApiUrl.RejectOrderUrl;
                    }
                    Url += _orderId;
                    var result = await cbase.AcceptRejectOrder(Url);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                        if (result.status)
                        {
                            if (status == UpdateOrderStatus.Accept)
                            {
                                await App.Current.MainPage.Navigation.PushPopupAsync(new WorkerAcceptPopup(Convert.ToInt32(_orderId)));
                                await _navigation.PopAsync();
                            }
                            //if (status == UpdateOrderStatus.Complete)
                            //{
                            //    OrderFeedbackPopup popup = new OrderFeedbackPopup(_orderId);
                            //    popup.Disappearing += Popup_Disappearing;
                            //    await _navigation.PushPopupAsync(popup);
                            //}

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

        private async void Popup_Disappearing(object sender, EventArgs e)
        {
            ShareOrderFeedbackPopup sPopup = new ShareOrderFeedbackPopup();
            await _navigation.PushPopupAsync(sPopup);
        }

        public Command acceptOrderCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var answer = await App.Current.MainPage.DisplayAlert("", AppResources.AcceptThisOrder
                        , AppResources.Yes, AppResources.No);
                    if (answer)
                    {
                        acceptOrder(UpdateOrderStatus.Accept);
                    }

                });
            }
        }

        public Command imgCallCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var phone = objData.PhoneNumber;
                    Device.OpenUri(new Uri(String.Format("tel:{0}", phone)));
                });
            }
        }

    }
}

