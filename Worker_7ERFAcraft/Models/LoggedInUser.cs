using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public static class CultureLanguage
    {
        public static string English { get; set; } = "en-US";
        public static string Arabic { get; set; } = "ar-AE";
        public static string Hindi { get; set; } = "hi";
        public static string Urdu { get; set; } = "ur";
        public static string Bangali { get; set; } = "bn-IN";
    }
    public class Lng
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }

        public string Language { get; set; }
    }
    public class LoginUserDetails
    {
        public static  long userId { get; set; }
        public static string name { get; set; }
        public static string phone { get; set; }
        public static int userType { get; set; }
        public static string image { get; set; }
    }
   public  class LoggedInUser
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public long userId { get; set; }
        public string name { get; set; }
        public string  phone { get; set; }
        public  int userType { get; set; }
        public string image { get; set; }
    }

    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }
        public string DeliveryCharges { get; set; }
    }
    }
