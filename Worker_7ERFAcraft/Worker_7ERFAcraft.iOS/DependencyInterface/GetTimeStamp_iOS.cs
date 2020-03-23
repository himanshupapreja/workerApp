using Firebase.Database;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.iOS.DependencyInterface;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetTimeStamp_iOS))]
namespace Worker_7ERFAcraft.iOS.DependencyInterface
{
    public class GetTimeStamp_iOS : IGetTimeStamp
    {
        public object TimeStamp()
        {
            var timeStamp = Convert(ServerValue.Timestamp); 
            return timeStamp as Object; 
        }

        private static IDictionary<string, string> Convert(NSDictionary nativeDict)
        {
            return nativeDict.ToDictionary<KeyValuePair<NSObject, NSObject>, string, string>(
                item => (NSString)item.Key, item => item.Value.ToString());
        }
    }
}