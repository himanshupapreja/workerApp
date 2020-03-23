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

namespace Worker_7ERFAcraft.ViewModels
{
    public class WorkerFullProfileViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        private string  _image;
        public string Image
        {
            get
            {
                return _image;

            }
            set
            {
                _image = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Image"));
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;

            }
            set
            {
                _name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
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

        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;

            }
            set
            {
                _phoneNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }

        private decimal? _rating=0;
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
        //UserCategories

        private ObservableCollection<UserCategory> _userCategories;
        public ObservableCollection<UserCategory> UserCategories
        {
            get
            {
                return _userCategories;

            }
            set
            {
                _userCategories = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserCategories"));
            }
        }
        ObservableCollection<UserReview> _list;

        private ObservableCollection<UserReview> _reviewList;
        public ObservableCollection<UserReview> ReviewList
        {
            get
            {
                return _reviewList;

            }
            set
            {
                _reviewList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ReviewList"));
            }
        }
        private decimal _lstViewCatHeight = 0;
        public decimal LstViewCatHeight
        {
            get
            {
                return _lstViewCatHeight;

            }
            set
            {
                _lstViewCatHeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LstViewCatHeight"));
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
        public event PropertyChangedEventHandler PropertyChanged;
        string _workerId;
        int _categoryId;
        public WorkerFullProfileViewModel(INavigation navigation,string workerId,int  categoryId)
        { 
            _navigation = navigation;
            _workerId = workerId;
            _categoryId = categoryId;

             getWorkerProfile();
        }

        public async void getWorkerProfile()
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

                    string Url = ApiUrl.GetWorkersProfile + "?workerId=" + _workerId;
                    var result = await cbase.GetWorkerProfile(Url);
                    if (result != null)
                    {
                        if (result.status)
                        {
                            Name = Common.FirstCharToUpper(result.workerProfileData.Name);
                            RegNo = result.workerProfileData.RegistrationNumber;
                            PhoneNumber = result.workerProfileData.PhoneNumber;
                            Location = result.workerProfileData.Location +", "+ result.workerProfileData.CountryName;
                            Image= result.workerProfileData.Image ==null ?"user.png": result.workerProfileData.Image ;
                            Rating = result.workerProfileData.Rating == null?0:result.workerProfileData.Rating;
                            showRating(result.workerProfileData.Rating??0);

                            TotalReviews = result.workerProfileData.TotalReviews == null ||
                                result.workerProfileData.TotalReviews =="0" ? "" :"("+ result.workerProfileData.TotalReviews+")";
                            LstViewHeight = result.workerProfileData.UserReviews!=null && result.workerProfileData.UserReviews.Count!=0 ?
                                result.workerProfileData.UserReviews.Count * 200:0;
                           if(result.workerProfileData.UserCategories != null && result.workerProfileData.UserCategories.Count != 0)
                            {
                                var count = result.workerProfileData.UserCategories.Count;
                                var newCount1 = count / 3;
                                var newCount2 = count % 3;
                                if(newCount2>0)
                                {
                                    newCount1++;
                                }
                                LstViewCatHeight = newCount1 * 150;
                            }

                            var objLng = App.Database.GetLng();
                            var _catList = new ObservableCollection<UserCategory>();
                            foreach (var item in result.workerProfileData.UserCategories)
                            {
                                _catList.Add(new UserCategory
                                {
                                     CategoryId=item.CategoryId,
                                    CategoryName = objLng.Language == (CultureLanguage.Arabic) ? item.CategoryName_Arabic
                                    : objLng.Language == (CultureLanguage.Bangali) ? item.CategoryName_Bangali
                                    : objLng.Language == (CultureLanguage.English) ? item.CategoryName
                                    : objLng.Language == (CultureLanguage.Hindi) ? item.CategoryName_Hindi
                                    : objLng.Language == (CultureLanguage.Urdu) ? item.CategoryName_Urdu
                                    : item.CategoryName,
                                    Image =item.Image,
                                     CategoryName_Arabic= item.CategoryName_Arabic,
                                    CategoryName_Bangali = item.CategoryName_Bangali,
                                    CategoryName_Hindi = item.CategoryName_Hindi,
                                    CategoryName_Urdu = item.CategoryName_Urdu,
                                });
                            }
                            UserCategories = _catList;

                            _list = new ObservableCollection<UserReview>();
                            foreach (var item in result.workerProfileData.UserReviews)
                            {
                                _list.Add(new UserReview
                                {
                                    CustomerImage = item.CustomerImage == null ? "user.png" : item.CustomerImage,
                                    CustomerName = Common.FirstCharToUpper(item.CustomerName),
                                    Rating = item.Rating == null ? 0 : item.Rating,
                                    CustomerId = item.CustomerId,
                                    Comment=item.Comment,
                                    ReviewDate=item.ReviewDate,
                                    ImgRate1= item.Rating >= 1 ?"ic_star_fill.png": "ic_star_fill_2.png",
                                    ImgRate2 = item.Rating >= 2? "ic_star_fill.png" : "ic_star_fill_2.png",
                                    ImgRate3 = item.Rating >= 3 ? "ic_star_fill.png" : "ic_star_fill_2.png",
                                    ImgRate4 = item.Rating >= 4 ? "ic_star_fill.png" : "ic_star_fill_2.png",
                                    ImgRate5 = item.Rating >= 5 ? "ic_star_fill.png" : "ic_star_fill_2.png"
                                });
                            }
                            ReviewList = _list;
                            Loader.CloseAllPopup();
                        }
                        else
                        { 
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
   
        public Command HireMeCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (LoginUserDetails.userId > 0)
                    {

                        WorkerModel m = new WorkerModel()
                        {
                            UserId = _workerId,
                            Name = Name,
                            RegNo = RegNo,
                            UserCategories = UserCategories.ToList(),
                            Rating = Rating,
                            Image = Image,
                        };
                        _navigation.PushAsync(new AddWorkDetailsPage(m, _categoryId));
                    }
                    else
                    {
                        _navigation.PushAsync(new LoginPage(true));
                    }
                });
            }
        }
    }
}

