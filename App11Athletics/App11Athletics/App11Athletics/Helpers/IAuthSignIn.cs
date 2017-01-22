using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
