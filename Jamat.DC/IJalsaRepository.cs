using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public interface IJalsaRepository
    {
        Task<IQueryable<Jalsa>> GetJalsas(int id);

        Jalsa GetJalsaById(int id);

        Task<Jalsa> GetJalsaDays(int id);

        bool SaveJalsa();

        bool AddJalsa();

        bool AddJalsaDay();

    }
}
