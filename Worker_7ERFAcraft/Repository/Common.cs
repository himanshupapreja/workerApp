using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Worker_7ERFAcraft.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Worker_7ERFAcraft.Repository
{
    public enum PaymentType
    {
        SevenDays = 1,
        InAdvance = 2
    }
    public enum OrderStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Canceled = 4
    }
    public enum UpdateOrderStatus
    {
        Accept = 0,
        Reject = 1,
        Complete = 2
    }
    public enum HomeScreenType
    {
        Map = 0,
        List = 1, 
    }
    public enum OrderType
    {
        OnGoing=0,
        History=1,
        Pending=2
    }

    public enum UserType
    {
        Customer=3,
        Worker=2,
        Driver = 5
    }

    public class Common
    {
        public static string GoogleMapPlacesKey = "AIzaSyC9kL6Afmu4aPHkOxMCqlWx6nndL-JQoLo";// "AIzaSyA5PGSK8DosNMH_ig9Cd-Y3OS7xhZCw308"; 
        public static string appName = "موجود Mawjood";
        public static string someErrorMsg = "Some error has occured please try again.";
        public static string signUpUserAddress = string.Empty;
        public static Plugin.Geolocator.Abstractions.Position signUpUserPosition;
        public async  static void GetLocation(bool isSignupUser=false)
        {
            try
            {
                var locator = Plugin.Geolocator.CrossGeolocator.Current;
                if (locator.IsGeolocationEnabled)
                {
                    locator.DesiredAccuracy = 120;
                    //var position =  locator.GetPositionAsync();
                    var timeout = TimeSpan.FromSeconds(10);
                    var position = await locator.GetPositionAsync(timeout,null,true);
                    if (position != null)
                    {
                        App.latitude = position.Latitude.ToString();
                        App.longitude = position.Longitude.ToString();
                        App.UpdateDeviceInfo();
                        if(isSignupUser)
                        {
                            signUpUserPosition = position;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        public async static void GetAddress( Position pos,Page page)
        { 
            try
            {
                Geocoder gc = new Geocoder();
                IEnumerable<string> possibleAddresses =
                    await gc.GetAddressesForPositionAsync(pos);
                foreach (var result in possibleAddresses)
                {
                    signUpUserAddress = result + "\n";
                    await App.Current.MainPage.Navigation.PushPopupAsync(new Pages.ShowMessage(signUpUserAddress));
                    await System.Threading.Tasks.Task.Delay(1000);
                    await App.Current.MainPage.Navigation.PopPopupAsync();
                    goto there; 
                }
                //var locator = Plugin.Geolocator.CrossGeolocator.Current;
                //if (locator.IsGeolocationEnabled)
                //{
                //    locator.DesiredAccuracy = 50;
                //    var addresses = await locator.GetAddressesForPositionAsync(pos);
                //    if (addresses != null)
                //    {
                //        foreach (var item in addresses)
                //        {
                //            signUpUserAddress = item + "\n";
                //            goto there;
                //        }
                //    }
                //}
                there: page.Title = signUpUserAddress; 
            }
            catch (Exception)
            {
                  
            }
        }
       
        public static string getMsg(Models.NotificationMessage nMsg)
        {
            string msg = string.Empty;
            var lng = App.Database.GetLng();
            if (lng != null && !string.IsNullOrEmpty(lng.Language))
            {
                if (lng.Language == Models.CultureLanguage.Arabic)
                {
                    msg = nMsg.msgAr;
                }
                else if (lng.Language == Models.CultureLanguage.Bangali)
                {
                    msg = nMsg.msgBa;
                }
                else if (lng.Language == Models.CultureLanguage.Hindi)
                {
                    msg = nMsg.msgHi;
                }
                else if (lng.Language == Models.CultureLanguage.Urdu)
                {
                    msg = nMsg.msgUr;
                }
                else
                {
                    msg = nMsg.msgEng;
                }
            }
            else
            {
                return nMsg.msgEng;
            }
            return msg;
        }
        public static double CalculateDistance(double Lat1, double Long1, double Lat2, double Long2)
        {
            double dDistance = Double.MinValue;
            double dLat1InRad = Lat1 * (Math.PI / 180.0);
            double dLong1InRad = Long1 * (Math.PI / 180.0);
            double dLat2InRad = Lat2 * (Math.PI / 180.0);
            double dLong2InRad = Long2 * (Math.PI / 180.0);

            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                       Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            double c = 2.0 * Math.Asin(Math.Sqrt(a));


            const Double kEarthRadiusKms = 6376.5;
            dDistance = kEarthRadiusKms * c;

            return dDistance;
        }
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        /// <summary>
        /// Make first char of input to upper case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
        public static List<string> getTimeSlotsoLD( )
        {
            List<string> lst = new List<string>();

            lst.Add("12:00 AM - 01:00 AM");
            lst.Add("01:00 AM - 02:00 AM");
            lst.Add("02:00 PM - 03:00 AM");
            lst.Add("03:00 PM - 04:00 AM");
            lst.Add("04:00 PM - 05:00 AM");
            lst.Add("05:00 PM - 06:00 AM");
            lst.Add("06:00 PM - 07:00 AM");
            lst.Add("07:00 PM - 08:00 AM");
            lst.Add("08:00 PM - 09:00 AM");
            lst.Add("09:00 AM - 10:00 AM");
            lst.Add("10:00 AM - 11:00 AM");
            lst.Add("11:00 AM - 12:00 PM");
            lst.Add("12:00 PM - 01:00 PM");
            lst.Add("01:00 PM - 02:00 PM");
            lst.Add("02:00 PM - 03:00 PM");
            lst.Add("03:00 PM - 04:00 PM");
            lst.Add("04:00 PM - 05:00 PM");
            lst.Add("05:00 PM - 06:00 PM");
            lst.Add("06:00 PM - 07:00 PM");
            lst.Add("07:00 PM - 08:00 PM");
            lst.Add("08:00 PM - 09:00 PM");
            lst.Add("09:00 PM - 10:00 PM");
            lst.Add("10:00 PM - 11:00 PM");
            lst.Add("11:00 PM - 12:00 AM");
            
            return lst;
        }
        public static List<string> getTimeSlots()
        {
            List<string> lst = new List<string>();

            lst.Add("12:00 AM");
            lst.Add("01:00 AM");
            lst.Add("02:00 AM");
            lst.Add("03:00 AM");
            lst.Add("04:00 AM");
            lst.Add("05:00 AM");
            lst.Add("06:00 AM");
            lst.Add("07:00 AM");
            lst.Add("08:00 AM");
            lst.Add("09:00 AM");
            lst.Add("10:00 AM");
            lst.Add("11:00 AM");
            lst.Add("12:00 PM");
            lst.Add("01:00 PM");
            lst.Add("02:00 PM");
            lst.Add("03:00 PM");
            lst.Add("04:00 PM");
            lst.Add("05:00 PM");
            lst.Add("06:00 PM");
            lst.Add("07:00 PM");
            lst.Add("08:00 PM");
            lst.Add("09:00 PM");
            lst.Add("10:00 PM");
            lst.Add("11:00 PM");

            return lst;
        }


        /// <summary>
        /// Validate email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>status</returns>
        public static bool CheckValidEmail(string Email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Check Internet Connection
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnection()
        {
            if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                //App.Current.MainPage.DisplayAlert("", "Please check your Internet connection.", "OK");
                return false;
            }
            else
            {
                return true;
            }
        }

         


    }
}
