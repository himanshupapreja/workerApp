using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class AboutUsData
    {
        public int AboutUsId { get; set; }
        public string Text { get; set; }
        public string Email { get; set; }
        public string PhoneNumber1 { get; set; }
        public string Location { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public string PhoneNumber4 { get; set; }
    }

    public class wsAboutUs
    {
        public bool status { get; set; }
        public string message { get; set; }
        public AboutUsData aboutUsData { get; set; }
    }
    //public class wsAboutUs:wsBase
    //{
    //    public string Email { get; set; }
    //    public string PhoneNumber1 { get; set; }
    //    public string PhoneNumber2 { get; set; }
    //    public string PhoneNumber3 { get; set; }
    //    public string PhoneNumber4 { get; set; }
    //}
    public class AboutUsModel
    {
        public string Email { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public string PhoneNumber4 { get; set; }
    }
}
