using System;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Repository
{
    public static class DataClass
    {
        public static int PickerSelectedIndex = 0;
    }
    public class App_FontFamilies
    {
       
        /// <summary>
        /// for page titles
        /// </summary>
        /// <value>The page titles.</value>
        public static string HeadingFont
        {
            get
            {
                return "IBMPlexSans-SemiBold";
            }
        }
        /// <summary>
        /// for page titles
        /// </summary>
        /// <value>The page titles.</value>
        public static string BodyFont
        {
            get
            {
                return "IBMPlexSans-Regular";
            }
        }
        public static string MediumFont
        {
            get
            {
                return "IBMPlexSans-Medium";
            }
        }

        /// <summary>
        /// PageDiscription 
        /// </summary>
        /// <value>The page titles.</value>


        public static string LightHeadingFont 
        {
            get
            {
                return "IBMPlexSans-Light";
            }

        }

    }
}
