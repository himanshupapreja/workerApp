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
using System.Net.Http;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;  
        public event PropertyChangedEventHandler PropertyChanged;
        postSignUpData _postData = null;

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

                if (string.IsNullOrEmpty(SearchText))
                {
                    CategoryList = lstCategories;
                }
                else
                {
                    var newList = lstCategories.Where(x => x.CategoryName.ToLower().Contains(SearchText)).ToList();
                    CategoryList = newList; 
                } 
                RaisePropertyChanged(nameof(CategoryList));
              
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
        byte[] _imageProfile;
        byte[] _imageResidency;
        Lng objLng;
        public CategoryViewModel(INavigation navigation, postSignUpData postData, byte[] imageProfile, 
            byte[] imageResidency)
        {
            _postData = postData;
            _navigation = navigation;
            _imageProfile = imageProfile;
            _imageResidency = imageResidency;
            objLng = App.Database.GetLng();
            Device.BeginInvokeOnMainThread(() => {
                getCategories();
            });
        }
        public async void getCategories()
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
                    var result = await cbase.GetCategories(ApiUrl.GetCategories);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
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
                                    BGOpacity = 0.5,
                                    Image = "ic_selected_tile_new.png",
                                });
                            }
                            lstCategories = _categoryList;
                            CategoryList = _categoryList;
                            RaisePropertyChanged(nameof(CategoryList));
                        }

                       
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

        public Command btnSelectCommond
        {
            get
            {
                return new Command((e) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var data = (CategoryModel)e;
                        var index = CategoryList.FindIndex(x => x.CategoryId ==
                          data.CategoryId);
                        if(data.isSelected)
                        {
                            CategoryList[index].Image = "ic_selected_tile_new.png"; //"ic_selected_tile.png";
                            CategoryList[index].SelectedImage = "";
                            CategoryList[index].isSelected = false;
                            CategoryList[index].HasShadow = false;
                            CategoryList[index].BGOpacity = 0.5;
                        }
                        else
                        {
                            CategoryList[index].Image =  "ic_selected_tile.png";
                            CategoryList[index].SelectedImage = "select_service.png";
                            CategoryList[index].isSelected = true;
                            CategoryList[index].HasShadow = true;
                            CategoryList[index].BGOpacity = 1;
                        }
                       
                    });
                });
            }
        }


      
        public Command doneBtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var selectedCategories = CategoryList.Where(x => x.isSelected).ToList();
                    if(selectedCategories.Count==0)
                    {
                        await _navigation.PushPopupAsync(new ShowMessage(AppResources.SelectatleastoneCategory));
                        await Task.Delay(1000);
                        await _navigation.PopPopupAsync();
                        return;
                    }
                    else
                    {
                        List<UserCategory> lstUserCategories = new List<UserCategory>();
                        foreach (var item in selectedCategories)
                        {
                            lstUserCategories.Add(new UserCategory()
                            {
                                CategoryId=item.CategoryId
                            });
                        }
                        _postData.UserCategories = lstUserCategories;

                    }
                       await Register();

                });
            }
        }
      
        public async Task Register()
        {
            var DeviceId = Device.RuntimePlatform == Device.Android ? 1 : 2;
            try
            {
                try
                {
                    string boundary = "---8d0f01e6b3b5dafaaadaad";
                    MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);

                    multipartContent.Add(new StringContent(_postData.Name), "Name");
                    multipartContent.Add(new StringContent(!string.IsNullOrEmpty(_postData.UserName) ? _postData.UserName : "" ), "UserName");
                    multipartContent.Add(new StringContent(_postData.PhoneNumber), "PhoneNumber");
                    multipartContent.Add(new StringContent(_postData.Password), "Password");
                    multipartContent.Add(new StringContent(!string.IsNullOrEmpty(_postData.Location)?_postData.Location:""), "Location");
                    multipartContent.Add(new StringContent(_postData.Latitude), "Latitude");
                    multipartContent.Add(new StringContent(_postData.Longitude), "Longitude");
                    multipartContent.Add(new StringContent(!string.IsNullOrEmpty(_postData.ResidencyNumber) ?_postData.ResidencyNumber:""), "ResidencyNumber");
                    multipartContent.Add(new StringContent(_postData.CountryId.ToString()), "CountryId");
                    multipartContent.Add(new StringContent(_postData.CountryCode.ToString()), "CountryCode");
                    multipartContent.Add(new StringContent((Convert.ToInt32(UserType.Worker)).ToString()), "UserType");
                    multipartContent.Add(new StringContent(DeviceId.ToString()), "DeviceId");
                    multipartContent.Add(new StringContent(App.DeviceToken), "DeviceToken");
                    multipartContent.Add(new StringContent(Convert.ToInt32((PaymentType.SevenDays)).ToString()), "AccountType");
                    
                    for (int i = 0; i < _postData.UserCategories.Count; i++)
                    {
                        multipartContent.Add(new StringContent(_postData.UserCategories[i].CategoryId.ToString()), "UserCategories["+i+"]CategoryId");
                    }
                    try
                    {
                        if (_imageProfile != null)
                        {
                            string Name = string.Empty;
                            string FileName = string.Empty;

                            FileName = "UserImage.jpeg";
                            Name = "UserImage";


                            var fileContent = new ByteArrayContent(_imageProfile);

                            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                            {
                                Name = Name,
                                FileName = FileName,
                            };
                            multipartContent.Add(fileContent);
                        }
                        if (_imageResidency != null)
                        {
                            string Name = string.Empty;
                            string FileName = string.Empty;

                            FileName = "ResidencyImage.jpeg";
                            Name = "ResidencyImage";


                            var fileContent = new ByteArrayContent(_imageResidency);

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
                    string url = ApiUrl.SignUpUrl;
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
                                if (result.status)
                                {
                                    Loader.CloseAllPopup();
                                    string displayMsg = result.otpResponse == 1 ? AppResources.OtpSuccessResponse : AppResources.OtpFailureResponse;
                                    await App.Current.MainPage.DisplayAlert(result.otpResponse.ToString(), displayMsg, AppResources.Ok);

                                    if (result.otpResponse == 1)
                                    {
                                        _navigation.PushAsync(new OtpPage(result.userData.UserId.ToString())); 
                                    }
                                   
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

