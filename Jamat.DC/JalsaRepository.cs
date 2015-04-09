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
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
                
            }
        }

        public bool AddJalsa(Jalsa newJalsa)
        {
            try
            {
                newJalsa.CreatedBy = 1;
                newJalsa.CreatedOn = DateTime.Now;
                _ctx.Jalsas.Add(newJalsa);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddJalsaDay(JalsaDay newJalsaDay)
        {
            try
            {
                newJalsaDay.CreatedBy = 1;
                newJalsaDay.CreatedOn = DateTime.Now;
                _ctx.JalsaDays.Add(newJalsaDay);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
                .GroupBy(x => new
                {
                    x.CountryId, x.RegionId, x.CountryDetail.CountryName, x.RegionDetail.RegionName, x.FmailyPersonName, x.ContactNo
                })
                .Select(g => new
                {
                    countryID = g.Key.CountryId, 
                    countryName = g.Key.CountryName,
                    regionId= g.Key.RegionId, 
                    regionName = g.Key.RegionName,
                    totalAnsar = g.Max(r=>r.Ansar),
                    totalKhudam = g.Max(r => r.Khuddam),
                    totalAtfal = g.Max(r => r.Atfal),
                    totalNasarat = g.Max(r => r.Nassrat),
                    totalLajnat = g.Max(r => r.Lajnaat),
                    totalChild = g.Max(r => r.Child),
                    grandTotal = g.Max(r => r.Ansar) + g.Max(r => r.Khuddam) + 
                                g.Max(r => r.Atfal) + g.Max(r => r.Nassrat) + 
                                g.Max(r => r.Lajnaat) + g.Max(r => r.Child)

                })
                .GroupBy(o=> new { o.countryID, o.countryName, o.regionId, o.regionName})
                .Select(k=> new
                {
                    countryId1 = k.Key.countryID,
                    countryName1 = k.Key.countryName,
                    regionId1 = k.Key.regionId,
                    regionName1 = k.Key.regionName,
                    totalAnsar1 = k.Sum(r=>r.totalAnsar),
                    totalKhudam1 = k.Sum(r => r.totalKhudam),
                    totalAtfal1 = k.Sum(r => r.totalAtfal),
                    totalNasarat1 = k.Sum(r => r.totalNasarat),
                    totalLajnat1 = k.Sum(r => r.totalLajnat),
                    totalChild1 = k.Sum(r => r.totalChild),
                    grandTotal1 = k.Sum(r => r.totalAnsar) + k.Sum(r => r.totalKhudam) +
                                k.Sum(r => r.totalAtfal) + k.Sum(r => r.totalNasarat) +
                                k.Sum(r => r.totalLajnat) + k.Sum(r => r.totalChild)
 
                });


        }

        public IQueryable GetJalsaSummaryByCountry(int id)
        {
            return _ctx.JalsaDays
                .Where(c => c.JalsaId == id)
                .Include(c => c.CountryDetail)
                //.Include(c => c.RegionDetail)
                //.SelectMany(c=>c.)
                .GroupBy(x => new
                {
                    x.CountryId,
                    //x.RegionId,
                    x.CountryDetail.CountryName,
                    //x.RegionDetail.RegionName,
                    x.FmailyPersonName,
                    x.ContactNo
                })
                .Select(g => new
                {
                    countryID = g.Key.CountryId,
                    countryName = g.Key.CountryName,
                    //regionId = g.Key.RegionId,
                    //regionName = g.Key.RegionName,
                    totalAnsar = g.Max(r => r.Ansar),
                    totalKhudam = g.Max(r => r.Khuddam),
                    totalAtfal = g.Max(r => r.Atfal),
                    totalNasarat = g.Max(r => r.Nassrat),
                    totalLajnat = g.Max(r => r.Lajnaat),
                    totalChild = g.Max(r => r.Child),
                    grandTotal = g.Max(r => r.Ansar) + g.Max(r => r.Khuddam) +
                                g.Max(r => r.Atfal) + g.Max(r => r.Nassrat) +
                                g.Max(r => r.Lajnaat) + g.Max(r => r.Child)

                })
                .GroupBy(o => new { o.countryID, o.countryName})
                .Select(k => new
                {
                    countryId1 = k.Key.countryID,
                    countryName1 = k.Key.countryName,
                    //regionId1 = k.Key.regionId,
                    //regionName1 = k.Key.regionName,
                    totalAnsar1 = k.Sum(r => r.totalAnsar),
                    totalKhudam1 = k.Sum(r => r.totalKhudam),
                    totalAtfal1 = k.Sum(r => r.totalAtfal),
                    totalNasarat1 = k.Sum(r => r.totalNasarat),
                    totalLajnat1 = k.Sum(r => r.totalLajnat),
                    totalChild1 = k.Sum(r => r.totalChild),
                    grandTotal1 = k.Sum(r => r.totalAnsar) + k.Sum(r => r.totalKhudam) +
                                k.Sum(r => r.totalAtfal) + k.Sum(r => r.totalNasarat) +
                                k.Sum(r => r.totalLajnat) + k.Sum(r => r.totalChild)

                });


        }

        public IQueryable<Jalsa> GetJalsaList()
        {
            return _ctx.Jalsas;

        }
    }
}
