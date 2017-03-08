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
