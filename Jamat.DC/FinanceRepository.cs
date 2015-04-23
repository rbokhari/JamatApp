using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public class FinanceRepository : IFinanceRepository
    {
        DbEntityContext _ctx;

        public FinanceRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IQueryable<FinancialYear>> GetFiancialYears()
        {
            return await Task.Run(() => _ctx.FinancialYears.Include(a=>a.ChandaTypeDetail));
        }

        public async Task<FinancialYear> GetFiancialYear(int id)
        {
            return await _ctx.FinancialYears
                .Where(r => r.YearId == id)
                .Include(a=>a.ChandaTypeDetail)
                .FirstOrDefaultAsync();
        }

        public bool AddFinancialYear(FinancialYear newFinancialYear)
        {
            try
            {
                newFinancialYear.CreatedBy = 1;
                newFinancialYear.CreatedOn = DateTime.UtcNow;
                _ctx.FinancialYears.Add(newFinancialYear);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}
