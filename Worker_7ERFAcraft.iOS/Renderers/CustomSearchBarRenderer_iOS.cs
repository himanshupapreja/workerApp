using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.iOS;
using Worker_7ERFAcraft.iOS.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using UIKit;
using Foundation;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer_iOS))]

namespace Worker_7ERFAcraft.iOS.Renderers
{
    public class CustomSearchBarRenderer_iOS : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //  Control.BackgroundColor = UIColor.Clear;
            if (Control != null)
            {
                var uiTextField = (UITextField)Control.ValueForKey(new NSString("_searchField"));

                // uiTextField.Font = YourFontHere;
                // uiTextField.TextColor = UIColor.White;
                uiTextField.BackgroundColor = UIColor.White;
                // Placeholder colour is white 
                uiTextField.AttributedPlaceholder = new NSAttributedString("Search Client", null, UIColor.Black);
                Control.ShowsCancelButton = false;


                //var csb = (POC.CustomControls.CustomSearchBar)Element;
                // if (csb.BarTint != null)
                //   Control.BarTintColor = UIColor.Clear;// csb.BarTint.GetValueOrDefault().ToUIColor();
                //Control.BarStyle = (UIBarStyle)Enum.Parse(typeof(UIBarStyle), csb.BarStyle);
                //Control.SearchBarStyle = (UISearchBarStyle)Enum.Parse(typeof(UISearchBarStyle), csb.BarStyle);
            }
        }
    }
}