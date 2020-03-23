using System;
using System.Collections.Generic;
using System.Text;
using Worker_7ERFAcraft.Repository;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Models
{
    public class MyAccountData
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string RegistrationNumber { get; set; }
        public string UserImage { get; set; }
        public double? Rating { get; set; }
        public int TotalReviews { get; set; }
        public int AccountType { get; set; }
        public string  TotalBalance { get; set; }
        public string YourDebt { get; set; }
        public string TotalJobs { get; set; }
        public string LongProjectPercentage { get; set; }
        public bool? IsAvailable { get; set; }
        public List<LongOrder> LongOrders { get; set; }
        public string CurrencyCode { get; set; }
    }
    public class LongOrder
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int WorkerId { get; set; }
        public string WorkNote { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
       // public DateTime AcceptDate { get; set; }
        public DateTime WorkDateFrom { get; set; }
        public string MonthYearName { get { return WorkDateFrom.ToString("dddd, dd MMMM yyyy"); } }
        public string CustomerName { get; set; }
        public string WorkerName { get; set; }
        public string StatusText { get; set; }
        public string StatusTextCaps { get { return StatusText.ToUpper(); } }
        public Color StatusTextColor
        {
            get
            {
                if (StatusTextCaps == OrderStatus.Completed.ToString().ToUpper())
                {
                    return Color.FromHex("#74e5b5");
                }
                else if (StatusTextCaps == OrderStatus.Canceled.ToString().ToUpper())
                {
                    return Color.FromHex("#ff1313");
                }
                else
                {
                    return Color.FromHex("#000000");
                }

            }
        }
        public string CategoryName { get; set; }
        public int TotalDays { get; set; }
        public string StrTotalDays { get { return "TotalDays: " + TotalDays.ToString(); } }
    }
    public class wsAccountData:wsBase
    { 
        public MyAccountData myAccountData { get; set; }
    }

    public class DriverIsAvailable
    {
        public bool IsAvailable { get; set; }
    }

    public class DriverIsAvailableData
    {
        public bool status { get; set; }
        public object message { get; set; }
        public MyAccountData myAccountData { get; set; }
    }

    public class ChatUserData
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ChatUserList
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<ChatUserData> data { get; set; }
    }
}
