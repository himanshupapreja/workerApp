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
using System.Net.Http;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class AddBalanceViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;
        
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


    public AddBalanceViewModel(INavigation navigation)
        { 
            _navigation = navigation; 
        }
     

        

        
        byte[] _imagefile;
        public Command ImageReceiptCommand
        {
            get
            {
                return new Command(async (e) =>
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
                            ImageReceipt = ImageSource.FromStream(() => new MemoryStream(_imagefile));

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
                                ImageReceipt = ImageSource.FromStream(() => new MemoryStream(_imagefile));
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
        /// <summary>
        /// This event will be used for update payment
        /// </summary>
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

                            await updateData();
                        }
                    }

                });
            }
        }


        public async Task updateData()
        {
            try
            {
                string boundary = "---8d0f01e6b3b5dafaaadaad";
                MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);

                multipartContent.Add(new StringContent(LoginUserDetails.userId.ToString()), "WorkerId");
                multipartContent.Add(new StringContent(Balance), "Bill");
                multipartContent.Add(new StringContent(!string.IsNullOrEmpty( ReceiptNumber)?ReceiptNumber:""), "ReceiptNumber");
                try
                {
                    if (_imagefile != null)
                    {
                        string Name = string.Empty;
                        string FileName = string.Empty;

                        FileName = "ReceiptImage.jpeg";
                        Name = "ReceiptImage";


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

                string url = ApiUrl.AddBalanceUrl;
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
                                AddBalancePopup.CloseAllPopup();
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


        public string CheckValidations()
        {
            string msg = string.Empty; 
                if (string.IsNullOrEmpty(Balance))
                {
                    msg += AppResources.PleaseEnterBalance + Environment.NewLine;
                }
                //if (_imagefile == null && string.IsNullOrEmpty(ReceiptNumber))
                //{
                //    msg += AppResources.PleaseEnterReceiptNumberorChooseReceiptImage + Environment.NewLine;
                //}
            if (_imagefile == null )
            {
                msg += AppResources.ChooseReceiptImage + Environment.NewLine;
            }
            return msg;
        }
    }
}

