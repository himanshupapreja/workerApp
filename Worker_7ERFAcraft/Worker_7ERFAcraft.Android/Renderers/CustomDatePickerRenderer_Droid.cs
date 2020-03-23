using Android.Content;
using Worker_7ERFAcraft.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker),
    typeof(CustomDatePickerRenderer_Droid))]
namespace Worker_7ERFAcraft.Droid.Renderers
{
    public class CustomDatePickerRenderer_Droid :
        DatePickerRenderer
    {
        public CustomDatePickerRenderer_Droid(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}