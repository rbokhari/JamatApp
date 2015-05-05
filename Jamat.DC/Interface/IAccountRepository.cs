using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC.Interface
{
    public interface IAccountRepository
    {
        User GetUser(string username, string userPass);
    }
}
