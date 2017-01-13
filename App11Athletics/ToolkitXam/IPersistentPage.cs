using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToolkitXam
{
    public interface IPersistentPage
    {
        void Save(string prefix);

        void Restore(string prefix);
    }
}
