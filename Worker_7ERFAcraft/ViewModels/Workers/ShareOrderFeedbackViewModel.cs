using System;
using System.ComponentModel;
using System.Threading.Tasks; 
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Repository;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class ShareOrderFeedbackViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
          

        
        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Comment"));
            }
        }

         

         

        INavigation NavigationService; 
        public ShareOrderFeedbackViewModel(INavigation navigation)
        { 
            NavigationService = navigation;  
        }

     



        public Command submitBtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    string msg = string.Empty; 
                    if (string.IsNullOrEmpty(Comment) )
                    {
                        msg +=  AppResources.EnterComments + Environment.NewLine; 
                    }
                    if (!string.IsNullOrEmpty(msg))
                    { 
                        await NavigationService.PushPopupAsync(new ShowMessage(msg));
                        await Task.Delay(1000);
                        await NavigationService.PopPopupAsync();
                        return;
                    }
                    DependencyService.Get<DependencyInterface.IShare>().Share(Comment);
                    Loader.CloseAllPopup();
                });
            }
        }


     


       


    }
}
