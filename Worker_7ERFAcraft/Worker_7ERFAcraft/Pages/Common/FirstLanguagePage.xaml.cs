using System;
using System.Collections.Generic;
using System.Globalization;
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
	public partial class FirstLanguagePage : ContentPage
    {   
        public FirstLanguagePage()
        {   
            InitializeComponent(); 
            NavigationPage.SetHasNavigationBar(this, false);
            pickerLangauge.Title = Resx.AppResources.ChooseLanguage;
            pickerLangauge.Items.Add(AppResources.English);
            pickerLangauge.Items.Add(AppResources.Arabic);
            pickerLangauge.Items.Add(AppResources.Hindi);
            pickerLangauge.Items.Add(AppResources.Urdu);
            pickerLangauge.Items.Add(AppResources.Bangali);
        }

        private void PickerLangauge_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selLanguage = (sender as Picker).SelectedItem.ToString();
            Lng objLng = new Lng();
            if (objLng == null)
            {
                objLng = new Models.Lng();
            }
            if (selLanguage == AppResources.English)
            {
                objLng.Language = CultureLanguage.English;
            }
            else if (selLanguage == AppResources.Arabic)
            {
                objLng.Language = CultureLanguage.Arabic;
            }
            else if (selLanguage == AppResources.Hindi)
            {
                objLng.Language = CultureLanguage.Hindi;
            }
            else if (selLanguage == AppResources.Bangali)
            {
                objLng.Language = CultureLanguage.Bangali;
            }
            else if (selLanguage == AppResources.Urdu)
            {
                objLng.Language = CultureLanguage.Urdu;
            }
            else
            {
                objLng.Language = CultureLanguage.English;
            }
            App.Database.SaveUpdateLng(objLng);
            L10n.SetLocale();
            var netLanguage = DependencyService.Get<Worker_7ERFAcraft.DependencyInterface.ILocale>().GetCurrent();
            AppResources.Culture = new CultureInfo(netLanguage);

            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

    }
}