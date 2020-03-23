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

[assembly: ExportRenderer(typeof(ScrollView), typeof(ScrollBarDisabledRenderer))]

namespace Worker_7ERFAcraft.iOS.Renderers
{
    public class ScrollBarDisabledRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           // ShowsHorizontalScrollIndicator = false;
            //ShowsVerticalScrollIndicator = false;
        }
    }
}