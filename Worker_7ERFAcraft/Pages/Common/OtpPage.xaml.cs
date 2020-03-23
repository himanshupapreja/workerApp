using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtpPage : ContentPage
    {
        string  userId = string.Empty;
        string otp = string.Empty;
        Lng lng;
        public OtpPage(string _userId, string _otp)
        {
            InitializeComponent(); 
            userId = _userId;
            otp = _otp;
             
            Title = Resx.AppResources.OTPVerification; 
            txtFirstNumber.Focus();

            lng = App.Database.GetLng();
            //if (lng != null && !string.IsNullOrEmpty(lng.Language))
            //{
            //    if (lng.Language == Models.CultureLanguage.Arabic || lng.Language == Models.CultureLanguage.Urdu)
            //    {
            //        this.FlowDirection = FlowDirection.RightToLeft;
            //    }
            //    else
            //    {
            //        this.FlowDirection = FlowDirection.LeftToRight;
            //    }
            //}
            //else
            //{
            //    this.FlowDirection = FlowDirection.LeftToRight;
            //}
        }
        private void ResendOtp_Tapped(object sender, EventArgs e)
        {
            ResendOtp();
        }

        private async void done_Clicked(object sender, EventArgs e)
        {
            //Validation Part 
            if (string.IsNullOrEmpty(txtFirstNumber.Text))
            {
                txtFirstNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSecondNumber.Text))
            {
                txtSecondNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtThirdNumber.Text))
            {
                txtThirdNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFourthNumber.Text))
            {
                txtFourthNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFifthNumber.Text))
            {
                txtFifthNumber.Focus();
                return;
            } 
            try
            {
                if (!Common.CheckConnection())
                {
                    await Navigation.PushPopupAsync(new NoInternetPopup());
                    return;
                }
                string otp = txtFirstNumber.Text.ToString() + txtSecondNumber.Text.ToString() + txtThirdNumber.Text.ToString() + txtFourthNumber.Text.ToString() + txtFifthNumber.Text.ToString();
                int newotp = Convert.ToInt32(otp.Trim());
                await Navigation.PushPopupAsync(new Loader());

                string postData = "userId=" + userId + "&otp=" + newotp;
                string Url = ApiUrl.PhoneVerifyUrl;
                HttpClientBase cbase = new HttpClientBase();
                var result = await cbase.VerifyOtp(Url+postData, "");
                if (result != null)
                {
                    Loader.CloseAllPopup();
                    await DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                    if (result.status)
                    {
                        LoggedInUser objLoggedInUser = new LoggedInUser();
                        objLoggedInUser.userId = result.userData.UserId;
                        objLoggedInUser.phone = result.userData.PhoneNumber;
                        objLoggedInUser.name = result.userData.Name;
                        objLoggedInUser.userType = result.userData.UserType ?? (int)UserType.Customer;
                        objLoggedInUser.image = result.userData.Image;

                        App.Database.SaveUpdateLoggedInUser(objLoggedInUser);
                        LoginUserDetails.userId = result.userData.UserId;
                        LoginUserDetails.name = result.userData.Name;
                        LoginUserDetails.phone = result.userData.PhoneNumber;
                        LoginUserDetails.userType = result.userData.UserType ?? (int)UserType.Customer;
                        LoginUserDetails.image = result.userData.Image; 

                        App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                    }
                    else 
                    {
                        txtFirstNumber.Text = "";
                        txtSecondNumber.Text = "";
                        txtThirdNumber.Text = "";
                        txtFourthNumber.Text = "";
                        txtFifthNumber.Text = "";
                        txtFirstNumber.Focus();
                    } 
                }
                else
                {
                    Loader.CloseAllPopup();
                    await DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                }
            }
            catch (Exception)
            {
                
            }
        }
        
        public async void ResendOtp()
        {
            try
            {
                if (!Common .CheckConnection())
                {
                    await Navigation.PushPopupAsync(new NoInternetPopup());
                    return;
                }
                HttpClientBase cbase = new HttpClientBase();
                await Navigation.PushPopupAsync(new Loader());
                string Url = ApiUrl.ResendOtpUrl + "userId=" + userId ;
                var result = await cbase.VerifyOtp(Url,"");
                if (result != null)
                {
                    Loader.CloseAllPopup();
                  
                    await DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                    await DisplayAlert("", result.userData.OTP, AppResources.Ok);
                    if (result.status)
                    {
                        txtFirstNumber.Text = "";
                        txtSecondNumber.Text = "";
                        txtThirdNumber.Text = "";
                        txtFourthNumber.Text = "";
                        txtFifthNumber.Text = ""; 
                         
                    }
                    
                    txtFirstNumber.Focus();
                }
                else
                {
                    Loader.CloseAllPopup();
                    await DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                }
            }
            catch (Exception)
            {
              
            }
        }
        private void Back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        //Auto cursor implementation

        private void click_otp1(object sender, EventArgs e)
        {
            string _text = txtFirstNumber.Text;
            if (txtFirstNumber.Text.Length >= 1)
            {
                if (txtFirstNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtFirstNumber.Text = _text;
                }
                txtSecondNumber.Focus();

            }
            else
            {
                txtFirstNumber.Focus();
            }
        }
        private void click_otp2(object sender, EventArgs e)
        {
            string _text = txtSecondNumber.Text;
            if (txtSecondNumber.Text.Length >= 1)
            {
                if (txtSecondNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtSecondNumber.Text = _text;
                }
                txtThirdNumber.Focus();
            }
            else
            {
                txtFirstNumber.Focus();
            }
        }
        private void click_otp3(object sender, EventArgs e)
        {
            string _text = txtThirdNumber.Text;
            if (txtThirdNumber.Text.Length >= 1)
            {
                if (txtThirdNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtThirdNumber.Text = _text;
                }
                txtFourthNumber.Focus();

            }
            else
            {
                txtSecondNumber.Focus();
            }
        }
        private void click_otp4(object sender, EventArgs e)
        {
            string _text = txtFourthNumber.Text;
            if (txtFourthNumber.Text.Length >= 1)
            {
                if (txtFourthNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtFourthNumber.Text = _text;

                }

                txtFifthNumber.Focus();
            }
            else
            {
                txtThirdNumber.Focus();
            }
        }
        private void click_otp5(object sender, EventArgs e)
        {
            string _text = txtFifthNumber.Text;
            if (txtFifthNumber.Text.Length >= 1)
            {
                if (txtFifthNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtFifthNumber.Text = _text;

                }
            }
            else
            {
                txtFourthNumber.Focus();
            }
        }
    }
}