using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.CustomControls;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddWorkDetailsPage : ContentPage
	{
        string _workerId = string.Empty;
        int _categoryId =0;
        WorkerModel _workerData;
        public AddWorkDetailsPage (WorkerModel workerData, int categoryId)
		{
			InitializeComponent ();
            _workerData = workerData;

             Title = Resx.AppResources.AddDetails; 
            _categoryId = categoryId;

            BindingContext = new AddWorkDetailsViewModel(Navigation, _workerData, _categoryId);


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
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
            
        //}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}