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

namespace Worker_7ERFAcraft.ViewModels
{
    public class WorkDetailViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;

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
        private bool _isCancelButton = false;
        public bool IsCancelButton
        {
            get
            {
                return _isCancelButton;

            }
            set
            {
                _isCancelButton = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsCancelButton"));
            }
        }
        private bool _isRatingShown = false;
        public bool  IsRatingShown
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
        private string _workerImage;
        public string WorkerImage
        {
            get
            {
                return _workerImage;

            }
            set
            {
                _workerImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkerImage"));
            }
        }
        private string _statusText;
        public string StatusText
        {
            get
            {
                return _statusText;

            }
            set
            {
                _statusText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
            }
        }
        private Color _statusTextColor;
        public Color StatusTextColor
        {
            get
            {
                return _statusTextColor;

            }
            set
            {
                _statusTextColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StatusTextColor"));
            }
        }
        
        private string _workerRegNo;
        public string WorkerRegNo
        {
            get
            {
                return _workerRegNo;

            }
            set
            {
                _workerRegNo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkerRegNo"));
            }
        }
        private string _workerName;
        public string WorkerName
        {
            get
            {
                return _workerName;

            }
            set
            {
                _workerName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkerName"));
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
        private decimal _lstViewHeight=0;
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
        Order _order ;
        public event PropertyChangedEventHandler PropertyChanged; 
        public WorkDetailViewModel(INavigation navigation, Order order )
        { 
            _navigation = navigation;
            _order = order ;
            getOrderDetail();
        }
        WorkDetailData objData = null;
        int _orderStatus;

        
        public Command ViewImageCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var data = (OrderMedia)e;
                    _navigation.PushAsync(new ZoomImagePage( data.MediaName));
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
                            msg = "I used  استخدمت "+Common.appName+" تطبيق موجود app for searching for  ابحث في التطبيق عن"+_order.CategoryName+" طلبت في قسم   و لقد وجدت عاملا ممتاز and I find an excellent worker "+WorkerName+"  اسم العامل)(جرب التطبيق بنفسك  try the app yourself )" + 
                            System.Environment.NewLine + applInk;
                        }
                        else
                        {
                            msg = "I used " + Common.appName + " app for searching for " + _order.CategoryName + " and I find an excellent worker " +
                WorkerName + " try the app yourself " + System.Environment.NewLine + applInk;
                        }
                    }
                    else
                    {
                        msg = "I used " + Common.appName + " app for searching for " + _order.CategoryName + " and I find an excellent worker " +
                WorkerName + " try the app yourself " + System.Environment.NewLine + applInk;
                    }
                   
