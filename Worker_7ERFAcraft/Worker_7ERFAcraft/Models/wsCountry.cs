using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class Prediction : INotifyPropertyChanged
    {
        public string description { get; set; }
        public string id { get; set; }
        //public List matched_substrings { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        //public StructuredFormatting structured_formatting { get; set; }
        // public List terms { get; set; }
        // public List types { get; set; } 
    }
    public class wsGooglePlaces
    {
        public List<Prediction> predictions { get; set; }
        public string status { get; set; }
    }
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }

    public class wsCountry : wsBase
    { 
        public List<Country> countryData { get; set; }
    }
    public class wsForgotPassContDetails : wsBase
    {
        public string  contactNumber { get; set; }
    }
}
