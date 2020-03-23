using Plugin.AudioRecorder;
using System;
using Worker_7ERFAcraft.Models;
using Worker_7ERFAcraft.ViewModels.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Worker_7ERFAcraft.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DriverChatDetailPage : ContentPage
	{
        AudioRecorderService recorder = new AudioRecorderService();

        public static bool timer = false;
        public static int ReciverId = 0;
		public DriverChatDetailPage()
		{
			InitializeComponent ();
            timer = true;
            Title = Resx.AppResources.Chat;
            DriverChatingViewModel model = new DriverChatingViewModel();
            BindingContext = model;
            recorder.StopRecordingOnSilence = false;

            try
            {
                //MessagingCenter.Subscribe<string>(this, "TextFocus", (sender) =>
                //{
                //    txt_msg.Text = "";
                //    cursor = true;
                //});
            }
            catch (Exception ex)
            {
            }
            //cursor = false;
             
            //txt_msg.Unfocused += Txt_Msg_Unfocused;
        }

        //public static bool cursor = false;


        //void Txt_Msg_Unfocused(object sender, FocusEventArgs e)
        //{
        //    if (cursor == true)
        //    {

        //        txt_msg.Focus();

        //    }
        //    cursor = false;
        //}
        private async void Button_Pressed(object sender, EventArgs e)
        {
            try
            {
                txt_msg.Unfocus();
                if (!recorder.IsRecording)
                {
                    lblRecordTime.IsVisible = true;
                    await recorder.StartRecording();
                }
                int sec = 0;
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    sec++;
                    if (sec < 10)
                    {
                        lblRecordTime.Text = "00:" + "0" + sec;
                    }
                    else
                    {
                        lblRecordTime.Text = "00:" + sec;
                    }
                    if (!recorder.IsRecording)
                    {
                        lblRecordTime.Text = "00:00";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }

        private async void Button_Released(object sender, EventArgs e)
        {
            try
            {
                if (recorder.IsRecording)
                {
                    lblRecordTime.IsVisible = false;
                    var TotalAudioTimeout = recorder.AudioSilenceTimeout;// TotalAudioTimeout;
                    await recorder.StopRecording();

                    var filePath = recorder.FilePath;
                    MessagingCenter.Send(new Audio
                    {
                        AudioPath = filePath,
                        DataStream = recorder.GetAudioFileStream(),
                        TotalAudioTimeout = lblRecordTime.Text
                    }, "RecordAudioOneToOne");
                }

            }
            catch (Exception)
            {

            }
        }

    }
}