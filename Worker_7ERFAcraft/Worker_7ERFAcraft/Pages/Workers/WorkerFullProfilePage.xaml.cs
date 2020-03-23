using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkerFullProfilePage : ContentPage
	{
        string _workerId = string.Empty;
        int _categoryId = 0;
        public WorkerFullProfilePage(string workerId,int categoryId)
        {
            InitializeComponent();
            _workerId = workerId;
            _categoryId = categoryId;

            Title =Resx.AppResources.WorkerProfile;// "Worker Profile";
            BindingContext = new WorkerFullProfileViewModel(Navigation, _workerId, _categoryId);

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

       
    }
}