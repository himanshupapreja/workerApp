using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.iOS.DependencyInterface;
using UIKit;
using Xamarin.Forms;
using Worker_7ERFAcraft.Models;

[assembly: Dependency(typeof(Locale_iOS))]
namespace Worker_7ERFAcraft.iOS.DependencyInterface
{
    public class Locale_iOS : ILocale
    {
        public void SetLocale()
        {
            try
            {
                var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
                var netLocale = iosLocaleAuto.Replace("_", "-");
                var ci = new System.Globalization.CultureInfo(netLocale);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch
            {
            }
        }
        public string GetCurrent()
        {
            var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
            var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;

            var netLocale = iosLocaleAuto.Replace("_", "-");
            var netLanguage = iosLanguageAuto.Replace("_", "-");

            try
            {
                var sqlLiteResult = App.Database.GetLng();
                if (sqlLiteResult != null)
                {
                    if (sqlLiteResult.Language == CultureLanguage.Arabic)
                    {
                        netLanguage = CultureLanguage.Arabic;// "ar-AE";
                        netLocale = CultureLanguage.Arabic;// "ar-AE";
                    }
                    else if (sqlLiteResult.Language == CultureLanguage.Bangali)
                    {
                        netLanguage = CultureLanguage.Bangali;// "ar-AE";
                        netLocale = CultureLanguage.Bangali;// "ar-AE";
                    }
                    else if (sqlLiteResult.Language == CultureLanguage.Hindi)
                    {
                        netLanguage = CultureLanguage.Hindi;// "ar-AE";
                        netLocale = CultureLanguage.Hindi;// "ar-AE";
                    }
                    else if (sqlLiteResult.Language == CultureLanguage.Urdu)
                    {
                        netLanguage = CultureLanguage.Urdu;// "ar-AE";
                        netLocale = CultureLanguage.Urdu;// "ar-AE";
                    }
                    else
                    {
                        netLanguage = CultureLanguage.English;
                        netLocale = CultureLanguage.English;
                    }
                }
                else
                {
                    netLanguage = CultureLanguage.English;
                    netLocale = CultureLanguage.English;
                }
            }
            catch (Exception)
            {
            }

            #region Debugging Info


            Console.WriteLine("nslocaleid:" + iosLocaleAuto);
            Console.WriteLine("nslanguage:" + iosLanguageAuto);
            Console.WriteLine("ios:" + iosLanguageAuto + " " + iosLocaleAuto);
            Console.WriteLine("net:" + netLanguage + " " + netLocale);

            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("thread:  " + Thread.CurrentThread.CurrentCulture);
            Console.WriteLine("threadui:" + Thread.CurrentThread.CurrentUICulture);
            #endregion

            //if (NSLocale.PreferredLanguages.Length > 0)
            //{
            //    var pref = NSLocale.PreferredLanguages[0];
            //    netLanguage = pref.Replace("_", "-");
            //    Console.WriteLine("preferred:" + netLanguage);
            //}
            //else
            //{
            //    netLanguage = "en"; // default, shouldn't really happen :)
            //}
            return netLanguage;
        }
    }
}