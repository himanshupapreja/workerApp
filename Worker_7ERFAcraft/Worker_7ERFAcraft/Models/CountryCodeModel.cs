using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class CountryCodeModel
    {
        public string name { get; set; }
        public string dial_code { get; set; }
        public string code { get; set; }
    }
    public class CountryCodeResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<CountryCodeModel> data { get; set; }
    }
}
