using System;
using Xamarin.Forms;
using System.Threading;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Models;

[assembly: Dependency(typeof(Worker_7ERFAcraft.Droid.DependencyInterface.Locale_Android))]
namespace Worker_7ERFAcraft.Droid.DependencyInterface
{
    public class Locale_Android : ILocale
    {
        public void SetLocale()
        {
            try
            {
            var androidLocale = Java.Util.Locale.Default; // user's preferred locale
            var netLocale = androidLocale.ToString().Replace("_", "-");
            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch (Exception)
            {

                
            }
        }

        /// <remarks>
        /// Not sure if we can cache this info rather than querying every time
        /// </remarks>
        public string GetCurrent()
        {
            var androidLocale = Java.Util.Locale.Default; // user's preferred locale
 
            var netLanguage = androidLocale.Language.Replace("_", "-");
            var netLocale = androidLocale.ToString().Replace("_", "-");


            try
            {
                 var objLng = App.Database.GetLng();

                if (objLng.Language == CultureLanguage.Arabic)
                {
                    netLanguage = CultureLanguage.Arabic;// "ar-AE";
                    netLocale = CultureLanguage.Arabic;// "ar-AE";
                }
                else if (objLng.Language == CultureLanguage.Bangali)
                {
                    netLanguage = CultureLanguage.Bangali;// "ar-AE";
                    netLocale = CultureLanguage.Bangali;// "ar-AE";
                }
                else if (objLng.Language == CultureLanguage.Hindi)
                {
                    netLanguage = CultureLanguage.Hindi;// "ar-AE";
                    netLocale = CultureLanguage.Hindi;// "ar-AE";
                }
                else if (objLng.Language == CultureLanguage.Urdu)
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
            catch (Exception)
            {
            }



            #region Debugging output
            Console.WriteLine("android:  " + androidLocale.ToString());
            Console.WriteLine("netlang:  " + netLanguage);
            Console.WriteLine("netlocale:" + netLocale);

            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("thread:  " + Thread.CurrentThread.CurrentCulture);
            Console.WriteLine("threadui:" + Thread.CurrentThread.CurrentUICulture);
            #endregion

            return netLocale;
        }
    }
}