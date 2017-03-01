using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App11Athletics.Helpers
{
    public interface ICleanUp
    {

        /// <summary>
        /// "Cleans" the page
        /// </summary>
        Task Cleanup();

    }
}
