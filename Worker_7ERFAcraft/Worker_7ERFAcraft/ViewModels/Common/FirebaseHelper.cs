using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;

namespace Worker_7ERFAcraft.Helpers
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://alpine-zodiac-232721.firebaseio.com/");

        public async Task<List<ChatModel>> GetChatForUserID(int senderUserId, int recieverUserId)
        {
            //if (LoginUserDetails.userType == UserType.Driver.GetHashCode())
            //{
                return (await firebase
                  .Child("Chat").Child(senderUserId.ToString()).Child(recieverUserId.ToString())
                  .OnceAsync<ChatModel>()).Select(item => new ChatModel
                  {
                      IsSender = item.Object.IsSender,
                      Message = item.Object.Message,
                      SenderUserId = item.Object.SenderUserId,
                      MessageTime = item.Object.MessageTime,
                      RecieverUserId = item.Object.RecieverUserId,
                      ImageUrl = item.Object.ImageUrl,
                      IsImg = item.Object.IsImg,
                      IsMsg = item.Object.IsMsg,
                      FilePath = item.Object.FilePath,
                      IsVoiceNote = item.Object.IsVoiceNote,
                      VoiceNoteUrl = item.Object.VoiceNoteUrl,
                      TotalAudioTimeout = item.Object.TotalAudioTimeout,
                      TimeStamp = item.Object.TimeStamp
                  }).OrderByDescending(x => x.TimeStamp).ToList();
            //}
            //else
            //{
            //    return (await firebase
            //     .Child("Chat").Child(recieverUserId.ToString()).Child(senderUserId.ToString())
            //     .OnceAsync<ChatModel>()).Select(item => new ChatModel
            //     {
            //         Message = item.Object.Message,
            //         SenderUserId = item.Object.SenderUserId,
            //         MessageTime = item.Object.MessageTime,
            //         RecieverUserId = item.Object.RecieverUserId,
            //         ImageUrl = item.Object.ImageUrl,
            //         IsImg = item.Object.IsImg,
            //         IsMsg = item.Object.IsMsg,
            //         FilePath = item.Object.FilePath,
            //         IsVoiceNote = item.Object.IsVoiceNote,
            //         VoiceNoteUrl = item.Object.VoiceNoteUrl,
            //         TotalAudioTimeout = item.Object.TotalAudioTimeout,
            //         TimeStamp = item.Object.TimeStamp
            //     }).OrderByDescending(x => x.TimeStamp).ToList();
            //}
        }

        public async Task<bool> AddChatMessage(ChatModel chatModel)
        {
            try
            {
                await firebase
                      .Child("Chat").Child(chatModel.SenderUserId.ToString()).Child(chatModel.RecieverUserId.ToString()).PostAsync(chatModel);
                var chatModel1 = new ChatModel()
                {
                    IsSender = !chatModel.IsSender,
                    RecieverUserId = chatModel.SenderUserId,
                    SenderUserId = chatModel.RecieverUserId,
                    FilePath = chatModel.FilePath,
                    ImageUrl = chatModel.ImageUrl,
                    IsImg = chatModel.IsImg,
                    IsMsg = chatModel.IsMsg,
                    IsVoiceNote = chatModel.IsVoiceNote,
                    Message = chatModel.Message,
                    MessageTime = chatModel.MessageTime,
                    TotalAudioTimeout = chatModel.TotalAudioTimeout,
                    VoiceNoteUrl = chatModel.VoiceNoteUrl,
                    TimeStamp = chatModel.TimeStamp
                };
                await firebase
                      .Child("Chat").Child(chatModel1.SenderUserId.ToString()).Child(chatModel1.RecieverUserId.ToString()).PostAsync(chatModel1);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddingChatToFirebase_Exception:- " + ex.Message);
                return false;
            }
        }

        public async Task DeleteSingleChat(long senderUserId)
        {
            await firebase.Child("Chat").Child(senderUserId.ToString()).DeleteAsync();
        }

        public async Task<string> StoreImages(Stream imageStream, string imageName)
        {
            string imgurl = string.Empty;
            try
            {
                var stroageImage = await new FirebaseStorage("alpine-zodiac-232721.appspot.com")
                .Child("Images")
                .Child(imageName)
                .PutAsync(imageStream);
                imgurl = stroageImage;
            }
            catch (Exception ex)
            {
                
            }
            
            return imgurl;
        }


        public async Task<string> StoreAudio(Stream audioStream, string audioName)
        {
            string audiourl = string.Empty;
            try
            {
                var stroageAudio = await new FirebaseStorage("alpine-zodiac-232721.appspot.com")
                .Child("Audios")
                .Child(audioName)
                .PutAsync(audioStream);
                audiourl = stroageAudio;
            }
            catch (Exception ex)
            {

            }

            return audiourl;
        }
    }
}