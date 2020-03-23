using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Models
{
    #region WorkList
    public class WorkerModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public string Image { get; set; }
        public decimal? Rating { get; set; }
        public string TotalReviews { get; set; }
        public ImageSource ImgRate1 { get { return Rating != null && Rating >= 1 ? "ic_star_fill.png" : "ic_star_fill_2.png"; } }
        public ImageSource ImgRate2 { get { return Rating != null && Rating >= 2 ? "ic_star_fill.png" : "ic_star_fill_2.png"; } }
        public ImageSource ImgRate3 { get { return Rating != null && Rating >= 3 ? "ic_star_fill.png" : "ic_star_fill_2.png"; } }
        public ImageSource ImgRate4 { get { return Rating != null && Rating >= 4 ? "ic_star_fill.png" : "ic_star_fill_2.png"; } }
        public ImageSource ImgRate5 { get { return Rating != null && Rating >= 5 ? "ic_star_fill.png" : "ic_star_fill_2.png"; } }
        public List<UserCategory> UserCategories { get; set; }
    }
    public class Worker
    {
        public Int64 UserId { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Image { get; set; }
        public decimal? Rating { get; set; }
        public string TotalReviews { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public double Distance { get; set; }
        public List<UserCategory> UserCategories { get; set; }
    }

    public class wsWorkerList
    {
        public bool status { get; set; }
        public object message { get; set; }
        public List<Worker> categoryWorkersData { get; set; }
    }
    #endregion
    #region WorkerProfile

    public class UserReview
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public decimal? Rating { get; set; }
        public ImageSource ImgRate1 { get; set; }
        public ImageSource ImgRate2 { get; set; }
        public ImageSource ImgRate3 { get; set; }
        public ImageSource ImgRate4 { get; set; }
        public ImageSource ImgRate5 { get; set; }
        public string CustomerImage { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string StrReviewDate
        {
            get
            {
                return ReviewDate == null ? "" : Convert.ToDateTime(ReviewDate).ToString("dd-MM-yyyy HH:mm");
            }
        }
        public decimal LstViewRowHeight { get { return Comment == null ? 150 : getHeight(Comment.Length); } }

        public static decimal getHeight(int len)
        {
            return 250;
        }
        public ImageSource StarOneImage { get { return Rating != null && Rating >= 1 ? "ic_star_yellow.png" : "ic_star_outline_yellow.png"; } }
        public ImageSource StarTwoImage { get { return Rating != null && Rating >= 2 ? "ic_star_yellow.png" : "ic_star_outline_yellow.png"; } }
        public ImageSource StarThreeImage { get { return Rating != null && Rating >= 3 ? "ic_star_yellow.png" : "ic_star_outline_yellow.png"; } }
        public ImageSource StarFourImage { get { return Rating != null && Rating >= 4 ? "ic_star_yellow.png" : "ic_star_outline_yellow.png"; } }
        public ImageSource StarFiveImage { get { return Rating != null && Rating == 5 ? "ic_star_yellow.png" : "ic_star_outline_yellow.png"; } }
    }


    public class WorkerProfileData
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public decimal? Rating { get; set; }
        public string TotalReviews { get; set; }
        public object DeviceId { get; set; }
        public object DeviceToken { get; set; }
        public string RegistrationNumber { get; set; }
        public List<UserCategory> UserCategories { get; set; }
        public List<UserReview> UserReviews { get; set; }
    }

    public class wsWorkerProfile
    {
        public bool status { get; set; }
        public string message { get; set; }
        public WorkerProfileData workerProfileData { get; set; }
    }
    #endregion

    #region Add Work Detail

    public class BindImage : INotifyPropertyChanged
    {
        public int index { get; set; }
        public string text { get; set; }
        public bool HasImage { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public bool IsvisibleExistingImage { get; set; }
        public bool IsvisibleNewImage { get; set; }
        //public ImageSource imageData { get; set; }
        private ImageSource _imageData;
        public ImageSource imageData
        {
            get { return _imageData; }
            set
            {
                _imageData = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    #endregion

    public class DriverListModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<DriverList> data { get; set; }
        public NotificationMessage notificationMessage { get; set; }
    }

    public class DriverList
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string RegistrationNumber { get; set; }
        public double Distance { get; set; }
        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public object Price { get; set; }
        public object EstimatedTime { get; set; }
    }
}
