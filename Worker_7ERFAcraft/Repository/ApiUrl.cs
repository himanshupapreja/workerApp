using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker_7ERFAcraft.Repository
{
    public class ApiUrl
    {
        public const string wsBaseUrl =
           //"https://122.160.233.58:8033/api/";
           "http://appmantechnologies.com:8033/api/";
           //"https://workerapp.oremtechnologies.com/api/";

        #region User Account 
        public const string GetCategories = wsBaseUrl + "account/GetCategories";
        public const string GetCategoriesByUserId = wsBaseUrl + "account/GetCategoriesByUserId?";
        public const string SearchCategoryByUserId = wsBaseUrl + "account/SearchCategoryByUserId?categoryName=";

        public const string GetCountries = wsBaseUrl + "account/GetCountries";
        public const string LoginUrl = wsBaseUrl + "account/Login";
        public const string AddChatUrl = wsBaseUrl + "Account/AddChatRequest";

        public const string SignUpUrl = wsBaseUrl + "account/SignUp";
        public const string PhoneVerifyUrl = wsBaseUrl + "account/PhoneVerify?";
        public const string ResendOtpUrl = wsBaseUrl + "account/ResendOtp?";

        public const string ForgotPasswordUrl = wsBaseUrl + "account/ForgotPassword";

        public const string ChangePasswordUrl = wsBaseUrl + "account/ChangePassword";

        public const string HomeVideoUrl = wsBaseUrl + "account/ScreenMedia";
        public const string PreHomeScreenMediaUrl = wsBaseUrl + "account/PreHomeScreenMedia";
        public const string HomeScreenAdsUrl = wsBaseUrl + "account/HomeScreenAds";

        public const string UpdateDeviceInfoUrl = wsBaseUrl + "account/UpdateDeviceInfo";
        public const string GetForgotPassContDeatilsUrl = wsBaseUrl + "account/GetForgotPassContDeatils";
        public const string AppVideoUrl = wsBaseUrl + "account/AppVideo";

        public const string GetUserProfile = wsBaseUrl + "account/ViewProfile?userId=";

        public const string GetTerms = wsBaseUrl + "account/Terms";

        public const string GetPrivacyPolicy = wsBaseUrl + "account/PrivacyPolicy";

        public const string GetAboutUs = wsBaseUrl + "account/AboutUs";

        public const string ContactUs = wsBaseUrl + "account/ContactUs";

        public const string EditProfile = wsBaseUrl + "account/EditProfile"; 


        
        #endregion

        #region Order
        public const string GetWorkersByCatId = wsBaseUrl + "order/WorkersByCategory";
        public const string GetWorkersProfile = wsBaseUrl + "order/WorkerProfile";
        public const string AddWorkDetail = wsBaseUrl + "order/AddOrder";

        public const string WorkerBookingHistory = wsBaseUrl + "order/WorkerBookingHistory?workerId=";
        public const string WorkerBookingOngoing = wsBaseUrl + "order/WorkerBookingOngoing?workerId=";
        public const string WorkerBookingPending = wsBaseUrl + "order/WorkerBookingPending?workerId=";
        public const string GetAccountDataUrl = wsBaseUrl + "order/MyAccount?userId=";
        public const string GetDriverDataUrl = wsBaseUrl + "order/DriverAccount?userId=";
        public const string UpdateAvailabilityUrl = wsBaseUrl + "order/AvailableForJob?userId=";
        public const string AddBalanceUrl = wsBaseUrl + "order/BillPayment";

        public const string AcceptOrderUrl = wsBaseUrl + "order/AcceptOrder?orderId=";
        public const string RejectOrderUrl = wsBaseUrl + "order/RejectOrder?orderId=";
        public const string AddDriverRequestUrl = wsBaseUrl + "/Driver/AddDriverRequest";
        public const string CompleteOrderUrl = wsBaseUrl + "order/CompleteOrder?orderId=";

        public const string CancelOrderByCustomerUrl = wsBaseUrl + "Order/CancelOrder";
        public const string GetCancellationReasonsUrl = wsBaseUrl + "Order/CancellationReasonsList";

        public const string WorkerWorkDetailUrl = wsBaseUrl + "order/WorkerWorkDetail?orderId=";
        public const string WorkDetailUrl = wsBaseUrl + "order/WorkDetail?orderId=";

        public const string CustomerBookingOngoingUrl = wsBaseUrl + "order/CustomerBookingOngoing?customerId=";
        public const string CustomerBookingHistoryUrl = wsBaseUrl + "order/CustomerBookingHistory?customerId=";
        public const string AddRatingUrl = wsBaseUrl + "order/AddRating";
        public const string AddWorkerFeedbackUrl = wsBaseUrl + "order/AddWorkerFeedback";
        public const string ChatUserUrl = wsBaseUrl + "Account/GetChat";

        public const string GetDriverRequestsUrl = wsBaseUrl + "/Driver/GetDriverRequests?driverId=";
        #endregion
    }
}
