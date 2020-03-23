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
using System.IO;
using Plugin.Media;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Linq;
using System.Net;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    internal class ProgressableStreamContent : HttpContent
    {

        /// <summary>
        /// Lets keep buffer of 20kb
        /// </summary>
        private const int defaultBufferSize = 5 * 4096;

        private HttpContent content;
        private int bufferSize;
        //private bool contentConsumed;
        private Action<long, long> progress;

        public ProgressableStreamContent(HttpContent content, Action<long, long> progress) : this(content, defaultBufferSize, progress) { }

        public ProgressableStreamContent(HttpContent content, int bufferSize, Action<long, long> progress)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }

            this.content = content;
            this.bufferSize = bufferSize;
            this.progress = progress;

            foreach (var h in content.Headers)
            {
                this.Headers.Add(h.Key, h.Value);
            }
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {

            return Task.Run(async () =>
            {
                var buffer = new Byte[this.bufferSize];
                long size;
                TryComputeLength(out size);
                var uploaded = 0;


                using (var sinput = await content.ReadAsStreamAsync())
                {
                    while (true)
                    {
                        var length = sinput.Read(buffer, 0, buffer.Length);
                        if (length <= 0) break;

                        //downloader.Uploaded = uploaded += length;
                        uploaded += length;
                        progress?.Invoke(uploaded, size);

                        //System.Diagnostics.Debug.WriteLine($"Bytes sent {uploaded} of {size}");

                        stream.Write(buffer, 0, length);
                        stream.Flush();
                    }
                }
                stream.Flush();
            });
        }

        protected override bool TryComputeLength(out long length)
        {
            length = content.Headers.ContentLength.GetValueOrDefault();
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                content.Dispose();
            }
            base.Dispose(disposing);
        }

    }
    public class Attachment
    {
        public string type { get; set; }
        public byte[] data { get; set; }
    }

    public class AddWorkDetailsViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;


        #region Service 
        private ObservableCollection<string> _servicesList;
        public ObservableCollection<string> ServicesList
        {
            get
            {
                return _servicesList;

            }
            set
            {
                _servicesList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ServicesList"));
            }
        }

        public int selectedService { get; set; }
        public List<UserCategory> lstServices { get; set; }

        string _selectedServiceItem;
        public string SelectedServiceItem
        {
            get
            {
                return _selectedServiceItem;
            }
            set
            {
                if (value != null)
                {
                    _selectedServiceItem = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedServiceItem"));
                    if (_selectedServiceItem != null)
                    {
                        if (lstServices != null && lstServices.Count > 0)
                        {
                            if (objLng.Language == (CultureLanguage.Arabic))
                            {
                                selectedService = lstServices.Where(x => x.CategoryName_Arabic == _selectedServiceItem)
                                    .FirstOrDefault().CategoryId;
                            }
                            else if (objLng.Language == (CultureLanguage.Bangali))
                            {
                                selectedService = lstServices.Where(x => x.CategoryName_Bangali == _selectedServiceItem)
                                    .FirstOrDefault().CategoryId;
                            }
                            else if (objLng.Language == (CultureLanguage.Hindi))
                            {
                                selectedService = lstServices.Where(x => x.CategoryName_Hindi == _selectedServiceItem)
                                    .FirstOrDefault().CategoryId;
                            }
                            else if (objLng.Language == (CultureLanguage.Urdu))
                            {
                                selectedService = lstServices.Where(x => x.CategoryName_Urdu == _selectedServiceItem)
                                    .FirstOrDefault().CategoryId;
                            }
                            else
                            {
                                selectedService = lstServices.Where(x => x.CategoryName == _selectedServiceItem)
                                    .FirstOrDefault().CategoryId;
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region ToTimeSlot 
        private List<string> _toTimeSlots;
        public List<string> ToTimeSlots
        {
            get
            {
                return _toTimeSlots;

            }
            set
            {
                _toTimeSlots = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ToTimeSlots"));
            }
        }


        string _selectedToTimeSlot;
        public string SelectedToTimeSlot
        {
            get
            {
                return _selectedToTimeSlot;
            }
            set
            {
                _selectedToTimeSlot = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedToTimeSlot"));
            }
        }


        #endregion
        #region FromTimeSlot 
        private List<string> _fromTimeSlots;
        public List<string> FromTimeSlots
        {
            get
            {
                return _fromTimeSlots;

            }
            set
            {
                _fromTimeSlots = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FromTimeSlots"));
            }
        }


        string _selectedFromTimeSlot;
        public string SelectedFromTimeSlot
        {
            get
            {
                return _selectedFromTimeSlot;
            }
            set
            {
                _selectedFromTimeSlot = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedFromTimeSlot"));
            }
        }


        #endregion


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
        private decimal? _rating;
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

        private DateTime _workDateFrom = DateTime.Now;
        public DateTime WorkDateFrom
        {
            get
            {
                return _workDateFrom;

            }
            set
            {
                _workDateFrom = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkDateFrom"));
            }
        }

        private DateTime _workDateTo = DateTime.Now;
        public DateTime WorkDateTo
        {
            get
            {
                return _workDateTo;

            }
            set
            {
                _workDateTo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WorkDateTo"));
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


        private bool isvisibleExistingImage;
        public bool IsvisibleExistingImage
        {
            get
            {
                return isvisibleExistingImage;
            }
            set
            {
                isvisibleExistingImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsvisibleExistingImage"));
            }
        }

        private bool isvisibleNewImage;
        public bool IsvisibleNewImage
        {
            get
            {
                return isvisibleNewImage;
            }
            set
            {
                isvisibleNewImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsvisibleNewImage"));
            }
        }
        private ImageSource imageData;
        public ImageSource ImageData
        {
            get
            {
                return imageData;
            }
            set
            {
                imageData = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageData"));
            }
        }
        private int gridHeight;
        public int GridHeight
        {
            get
            {
                return gridHeight;
            }
            set
            {
                gridHeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("GridHeight"));
            }
        }
        private bool isvisibleProgressBar = false;
        public bool IsvisibleProgressBar
        {
            get
            {
                return isvisibleProgressBar;
            }
            set
            {
                isvisibleProgressBar = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsvisibleProgressBar"));
            }
        }
        private string _progressPer;
        public string ProgressPer
        {
            get
            {
                return _progressPer;
            }
            set
            {
                _progressPer = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProgressPer"));
            }
        }
        private double _maximumVal;
        public double MaximumVal
        {
            get
            {
                return _maximumVal;
            }
            set
            {
                _maximumVal = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MaximumVal"));
            }
        }

        public ObservableCollection<BindImage> ProductList { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;
        WorkerModel _workerData;
        Lng objLng; 
        public static double loaderPer;

        public AddWorkDetailsViewModel(INavigation navigation, WorkerModel workerData, int categoryId)
        {
            objLng = App.Database.GetLng();
            _navigation = navigation;
            _workerData = workerData;
            _name = Common.FirstCharToUpper(workerData.Name);
            _regNo = workerData.RegNo;
            _rating = workerData.Rating;
            showRating(_rating??0);
            _userImage = workerData.Image;

            lstServices = workerData.UserCategories;
            var _sList = new ObservableCollection<string>();
            ;
            foreach (var item in lstServices)
            {
                var catName = objLng.Language == (CultureLanguage.Arabic) ? item.CategoryName_Arabic
                                    : objLng.Language == (CultureLanguage.Bangali) ? item.CategoryName_Bangali
                                    : objLng.Language == (CultureLanguage.English) ? item.CategoryName
                                    : objLng.Language == (CultureLanguage.Hindi) ? item.CategoryName_Hindi
                                    : objLng.Language == (CultureLanguage.Urdu) ? item.CategoryName_Urdu
                                    : item.CategoryName;
                _sList.Add(catName);
            }
            if (categoryId > 0)
            {
                selectedService = lstServices.Where(x => x.CategoryId == categoryId).FirstOrDefault().CategoryId;
                if (objLng.Language == (CultureLanguage.Arabic))
                {
                    _selectedServiceItem = lstServices.Where(x => x.CategoryId == categoryId).FirstOrDefault().CategoryName_Arabic;
                }
                else if (objLng.Language == (CultureLanguage.Bangali))
                {
                    _selectedServiceItem = lstServices.Where(x => x.CategoryId == categoryId).FirstOrDefault().CategoryName_Bangali;
                }
                else if (objLng.Language == (CultureLanguage.Hindi))
                {
                    _selectedServiceItem = lstServices.Where(x => x.CategoryId == categoryId).FirstOrDefault().CategoryName_Hindi;
                }
                else if (objLng.Language == (CultureLanguage.Urdu))
                {
                    _selectedServiceItem = lstServices.Where(x => x.CategoryId == categoryId).FirstOrDefault().CategoryName_Urdu;
                }
                else
                {
                    _selectedServiceItem = lstServices.Where(x => x.CategoryId == categoryId).FirstOrDefault().CategoryName;
                }

            }
            _servicesList = _sList;

            _fromTimeSlots = Common.getTimeSlots();
            _toTimeSlots = Common.getTimeSlots();
            gridHeight = 160;
            bindImages();
        }

        void showRating(decimal rating)
        {
            if (rating >= 5)
            {
                _imgRate1 = "ic_star_fill.png";
                _imgRate2 = "ic_star_fill.png";
                _imgRate3 = "ic_star_fill.png";
                _imgRate4 = "ic_star_fill.png";
                _imgRate5 = "ic_star_fill.png";
            }
            else if (rating >= 4)
            {
                _imgRate1 = "ic_star_fill.png";
                _imgRate2 = "ic_star_fill.png";
                _imgRate3 = "ic_star_fill.png";
                _imgRate4 = "ic_star_fill.png";
            }
            else if (rating >= 3)
            {
                _imgRate1 = "ic_star_fill.png";
                _imgRate2 = "ic_star_fill.png";
                _imgRate3 = "ic_star_fill.png";
            }
            else if (rating >= 2)
            {
                _imgRate1 = "ic_star_fill.png";
                _imgRate2 = "ic_star_fill.png";
            }
            else if (rating >= 1)
            {
                _imgRate1 = "ic_star_fill.png";
            }
        }
        List<Attachment> lstImageBytes = new List<Attachment>();
        //List<byte[]> lstImageBytes = new List<byte[]>();
        void bindImages()
        {
            var items = new ObservableCollection<BindImage>();
            items.Add(new BindImage
            {
                HasImage = false,
                IsvisibleExistingImage = false,
                IsvisibleNewImage = true
            });
            ProductList = items;
        }
        void updateHeight()
        {
            if (ProductList.Count > 1)
            {
                var count2 = (ProductList.Count / 2) + (ProductList.Count % 2);
                count2 += 1;
                GridHeight = count2 * 120;
            }
        }

        public Command submitBtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                        if (!Common.CheckConnection())
                        {
                            await _navigation.PushPopupAsync(new NoInternetPopup());
                            return;
                        }
                        string msg = string.Empty;
                        if (selectedService == 0)
                        {
                            msg += AppResources.SelectService + System.Environment.NewLine;
                        }
                        if (WorkDateFrom == WorkDateTo)
                        {
                            if (string.IsNullOrEmpty(SelectedFromTimeSlot))
                            {
                                msg += AppResources.ChooseTimeFrom + System.Environment.NewLine;
                            }
                            if (string.IsNullOrEmpty(SelectedToTimeSlot))
                            {
                                msg += AppResources.ChooseTimeTo + System.Environment.NewLine;
                            }
                        }

                        if (!string.IsNullOrEmpty(msg))
                        {
                            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(msg));
                            await Task.Delay(1000);
                            await _navigation.PopPopupAsync();
                            return;
                        }
                        else
                        {
                            try
                            {
                                SelectedFromTimeSlot = SelectedFromTimeSlot == null ? "" : SelectedFromTimeSlot;
                                SelectedToTimeSlot = SelectedToTimeSlot == null ? "" : SelectedToTimeSlot;

                                string boundary = "---8d0f01e6b3b5dafaaadaad";
                                MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
                                var date1 = WorkDateFrom.ToString("MM-dd-yyyy");
                                var date2 = WorkDateTo.ToString("MM-dd-yyyy");
                                multipartContent.Add(new StringContent(selectedService.ToString()), "CategoryId");
                                multipartContent.Add(new StringContent(LoginUserDetails.userId.ToString()), "CustomerId");
                                multipartContent.Add(new StringContent(_workerData.UserId.ToString()), "WorkerId");
                                multipartContent.Add(new StringContent(date1), "WorkDateFrom");
                                multipartContent.Add(new StringContent(date2), "WorkDateTo");
                                multipartContent.Add(new StringContent(SelectedFromTimeSlot), "WorkHoursFrom");
                                multipartContent.Add(new StringContent(SelectedToTimeSlot), "WorkHoursTo");
                                multipartContent.Add(new StringContent(!string.IsNullOrEmpty(WorkNotes)?WorkNotes : ""), "WorkNote");
                            
                                try
                                {
                                    int imgCount = 0;
                                    int vidCount = 0;
                                    if (lstImageBytes != null && lstImageBytes.Count > 0)
                                    {
                                        foreach (var item in lstImageBytes)
                                        {
                                            string Name = string.Empty;
                                            string FileName = string.Empty;
                                            if (item.type == "image")
                                            {
                                                imgCount++;
                                                FileName = "image_" + imgCount + ".jpeg"; ;
                                                Name = "image";
                                            }
                                            else
                                            {
                                                vidCount++;
                                                FileName = "video_" + imgCount + ".mp4"; ;
                                                Name = "video";
                                            }

                                            var fileContent = new ByteArrayContent(item.data);

                                            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                                            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                            {
                                                Name = Name,
                                                FileName = FileName,
                                            };
                                            multipartContent.Add(fileContent);
                                        }
                                    }
                                }
                                catch { }
                                HttpClient httpClient = new HttpClient();
                                //IsvisibleProgressBar = true;

                                await _navigation.PushPopupAsync(new LoaderWithPer());

                                string latitude = string.Empty, longitude = string.Empty;
                                string url = ApiUrl.AddWorkDetail;
                            loaderPer = 0;
                                var progressContent = new ProgressableStreamContent(
         multipartContent,
         4096,
         (sent, total) =>
         {

             var per = (sent * 100) / total;
             MessagingCenter.Send<AddWorkDetailsViewModel>(this, "UpdateLoaderValues");
             // LoaderWithPer.sfLinearProgressBar.Progress = sent;
             //LoaderWithPer.sfLinearProgressBar.Maximum = total;
             loaderPer = per;
            // LoaderWithPer.sfCircularProgressBar.Progress = per;
             //LoaderWithPer.sfCircularProgressBar.Maximum = 100;

             Console.WriteLine("Uploading {0}/{1}", sent, total);
         });
                                HttpResponseMessage response = await httpClient.PostAsync(url, progressContent);
                                //string content1 = await response.Content.ReadAsStringAsync();
                                if (response.IsSuccessStatusCode)
                                {
                                    string content = await response.Content.ReadAsStringAsync();
                                    if (content != null)
                                    {
                                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<wsBase>(content);

                                        if (result != null)
                                        {
                                            LoaderWithPer.CloseAllPopup();
                                            await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage),
                                               AppResources.Ok);
                                            if (result.status)
                                            {
                                                HomeMasterPage._masterPage.Detail = new NavigationPage(new CustomerHomePage());

                                            }
                                        }
                                        else
                                        {
                                            LoaderWithPer.CloseAllPopup();
                                            await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                                        }
                                    }
                                    else
                                    {
                                        LoaderWithPer.CloseAllPopup();
                                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                                    }
                                }
                                else
                                {
                                    LoaderWithPer.CloseAllPopup();
                                    await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                                }
                            }
                            catch (Exception ex)
                            {
                                await App.Current.MainPage.DisplayAlert("", ex.Message, AppResources.Ok);
                            }
                            finally
                            {
                            }
                        }
                    });
               // });
            }
        }
        public Command btnDeleteImageCommand
        {
            get
            {
                return new Command((e) =>
                {
                    try
                    {

                        var data = (BindImage)e;
                        var index = data.index;
                        lstImageBytes.RemoveAt(index);
                        ProductList.RemoveAt(index);
                        updateHeight();
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }
        byte[] _imagefile;
        public Command btnAddImageCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var data = (BindImage)e;

                    try
                    {
                        //var action = await App.Current.MainPage.DisplayActionSheet(AppResources.ChoosePhotoVideo,
                        //    AppResources.Cancel, null, AppResources.TakePhoto, AppResources.ChoosePhotofromGallery, AppResources.CaptureVideo);
                        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.ChoosePhoto,
                            AppResources.Cancel, null, AppResources.TakePhoto, AppResources.ChoosePhotofromGallery );
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
                                Name = "test.jpg",

                            });
                            if (file == null)
                                return;
                            var albumPath = file.AlbumPath;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.GetStream().CopyTo(memoryStream);
                                var myfile = memoryStream.ToArray();
                                _imagefile = myfile;
                                file.Dispose();
                            }
                            //lstImageBytes.Add(_imagefile);
                            lstImageBytes.Add(new Attachment()
                            {
                                data = _imagefile,
                                type = "image",
                            });

                            var _items = ProductList;
                            _items.Insert(_items.Count - 1, new BindImage()
                            {
                                index = _items.Count - 1,
                                HasImage = true,
                                imageData = ImageSource.FromStream(() => new MemoryStream(_imagefile)),
                                IsvisibleExistingImage = true,
                                IsvisibleNewImage = false,
                                IsImage = true,
                                IsVideo = false,
                                text = albumPath
                            });
                            ProductList = _items;   // swap the collection for the new one
                            RaisePropertyChanged(nameof(ProductList));
                            updateHeight();
                        }
                        else if (action == AppResources.ChoosePhotofromGallery)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file != null)
                            {
                                var albumPath = file.AlbumPath;
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    var myfile = memoryStream.ToArray();
                                    _imagefile = myfile;
                                    file.Dispose();
                                }
                                // lstImageBytes.Add(_imagefile);
                                lstImageBytes.Add(new Attachment()
                                {
                                    data = _imagefile,
                                    type = "image",
                                });
                                var _items = ProductList;
                                if (_items != null)
                                {
                                    _items.Insert(_items.Count - 1, new BindImage()
                                    {
                                        index = _items.Count - 1,
                                        HasImage = true,
                                        imageData = ImageSource.FromStream(() => new MemoryStream(_imagefile)),
                                        IsvisibleExistingImage = true,
                                        IsvisibleNewImage = false,
                                        IsImage = true,
                                        IsVideo = false,
                                        text = albumPath
                                    });
                                }
                                ProductList = _items;   // swap the collection for the new one
                                RaisePropertyChanged(nameof(ProductList));
                                updateHeight();
                            }
                        }
                        else if (action == AppResources.CaptureVideo)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
                            {
                                return;
                            }
                            if (Device.RuntimePlatform == "iOS")
                            {
                                await Task.Delay(1000);
                            }
                            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
                            {
                                Directory = "Sample",
                                Name = "test.mp4",
                                DesiredLength = TimeSpan.FromMinutes(1),
                                Quality = Plugin.Media.Abstractions.VideoQuality.Medium
                            });
                            if (file == null)
                                return;
                            var albumPath = file.AlbumPath;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.GetStream().CopyTo(memoryStream);
                                var myfile = memoryStream.ToArray();
                                _imagefile = myfile;
                                file.Dispose();
                            }
                            //lstImageBytes.Add(_imagefile);
                            lstImageBytes.Add(new Attachment()
                            {
                                data = _imagefile,
                                type = "video",
                            });
                            var _items = ProductList;
                            _items.Insert(_items.Count - 1, new BindImage()
                            {
                                index = _items.Count - 1,
                                HasImage = true,
                                imageData = ImageSource.FromStream(() => new MemoryStream(_imagefile)),
                                IsvisibleExistingImage = true,
                                IsvisibleNewImage = false,
                                IsImage = false,
                                IsVideo = true,
                                text = albumPath
                            });
                            ProductList = _items;   // swap the collection for the new one
                            RaisePropertyChanged(nameof(ProductList));
                            updateHeight();
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

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }





    }
}

