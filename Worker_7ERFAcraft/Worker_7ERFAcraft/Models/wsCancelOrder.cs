using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class CancellationReasons 
    {
        public int ReasonId { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }

    public class wsCancelOrder
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<CancellationReasons > cancellationReasonsData { get; set; }
    }
    
}
