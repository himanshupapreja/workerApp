using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Linq;
using Xamarin.Forms.Maps;

namespace Worker_7ERFAcraft.ViewModels
{
    public class CustomerHomeViewModel : INotifyPropertyChanged
    {
        public static CustomerHomeViewModel model;
        
        static Color selectedColor = Color.FromHex("#fdc99b");
        static Color deselectedColor = (Color)App.Current.Resources["MainColor"];// Color.FromHex("#d2d2d2");
        HomeScreenType selectedType = HomeScreenType.List;
        #region textColor
        private Color _mapTextColor = deselectedColor;
        public Color MapTextColor
        {
            get
            {
                return _mapTextColor;
            }
            set
            {
                _mapTextColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MapTextColor"));
            }
        }
        private Color _listTextColor = selectedColor;
        public Color ListTextColor
        {
            get
            {
                return _listTextColor;
            }
            set
            {
                _listTextColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ListTextColor"));
            }
        }
        
        #endregion

        #region boxBgColor
        private Color _mapBoxBgColor = deselectedColor;
        public Color MapBoxBgColor
        {
            get
            {
                return _mapBoxBgColor;
            }
            set
            {
                _mapBoxBgColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MapBoxBgColor"));
            }
        }
        private Color _listBoxBgColor = selectedColor;
        public Color ListBoxBgColor
        {
            get
            {
                return _listBoxBgColor;
            }
            set
            {
                _listBoxBgColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ListBoxBgColor"));
            }
        }
        private bool _isMap = false;
        public bool IsMap
        {
            get
            {
                return _isMap;
            }
            set
            {
                _isMap = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsMap"));
            }
        }
        private bool _isList=true;
        public bool IsList
        {
            get
            {
                return _isList;
            }
            set
            {
                _isList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsList"));
            }
        }

        
        #endregion
        public INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;
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
        List<CategoryModel> lstCategories;


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
                if (selectedType == HomeScreenType.List)
                {
                    if (string.IsNullOrEmpty(SearchText))
                    {

                        //getCategories();
                        CategoryList = lstCategories;

                    }
                    else
                    {
                        try
                        {
                            var newList = lstCategories.Where(x => x.CategoryName.ToLower().Contains(SearchText)).ToList();
                            CategoryList = newList;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }
        private List<CategoryModel> _categoryList;
        public List<CategoryModel> CategoryList
        {
            get
            {
                return _categoryList;

            }
            set
            {
                _categoryList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CategoryList"));
            }
        }
        CategoryModel _catSelectedItem;
        public CategoryModel CatSelectedItem
        {
            get
            {
                return _catSelectedItem;
            }
            set
            {
                _catSelectedItem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CatSelectedItem"));
            }
        }

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
                "&types=geocode&key="+Common.GoogleMapPlacesKey;

                
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
                CustomerHomePage.latitude = result.Latitude.ToString();
                CustomerHomePage.longitude = result.Longitude.ToString();
                MessagingCenter.Send<CustomerHomeViewModel>(this, "UpdateMapLocation");
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
        Lng objLng;
        public CustomerHomeViewModel(INavigation navigation)
        { 
            _navigation = navigation;
            objLng = App.Database.GetLng();
            App.oldPos = new Position(Convert.ToDouble(App.latitude), Convert.ToDouble(App.longitude));
            Device.BeginInvokeOnMainThread(() => {
                getCategories();
               
            }); 
        }
        public  async void getCategories(string searchText="")
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
                    CategoryList = new List<CategoryModel>();
                       _isLoading = true;
                    IsLoading = true;
                    var cbase = new HttpClientBase();
                    string url = string.Empty;
                    if(!string.IsNullOrEmpty(searchText))
                    {
                        url = ApiUrl.SearchCategoryByUserId + searchText+"&";
                    }
                    else
                    {
                        url = ApiUrl.GetCategoriesByUserId;
                    }
                   // await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                    var result = await cbase.GetCategories(url+ "curLatitude=" + App.latitude+ "&curLongitude=" + App.longitude+
                        "&newLatitude=" + App.oldPos.Latitude + "&newLongitude=" + App.oldPos.Longitude );
                    if (result != null)
                    {
                        _isLoading = false;
                        IsLoading = false;
                        //Loader.CloseAllPopup();
                        if (result.status)
                        {

                            var _categoryList = new List<CategoryModel>();
                            foreach (var item in result.categoryData)
                            {
                                _categoryList.Add(new CategoryModel()
                                {
                                    CategoryImage = item.Image,
                                    CategoryName = objLng.Language == (CultureLanguage.Arabic) ? item.CategoryName_Arabic
                                    : objLng.Language == (CultureLanguage.Bangali) ? item.CategoryName_Bangali
                                    : objLng.Language == (CultureLanguage.English) ? item.CategoryName
                                    : objLng.Language == (CultureLanguage.Hindi) ? item.CategoryName_Hindi
                                    : objLng.Language == (CultureLanguage.Urdu) ? item.CategoryName_Urdu
                                    : item.CategoryName,
                                    HasShadow = false,
                                    CategoryId = item.CategoryId,
                                    SelectedImage = "",
                                    isSelected = false,
                                });
                            }
                            lstCategories = _categoryList;
                            CategoryList = _categoryList;
                            RaisePropertyChanged(nameof(CategoryList));
                            CustomerHomePage.lstWorkers = result.workersData;
                            CustomerHomePage.latitude = App.oldPos.Latitude.ToString();
                            CustomerHomePage.longitude = App.oldPos.Longitude.ToString();
                            MessagingCenter.Send<CustomerHomeViewModel>(this, "ShowWorkersLocation");
                            //IsList = false;
                            //IsMap = true;
                        }
                        else
                        {
                            CustomerHomePage.lstWorkers = new List<Models.Worker>(); ;
                            MessagingCenter.Send<CustomerHomeViewModel>(this, "ShowWorkersLocation");
                        } 
                    }
                    else
                    {
                        CustomerHomePage.lstWorkers = new List<Models.Worker>(); ;
                        MessagingCenter.Send<CustomerHomeViewModel>(this, "ShowWorkersLocation");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        public void CollectionDidChange(object sender,
                                NotifyCollectionChangedEventArgs e)
        {

        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Command MapCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (selectedType != HomeScreenType.Map)
                    {
                        MessagingCenter.Send<CustomerHomeViewModel>(this, "UpdateMapLocation");
                        MapTextColor = selectedColor;
                        MapBoxBgColor = selectedColor;

                        ListTextColor = deselectedColor;
                        ListBoxBgColor = deselectedColor;

                        IsList = false;
                        IsMap = true;
                        selectedType = HomeScreenType.Map;
                         
                    }


                });
            }
        }

        public Command ListCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (selectedType != HomeScreenType.List)
                    {
                        MapTextColor = deselectedColor;
                        MapBoxBgColor = deselectedColor;

                        ListTextColor =  selectedColor;
                        ListBoxBgColor =  selectedColor;
                        IsList = true;
                        IsMap = false;
                        selectedType = HomeScreenType.List;
                         
                    }
                });
            }
        }
        public Command SearchReturnCommand
        {
            get
            {
                return new Command((e) =>
                {
                    //if (!string.IsNullOrEmpty(SearchText))
                    //{
                    //    getCategories(SearchText);
                    //}

                    //RaisePropertyChanged(nameof(CategoryList));
                });
            }
        }
        public Command btnSelectCommond
        {
            get
            {
                return new Command((e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var data = (CategoryModel)e;
                        //var index = CategoryList.FindIndex(x => x.CategoryId ==
                        //  data.CategoryId);
                        _navigation.PushAsync(new WorkerListPage(data.CategoryId ));

                    });
                });
            }
        } 
    }
}

