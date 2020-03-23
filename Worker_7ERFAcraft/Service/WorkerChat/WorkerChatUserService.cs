using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.Repository;
using Worker_7ERFAcraft.ViewModels.Driver;

namespace Worker_7ERFAcraft.Service.Driver
{
    public class WorkerChatUserService
    {
        public static bool loaderCheck = false;
        public async Task<List<ChatUserData>> GetItemsAsync(int pageIndex, int pageSize)
        {

            var cbase = new HttpClientBase();
            var result = await cbase.GetChatUsers(ApiUrl.ChatUserUrl + "?userId=" + LoginUserDetails.userId);
            List<ChatUserData> lst = new List<ChatUserData>();
            foreach (var item in result.data)
            {
                lst.Add(new ChatUserData { CreatedDate = item.CreatedDate, Name = item.Name, PicturePath = string.IsNullOrEmpty(item.PicturePath) ? "user.png" : item.PicturePath, UserId = item.UserId });
            }
            return lst;
        }
    }
}
