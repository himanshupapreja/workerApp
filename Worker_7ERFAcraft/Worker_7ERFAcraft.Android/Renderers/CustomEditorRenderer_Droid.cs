using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content; 
using Android.Widget;

using Xamarin.Forms; 
using Worker_7ERFAcraft.Droid.Renderers;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer (typeof(Editor), typeof(CustomEditorRenderer_Droid))]
namespace Worker_7ERFAcraft.Droid.Renderers
{
    public class CustomEditorRenderer_Droid : EditorRenderer
    {
        public CustomEditorRenderer_Droid()
        {
        }

        protected override void OnElementChanged(
            ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }

         
    }
}

