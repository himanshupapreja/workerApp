using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Worker_7ERFAcraft.Repository;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Models
{
    public class Order : INotifyPropertyChanged
    {
        public int OrderId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryName_Hindi { get; set; }
        public string CategoryName_Urdu { get; set; }
        public string CategoryName_Bangali { get; set; }
        public string CategoryName_Arabic { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public Color StatusTextColor
        {
            get
            {
                if(StatusText == Resx.AppResources.Completed.ToUpper())
                {
                    return Color.FromHex("#74e5b5");
                }
                else if (StatusText == Resx.AppResources.Canceled.ToUpper())
                {
                    return Color.FromHex("#ff1313");
                }
                else
                {
                    return Color.FromHex("#000000");
                }
               
            }
        }
        public string WorkerName { get; set; }
        public string WorkerImage { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        public string WorkerRegistrationNumber { get; set; }
        public string CustomerRegistrationNumber { get; set; }
        public DateTime WorkDateFrom { get; set; }
        public DateTime WorkDateTo { get; set; }
        public string WorkHoursFrom { get; set; }
        public string WorkHoursTo { get; set; }
        public string WorkDate
        {
            get
            {
                if(WorkDateFrom == WorkDateTo)
                {
                    return WorkDateFrom.ToString("dd-MM-yyyy") +" " + WorkHoursFrom+"-"+ WorkHoursTo;
                }
                else
                {
                    return WorkDateFrom.ToString("dd-MM-yyyy") + " - " + WorkDateTo.ToString("dd-MM-yyyy");
                } 
            }
        }
        private bool _isPending;
        public bool IsPending
        {
            get { return _isPending; }
            set
            {
                _isPending = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }
        private bool _isOnGoing;
        public bool IsOnGoing
        {
            get { return _isOnGoing; }
            set
            {
                _isOnGoing = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        } 


        private bool _isHistory;
        public bool IsHistory
        {
            get { return _isHistory; }
            set
            {
                _isHistory = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }

    public class wsOrder
    {
        public bool status { get; set; }
        public object message { get; set; }
        public List<Order> customerBookingData { get; set; }
    }


    #region Order Detail

    
    public class OrderMedia
    {
        public string MediaName { get; set; }
        public Uri MediaNameUrl { get { return  !string.IsNullOrEmpty(MediaName)? new Uri(MediaName):null ; } }
        public string MediaType { get; set; }
        //public bool  IsImage { get { return MediaType.ToLower() == "video" ? false  : true; } }
        //public bool IsVideo { get { return MediaType.ToLower() == "video" ? true : false; } }
        public bool IsImage
        {
            get
            {
                var mArr = MediaName.Split('.');
                if(mArr.Length==3)
                {
                    var ext = mArr[2];
                    if(ext.ToLower()=="jpeg" || ext.ToLower() == "jpg" || ext.ToLower() == "png")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                } 
            }
        }
        public bool IsVideo { get { return !IsImage; } }

    }

    public class WorkDetailData
    {
        public int OrderId { get; set; }
        public object CategoryName { get; set; }
        public string Location { get; set; }
        public string WorkNote { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        public string WorkerName { get; set; }
        public string WorkerImage { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime WorkDateFrom { get; set; }
        public DateTime WorkDateTo { get; set; }
        public string WorkHoursFrom { get; set; }
        public string WorkHoursTo { get; set; }
        public string  Latitude { get; set; }
        public string Longitude { get; set; }
        public int Status { get; set; }
        public string TotalReviews { get; set; }
        public decimal? Rating { get; set; }
        public List<OrderMedia> OrderMedias { get; set; }
        public string RegistrationNumber { get; set; }
        public string WorkerPhone { get; set; }
        public string WorkerLocation { get; set; }
        public string WorkerLatitude { get; set; }
        public string WorkerLongitude { get; set; }
    }

    public class wsOrderDetail
    {
        public bool status { get; set; }
        public object message { get; set; }
        public WorkDetailData workDetailData { get; set; }
        public List<DriverList> drivers { get; set; }
    }

    #endregion

    #region Customer Bookings
    public class CustomerBookingData
    {
        public int OrderId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryName_Hindi { get; set; }
        public string CategoryName_Urdu { get; set; }
        public string CategoryName_Bangali { get; set; }
        public string CategoryName_Arabic { get; set; }
        public string CategoryImage { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public string Location { get; set; }
        public object Latitude { get; set; }
        public object Longitude { get; set; }
        public DateTime WorkDateFrom { get; set; }
        public DateTime WorkDateTo { get; set; }
        public string WorkHoursFrom { get; set; }
        public string WorkHoursTo { get; set; }
    }

    public class wsCustomerBooking
    {
        public bool status { get; set; }
        public object message { get; set; }
        public List<CustomerBookingData> customerBookingData { get; set; }
    }
    #endregion

}
