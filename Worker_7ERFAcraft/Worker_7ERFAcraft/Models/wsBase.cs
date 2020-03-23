using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
   public class wsBase
    { 
        public bool status { get; set; }
        public string message { get; set; }
        public NotificationMessage notificationMessage { get; set; }
    }
}
