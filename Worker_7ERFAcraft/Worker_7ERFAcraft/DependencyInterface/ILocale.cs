using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker_7ERFAcraft.DependencyInterface
{
    public interface ILocale
    {
        string GetCurrent();
        void SetLocale();
    }
}
