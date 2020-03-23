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
using System.Linq;
using Worker_7ERFAcraft.Resx;

namespace Worker_7ERFAcraft.ViewModels
{
    public class CancellationReasonsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region Country 
        private ObservableCollection<string> _reasonList;
        public ObservableCollection<string> ReasonList
        {
            get
            {
                return _reasonList;

            }
            set
            {
                _reasonList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ReasonList"));
            }
        }

        public string selectedReason { get; set; }
        public List<CancellationReasons> lstReasons { get; set; }

        string _selectedReasonItem;
        public string SelectedReasonItem
        {
            get
            {
                return _selectedReasonItem;
            }
            set
            {
                if (value != null)
                {
                    _selectedReasonItem = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedReasonItem"));
                    if (_selectedReasonItem != null)
                    {
                        if (lstReasons != null && lstReasons.Count > 0)
                        {
                            selectedReason = lstReasons.Where(x => x.Reason == _selectedReasonItem)
                                .FirstOrDefault().Reason;
                        }
                    }
                }
            }
        }
        #endregion

        
        
        private string _otherReason;
        public string OtherReason
        {
            get
            {
                return _otherReason;
            }
            set
            {
                _otherReason = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OtherReason"));
            }
        }

         

         

        INavigation NavigationService;
        string _orderId;
        public CancellationReasonsViewModel(INavigation navigation,string orderId)
        {
            _orderId = orderId;
            NavigationService = navigation;
            getReasons();
        }


        public async void getReasons()
        {
            if (!Common.CheckConnection())
            {
                await NavigationService.PushPopupAsync(new NoInternetPopup());
                return;
            }
            else
            {
                try
                {
                    var cbase = new HttpClientBase();
                    var result = await cbase.GetCancellationReasons (ApiUrl.GetCancellationReasonsUrl);
                    if (result != null)
                    {
                        if (result.status)
                        {
                            lstReasons = result.cancellationReasonsData;
                        }

                        var _countryList = new ObservableCollection<string>();
                        foreach (var item in lstReasons)
                        {
                            _countryList.Add(item.Reason);
                        }
                        ReasonList = _countryList;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public Command cancelBtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    string msg = string.Empty;
                    if (string.IsNullOrEmpty(selectedReason) && string.IsNullOrEmpty(OtherReason))
                    {
                        msg += AppResources.ChooseCancellationReasonorEnterOtherReason + Environment.NewLine;
                        await NavigationService.PushPopupAsync(new ShowMessage(msg));
                        await Task.Delay(1000);
                        await NavigationService.PopPopupAsync();
                        return;
                    }
                    string cancelReason = !string.IsNullOrEmpty(selectedReason) ? selectedReason: OtherReason ;
                    cancelOrder(cancelReason); 
                });
            }
        }

        public async void cancelOrder( string cancelReason)
        {
            if (!Common.CheckConnection())
            {
                await NavigationService.PushPopupAsync(new NoInternetPopup());
                return;
            }
            else
            {
                try
                {
                    await NavigationService.PushPopupAsync(new Loader());
                    HttpClientBase cbase = new HttpClientBase();
                    string Url = ApiUrl.CancelOrderByCustomerUrl;
                    Url += "?orderId=" + _orderId + "&cancelReason=" + cancelReason;
                    var result = await cbase.CancelOrder(Url);
                    if (result != null)
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.getMsg(result.notificationMessage), AppResources.Ok);
                        if (result.status)
                        {
                            CancellationReasonsPopup.isCancelDone = true;
                            CancellationReasonsPopup.ClosePopup();
                        }
                    }
                    else
                    {
                        Loader.CloseAllPopup();
                        await App.Current.MainPage.DisplayAlert("", Common.someErrorMsg, AppResources.Ok);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }






    }
}
