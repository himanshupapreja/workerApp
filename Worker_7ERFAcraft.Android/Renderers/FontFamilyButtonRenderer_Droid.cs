using Android.Content;
using Android.Graphics;
using Worker_7ERFAcraft.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(FontFamilyButtonRenderer_Droid))]
namespace Worker_7ERFAcraft.Droid.Renderers
{
    public class FontFamilyButtonRenderer_Droid : ButtonRenderer
    {
        public FontFamilyButtonRenderer_Droid(Context context) : base(context)
        {

        }
        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
            {
                Typeface font;
                font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.FontFamily + ".otf");


                Control.Typeface = font;
            }
        }


    }
}