using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class DriverOrderModel
    {
        public bool status { get; set; }
        public object message { get; set; }
        public List<DriversOrder> data { get; set; }
    }

    public class DriversOrder
    {
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
        public object Price { get; set; }
        public object EstimatedTime { get; set; }
        public int? RequestStatus { get; set; }
    }
}
