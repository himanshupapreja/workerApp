using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class ChatListModel
    {
        public int SenderUserId { get; set; }
        [JsonProperty("UserId")]
        public int RecieverUserId { get; set; }
        [JsonProperty("PicturePath")]
        public string UserPic { get; set; }
        [JsonProperty("Name")]
        public string UserName { get; set; }
        
    }

    public class Audio
    {
        public string AudioPath { get; set; }
        public Stream DataStream { get; set; }
        public string TotalAudioTimeout { get; set; }
    }

    public class ChatListResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<ChatListModel> data { get; set; }
    }


    public class ChatModel
    {
        public int SenderUserId { get; set; }

        public int RecieverUserId { get; set; }

        public string MessageTime { get; set; }

        public string Message { get; set; }

        public bool IsMsg { get; set; }

        public bool IsImg { get; set; }

        public string ImageUrl { get; set; }

        public string VoiceNoteUrl { get; set; }

        public bool IsVoiceNote { get; set; }

        public object TimeStamp { get; set; }

        public string TotalAudioTimeout { get; set; }

        public string FilePath { get; set; }
    }

    public class ChatResponse : INotifyPropertyChanged
    {
        public string FilePath { get; set; }

        public string Time { get; set; }

        public int SenderUserId { get; set; }

        public int RecieverUserId { get; set; }

        public string Message { get; set; }

        public bool IsSend { get; set; }

        public bool IsReceived { get; set; }

        public bool IsMsg { get; set; }

        public bool IsImg { get; set; }

        public bool IsVoiceNote { get; set; }

        private string _voiceNoteUrl;

        public string VoiceNoteUrl
        {
            get
            {
                return _voiceNoteUrl;
            }
            set
            {
                _voiceNoteUrl = value;

                OnPropertyChanged();
            }
        }

        public string TotalAudioTimeout { get; set; }
       
        private string _imgUrl;

        public string ImageUrl
        {
            get
            {
                return _imgUrl;
            }
            set
            {
                _imgUrl = value;

                OnPropertyChanged();
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;

                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ChatDetailModelApi
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    }

    public class Chatdetailresponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
}
