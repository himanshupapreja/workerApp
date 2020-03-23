using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Worker_7ERFAcraft.iOS.DependencyInterface;

[assembly: Dependency(typeof(FileHelper))]
namespace Worker_7ERFAcraft.iOS.DependencyInterface
    {
        public class FileHelper : Worker_7ERFAcraft.DependencyInterface.IFileHelper
    {
            public string GetLocalFilePath(string filename)
            {
                string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

                if (!Directory.Exists(libFolder))
                {
                    Directory.CreateDirectory(libFolder);
                }

                return Path.Combine(libFolder, filename);
            }
        }
    
}
