using Firebase.Database;
using Java.Lang;
using Worker_7ERFAcraft.DependencyInterface;
using Worker_7ERFAcraft.Droid.DependencyInterface;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetTimeStamp_Droid))]
namespace Worker_7ERFAcraft.Droid.DependencyInterface
{
    public class GetTimeStamp_Droid : IGetTimeStamp
    {
        public object TimeStamp()
        {
            return ServerValue.Timestamp as Object;

        }
    }
}