using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.ViewModels.Worker;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Repository
{
    public class HttpClientBase : HttpClient
    {
        private static readonly HttpClientBase _instance = new HttpClientBase();
        CancellationTokenSource cts;
        static HttpClientBase()
        {

        }
        public HttpClientBase() : base()
        {
            TimeSpan time = new TimeSpan(0, 0, 60);
            Timeout = time;
            cts = new CancellationTokenSource();
            cts.CancelAfter(time);

        }
        public async Task<wsGooglePlaces> getGooglePlaces(string url)
        {
            wsGooglePlaces objData = new wsGooglePlaces();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    TimeSpan time = new TimeSpan(0, 0, 20);
                    client.Timeout = time;
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsGooglePlaces>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        /// <summary>
        /// This is   Update Device Info
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsBase> UpdateDeviceInfo(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsHomeData objData = new wsHomeData();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsHomeData>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give home video
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsHomeData> GetHomeVideo(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsHomeData objData = new wsHomeData();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsHomeData>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give home video
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsAppVideo> GetAppVideo(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsAppVideo objData = new wsAppVideo();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsAppVideo>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give the list of Categories
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsBase> UpdateAvailability(string url,string postData)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsBase objData = new wsBase();
            try
            {
                StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsBase>(await result.Content.ReadAsStringAsync());

            }

            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give the list of Categories
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsAccountData> GetAccountData(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsAccountData objData = new wsAccountData();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsAccountData>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        public async Task<DriverIsAvailableData> GetDriverData(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            DriverIsAvailableData objData = new DriverIsAvailableData();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<DriverIsAvailableData>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give the user payment options
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsCountry> GetPaymentOptions(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsCountry objData = new wsCountry();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsCountry>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give Forgot Password Details
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsForgotPassContDetails> GetForgotPasswordContactDeatils(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsForgotPassContDetails objData = new wsForgotPassContDetails();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsForgotPassContDetails>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData;
            // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give the list of Countries
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsCountry> GetCountries(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsCountry objData = new wsCountry();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsCountry>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; 
            // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give the list of Cancellation Reasons
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsCancelOrder> GetCancellationReasons(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsCancelOrder objData = new wsCancelOrder();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsCancelOrder>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give the list of Categories
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsCategories> GetCategories(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsCategories objData = new wsCategories();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsCategories>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }

        /// <summary>
        /// This is the method that will perform the login operation in the application.
        /// we are sending the post data here which includes  phone, password.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsUser> Login(string url, string postData)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsUser objData = new wsUser();
            try
            {
                StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsUser>(await result.Content.ReadAsStringAsync());

            }

            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        public async Task<ChatUser> AddChat(string url, string postData)
        {
            // making the object of the response class which will use for deserialization of the response.
            ChatUser objData = new ChatUser();
            try
            {
                StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<ChatUser>(await result.Content.ReadAsStringAsync());

            }

            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        public async Task<wsUser> VerifyOtp(string url, string postData)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsUser objData = new wsUser();
            try
            {
                StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsUser>(await result.Content.ReadAsStringAsync());

            }

            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will perform the forgot password operation in the application.
        /// we are sending the post data here which includes email.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<wsBase> ForgotPassword(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsBase objData = new wsBase();
            try
            {
                StringContent str = new StringContent(url, Encoding.UTF8, "application/x-www-form-urlencoded");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsBase>(await result.Content.ReadAsStringAsync());

            }

            catch (Exception) { }
            finally { }
            return objData; // returning the object of the response
        }
        #region Order
        /// <summary>
        /// This is the method that will give the list of Workers ByCat Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsWorkerList> GetWorkersByCatId(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsWorkerList objData = new wsWorkerList();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsWorkerList>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }

        /// <summary>
        /// This is the method that will give the list of Workers ByCat Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsWorkerProfile> GetWorkerProfile(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsWorkerProfile objData = new wsWorkerProfile();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsWorkerProfile>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }


        /// <summary>
        /// This is the method that will give the list of Workers ByCat Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsOrder> GetWorkerOrders(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsOrder objData = new wsOrder();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                        objData = JsonConvert.DeserializeObject<wsOrder>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }


        public async Task<DriverOrderModel> GetDriverOrders(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            DriverOrderModel objData = new DriverOrderModel();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<DriverOrderModel>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }

        /// <summary>
        /// This is the method that will give the list of Workers ByCat Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsCustomerBooking> GetCustomerBookings(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsCustomerBooking objData = new wsCustomerBooking();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsCustomerBooking>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give the list of Workers ByCat Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsBase> AcceptRejectOrder(string url,string postData="")
        {
            // making the object of the response class which will use for deserialization of the response.
            wsBase objData = new wsBase();
            try
            {
                StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsBase>(await result.Content.ReadAsStringAsync());
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }

        public async Task<DriverListModel> AddDriverRequest(string url, AddRequestModel addRequestModel)
        {
            // making the object of the response class which will use for deserialization of the response.
            DriverListModel objData = new DriverListModel();
            try
            {
                string json = JsonConvert.SerializeObject(addRequestModel);
                StringContent str = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<DriverListModel>(await result.Content.ReadAsStringAsync());
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }


        /// <summary>
        /// This is the method that will give the list of Workers ByCat Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsBase> CancelOrder(string url, string postData = "")
        {
            // making the object of the response class which will use for deserialization of the response.
            wsBase objData = new wsBase();
            try
            {
                StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsBase>(await result.Content.ReadAsStringAsync());
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }

        /// <summary>
        /// This is the method that will give the list of Workers ByCat Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsBase> RateWorker(string url, string postData = "")
        {
            // making the object of the response class which will use for deserialization of the response.
            wsBase objData = new wsBase();
            try
            {
                StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsBase>(await result.Content.ReadAsStringAsync());
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }

        /// <summary>
        /// This is the method that will give the list of Workers ByCat Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsOrderDetail> GetOrderDetail(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsOrderDetail objData = new wsOrderDetail();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsOrderDetail>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }
        #endregion




        #region User Account

        /// <summary>
        /// This is the method that will allow us to get the terms and conditions data from the api.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<TermsConditionsModel> GetTermsData(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            TermsConditionsModel objData = new TermsConditionsModel();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<TermsConditionsModel>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception) { }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will allow us to get the PrivacyPolicy data from the api.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<PrivacyPolicyModel> GetPrivacyPolicyData(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            PrivacyPolicyModel objData = new PrivacyPolicyModel();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<PrivacyPolicyModel>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception) { }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will perform the change password operation in the application.
        /// we are sending the post data here which includes userId, old password,new password.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public async Task<wsBase> ChangePassword(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsBase objData = new wsBase();
            try
            {
                var result = await GetAsync(new Uri(url));
                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsBase>(await result.Content.ReadAsStringAsync());

            }

            catch (Exception) { }
            finally { }
            return objData; // returning the object of the response
        }

        public async Task<wsBase> ContactUs(string url, ContactUsModel contactUsModel)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsBase objData = new wsBase();
            try
            {
                var postData = JsonConvert.SerializeObject(contactUsModel);

                StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                var result = await PostAsync(new Uri(url), str);

                var place = result.Content.ReadAsStringAsync().Result;
                // deserialization of the response returning from the api
                objData = JsonConvert.DeserializeObject<wsBase>(await result.Content.ReadAsStringAsync());

            }

            catch (Exception) { }
            finally { }
            return objData; // returning the object of the response
        }
        /// <summary>
        /// This is the method that will give the profile data of logged in user.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsProfile> GetUserProfile(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsProfile objData = new wsProfile();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsProfile>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex) 
            {
            }
            finally { }
            return objData; // returning the object of the response
        }



         
        /// <summary>
        /// This is the method that will allow us to get the about us data from the api. 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<wsAboutUs> GetAboutUsData(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            wsAboutUs objData = new wsAboutUs();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<wsAboutUs>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception) { }
            finally { }
            return objData; // returning the object of the response
        }


        public async Task<ChatUserList> GetChatUsers(string url)
        {
            // making the object of the response class which will use for deserialization of the response.
            ChatUserList objData = new ChatUserList();
            try
            {
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    var stream = result.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<ChatUserList>(content);
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
            }
            finally { }
            return objData; // returning the object of the response
        }


        /*




         /// <summary>
         /// This is the method which will allow us to edit the profile of the user.
         /// </summary>
         /// <param name="url"></param>
         /// <param name="postData"></param>
         /// <returns></returns>
         public async Task<EditProfileReponse> EditProfile(string url, string postData)
         {
             // making the object of the response class which will use for deserialization of the response.
             EditProfileReponse objData = new EditProfileReponse();
             try
             {
                 // StringContent str = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
                 StringContent str = new StringContent(postData, Encoding.UTF8, "application/json");
                 var result = await PostAsync(new Uri(url), str);
                 var place = result.Content.ReadAsStringAsync().Result;
                 // deserialization of the response returning from the api
                 objData = JsonConvert.DeserializeObject<EditProfileReponse>(await result.Content.ReadAsStringAsync());

             }

             catch (Exception) { }
             finally { }
             return objData; // returning the object of the response
         }
         */

        #endregion






    }
}
