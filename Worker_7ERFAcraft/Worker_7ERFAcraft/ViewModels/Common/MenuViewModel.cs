using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Net.Http;
using Worker_7ERFAcraft.Pages.Driver;

namespace Worker_7ERFAcraft.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;  
        public event PropertyChangedEventHandler PropertyChanged;

        private ImageSource _userImage;
        public ImageSource UserImage
        {
            get
            {
                return _userImage;

            }
            set
            {
                _userImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserImage"));
            }
        }
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;

            }
            set
            {
                _userName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }

        private List<MasterPageItem> _menuList;
        public List<MasterPageItem> MenuList
        {
            get
            {
                return _menuList;

            }
            set
            {
                _menuList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MenuList"));
            }
        }
        int _oldIndex;
        MasterPageItem _menuSelectedItem;
        public MasterPageItem MenuSelectedItem
        {
            get
            {
                return _menuSelectedItem;
            }
            set
            {
                _menuSelectedItem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MenuSelectedItem"));
                try
                {


                    if (_menuSelectedItem != null)
                    {
                        MenuList[_oldIndex].BGColor = AppColor.bgColor;
                        MenuList[_oldIndex].TxtColor = Color.Gray;
                        MenuList[_oldIndex].Icon = getIcon(MenuList[_oldIndex].Title, false );


                        _menuSelectedItem.BGColor = AppColor.mainColor;
                        _menuSelectedItem.TxtColor = Color.White;
                        _menuSelectedItem.Icon = getIcon(_menuSelectedItem.Title, true);

                        var oldIndex =
                    _oldIndex = MenuList.IndexOf(_menuSelectedItem);
                        if (_menuSelectedItem.TargetType == typeof(Logout))
                        {
                            _logOut();
                            _menuSelectedItem = null;
                            HomeMasterPage._masterPage.IsPresented = false;
                            return;
                        }
                        else if (_menuSelectedItem.TargetType == typeof(LoginPage))
                        {
                            _menuSelectedItem = null;
                            HomeMasterPage._masterPage.IsPresented = false;
                            App.Current.MainPage = new NavigationPage(new LoginPage());
                            return;
                        }
                        else
                        {
                            HomeMasterPage._masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(_menuSelectedItem.TargetType));
                            _menuSelectedItem = null;
                            HomeMasterPage._masterPage.IsPresented = false;
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        string getIcon(string title, bool isSelected)
        {
            string iconSource = string.Empty;
            if (title == Resx.AppResources.Home )
            {
                iconSource = isSelected ? "ic_menu_home_select.png" : "ic_menu_home_unselect.png";
            }
            else if(title == Resx.AppResources.Profile)
            {
                iconSource = isSelected ? "ic_menu_profile_select.png" : "ic_menu_profile_unselect.png";
            }
            else if (title == Resx.AppResources.MyBookings)
            {
                iconSource = isSelected ? "ic_menu_my_booking_select.png" : "ic_menu_my_booking_unselect.png";
            }
            else if (title == Resx.AppResources.Help)
            {
                iconSource = isSelected ? "ic_menu_help_select.png" : "ic_menu_help_unselect.png";
            }
            else if (title == Resx.AppResources.Logout)
            {
                iconSource = isSelected ? "ic_menu_logout_select.png" : "ic_menu_logout_unselect.png";
            }
            else if (title == Resx.AppResources.Login)
            {
                iconSource = isSelected ? "login_2.png" : "login_1.png";
            }
            else if (title == Resx.AppResources.MyAccount)
            {
                iconSource = isSelected ? "ic_menu_my_account_select.png" : "ic_menu_my_account_unselect.png";
            }
            else if (title == Resx.AppResources.Chat)
            {
                iconSource = isSelected ? "ic_chat_select.png" : "ic_chat.png";
            }
            else if (title == Resx.AppResources.AppVideo)
            {
                iconSource = isSelected ? "ic_menu_app_video_select.png" : "ic_menu_app_video_unselect.png";
            }
            else if (title == Resx.AppResources.Calculator)
            {
                iconSource = isSelected ? "ic_menu_calculator_select.png" : "ic_menu_calculator_unselect.png";
            }
            else if (title == Resx.AppResources.ContactUs)
            {
                iconSource = isSelected ? "ic_help_contact_us_white.png" : "ic_help_contact_us.png";
            }
            else if (title == Resx.AppResources.PrivacyPolicy)
            {
                iconSource = isSelected ? "ic_help_privacy_white.png" : "ic_help_privacy.png";
            }
            else if (title == Resx.AppResources.TermsConditions)
            {
                iconSource = isSelected ? "ic_help_terms_white.png" : "ic_help_terms.png";
            }
            else if (title == Resx.AppResources.ChangeLangauge)
            {
                iconSource = isSelected ? "ic_change_language_white.png" : "ic_change_language.png";
            }
            return iconSource;
        }
        async void _logOut()
        {
            var answer = await App.Current.MainPage.DisplayAlert(Resx.AppResources.Logout,
               Resx.AppResources.LogoutMsg, Resx.AppResources.Yes, Resx.AppResources.No);
            if (answer)
            {
                App.Database.ClearLoginDetails();
                LoginUserDetails.userId = 0;
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
        public Command CloseCommand
        {
            get
            {
                return new Command((e) =>
                {
                    HomeMasterPage._masterPage.IsPresented = false;
                });
            }
        }
        
        public MenuViewModel(INavigation navigation)
        { 
            _navigation = navigation;

            //MessagingCenter.Subscribe<LoginViewModel>(this, "Guestlogin", (sender) =>
            //{
            //    MenuList[MenuList.Count - 1].Title = Resx.AppResources.Logout;
            //    MenuList[MenuList.Count - 1].Icon = "ic_menu_logout_unselect.png";
            //    MenuList[MenuList.Count - 1].TargetType = typeof(Logout);
            //    MenuList[MenuList.Count - 1].BGColor = AppColor.bgColor;
            //    MenuList[MenuList.Count - 1].TxtColor = AppColor.grayColor;
            //    MenuList.Insert(1,new MasterPageItem
            //    {
            //        Title = Resx.AppResources.Profile,
            //        Icon = "ic_menu_profile_unselect.png",
            //        TargetType = typeof(CustomerProfilePage),
            //        BGColor = AppColor.bgColor,
            //        TxtColor = AppColor.grayColor,
            //    });

            //    MenuList.Insert(2,new MasterPageItem
            //    {
            //        Title = Resx.AppResources.MyBookings,
            //        Icon = "ic_menu_my_booking_unselect.png",
            //        TargetType = typeof(CustomerBookingsPage),
            //        BGColor = AppColor.bgColor,
            //        TxtColor = AppColor.grayColor,
            //    });
            //    RaisePropertyChanged(nameof(MenuList));
            //    MessagingCenter.Unsubscribe<LoginViewModel>(sender, "Guestlogin");
            //});

            MessagingCenter.Subscribe<WorkerProfileViewModel>(this, "Hi", (sender) =>
            {
                if (!string.IsNullOrEmpty(LoginUserDetails.image))
                {
                    UserImage = new UriImageSource()
                    {
                        Uri = new Uri(LoginUserDetails.image),
                        CacheValidity = TimeSpan.FromHours(24),
                        CachingEnabled = true
                    };
                }
                UserName = LoginUserDetails.name;
            });
            MessagingCenter.Subscribe<CustomerProfileViewModel>(this, "Hi", (sender) =>
            {
                if (!string.IsNullOrEmpty(LoginUserDetails.image))
                {
                    UserImage = new UriImageSource()
                    {
                        Uri = new Uri(LoginUserDetails.image),
                        CacheValidity = TimeSpan.FromHours(24),
                        CachingEnabled = true
                    };
                }
                UserName = LoginUserDetails.name;
            });
            if (!string.IsNullOrEmpty(LoginUserDetails.image))
            {
                _userImage = new UriImageSource()
                {
                    Uri = new Uri(LoginUserDetails.image),
                    CacheValidity = TimeSpan.FromHours(24),
                    CachingEnabled = true
                };
            }
            else
            {
                _userImage = "user.png";
            }
            _userName = LoginUserDetails.name;
            _menuList = new List<MasterPageItem>();
            //customer
            if (LoginUserDetails.userId == 0 ||  LoginUserDetails.userType == (int)UserType.Customer)
            { 
                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.Home,
                    Icon = "ic_menu_home_select.png",
                    TargetType = typeof(CustomerHomePage),
                    BGColor=AppColor.mainColor,
                    TxtColor = Color.White,
                });
                if (LoginUserDetails.userId > 0)
                {
                    _menuList.Add(new MasterPageItem
                    {
                        Title = Resx.AppResources.Profile,
                        Icon = "ic_menu_profile_unselect.png",
                        TargetType = typeof(CustomerProfilePage),
                        BGColor = AppColor.bgColor,
                        TxtColor = AppColor.grayColor,
                    });

                    _menuList.Add(new MasterPageItem
                    {
                        Title = Resx.AppResources.MyBookings,
                        Icon = "ic_menu_my_booking_unselect.png",
                        TargetType = typeof(CustomerBookingsPage),
                        BGColor = AppColor.bgColor,
                        TxtColor = AppColor.grayColor,
                    });

                }
            }
            //worker
            else if(LoginUserDetails.userId == 0 || LoginUserDetails.userType == (int)UserType.Worker)
            {

                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.Home,
                    Icon = "ic_menu_home_select.png",
                    TargetType = typeof(WorkerNewHomePage),
                    BGColor = AppColor.mainColor,
                    TxtColor = Color.White,
                });

                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.Profile,
                    Icon = "ic_menu_profile_unselect.png",
                    TargetType = typeof(WorkerProfilePage),
                    BGColor = AppColor.bgColor,
                    TxtColor = AppColor.grayColor,
                });

                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.MyAccount,
                    Icon = "ic_menu_my_account_unselect.png",
                    TargetType = typeof(MyAccountPage),
                    BGColor = AppColor.bgColor,
                    TxtColor = AppColor.grayColor,
                });
                //_menuList.Add(new MasterPageItem
                //{
                //    Title = Resx.AppResources.Chat,
                //    Icon = "ic_chat.png",
                //    TargetType = typeof(WorkerChat),
                //    BGColor = AppColor.bgColor,
                //    TxtColor = AppColor.grayColor,
                //});
                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.Calculator,
                    Icon = "ic_menu_calculator_unselect.png",
                    TargetType = typeof(CalculatorPage),
                    BGColor = AppColor.bgColor,
                    TxtColor = AppColor.grayColor,
                });
            }
            //driver
            else 
            {

                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.Home,
                    Icon = "ic_menu_home_select.png",
                    TargetType = typeof(DriverNewHomePage),
                    BGColor = AppColor.mainColor,
                    TxtColor = Color.White,
                });

                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.Profile,
                    Icon = "ic_menu_profile_unselect.png",
                    TargetType = typeof(DriverProfilePage),
                    BGColor = AppColor.bgColor,
                    TxtColor = AppColor.grayColor,
                });

                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.MyAccount,
                    Icon = "ic_menu_my_account_unselect.png",
                    TargetType = typeof(DriverAccountPage),
                    BGColor = AppColor.bgColor,
                    TxtColor = AppColor.grayColor,
                });
                //_menuList.Add(new MasterPageItem
                //{
                //    Title = Resx.AppResources.Chat,
                //    Icon = "ic_chat.png",
                //    TargetType = typeof(DriverChat),
                //    BGColor = AppColor.bgColor,
                //    TxtColor = AppColor.grayColor,
                //});

            }
            _menuList.Add(new MasterPageItem
            {
                Title = Resx.AppResources.AppVideo,
                Icon = "ic_menu_app_video_unselect.png",
                TargetType = typeof(AppVideoPage),
                BGColor = AppColor.bgColor,
                TxtColor = AppColor.grayColor,
            });
            _menuList.Add(new MasterPageItem
            {
                Title = Resx.AppResources.ContactUs,
                Icon = "ic_help_contact_us.png",
                TargetType = typeof(ContactUsPage),
                BGColor = AppColor.bgColor,
                TxtColor = AppColor.grayColor,
            });
            _menuList.Add(new MasterPageItem
            {
                Title = Resx.AppResources.PrivacyPolicy,
                Icon = "ic_help_privacy.png",
                TargetType = typeof(PrivacyPolicyPage),
                BGColor = AppColor.bgColor,
                TxtColor = AppColor.grayColor,
            });
            _menuList.Add(new MasterPageItem
            {
                Title = Resx.AppResources.TermsConditions,
                Icon = "ic_help_terms.png",
                TargetType = typeof(TermsConditionsPage),
                BGColor = AppColor.bgColor,
                TxtColor = AppColor.grayColor,
            });
            _menuList.Add(new MasterPageItem
            {
                Title = Resx.AppResources.ChangeLangauge,
                Icon = "ic_change_language.png",
                TargetType = typeof(ChangeLanguagePage),
                BGColor = AppColor.bgColor,
                TxtColor = AppColor.grayColor,
            });
            if (LoginUserDetails.userId != 0)
            {
                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.Logout,
                    Icon = "ic_menu_logout_unselect.png",
                    TargetType = typeof(Logout),
                    BGColor = AppColor.bgColor,
                    TxtColor = AppColor.grayColor,
                });
            }
            else
            {
                _menuList.Add(new MasterPageItem
                {
                    Title = Resx.AppResources.Login,
                    Icon = "login_1.png",
                    TargetType = typeof(LoginPage),
                    BGColor = AppColor.bgColor,
                    TxtColor = AppColor.grayColor,
                });
            }
        }
   
        public void CollectionDidChange(object sender,
                                NotifyCollectionChangedEventArgs e)
        {

        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        } 

    }
}

