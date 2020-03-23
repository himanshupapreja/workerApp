using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class DriverOrderModel
    {
        public bool status { get; set; }
        public object message { get; set; }
        public List<DriversOrder> data { get; set; }
    }

    public class DriversOrder : INotifyPropertyChanged
    {
        public long Id { get; set; }
        public int OrderId { get; set; }
        public int RequestId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerRegNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLocation { get; set; }
        public double CustomerLatitude { get; set; }
        public double CustomerLongitude { get; set; }
        public int WorkerId { get; set; }
        public string WorkerRegNumber { get; set; }
        public string WorkerName { get; set; }
        public string WorkerLocation { get; set; }
        public double WorkerLatitude { get; set; }
        public double WorkerLongitude { get; set; }
        public double? Price { get; set; }
        public string EstimatedTime { get; set; }
        public int? RequestStatus { get; set; }
        public string SendQuoteText { get; set; }
        public string RideStatusText { get; set; }
        public bool IsNewVisible { get; set; }
        public bool IsOnGoingVisible { get; set; }
        public bool IsHistoryVisible { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsHistory
        {
            get { return _isHistory; }
            set
            {
                _isHistory = value;

                OnPropertyChanged(); // Notify, that Title has been changed
            }
        }

        //[JsonIgnore]

        //private string _sendQuoteText;

        //public event PropertyChangedEventHandler PropertyChanged;

        //public string SendQuoteText
        //{
        //    get
        //    {
        //        return _sendQuoteText;
        //    }
        //    set
        //    {
        //        _sendQuoteText = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs("SendQuoteText"));
        //    }
        //}
    }

    //public class DriversOrderModel: DriversOrder
    //{
    //    public string SendQuoteText { get; set; }
    //}
}
