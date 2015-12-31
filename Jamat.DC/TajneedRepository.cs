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

        public async Task<IQueryable<Tajneed>> GetTajneedList()
        {
            return await Task.Run(() => 
                _ctx.Tajneeds
                .Include(c=>c.AuxilaryDetail)
                .Include(c=>c.RegionDetail)
                .Include(c=>c.NationalityDetail)
                .Include(c=>c.TajneedIncomes.Select(a=>a.TypeName))
                .OrderBy(x=>x.FirstName));
        }

        public async Task<IQueryable<Tajneed>> GetTajneedListByAuxilaryId(int id)
        {
            return await Task.Run(() => 
                _ctx.Tajneeds
                .Where(c=>c.AuxilaryId == id)
                .Include(c => c.AuxilaryDetail)
                .Include(c => c.RegionDetail)
                .Include(c => c.NationalityDetail)
                .Include(c => c.TajneedIncomes.Select(a => a.TypeName)));
        }

        public async Task<IQueryable<Tajneed>> GetTajneedListByNationalityId(int id)
        {
            return await Task.Run(() =>
                _ctx.Tajneeds
                .Where(c => c.NationalityId == id)
                .Include(c => c.AuxilaryDetail)
                .Include(c => c.RegionDetail)
                .Include(c => c.NationalityDetail)
                .Include(c => c.TajneedIncomes.Select(a => a.TypeName)));

        }

        public async Task<IQueryable<Tajneed>> GetTajneedListByRegionId(int id)
        {
            return await Task.Run(() =>
                _ctx.Tajneeds
                .Where(c => c.RegionId == id)
                .Include(c => c.AuxilaryDetail)
                .Include(c => c.RegionDetail)
                .Include(c=>c.NationalityDetail)
                .Include(c => c.TajneedIncomes.Select(a => a.TypeName)));
        }

        public async Task<IQueryable<Tajneed>> GetTajneedListByMosi()
        {
            return await Task.Run(() =>
                _ctx.Tajneeds
                .Where(c => c.IsMosi == 1)
                .Include(c => c.AuxilaryDetail)
                .Include(c => c.RegionDetail)
                .Include(c => c.NationalityDetail)
                .Include(c => c.TajneedIncomes.Select(a => a.TypeName)));
        }

        public async Task<IQueryable<Tajneed>> GetTajneedSearch(Tajneed search)
        {
            return await Task.Run(() =>
                _ctx.Tajneeds
                    .Where(c => 
                        c.FirstName.ToLower().Contains(search.FirstName.ToLower()) || 
                        c.FatherName.ToLower().Contains(search.FatherName.ToLower()) ||
                        c.HusbandName.ToLower().Contains(search.HusbandName.ToLower()) ||
                        c.MobileNumber.Contains(search.MobileNumber) ||
                        c.WassiyatNumber.Contains(search.WassiyatNumber) ||
                        c.ResidentNumber.Contains(search.ResidentNumber))
                    .Include(c => c.AuxilaryDetail)
                    .Include(c => c.RegionDetail)
                    .Include(c => c.NationalityDetail)
                    .Include(c => c.TajneedIncomes.Select(a => a.TypeName)));
        }

        public IQueryable<Tajneed> GetTajneed(int id)
        {
            return _ctx.Tajneeds.Where(r => r.Id == id)
                .Include(c => c.NationalityDetail)
                .Include(c => c.AuxilaryDetail)
                .Include(c => c.CountryDetail)
                .Include(c => c.RegionDetail)
                .Include(c => c.NationalityDetail)
                .Include(c => c.Chandas.Select(a => a.ChandaDetails.Select(b => b.YearDetail)))
                .Include(c => c.Chandas.Select(a => a.ChandaDetails.Select(b => b.MonthDetail)))
                .Include(c => c.Chandas.Select(a => a.ChandaDetails.Select(b => b.TypeDetail)))
                .Include(c => c.Chandas.Select(a => a.ChandaDetails.Select(b => b.SubTypeDetail)))
                .Include(c => c.TajneedIncomes.Select(a => a.TypeName));
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                // TODO log this error
                return false ;
            }
        }

        public bool AddTajneed(Tajneed newTajneed)
        {
            try
            {
                _ctx.Tajneeds.Add(newTajneed);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }

        public bool UpdateTajneed(Tajneed updateTajneed)
        {
            try
            {
                var tajneedFetch = GetTajneed(updateTajneed.Id).FirstOrDefault();
                var attachTajneed = _ctx.Entry(tajneedFetch);
                attachTajneed.CurrentValues.SetValues(updateTajneed);
                //_ctx.Entry(updateTajneed).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }


        public bool AddIncome(TajneedIncome newIncome)
        {
            try
            {

                _ctx.TajneedIncomes.Add(newIncome);
                return true;
            }
            catch (Exception)
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
            catch (Exception)
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

        public bool UpdateDocuments(TajneedCard tajneedCard)
        {
            var card = _ctx.TajneedCards
                    .SingleOrDefault(c => 
                            c.TajneedId == tajneedCard.TajneedId && 
                            c.CardTypeId == tajneedCard.CardTypeId);

            if (card!= null)
            {
                _ctx.TajneedCards.Remove(card);
            }

            _ctx.TajneedCards.Add(tajneedCard);

            return true;
        }


    }
}
