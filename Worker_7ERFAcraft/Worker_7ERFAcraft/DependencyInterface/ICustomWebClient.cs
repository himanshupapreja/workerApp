using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Worker_7ERFAcraft.DependencyInterface
{
    public interface ICustomWebClient
    {
        Task DownloadFile(string url, string location);
    }
}
