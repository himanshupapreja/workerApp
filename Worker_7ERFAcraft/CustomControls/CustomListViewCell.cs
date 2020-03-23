using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.CustomControls
{
    public class CustomViewCell : ViewCell
    {
        public CustomViewCell()
        {
            Image image = new Image
            {
                HeightRequest = 25,
                WidthRequest = 25
            };
            image.SetBinding(Image.SourceProperty, new Binding("Icon"));

            Label lbl = new Label
            {
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                FontFamily = "IBMPlexSans-Medium",
                StyleId = "IBMPlexSans-Medium"
            };
            lbl.SetBinding(Label.TextProperty, new Binding("Title"));
            lbl.SetBinding(Label.TextColorProperty , new Binding("TxtColor")); 
            var sl_projectinfo = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(15),
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Children =
                    {
                        image,lbl
                    }
            };
            sl_projectinfo.SetBinding(StackLayout.BackgroundColorProperty, new Binding("BGColor"));
            View = sl_projectinfo;
        }

    }
   
}
