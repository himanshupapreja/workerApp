﻿using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.Media;
using System.IO;
using System.Net.Http;
using Xamarin.Forms.Maps;
using System.Runtime.CompilerServices;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class CustomerProfileViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        #region Country 
        private ObservableCollection<string> _countryList;
        public ObservableCollection<string> CountryList
        {
            get
            {
                return _countryList;

            }
            set
            {
                _countryList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CountryList"));
            }
        }

        public int selectedCountry { get; set; }
        public List<Country> lstCountries { get; set; }

        string _selectedCountryItem;
        public string SelectedCountryItem
        {
            get
            {
                return _selectedCountryItem;
            }
            set
            {
                if (value != null)
                {
                    _selectedCountryItem = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCountryItem"));
                    if (_selectedCountryItem != null)
                    {
                        if (lstCountries != null && lstCountries.Count > 0)
                        {
                            selectedCountry = lstCountries.Where(x => x.CountryName == _selectedCountryItem)
                                .FirstOrDefault().CountryId;
                        }
                    }
                }
            }
        }
        #endregion
        private ImageSource _image;
        public ImageSource Image
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
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;

            }
            set
            {
                _userName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
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
        private string _location;
        public string oldLocation;
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
                    getLatLng();
                    LocationList = null;
                    RaisePropertyChanged(nameof(LocationList));
                    GridLocHeight = 0;
                }
            }
        }
        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        async void getLatLng()
        {
            Geocoder gc = new Geocoder();
            IEnumerable<Position> possibleAddresses =
                await gc.GetPositionsForAddressAsync(Location);

            foreach (var result in possibleAddresses)
            {
                Common.signUpUserPosition = new Plugin.Geolocator.Abstractions.Position()
                {
                    Latitude = result.Latitude,
                    Longitude = result.Longitude
                };
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
        public event PropertyChangedEventHandler PropertyChanged; 
        public CustomerProfileViewModel(INavigation navigation)
        { 
            _navigation = navigation;
            getCountries();
            getProfileData();
        }

        public async void getCountries()
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
                    var result = await cbase.GetCountries(ApiUrl.GetCountries);
                    if (result != null)
                    {
                        if (result.status)
                        {
                            lstCountries = result.countryData;
                        }

                        var _countryList = new ObservableCollection<string>();
                        foreach (var item in lstCountries)
                        {
                            _countryList.Add(item.CountryName);
                        }
                        CountryList = _countryList;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        public async void getProfileData()
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
                    var result = await cbase.GetUserProfile(ApiUrl.GetUserProfile+LoginUserDetails.userId);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        if (result.status)
                        {
                            Image = string.IsNullOrEmpty(result.userProfileData.Image) ? "user.png" : result.userProfileData.Image;
                            UserName = result.userProfileData.UserName;
                            RegNo = result.userProfileData.RegistrationNumber;
                            FullName = result.userProfileData.Name;
                            isLocationAssigned = true;
                            oldLocation = result.userProfileData.Location;
                            Location = result.userProfileData.Location;
                            PhoneNumber = result.userProfileData.PhoneNumber;
                            SelectedCountryItem =lstCountries.Where(x=>x.CountryId== result.userProfileData.CountryId)
                                .FirstOrDefault().CountryName;
                        }
                    }
                    else
                    {
                        Loader.CloseAllPopup();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public Command ShowMapCommand
        {
            get
            {
                return new Command((e) =>
                {
                    MapLocationPage mapPage = new MapLocationPage();
                    mapPage.Disappearing += MapPage_Disappearing;
                    _navigation.PushAsync(mapPage);
                });
            }
        }

        private void MapPage_Disappearing(object sender, EventArgs e)
        {
            isLocationAssigned = true;
            oldLocation = Common.signUpUserAddress;
            Location = Common.signUpUserAddress;
        }
        byte[] _imagefile;
        public Command imgProfileCommand
        {
            get
            {
                return new Command(async(e) =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.ChoosePhoto, AppResources.Cancel, null,
                             AppResources.TakePhoto, AppResources.ChoosefromGallery);
                        string _selectedColor = string.Empty;
                        if (action == AppResources.TakePhoto)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            if (Device.RuntimePlatform == "iOS")
                            {
                                await Task.Delay(1000);
                            }
                            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg"
                            });
                            if (file == null)
                                return;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.GetStream().CopyTo(memoryStream);
                                var myfile = memoryStream.ToArray();
                                _imagefile = myfile;
                                file.Dispose();
                            }
                            Image = ImageSource.FromStream(() => new MemoryStream(_imagefile));

                        }
                        else if (action == AppResources.ChoosefromGallery)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file != null)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    var myfile = memoryStream.ToArray();
                                    _imagefile = myfile;
                                    file.Dispose();
                                }
                                Image = ImageSource.FromStream(() => new MemoryStream(_imagefile));
                            }
                        } 
                        else
                        {
                            return;
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }
        
        public Command changePasswordbtnCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new ChangePasswordPage());
                });
            }
        }
        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                msg += AppResources.PleaseEnterUsername + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(FullName))
            {
                msg += AppResources.PleaseEnterName + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                msg += AppResources.PleaseEnterPhoneNumber + Environment.NewLine;
            }
            if (selectedCountry == 0)
            {
                msg += AppResources.SelectCountry + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(Location))
            {
                msg += AppResources.PleaseEnterLocation + Environment.NewLine;
            }
            return msg;
        }
        public Command updatebtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var returnMessage = CheckValidations();
                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                        await _navigation.PushPopupAsync(new ShowMessage(returnMessage));
                        await Task.Delay(1000);
                        await _navigation.PopPopupAsync();
                        return;
                    }
                    else
                    {
                        if (!Common.CheckConnection())
                        {
                            await _navigation.PushPopupAsync(new NoInternetPopup());
                            return;
                        }
                        else
                        {
                            await UpdateProfile();
                        }
                    }

                });
            }
        }

        public async Task UpdateProfile()
        {
            try
            {
                try
                {
                    string boundary = "---8d0f01e6b3b5dafaaadaad";
                    MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
                    var pos = Common.signUpUserPosition;
                    var Latitude = pos != null && pos.Latitude != 0 ? pos.Latitude.ToString() : App.latitude;
                    var Longitude = pos != null && pos.Longitude != 0 ? pos.Longitude.ToString() : App.longitude;

                    multipartContent.Add(new StringContent(LoginUserDetails.userId.ToString()), "UserId");
                    multipartContent.Add(new StringContent(FullName), "Name");
                    multipartContent.Add(new StringContent(UserName), "UserName");
                    multipartContent.Add(new StringContent(PhoneNumber), "PhoneNumber");
                    multipartContent.Add(new StringContent(Location), "Location"); 
                    multipartContent.Add(new StringContent(selectedCountry.ToString()), "CountryId");
                    multipartContent.Add(new StringContent(Latitude), "Latitude");
                    multipartContent.Add(new StringContent(Longitude), "Longitude");
                    multipartContent.Add(new StringContent((Convert.ToInt32(UserType.Customer)).ToString()), "UserType");
                    try
                    {
                        if (_imagefile != null)
                        {
                            string Name = string.Empty;
                            string FileName = string.Empty;

                            FileName = "UserImage.jpeg";
                            Name = "UserImage";


                            var fileContent = new ByteArrayContent(_imagefile);

                            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                            {
                                Name = Name,
                                FileName = FileName,
                            };
                            multipartContent.Add(fileContent);
                        }
                    }
                    catch { }
                    HttpClient httpClient = new HttpClient();


                    await _navigation.PushPopupAsync(new Loader());

                    string latitude = string.Empty, longitude = string.Empty;
                    string url = ApiUrl.EditProfile;
                    HttpResponseMessage response = await httpClient.PostAsync(url, multipartContent);
                    string content1 = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (content != null)
                        {
                            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<wsUser>(content);

                            if (result != null)
                            {
                                Loader.CloseAllPopup();
                                await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                                if (result.status)
                                {
                                    var objDbUser = App.Database.GetLoggedInUser();
                                    objDbUser.name = FullName;
                                    LoginUserDetails.name = FullName;
                                    if (_imagefile != null)
                                    {
                                        objDbUser.image = result.userData.Image;
                                        LoginUserDetails.image = result.userData.Image;
                                    }
                                    App.Database.SaveUpdateLoggedInUser(objDbUser);
                                    MessagingCenter.Send<CustomerProfileViewModel>(this, "Hi");
                                }
                            }
                            else
                            {
                                Loader.CloseAllPopup();
                                await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                            }
                        }
                        else
                        {
                            Loader.CloseAllPopup();
                            await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
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
                    if (Loader.isLoading)
                    {
                        Loader.CloseAllPopup();
                    }
                    await App.Current.MainPage.DisplayAlert("", ex.Message, AppResources.Ok);
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, AppResources.Ok);
            }
        }

    }
}

