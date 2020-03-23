//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using Worker_7ERFAcraft.DependencyInterface;
//using Worker_7ERFAcraft.Helpers;
//using Worker_7ERFAcraft.Models;
//using Worker_7ERFAcraft.Pages;
//using Worker_7ERFAcraft.Repository;
//using Worker_7ERFAcraft.Service;
//using Xamarin.Forms;
//using Xamarin.Forms.Extended;

//namespace Worker_7ERFAcraft.ViewModels.Driver
//{
//    public class WorkerChatingViewModel : INotifyPropertyChanged
//    {
//        private bool _isBusy;
//        private const int PageSize = 1;
//        //readonly WorkerChatingService _dataService = new WorkerChatingService();
//        public static int rowCount = 0;
//        //public InfiniteScrollCollection<ChatDetailListModel> Items { get; }

//        public bool IsBusy
//        {
//            get => _isBusy;
//            set
//            {
//                _isBusy = value;
//                OnPropertyChanged();
//            }
//        }
//        private string _msg;
//        public string Msg
//        {
//            get
//            {
//                return _msg;
//            }
//            set
//            {
//                _msg = value;
//                PropertyChanged(this, new PropertyChangedEventArgs("Msg"));
//            }
//        }
//        public Command chat_Tapped
//        {
//            get
//            {
//                return new Command(async (e) =>
//                {
//                    string aa = Msg;

//                    Items.Insert(0, new ChatDetailListModel { IsReciver = false, IsSender = true, RecieverUserId = WorkerChatDetailPage.ReciverId, SenderUserId = Convert.ToInt32(LoginUserDetails.userId), TimeStamp = DependencyService.Get<IGetTimeStamp>().TimeStamp(), UserMessage = aa, UserMessageTime = DateTime.Now.ToString("MM/dd/yyyy, hh:mm:ss tt"), });
//                    MessagingCenter.Send("", "TextFocus");

//                    try
//                    {
//                        if (!string.IsNullOrEmpty(aa) && !string.IsNullOrWhiteSpace(aa))
//                        {

//                            var item = new ChatDetailListModel()
//                            {
//                                SenderUserId = Convert.ToInt32(LoginUserDetails.userId),
//                                RecieverUserId = WorkerChatDetailPage.ReciverId,
//                                UserMessage = aa,
//                                UserMessageTime = DateTime.Now.ToString("MM/dd/yyyy, hh:mm:ss tt"),
//                                IsSender = true,
//                                TimeStamp = DependencyService.Get<IGetTimeStamp>().TimeStamp()
//                            };

//                            try
//                            {
//                                AddChatUser lData = new AddChatUser()
//                                {
//                                    FromUserId = Convert.ToInt32(LoginUserDetails.userId),
//                                    ToUserId = WorkerChatDetailPage.ReciverId
//                                };
//                                string postData = Newtonsoft.Json.JsonConvert.SerializeObject(lData);
//                                string url = ApiUrl.AddChatUrl;
//                                var cbase = new HttpClientBase();
//                                var result = await cbase.AddChat(url, postData);
//                            }
//                            catch (Exception ex)
//                            {
//                            }


//                            var data = await FirebaseHelper.AddChatMessage(item);


//                        }

//                    }
//                    catch (Exception ex)
//                    {

//                    }

//                });
//            }
//        }

//        public WorkerChatingViewModel()
//        {

//            Items = new InfiniteScrollCollection<ChatDetailListModel>
//            {
//                OnLoadMore = async () =>
//                {
//                    IsBusy = true;

//                    // load the next page
//                    var page = Items.Count / PageSize;

//                    var items = await _dataService.GetItemsAsync(page, PageSize);

//                    IsBusy = false;

//                    // return the items that need to be added
//                    return items;
//                },
//                OnCanLoadMore = () =>
//                {
//                    return Items.Count < rowCount;
//                }
//            };

//            DownloadDataAsync();

            
//            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
//            {
//                update();
//                return WorkerChatDetailPage.timer;
//            });
//        }



//        public async void update()
//        {
//            try
//            {
//                var chatlistdetail = await FirebaseHelper.GetChatForUserID(Convert.ToInt32(LoginUserDetails.userId), WorkerChatDetailPage.ReciverId);
//                if (chatlistdetail.Count > rowCount)
//                {

//                    var chatdetail = chatlistdetail.OrderBy(x=>x.TimeStamp).Skip(rowCount).ToList();
                    
//                    foreach (var item in chatdetail)
//                    {
//                        if (item.IsReciver)
//                        {
//                            Items.Insert(0, new ChatDetailListModel { IsReciver = item.IsReciver, IsSender = item.IsSender, RecieverUserId = item.RecieverUserId, SenderUserId = item.SenderUserId, TimeStamp = item.TimeStamp, UserMessage = item.UserMessage, UserMessageTime = item.UserMessageTime, });
                            
//                        }
//                    }
//                    rowCount = chatlistdetail.Count;
//                }
//            }
//            catch (Exception ex)
//            {
//            }
//        }

//        private async Task DownloadDataAsync()
//        {
//            var items = await _dataService.GetItemsAsync(pageIndex: 0, pageSize: PageSize);

//            Items.AddRange(items);
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }







//    }
//}
