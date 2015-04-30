using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
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
            return await Task.Run(() => 
                _ctx.FinancialYears
                .Include(a=>a.ChandaTypeDetail)
                .Include(a=>a.AuxilaryDetail));
        }

        public async Task<FinancialYear> GetFiancialYear(int id)
        {
            return await _ctx.FinancialYears
                .Where(r => r.YearId == id)
                .Include(a => a.ChandaTypeDetail)
                .Include(a => a.AuxilaryDetail)
                .Include(c => c.YearBudget)
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

        public IQueryable GetTajneedAuxilaryIncome(int auxilaryId)
        {
            return _ctx.Tajneeds
                .Where(a=>a.AuxilaryId == auxilaryId)
                .Include(c => c.AuxilaryDetail)
                .Include(c => c.TajneedIncomes)
                .Select(x => new
                {
                    chandaAuxilaryId = x.AuxilaryId,
                    //chandaTypeId = x.AuxilaryId,
                    //chandaTypeName = x.AuxilaryDetail.NameEn,
                    income = x.TajneedIncomes.Sum(c => c.IncomeAmount) 
                    //chandaTotal = x.TajneedIncomes.Sum(c => c.IncomeAmount),
                    //markazShare = x.TajneedIncomes.Sum(c => c.IncomeAmount),
                    //localShare = x.TajneedIncomes.Sum(c => c.IncomeAmount)
                })
                .GroupBy(a=>a.chandaAuxilaryId)
                .Select(a=> new
                {
                    incomeTotal = a.Sum(b => b.income)
                });

        }



        public bool AddFinancialYearBudget(FinancialYearBudget newFinancialYearBudget)
        {
            try
            {
                newFinancialYearBudget.CreatedBy = 1;
                newFinancialYearBudget.CreatedOn = DateTime.UtcNow;
                _ctx.FinancialYearBudgets.Add(newFinancialYearBudget);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public async Task<IQueryable<FinancialYearBudget>> GetFiancialYearBudgets()
        {
            return await Task.Run(() =>
                _ctx.FinancialYearBudgets);
        }


        public async Task<FinancialYearBudget> GetFiancialYearBudget(int yearId)
        {
            return await _ctx.FinancialYearBudgets
                .Where(c => c.YearId == yearId)
                .FirstOrDefaultAsync();
        }
    }

}
