using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Syncfusion.XForms.ProgressBar;
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
    public partial class LoaderWithPer : PopupPage
    {
        public static bool isLoading=false;
       // public static SfLinearProgressBar sfLinearProgressBar;
       // public static SfCircularProgressBar sfCircularProgressBar; 
        public LoaderWithPer()
        {
            InitializeComponent();
            isLoading = true;
            //sfLinearProgressBar = linearProgressBar;
            //sfCircularProgressBar = circularProgressBar;

            MessagingCenter.Subscribe<AddWorkDetailsViewModel>(this, "UpdateLoaderValues", (sender) =>
            {
                circularProgressBar.Progress = AddWorkDetailsViewModel.loaderPer;
                circularProgressBar.Maximum = 100;

                MessagingCenter.Unsubscribe<AddWorkDetailsViewModel>(sender, "UpdateLoaderValues");
            });


        }
        public static async void CloseAllPopup()
        {
            isLoading = false;
            await App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
    }
}