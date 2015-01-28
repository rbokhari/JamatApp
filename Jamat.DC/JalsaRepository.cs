using Jamat.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Jamat.DC
{
    public class JalsaRepository : IJalsaRepository, IDisposable
    {
        private DbEntityContext _ctx;

        public JalsaRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Jalsa> GetJalsas(int id, int day)
        {
            //return
            //    await
            //        Task.Run(
            //            () =>
            //                _ctx.Jalsas.Include(c => c.JalsaDays.Select(e => e.CountryDetail))
            //                    .Where(c => c.JalsaId == id));


            return _ctx.Jalsas
                .Where(c => c.JalsaDays.Any(r => r.DayId == day))
                .Include(c => c.JalsaDays.Select(d => d.CountryDetail))
                .Include(c => c.JalsaDays.Select(d => d.RegionDetail));

            //.Where(c => c.JalsaId == id);
            //.Include(c => c.JalsaDays.Select(e => e.CountryDetail));


        }

        public Jalsa GetJalsaById(int id)
        {
            return _ctx.Jalsas.Single(c => c.JalsaId == id);
        }

        public bool SaveJalsa()
        {
            return true;
        }

        public bool AddJalsa()
        {
            return true;
        }

        public bool AddJalsaDay()
        {
            return true;
        }


        public async Task<IQueryable<JalsaDay>> GetJalsaDays(int id, int day)
        {
            return await Task.Run(() => _ctx.JalsaDays
                .Include(c => c.CountryDetail)
                .Include(c => c.RegionDetail)
                .Where(c => c.JalsaId == id && c.DayId == day));
        }


        public void Dispose()
        {
            _ctx.Dispose();
        }




        public IQueryable GetJalsaSummary(int id) 
        {
            return _ctx.JalsaDays
                .Where(c => c.JalsaId == id)
                .Include(c=>c.CountryDetail)
                .Include(c=>c.RegionDetail)
                //.SelectMany(c=>c.)
                .GroupBy(x => new { x.CountryId, x.RegionId, x.CountryDetail.CountryName, x.RegionDetail.RegionName})
                .Select(g => new
                {
                    countryID = g.Key.CountryId, 
                    countryName = g.Key.CountryName,
                    regionId= g.Key.RegionId, 
                    regionName = g.Key.RegionName,
                    totalAnsar = g.Sum(r=>r.Ansar), 
                    totalKhudam = g.Sum(r=>r.Khuddam),
                    totalAtfal = g.Sum(r=>r.Atfal),
                    totalNasarat = g.Sum(r=>r.Nassrat),
                    totalLajnat = g.Sum(r=>r.Lajnaat),
                    totalChild = g.Sum(r=>r.Child),
                    grandTotal = g.Sum(r => r.Ansar) + g.Sum(r => r.Khuddam) + g.Sum(r => r.Atfal) + g.Sum(r => r.Nassrat) + g.Sum(r => r.Lajnaat) + g.Sum(r => r.Child)

                });


        }
    }
}
