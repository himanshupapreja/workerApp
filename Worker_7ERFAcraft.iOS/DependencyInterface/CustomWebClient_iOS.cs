using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Worker_7ERFAcraft.DependencyInterface;

namespace Worker_7ERFAcraft.iOS.DependencyInterface
{
    public class CustomWebClient_iOS : ICustomWebClient
    {
        public async Task DownloadFile(string url, string location)
        {
            if (!File.Exists(location))
            {
                using (var webClient = new WebClient())
                {
                    try
                    {
                        await webClient.DownloadFileTaskAsync(new Uri(url), location);
                    }
                    catch (Exception)
                    {
                        // don't do anything if the download of the resource fails
                    }
                }
            }
        }
    }
}