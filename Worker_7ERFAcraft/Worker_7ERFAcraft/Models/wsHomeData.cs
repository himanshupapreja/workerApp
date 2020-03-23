using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class HomeAdsData
    {
        public int HomeAdsId { get; set; }
        public string MediaName { get; set; }
        public string MediaType { get; set; }
        public bool IsActive { get; set; }
        public string AdUrl { get; set; }
        //public Uri MediaNameUrl { get { return !string.IsNullOrEmpty(MediaName) ? new Uri(MediaName) : null; } } 
        //public bool IsImage { get { return MediaType.ToLower() == "video" ? false : true; } }
        //public bool IsVideo { get { return MediaType.ToLower() == "video" ? true : false; } }
    }
    public class HomeData
    {
        public int ScreenMediaId { get; set; }
        public string MediaName { get; set; }
        public string MediaType { get; set; }
    }

    public class wsHomeData:wsBase
    { 
        public HomeData screenMediaData { get; set; }
        public List<HomeAdsData> homeAdsData { get; set; }
    }
    public class wsAppVideo:wsBase
    { 
        public string appVideoData { get; set; } 
    }
}
