using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.Helpers;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Pages;
using Worker_7ERFAcraft.Pages.Driver;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.Resx;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.ViewModels.Driver
{
    public class DriverChatingViewModel : INotifyPropertyChanged
    {
        MediaFile file = null;
        byte[] myfile = null;

        private readonly FirebaseHelper _firebasehelper;

        #region Properties

        private bool _isRecorderShown = true;
        public bool IsRecorderShown
        {
            get
            {
                return _isRecorderShown;
            }
            set
            {
                _isRecorderShown = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsRecorderShown"));
            }
        }

        private string _msg;
        public string Msg
        {
            get
            {
                return _msg;

            }
            set
            {
                _msg = value;
                OnPropertyChanged();
                if (!string.IsNullOrEmpty(_msg))
                {
                    IsRecorderShown = false;
                    IsSendMsgShown = true;
                }
                else
                {
                    if (!_isRecorderShown)
                    {
                        IsRecorderShown = true;
                        IsSendMsgShown = false;
                    }
                }
            }
        }

        private bool _sendMsgVisible = false;
        public bool SendMsgVisible
        {
            get
            {
                return _sendMsgVisible;

            }
            set
            {
                _sendMsgVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isSendMsgShown = false;
        public bool IsSendMsgShown
        {
            get
            {
                return _isSendMsgShown;
            }
            set
            {
                _isSendMsgShown = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSendMsgShown"));
            }
        }
        #endregion

        private ObservableCollection<ChatResponse> _list;

        private ObservableCollection<ChatResponse> _chatList;
        public ObservableCollection<ChatResponse> ChatList
        {
            get
            {
                return _chatList;

            }
            set
            {
                _chatList = value;
                OnPropertyChanged();
            }
        }


        public DriverChatingViewModel()
        {
            _firebasehelper = new FirebaseHelper();

            _list = new ObservableCollection<ChatResponse>();

            GetChat();

            MessagingCenter.Subscribe<Audio>(this, "RecordAudioOneToOne", async (sender) =>
            {
                try
                {
                    _list.Insert(0, new ChatResponse
                    {
                        Time = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                        IsSend = true,
                        IsReceived = false,
                        IsImg = false,
                        SenderUserId = Convert.ToInt32(LoginUserDetails.userId),
                        RecieverUserId = DriverChatDetailPage.ReciverId,
                        IsMsg = false,
                        IsVoiceNote = true,
                        VoiceNoteUrl = "",
                        TotalAudioTimeout = sender.TotalAudioTimeout,
                        IsLoading = true,
                    });

                    ChatList = _list;

                    RaisePropertyChanged(nameof(ChatList));

                    var returnUrl = await _firebasehelper.StoreAudio(sender.DataStream, Path.GetFileName(sender.AudioPath));
                    _list[0].VoiceNoteUrl = returnUrl;
                    _list[0].IsLoading = false;

                    ChatModel chatModel = new ChatModel();
                    chatModel.MessageTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                    chatModel.SenderUserId = Convert.ToInt32(LoginUserDetails.userId);
                    chatModel.RecieverUserId = DriverChatDetailPage.ReciverId;
                    chatModel.IsMsg = false;
                    chatModel.IsVoiceNote = true;
                    chatModel.IsImg = false;
                    chatModel.VoiceNoteUrl = returnUrl;
                    chatModel.TotalAudioTimeout = sender.TotalAudioTimeout;
                    chatModel.FilePath = Path.GetFileName(sender.AudioPath);
                    chatModel.TimeStamp = DependencyService.Get<IGetTimeStamp>().TimeStamp();
                    await _firebasehelper.AddChatMessage(chatModel);
                }
                catch (Exception)
                { }
                MessagingCenter.Unsubscribe<Audio>(sender, "RecordAudioOneToOne");
            });
        }

        public async void GetChat()
        {
            var getData = await _firebasehelper.GetChatForUserID(Convert.ToInt32(LoginUserDetails.userId), DriverChatDetailPage.ReciverId);

            foreach (var item in getData)
            {
                bool _isSent = item.SenderUserId == LoginUserDetails.userId ? true : false;
                bool _isReceived = item.RecieverUserId == LoginUserDetails.userId ? true : false;
                _list.Add(new ChatResponse
                {
                    Message = item.Message,
                    IsSend = _isSent,
                    IsReceived = _isReceived,
                    Time = item.MessageTime,
                    IsImg = item.IsImg,
                    IsMsg = item.IsMsg,
                    ImageUrl = item.ImageUrl,
                    TotalAudioTimeout = item.TotalAudioTimeout,
                    IsVoiceNote = item.IsVoiceNote,
                    VoiceNoteUrl = item.VoiceNoteUrl,
                    RecieverUserId = item.RecieverUserId,
                    SenderUserId = item.SenderUserId
                });
            }

            ChatList = _list;

            RaisePropertyChanged(nameof(ChatList));

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(300);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Update();
                        });

                    });
                }
                else
                {
                    Task.Run(async () =>
                    {
                        await Update();
                    });
                }
                return true;
            });
        }

        public async Task Update()
        {
            try
            {
                var getData = await _firebasehelper.GetChatForUserID(Convert.ToInt32(LoginUserDetails.userId), DriverChatDetailPage.ReciverId);

                if (getData.Count > 0)
                {
                    if (getData.Count > _list.Count)
                    {
                        var getLastItem = getData[getData.Count - 1];
                        if (getLastItem.SenderUserId != LoginUserDetails.userId)
                        {
                            bool _isSent = getLastItem.SenderUserId == LoginUserDetails.userId ? true : false;
                            bool _isReceived = getLastItem.RecieverUserId == LoginUserDetails.userId ? true : false;
                            _list.Insert(0, new ChatResponse
                            {
                                Message = getLastItem.Message,
                                IsSend = _isSent,
                                IsReceived = _isReceived,
                                Time = getLastItem.MessageTime,
                                IsImg = getLastItem.IsImg,
                                IsMsg = getLastItem.IsMsg,
                                ImageUrl = getLastItem.ImageUrl,
                                VoiceNoteUrl = getLastItem.VoiceNoteUrl,
                                IsVoiceNote = getLastItem.IsVoiceNote,
                                RecieverUserId = getLastItem.RecieverUserId,
                                SenderUserId = getLastItem.SenderUserId
                            });
                            ChatList = _list;

                            RaisePropertyChanged(nameof(ChatList));
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Commands

        public Command OpenImageCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var getModel = e as ChatResponse;
                    Application.Current.MainPage.Navigation.PushAsync(new ShowImageChatPage(getModel.ImageUrl));
                });
            }
        }

        public Command PlayAudioCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var getModel = e as ChatResponse;
                    Application.Current.MainPage.Navigation.PushAsync(new PlayVoiceNotePage(getModel));
                });
            }
        }

        public Command chat_Tapped
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (!Common.CheckConnection())
                        {

                        }
                        else
                        {
                            _list.Insert(0, new ChatResponse
                            {
                                Time = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                                Message = Msg.Trim(),
                                IsSend = true,
                                IsReceived = false,
                                IsImg = false,
                                IsMsg = true,
                                ImageUrl = string.Empty,
                                IsVoiceNote = false,
                            });

                            ChatList = _list;

                            RaisePropertyChanged(nameof(ChatList));

                            ChatModel chatmodel = new ChatModel();
                            chatmodel.MessageTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                            chatmodel.SenderUserId = Convert.ToInt32(LoginUserDetails.userId);
                            chatmodel.Message = Msg;
                            chatmodel.RecieverUserId = DriverChatDetailPage.ReciverId;
                            chatmodel.IsImg = false;
                            chatmodel.IsMsg = true;
                            chatmodel.IsVoiceNote = false;
                            chatmodel.TimeStamp = DependencyService.Get<IGetTimeStamp>().TimeStamp();
                            await _firebasehelper.AddChatMessage(chatmodel);

                            Msg = string.Empty;
                        }


                    }
                    catch (Exception ex)
                    {
                    }
                });
            }
        }

        public Command ImgCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    try
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.ChoosefromGallery, AppResources.Cancel, null,
                           AppResources.TakePhoto, AppResources.ChoosePhotofromGallery);

                        if (action == AppResources.TakePhoto)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            if (Device.RuntimePlatform == "iOS")
                            {
                                await Task.Delay(1000);
                            }
                            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg"
                            });
                            if (file == null)
                                return;
                            using (var memoryStream = new MemoryStream())
                            {
                                file.GetStream().CopyTo(memoryStream);
                                myfile = memoryStream.ToArray();
                            }
                        }
                        else if (action == AppResources.ChoosePhotofromGallery)
                        {
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            file = await CrossMedia.Current.PickPhotoAsync();
                            if (file != null)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    myfile = memoryStream.ToArray();
                                }

                            }
                        }
                        else
                        {
                            return;
                        }
                        _list.Insert(0, new ChatResponse
                        {
                            Time = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                            Message = string.Empty,
                            IsSend = true,
                            IsReceived = false,
                            IsImg = true,
                            IsMsg = false,
                            ImageUrl = "",
                            IsLoading = true,
                            FilePath = file.Path,
                            IsVoiceNote = false,
                        });

                        ChatList = _list;

                        RaisePropertyChanged(nameof(ChatList));

                        var returnUrl = await _firebasehelper.StoreImages(file.GetStream(), Path.GetFileName(file.Path));
                        var data = _list.Where(x => x.FilePath == file.Path).FirstOrDefault();
                        if (data != null)
                        {
                            var index = _list.IndexOf(data);
                            if (index > -1)
                            {
                                _list[index].ImageUrl = returnUrl;
                                _list[index].IsLoading = false;
                            }
                        }

                        ChatModel chatmodel = new ChatModel();
                        chatmodel.MessageTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                        chatmodel.SenderUserId = Convert.ToInt32(LoginUserDetails.userId);
                        chatmodel.RecieverUserId = DriverChatDetailPage.ReciverId;
                        chatmodel.IsImg = true;
                        chatmodel.IsMsg = false;
                        chatmodel.IsVoiceNote = false;
                        chatmodel.ImageUrl = returnUrl;
                        chatmodel.TimeStamp= DependencyService.Get<IGetTimeStamp>().TimeStamp();
                        await _firebasehelper.AddChatMessage(chatmodel);

                        file.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        #endregion

    }
}
