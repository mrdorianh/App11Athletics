using System.Threading.Tasks;

namespace App11Athletics.Helpers
{
    public interface IAuthSignIn
    {
        Task AuthSignIn();
        Task AuthLogOut();
        Task AuthRefresh();
    }
}
