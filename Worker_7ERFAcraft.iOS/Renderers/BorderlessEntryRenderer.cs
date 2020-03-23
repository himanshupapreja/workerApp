using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.iOS;
using Worker_7ERFAcraft.iOS.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer(typeof( Entry), typeof(BorderlessEntryRenderer))]

namespace Worker_7ERFAcraft.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
            Control.BackgroundColor = UIColor.Clear;

             
        }
    }
}