                    DependencyService.Get<IShare>().Share(msg);
                });
            }
        }
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
                       await _navigation.PushPopupAsync(new Loader());
                    HttpClientBase cbase = new HttpClientBase();
                    string Url   = ApiUrl.WorkDetailUrl+ _order.OrderId;  
                    var result = await cbase.GetOrderDetail(Url);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        if (result.status)
                        {
                            WorkerImage = string.IsNullOrEmpty(result.workDetailData.WorkerImage) ?"user.png"
                                : result.workDetailData.WorkerImage;
                            WorkerRegNo = result.workDetailData.RegistrationNumber;

                            WorkerName = Common.FirstCharToUpper(result.workDetailData.WorkerName);
                            StatusText = (int)OrderStatus.Pending == result.workDetailData.Status ? AppResources.Pending.ToUpper() :
                                    (int)OrderStatus.Canceled == result.workDetailData.Status ? AppResources.Canceled.ToUpper() :
                                    (int)OrderStatus.Completed == result.workDetailData.Status ? AppResources.Completed.ToUpper() :
                                    (int)OrderStatus.InProgress == result.workDetailData.Status ? AppResources.OnGoing.ToUpper() :"";

                            StatusTextColor = result.workDetailData.Status ==(int) OrderStatus.Completed?Color.FromHex("#74e5b5")
                                : result.workDetailData.Status == (int)OrderStatus.Canceled  ? Color.FromHex("#ff1313"): Color.FromHex("#000000"); 
                            Location = result.workDetailData.Location;
                            WorkNotes = result.workDetailData.WorkNote;
                            WorkDate = result.workDetailData.WorkDateFrom == result.workDetailData.WorkDateTo ?
                                result.workDetailData.WorkDateFrom.ToString("dd-MM-yyyy") : 
                                (result.workDetailData.WorkDateFrom.ToString("dd-MM-yyyy") + " - " + result.workDetailData.WorkDateTo.ToString("dd-MM-yyyy")) ;
                            WorkHours = result.workDetailData.WorkHoursFrom+"-"+ result.workDetailData.WorkHoursTo;
                            objData = result.workDetailData;
                            MediaList = result.workDetailData.OrderMedias;
                            Rating = result.workDetailData.Rating == null ? 0 : result.workDetailData.Rating;
                            showRating(result.workDetailData.Rating ?? 0);


                            TotalReviews = result.workDetailData.TotalReviews == null ||
                                result.workDetailData.TotalReviews == "0" ? "" : "(" + result.workDetailData.TotalReviews + ")";
                            LstViewHeight = result.workDetailData.OrderMedias != null ? result.workDetailData.OrderMedias.Count * 140:0;
                            _orderStatus = (int)result.workDetailData.Status;

                            if (result.workDetailData.Status == (int)OrderStatus.Completed)
                            {
                                IsRatingShown = true;
                                if (Rating == 0)
                                {
                                    RatePopup popup = new RatePopup(_order.OrderId.ToString());
                                    popup.Disappearing += Popup_Disappearing;
                                    await _navigation.PushPopupAsync(popup);
                                }
                            }
                            if (result.workDetailData.Status != (int)OrderStatus.Completed &&
                                result.workDetailData.Status != (int)OrderStatus.Canceled)
                            {
                                IsCancelButton = true;
                            }
                        }

                        if (result.workDetailData.Status == (int)OrderStatus.InProgress )
                        {
                            IsNotPending = true;
                            Location = result.workDetailData.WorkerLocation;
                            if (!string.IsNullOrEmpty(result.workDetailData.WorkerLatitude)
                                && !string.IsNullOrEmpty(result.workDetailData.WorkerLongitude))
                            {
                                WorkDetailPage.latitude = result.workDetailData.WorkerLatitude.ToString();
                                WorkDetailPage.longitude = result.workDetailData.WorkerLongitude.ToString();
                            }
                            else
                            {
                                WorkDetailPage.latitude = "0";
                                WorkDetailPage.longitude = "0";
                            }

                            WorkDetailPage.address = result.workDetailData.WorkerLocation.ToString();
                            MessagingCenter.Send<WorkDetailViewModel>(this, "Hi");
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
        private async void Popup_Disappearing(object sender, EventArgs e)
        {
            Rating = RatePopup.Rating;
            showRating(Rating??0);
            ShareOrderFeedbackPopup sPopup = new ShareOrderFeedbackPopup();
            await _navigation.PushPopupAsync(sPopup);
        }

 
        public Command cancelBtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {  
                        CancellationReasonsPopup cpopup = new CancellationReasonsPopup(_order.OrderId.ToString());
                        cpopup.Disappearing += cPopup_Disappearing;
                        await _navigation.PushPopupAsync(cpopup);
                    
                });
            }
        }

        private void cPopup_Disappearing(object sender, EventArgs e)
        {
            if (CancellationReasonsPopup.isCancelDone  )
            {
                StatusText = OrderStatus.Canceled.ToString(); 
                StatusTextColor= Color.FromHex("#ff1313");
                IsCancelButton = false;
            }
        }
        public Command RatingCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    if (_orderStatus == (int)OrderStatus.Completed && Rating==0)
                    {
                        RatePopup popup = new RatePopup(_order.OrderId.ToString());
                        popup.Disappearing += Popup_Disappearing;
                        await _navigation.PushPopupAsync(popup); 
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
                    var phone=objData.WorkerPhone;
                    Device.OpenUri(new Uri(String.Format("tel:{0}", phone)));
                });
            }
        } 

    }
}

