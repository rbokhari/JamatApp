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
        IQueryable<Jalsa> GetJalsaList();

        IQueryable<Jalsa> GetJalsas(int id, int day);

        Jalsa GetJalsaById(int id);

        Task<IQueryable<JalsaDay>> GetJalsaDays(int id, int day);

        IQueryable GetJalsaSummary(int id);
        IQueryable GetJalsaSummaryByCountry(int id);

        bool SaveJalsa();

        bool AddJalsa(Jalsa newJalsa);

        bool AddJalsaDay(JalsaDay newJalsaDay);

    }
}
