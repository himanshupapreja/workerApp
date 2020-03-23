using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Plugin.Media;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Globalization;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    
    public class ChangeLanguageViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;  
  
      
        private ImageSource _imageEnglish = "ic_off.png";
        public ImageSource ImageEnglish
        {
            get
            {
                return _imageEnglish;
            }
            set
            {
                _imageEnglish = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageEnglish"));
            }
        }

        private ImageSource _imageArabic = "ic_off.png";
        public ImageSource ImageArabic
        {
            get
            {
                return _imageArabic;
            }
            set
            {
                _imageArabic = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageArabic"));
            }
        }
        private ImageSource _imageHindi = "ic_off.png";
        public ImageSource ImageHindi
        {
            get
            {
                return _imageHindi;
            }
            set
            {
                _imageHindi = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageHindi"));
            }
        }
        private ImageSource _imageUrdu = "ic_off.png";
        public ImageSource ImageUrdu
        {
            get
            {
                return _imageUrdu;
            }
            set
            {
                _imageUrdu = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageUrdu"));
            }
        }
        private ImageSource _imageBangali = "ic_off.png";
        public ImageSource ImageBangali
        {
            get
            {
                return _imageBangali;
            }
            set
            {
                _imageBangali = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageBangali"));
            }
        }
        Lng objLan;
        public ChangeLanguageViewModel(INavigation navigation)
        { 
            _navigation = navigation;
             objLan = App.Database.GetLng();
            if(objLan !=null )
            {
                if(objLan.Language == CultureLanguage.Arabic)
                {
                    _imageArabic = "ic_on.png";
                    selectedLanguage = CultureLanguage.Arabic;
                }
                else if (objLan.Language == CultureLanguage.Bangali)
                {
                    _imageBangali = "ic_on.png";
                    selectedLanguage = CultureLanguage.Bangali;
                }
                else if (objLan.Language == CultureLanguage.English)
                {
                    _imageEnglish = "ic_on.png";
                    selectedLanguage = CultureLanguage.English;
                }
                else if (objLan.Language == CultureLanguage.Hindi)
                {
                    _imageHindi = "ic_on.png";
                    selectedLanguage = CultureLanguage.Hindi;
                }
                else if (objLan.Language == CultureLanguage.Urdu)
                {
                    _imageUrdu = "ic_on.png";
                    selectedLanguage = CultureLanguage.Urdu;
                } 
            }
        }




        string selectedLanguage = string.Empty;
       
       
        public Command BangaliCommand
        {
            get
            {
                return new Command((e) =>
                {
                    ImageArabic = "ic_off.png";
                    ImageBangali = "ic_on.png";
                    ImageEnglish = "ic_off.png";
                    ImageHindi = "ic_off.png";
                    ImageUrdu = "ic_off.png"; 
                    selectedLanguage = CultureLanguage.Bangali;
                    if (objLan == null)
                    {
                        objLan = new Lng();
                    }
                    objLan.Language = selectedLanguage;
                    App.Database.SaveUpdateLng(objLan);
                    L10n.SetLocale();
                    var netLanguage = DependencyService.Get<Worker_7ERFAcraft.DependencyInterface.ILocale>().GetCurrent();
                    AppResources.Culture = new CultureInfo(netLanguage);

                        App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                   
                });
            }
        }

        public Command UrduCommand
        {
            get
            {
                return new Command((e) =>
                {
                    ImageArabic = "ic_off.png";
                    ImageBangali = "ic_off.png";
                    ImageEnglish = "ic_off.png";
                    ImageHindi = "ic_off.png";
                    ImageUrdu = "ic_on.png";
                    selectedLanguage = CultureLanguage.Urdu;
                    if (objLan == null)
                    {
                        objLan = new Lng();
                    }
                    objLan.Language = selectedLanguage;

                    App.Database.SaveUpdateLng(objLan);
                    L10n.SetLocale();
                    var netLanguage = DependencyService.Get<Worker_7ERFAcraft.DependencyInterface.ILocale>().GetCurrent();
                    AppResources.Culture = new CultureInfo(netLanguage);

                        App.Current.MainPage = new NavigationPage(new HomeMasterPage());

                });
            }
        }

        public Command HindiCommand
        {
            get
            {
                return new Command((e) =>
                {
                    ImageArabic = "ic_off.png";
                    ImageBangali = "ic_off.png";
                    ImageEnglish = "ic_off.png";
                    ImageHindi = "ic_on.png";
                    ImageUrdu = "ic_off.png";
                    selectedLanguage = CultureLanguage.Hindi;
                    if (objLan == null)
                    {
                        objLan = new Lng();
                    }
                    objLan.Language = selectedLanguage;
                    App.Database.SaveUpdateLng(objLan);
                    L10n.SetLocale();
                    var netLanguage = DependencyService.Get<Worker_7ERFAcraft.DependencyInterface.ILocale>().GetCurrent();
                    AppResources.Culture = new CultureInfo(netLanguage);
                   
                        App.Current.MainPage = new NavigationPage(new HomeMasterPage());

                });
            }
        }
        public Command ArabicCommand
        {
            get
            {
                return new Command((e) =>
                {
                    ImageArabic = "ic_on.png";
                    ImageBangali = "ic_off.png";
                    ImageEnglish = "ic_off.png";
                    ImageHindi = "ic_off.png";
                    ImageUrdu = "ic_off.png";
                    selectedLanguage = CultureLanguage.Arabic;
                    if (objLan == null)
                    {
                        objLan = new Lng();
                    }
                    objLan.Language = selectedLanguage;
                    App.Database.SaveUpdateLng(objLan);
                    L10n.SetLocale();
                    var netLanguage = DependencyService.Get<Worker_7ERFAcraft.DependencyInterface.ILocale>().GetCurrent();
                    AppResources.Culture = new CultureInfo(netLanguage);
                   
                        App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                   
                });
            }
        }
        public Command EnglishCommand
        {
            get
            {
                return new Command((e) =>
                {
                    ImageArabic = "ic_off.png";
                    ImageBangali = "ic_off.png";
                    ImageEnglish = "ic_on.png";
                    ImageHindi = "ic_off.png";
                    ImageUrdu = "ic_off.png";
                    selectedLanguage = CultureLanguage.English;
                    if(objLan ==null)
                    {
                        objLan = new Lng();
                    }
                    objLan.Language = selectedLanguage; L10n.SetLocale();
                    App.Database.SaveUpdateLng(objLan);
                    L10n.SetLocale();
                    var netLanguage = DependencyService.Get<Worker_7ERFAcraft.DependencyInterface.ILocale>().GetCurrent();
                    AppResources.Culture = new CultureInfo(netLanguage);
                   
                        App.Current.MainPage = new NavigationPage(new HomeMasterPage());

                });
            }
        }
    }
}

