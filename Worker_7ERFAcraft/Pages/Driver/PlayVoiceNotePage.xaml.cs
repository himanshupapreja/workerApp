
using PCLStorage;
using System;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayVoiceNotePage : ContentPage
	{
        private ICustomWebClient customWebClient = DependencyService.Get<ICustomWebClient>();
        public PlayVoiceNotePage (ChatResponse getModel)
		{
			InitializeComponent ();

            webView.HeightRequest = App.ScreenHeight - 200;
            webView.WidthRequest = App.ScreenWidth;

            string fileName = string.Empty;
            string fileUrl = string.Empty;

            if (getModel.IsVoiceNote)
            {
                fileName = getModel.FilePath + getModel.Time + ".wav";
                fileUrl = getModel.VoiceNoteUrl;
            }

            fileName = fileName.Replace(" ", "");
            fileName = fileName.Replace("/", "");
            fileName = fileName.Replace(":", "");
            PlayAudioFile(fileUrl, fileName);
        }

        public async void PlayAudioFile(string fileUrl, string fileName)
        {
            string type = "audio/wav";

            var htmlString = @"<!DOCTYPE html>
                            <html>
                                <head>
                                 <title>Page Title</title>
                                </head>
                                <body> 
                                    <audio  controls><source src=" + fileName + " type=" + type + "></source></audio> </body> </html>";


            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Example", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("Default.html", CreationCollisionOption.ReplaceExisting);

            await file.WriteAllTextAsync(htmlString);
            await customWebClient.DownloadFile(fileUrl, folder.Path + "/" + fileName);

            PlayAudioFile();
            actLoading.IsVisible = false;
        }

        public void PlayAudioFile()
        {
            IFolder folder = FileSystem.Current.LocalStorage.GetFolderAsync("Example").Result;
            webView.Source = String.Format("file://{0}/{1}", folder.Path, "Default.html");
        }
    }
}