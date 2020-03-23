using Android.Widget;
using luvbombApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar),
    typeof(CustomSearchBarRenderer_Droid))]
namespace luvbombApp.Droid.Renderers
{
    public class CustomSearchBarRenderer_Droid :
        SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);


            if (Control != null)
            {
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);
               
                //this.Control.SetBackgroundColor(Android.Graphics.Color.Argb(0, 0, 0, 0));
            }
            if (e.OldElement == null)
            {
                LinearLayout linearLayout = this.Control.GetChildAt(0) as LinearLayout;
                linearLayout = linearLayout.GetChildAt(2) as LinearLayout;
                linearLayout = linearLayout.GetChildAt(1) as LinearLayout;

                //linearLayout.Background = Resources.GetDrawable(Resource.Drawable.dashboardbackbtns); //removes underline

                AutoCompleteTextView textView = linearLayout.GetChildAt(0) as AutoCompleteTextView;
                //textView.SetTextColor(Android.Graphics.Color.White);
                
                //modify for text appearance customization 
            }
        }
    }
}