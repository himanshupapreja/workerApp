using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.iOS;
using Worker_7ERFAcraft.iOS.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using UIKit;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer_iOS))]

namespace Worker_7ERFAcraft.iOS.Renderers
{
    public class CustomPickerRenderer_iOS : PickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
            Control.BackgroundColor = UIColor.Clear;

            try
            {
                Control.AttributedPlaceholder = new Foundation.NSAttributedString(Control.AttributedPlaceholder.Value, 
                                                                                  foregroundColor: UIColor.Black);
            }
            catch
            {

            }
        }


    }
}