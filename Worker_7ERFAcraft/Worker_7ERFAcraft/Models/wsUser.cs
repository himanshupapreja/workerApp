using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class UserData
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string ResidencyNumber { get; set; }
        public int? UserType { get; set; }
        public string UserTypeText { get; set; }
        public int? DeviceId { get; set; }
        public string DeviceToken { get; set; }
         
        public string Password { get; set; }
        public string Location { get; set; }
        public int? CountryId { get; set; } 
        public string CompanyName { get; set; }
        public List<UserCategory> UserCategories { get; set; }
        public List<UserCategory> Categories { get; set; }
        public string Image { get; set; }
        public string OTP { get; set; }
        public string RegistrationNumber { get; set; }
        public string ResidencyImage { get; set; }
        public string VehicleImage { get; set; }
        public string CopyOfVehicleFormImage { get; set; }
        public string CopyOfCivilRegisterImage { get; set; }

        public DateTime? VehicleExpiryDate { get; set; }
        public DateTime? VehicleFormExpiryDate { get; set; }
        public DateTime? CivilRegisterExpiryDate { get; set; }
    }

    public class wsUser : wsBase
    { 
        public int otpResponse { get; set; }
        public UserData userData { get; set; }
    } 
    public class postLoginData
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int DeviceId { get; set; }
        public string DeviceToken { get; set; }
    }

    public class UserCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryName_Hindi { get; set; }
        public string CategoryName_Urdu { get; set; }
        public string CategoryName_Bangali { get; set; }
        public string CategoryName_Arabic { get; set; }
        public string Image { get; set; }
    }

    public class postSignUpData
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string CountryId { get; set; }
        public string CountryCode { get; set; }
        public int UserType { get; set; }
        public int DeviceId { get; set; }
        public object DeviceToken { get; set; }
        public string CompanyName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ResidencyNumber { get; set; }
        public List<UserCategory> UserCategories { get; set; }
    }

    #region user profile Data
 

    public class wsProfile
    {
        public bool status { get; set; }
        public string message { get; set; }
        public UserData userProfileData { get; set; }
    }
    #endregion
    public class ChatUser
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class AddChatUser
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    }
}
