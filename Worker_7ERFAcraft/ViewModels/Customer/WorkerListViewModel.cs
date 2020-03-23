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
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Worker_7ERFAcraft.ViewModels
{
    public class WorkerListViewModel : INotifyPropertyChanged
    {
       public static WorkerListViewModel model;
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

        private bool _mainVisible=true;
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

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;

            }
            set
            {
                _searchText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SearchText"));
                try
                {

                    if (string.IsNullOrEmpty(SearchText))
                    {
                        var _list = new ObservableCollection<WorkerModel>();
                        var finalList = WorkerListPage.lstWorkers;
                        foreach (var item in finalList)
                        {
                            _list.Add(new WorkerModel
                            {
                                Image = item.Image == null ? "user.png" : item.Image,
                                Name = item.Name,
                                RegNo = item.RegistrationNumber,
                                Rating = item.Rating == null ? 0 : item.Rating,
                                UserId = item.UserId.ToString(),
                                TotalReviews = item.TotalReviews == null || item.TotalReviews == "0" ? ""
                                : "(" + item.TotalReviews + ")",
                                UserCategories = item.UserCategories
                            });
                        }
                        WorkerList = _list; ;
                    }
                    else
                    {
                        var _list = new ObservableCollection<WorkerModel>();
                        var finalList = WorkerListPage.lstWorkers.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())
                        || x.RegistrationNumber.ToLower().Contains(SearchText.ToLower())).ToList();
                        foreach (var item in finalList)
                        {
                            _list.Add(new WorkerModel
                            {
                                Image = item.Image == null ? "user.png" : item.Image,
                                Name = item.Name,
                                RegNo = item.RegistrationNumber,
                                Rating = item.Rating == null ? 0 : item.Rating,
                                UserId = item.UserId.ToString(),
                                TotalReviews = item.TotalReviews == null || item.TotalReviews == "0" ? ""
                                : "(" + item.TotalReviews + ")",
                                UserCategories = item.UserCategories
                            });
                        }
                        WorkerList = _list; ;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        WorkerModel _selectedWorkerItem;
        public WorkerModel SelectedWorkerItem
        {
            get
            {
                return _selectedWorkerItem;
            }
            set
            {
                if (value != null)
                {
                    _selectedWorkerItem = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedWorkerItem"));
                    if (_selectedWorkerItem != null)
                    {
                    //    _navigation.PushAsync(new AddWorkDetailsPage(_selectedWorkerItem.UserId.ToString(), _categoryId));
                    }
                    _selectedWorkerItem = null;
                }

            }
        }

        ObservableCollection<WorkerModel> _list;

        private ObservableCollection<WorkerModel> _workerList;
        public ObservableCollection<WorkerModel> WorkerList
        {
            get
            {
                return _workerList;

            }
            set
            {
                _workerList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkerList"));
            }
        }

        private WorkerModel _workerListSelected;

        public WorkerModel WorkerListSelected
        {
            get { return _workerListSelected; }
            set
            {
                if (_workerListSelected != null)
                {
                    _workerListSelected = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkerListSelected"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        #region Location Search
        private string _location;//="Current Location";
        public string oldLocation;//="Current Location";
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (oldLocation != _location || string.IsNullOrEmpty(_location))
                {
                    _location = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Location"));
                    textChanged();
                }
                else
                {
                    oldLocation = string.Empty;
                }
            }
        }
        bool isLocationAssigned = false;
        void textChanged()
        {
            if (!isLocationAssigned)
            {
                if (Location != string.Empty)
                {
                    GetPlaces(Location);
                }
                else
                {
                    LocationList = null;
                    GridLocHeight = 0;
                }
            }
            else
            {
                LocationList = null;
                GridLocHeight = 0;
                isLocationAssigned = false;
            }
        }
        public async void GetPlaces(string searchText)
        {
            try
            {
                string url = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=" + searchText +
                "&types=geocode&key=" + Common.GoogleMapPlacesKey;


                HttpClientBase cbase = new HttpClientBase();
                wsGooglePlaces data = await cbase.getGooglePlaces(url);
                if (data != null && data.predictions.Count > 0)
                {
                    LocationList = null;
                    var locationList = new
                        ObservableCollection<Prediction>();
                    foreach (var item in data.predictions)
                    {
                        locationList.Add(new Prediction()
                        {
                            description = item.description,
                            id = item.id,
                            place_id = item.place_id,
                            reference = item.reference,
                        });
                    }
                    LocationList = locationList;
                    RaisePropertyChanged(nameof(LocationList));
                    GridLocHeight = data.predictions.Count * 50;
                }

            }
            catch
            {
            }
            finally
            {
            }
        }
        public ObservableCollection<Prediction> LocationList { get; set; }
        private Prediction locSelectedItem;
        public Prediction LocSelectedItem
        {
            get { return locSelectedItem; }
            set
            {
                if (locSelectedItem != value)
                {
                    locSelectedItem = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("LocSelectedItem"));
                    isLocationAssigned = true;
                    Location = locSelectedItem.description;

                    oldLocation = Location;
                    App.IsSearch = true;
                    getLatLng();

                    LocationList = null;
                    RaisePropertyChanged(nameof(LocationList));
                    GridLocHeight = 0;
                }
            }
        }

        async void getLatLng()
        {
            Geocoder gc = new Geocoder();
            IEnumerable<Position> possibleAddresses =
                await gc.GetPositionsForAddressAsync(Location);

            foreach (var result in possibleAddresses)
            {
                App.oldPos = new Position(result.Latitude, result.Longitude);
                getWorkerList();
                //WorkerListPage.latitude = App.oldPos.Latitude.ToString();
                //WorkerListPage.longitude = App.oldPos.Longitude.ToString();
                //MessagingCenter.Send<WorkerListViewModel>(this, "UpdateMapLocationOnList");
                return;
            }
        }
        private int gridLocHeight;
        public int GridLocHeight
        {
            get
            {
                return gridLocHeight;
            }
            set
            {
                gridLocHeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("GridLocHeight"));
            }
        }
        #endregion
        int _categoryId;
        public WorkerListViewModel(INavigation navigation,int categoryId)
        { 
            _navigation = navigation;
            
            App.oldPos = new Position(Convert.ToDouble(App.latitude), Convert.ToDouble(App.longitude));
            _categoryId = categoryId;
            //Device.BeginInvokeOnMainThread(() =>
            //{
                getWorkerList();
            //});
        }

        public async void getWorkerList(bool isPullToRefresh = false)
        {
            if (!Common.CheckConnection())
            {
                await _navigation.PushPopupAsync(new NoInternetPopup());
                return;
                // InternetVisible = true;
                // MainVisible = false;
                //NoDataVisible = false;
            }
            else
            {
                 
                try
                { 
                    if (!isPullToRefresh)
                    {
                         await _navigation.PushPopupAsync(new Loader());
                        //_isLoading = true;
                        //try { IsLoading = true; } catch { }
                    }
                    WorkerList = new ObservableCollection<WorkerModel>();
                     HttpClientBase cbase = new HttpClientBase();

                    //string Url = ApiUrl.GetWorkersByCatId + "?categoryId="+_categoryId+ "&curLatitude=" + App.latitude + "&curLongitude=" + App.longitude +
                    //    "&newLatitude=" + App.oldPos.Latitude + "&newLongitude=" + App.oldPos.Longitude;

                    string Url = ApiUrl.GetWorkersByCatId + "?categoryId=" + _categoryId + "&curLatitude=" + App.oldPos.Latitude + "&curLongitude=" + App.oldPos.Longitude +
                       "&newLatitude=" + App.oldPos.Latitude + "&newLongitude=" + App.oldPos.Longitude;


                    var result = await cbase.GetWorkersByCatId(Url);
                    if (result != null)
                    {
                        if (result.status)
                        {
                            //InternetVisible = false;
                            //MainVisible = true;
                            //NoDataVisible = false;

                            _list = new ObservableCollection<WorkerModel>();
                            var finalList = result.categoryWorkersData; 
                            foreach (var item in finalList)
                            {
                                _list.Add(new WorkerModel
                                {
                                    Image = item.Image == null ? "user.png" : item.Image,
                                    Name =Common.FirstCharToUpper( item.Name),
                                    RegNo = item.RegistrationNumber,
                                    Rating = item.Rating == null ? 0 : item.Rating,
                                    UserId = item.UserId.ToString(),
                                    TotalReviews = item.TotalReviews == null || item.TotalReviews == "0" ? ""
                                    : "(" + item.TotalReviews + ")",
                                    UserCategories = item.UserCategories
                                });
                            }
                            WorkerList = _list;
                            WorkerListPage.lstWorkers = finalList;
                            WorkerListPage.latitude = App.oldPos.Latitude.ToString();
                            WorkerListPage.longitude = App.oldPos.Longitude.ToString();
                            MessagingCenter.Send<WorkerListViewModel>(this, "ShowWorkersLocationOnList");
                            if (!isPullToRefresh)
                            {
                                Loader.CloseAllPopup();
                                //_isLoading = false ;
                                //IsLoading = false ;
                            }
                        }
                        else
                        {
                            if (!isPullToRefresh)
                            {
                                Loader.CloseAllPopup();
                                //_isLoading = false ;
                                //IsLoading = false ;
                            }
                           //InternetVisible = false;
                           //MainVisible = true ;
                           //NoDataVisible = false ;

                        }
                    }
                    else
                    {
                        if (!isPullToRefresh)
                        {
                            Loader.CloseAllPopup();
                            //_isLoading = false ;
                            //IsLoading = false ;
                        }
                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, "OK");
                    }
                }
                catch (Exception ex)
                { 
                }
            }
        }
        

       public Command workerProfileCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var data = (WorkerModel)e;
                    if (data != null)
                    {
                        _navigation.PushAsync(new WorkerFullProfilePage(data.UserId.ToString(),_categoryId)); 
                    }
                });
            }
        }
        public Command workerDetailCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var data = (WorkerModel)e;
                    if (data != null)
                    { 
                        if(LoginUserDetails.userId>0)
                        {
                            _navigation.PushAsync(new AddWorkDetailsPage(data, _categoryId));
                        }
                        else
                        {
                            _navigation.PushAsync(new LoginPage(true));
                        } 
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
                    getWorkerList(true);
                    IsRefreshingData = false;
                });
            }
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}

