using System;
using luvbombApp.Droid.CustomRenderers;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace luvbombApp.Droid.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {

        }

        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
            if (e.OldElement == null)
            {  

                if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
                {
                    Typeface font;    font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.FontFamily + ".otf");
                     
                    Control.Typeface = font;
                }
            }

            if (Control != null)
            {
                // stop double triggering 
                Control.Focusable = false;
                Control.FocusableInTouchMode = false;
            }
        }
    }
}
