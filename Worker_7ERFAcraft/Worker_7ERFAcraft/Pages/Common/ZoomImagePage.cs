using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Worker_7ERFAcraft.CustomControls;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Pages
{
    public class ZoomImagePage : ContentPage
    {  
        public ZoomImagePage(string imagePath)
        {  
 
                Grid grid  = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand, 
                    Children = { 
                    new PinchToZoomContainer {
                        Content = new CachedImage { Source = imagePath ,Aspect=Aspect.AspectFill}
                    }
                }
                };
                Content = new StackLayout
                { 
                    Children = { 
                    grid
                }
                };
            
              
            
        }

        private void Tgr_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

       
    }
}
