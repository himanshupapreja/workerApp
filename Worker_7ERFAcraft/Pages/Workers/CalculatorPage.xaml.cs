using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorPage : ContentPage
	{
		public CalculatorPage ()
		{
			InitializeComponent ();
            Title = Resx.AppResources.Calculator;


            var lng = App.Database.GetLng();
            if (lng != null && !string.IsNullOrEmpty(lng.Language))
            {
                if (lng.Language == Models.CultureLanguage.Arabic || lng.Language == Models.CultureLanguage.Urdu)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                }
                else
                {
                    this.FlowDirection = FlowDirection.LeftToRight;
                }
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void TxtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblResults.Text = string.Empty;
            if (txtAmount.Text != string.Empty)
            {
                double val;
                double.TryParse(txtAmount.Text,out val);
                lblResults.Text = "5% of "+ txtAmount.Text +" = "+ ((val * 5) / 100).ToString();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            HomeMasterPage._masterPage.Detail = new
                NavigationPage((Page)Activator.CreateInstance(typeof(WorkerHomePage)));
            return true;
        }


    }
}