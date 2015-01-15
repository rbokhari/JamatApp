using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;
using System.Data.Entity;

namespace Jamat.DC
{
    public class TajneedRepository : ITajneedRepository
    {
        DbEntityContext _ctx;

        public TajneedRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<EntityFramework.Tajneed>> GetTajneedList()
        {
            return await Task.Run(() => _ctx.Tajneeds.Include(c=>c.AuxilaryDetail).Include(c=>c.RegionDetail));
        }

        public IQueryable<Tajneed> GetTajneed(int id)
        {
            return _ctx.Tajneeds.Where(r => r.Id == id)
                .Include(c=>c.NationalityDetail)
                .Include(c=>c.AuxilaryDetail)
                .Include(c=>c.CountryDetail)
                .Include(c=>c.RegionDetail)
                .Include(c=>c.TajneedIncomes.Select(a=>a.TypeName));
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false ;
            }
        }

        public bool AddTajneed(Tajneed newTajneed)
        {
            try
            {
                newTajneed.CreatedBy = 1;
                newTajneed.CreatedOn = DateTime.Now;
                _ctx.Tajneeds.Add(newTajneed);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }

        public bool UpdateTajneed(Tajneed updateTajneed)
        {
            try
            {
                _ctx.Entry(updateTajneed).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }



        public bool AddIncome(TajneedIncome newIncome)
        {
            try
            {
                newIncome.CreatedBy = 1;
                newIncome.CreatedOn = DateTime.Now;
                _ctx.TajneedIncomes.Add(newIncome);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
            
        }

        public bool UpdateIncome(TajneedIncome updateIncome)
        {
            try
            {
                _ctx.Entry(updateIncome).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }            
        }



        public async Task<int> GetTajneedCount()
        {
            return await Task.Run(() => _ctx.Tajneeds.Count());
        }


        public IQueryable<TajneedCount> GetTajneedAuxilaryCount()
        {
            return _ctx.Tajneeds
                .Where(c=>c.StatusId == 1)
                .Include(a=>a.AuxilaryDetail)
                .GroupBy(a => new { a.AuxilaryDetail.NameEn, a.AuxilaryId})
                .Select(c => new TajneedCount()
                {
                    CountId = c.Key.AuxilaryId, CountTotal = c.Count(), CountName = c.Key.NameEn
                });
        }


        public IQueryable<TajneedCount> GetTajneedRegionCount()
        {
            return _ctx.Tajneeds
                .Where(c => c.StatusId == 1)
                .Include(a => a.RegionDetail)
                .GroupBy(a => new {a.RegionDetail.RegionName, a.RegionId})
                .Select(c => new TajneedCount()
                {
                    CountId = c.Key.RegionId,
                    CountTotal = c.Count(),
                    CountName = c.Key.RegionName
                });

        }

        public IQueryable<TajneedCount> GetTajneedNationalityCount()
        {
            return _ctx.Tajneeds
                .Where(c => c.StatusId == 1)
                .Include(a => a.NationalityDetail)
                .GroupBy(a => new { a.NationalityDetail.NameEn, a.NationalityId })
                .Select(c => new TajneedCount()
                {
                    CountId = c.Key.NationalityId,
                    CountTotal = c.Count(),
                    CountName = c.Key.NameEn
                });

        }



        public IQueryable<TajneedCount> GetTajneedWassiyatCount()
        {
            return _ctx.Tajneeds
                .Where(c => c.StatusId == 1)
                .GroupBy(a => new { a.IsMosi })
                .Select(c => new TajneedCount()
                {
                    CountId = c.Key.IsMosi,
                    CountTotal = c.Count(),
                    CountName = c.Key.IsMosi == 1 ? "YES" : "NO"
                });
        }
    }
}
