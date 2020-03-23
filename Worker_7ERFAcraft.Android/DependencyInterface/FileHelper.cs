
using Worker_7ERFAcraft.Droid.DependencyInterface;
using System;
using System.IO;
using Xamarin.Forms;


[assembly: Dependency(typeof(FileHelper))]
namespace Worker_7ERFAcraft.Droid.DependencyInterface
{
    public class FileHelper : Worker_7ERFAcraft.DependencyInterface.IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}