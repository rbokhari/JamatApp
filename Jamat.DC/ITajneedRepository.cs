using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public interface ITajneedRepository
    {
        Task<IQueryable<Tajneed>> GetTajneedList();

        Tajneed GetTajneed(int id);

        bool Save();

        bool AddTajneed(Tajneed newTajneed);

        bool UpdateTajneed(Tajneed updateTajneed);

    }
}
