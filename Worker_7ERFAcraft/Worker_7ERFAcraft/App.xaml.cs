using Worker_7ERFAcraft.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Worker_7ERFAcraft.Data;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using System.Collections.Generic;
using Plugin.FirebasePushNotification;
using Worker_7ERFAcraft.Resx;
using System.Globalization;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Worker_7ERFAcraft
{
    public partial class App : Application
    {
        public static bool IsSearch = false;

        public static bool IsCustomerHomeScreen = false;
        public static bool IsCustomerWorkerListScreen = false;
        public static Xamarin.Forms.Maps.Position oldPos;

        public static bool IsHomeScreen = false;
        public static HomeData objHomeData;
        public static List<HomeAdsData> lstHomeAdsData;
        public static double ScreenWidth;
        public static double ScreenHeight;
        public static string latitude = "0";
        public static string longitude = "0";
        public static string DeviceToken = string.Empty;
        LoggedInUser getLoginUser;
        public App()
        {
           // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTU2NDlAMzEzNjJlMzQyZTMwZXk5dGdDamdQK1NpWjBweDAyeHFXdlJOMjFuMFkvVTlaWTRoY21xcG1LQT0=");
            InitializeComponent();
            getLoginUser = App.Database.GetLoggedInUser();
            Common.GetLocation();
            //MainPage = new NavigationPage(new OtpPage());

            L10n.SetLocale();
            var netLanguage = DependencyService.Get<ILocale>().GetCurrent(); 
            AppResources.Culture = new CultureInfo(netLanguage);

            var lData = App.Database.GetLng();
            if (lData == null)
            {
                
                MainPage = new NavigationPage(new FirstLanguagePage());
            }
            else
            {
                GetMainPage();
            }
          
        }
        public void GetMainPage()
        {
            if (getLoginUser != null)
            {
                if (!string.IsNullOrEmpty(getLoginUser.phone))
                {
                    LoginUserDetails.image = getLoginUser.image;
                    LoginUserDetails.phone = getLoginUser.phone;
                    LoginUserDetails.name = getLoginUser.name;
                    LoginUserDetails.userId = getLoginUser.userId;
                    LoginUserDetails.userType = getLoginUser.userType;

                    MainPage = new NavigationPage(new HomeVideoPage());
                    //MainPage = new NavigationPage(new HomeMasterPage());
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                MainPage = new NavigationPage(new  LoginPage());
            }
        }
    

        static DB_7ERFAcraft database;
        public static DB_7ERFAcraft Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new DB_7ERFAcraft(DependencyService.Get<IFileHelper>().GetLocalFilePath("DB_7ERFAcraft.db3"));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return database;
            }
        }
        public static async void UpdateDeviceInfo()
        {
            try
            {
                if (!string.IsNullOrEmpty(App.DeviceToken) && LoginUserDetails.userId!=0)
                {
                    var DeviceId = Device.RuntimePlatform == Device.Android ? 1 : 2;
                    HttpClientBase cbase = new HttpClientBase();
                    var result = await cbase.UpdateDeviceInfo(ApiUrl.UpdateDeviceInfoUrl + "?userId=" + LoginUserDetails.userId
                        + "&DeviceId=" + DeviceId + "&DeviceToken=" + App.DeviceToken + "&latitude=" +App.latitude+ "&longitude="+App.longitude);
                }
            }
            catch
            {
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
            CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                if (Device.RuntimePlatform == "Android")
                {
                    App.DeviceToken = p.Token;
                    UpdateDeviceInfo();
                }
                System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            };

            var aa = CrossFirebasePushNotification.Current.Token;
            if (Device.RuntimePlatform == "iOS")
            {
                App.DeviceToken = aa;
                UpdateDeviceInfo();
            }
            System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Received");
                    if (p.Data.ContainsKey("body"))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            // mPage.Message = $"{p.Data["body"]}";
                        });

                    }
                }
                catch (Exception ex)
                {

                }

            };
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
