using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;
using Jamat.GLobalVariables;

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
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<FinancialYear>> GetFiancialYears()
        {
            return await Task.Run(() => 
                _ctx.FinancialYears
                .Include(a=>a.ChandaTypeDetail)
                .Include(a=>a.AuxilaryDetail)
                .Include(a=>a.YearBudget
                        .Select(c=>c.FinancialYearBudgetSub)));
        }

        public async Task<FinancialYear> GetFiancialYear(int id)
        {
            return await _ctx.FinancialYears
                .Where(r => r.YearId == id)
                .Include(a => a.ChandaTypeDetail)
                .Include(a => a.AuxilaryDetail)
                .Include(c => c.YearBudget)
                .Include(a=>a.YearBudget.Select(c=>c.FinancialYearBudgetSub))
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
            catch (Exception)
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
            catch (Exception)
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


        public bool AddFinancialBudgetSub(FinancialYearBudgetSub newBudgetSub)
        {
            try
            {
                newBudgetSub.CreatedBy = 1;
                newBudgetSub.CreatedOn = DateTime.UtcNow;
                _ctx.FinancialYearBudgetSubs.Add(newBudgetSub);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<FinancialYearBudgetSub>> GetFiancialYearBudgetSubs(int budgetId)
        {
            return await Task.Run(() => _ctx.FinancialYearBudgetSubs.Where(c => c.BudgetId == budgetId));
        }


        public async Task<FinancialYearBudgetSub> GetFiancialYearBudgetSubsBySubId(int subBudgetId)
        {
            return await Task.Run(() => _ctx.FinancialYearBudgetSubs.Where(c => c.BudgetSubId == subBudgetId).SingleOrDefaultAsync());
        }


        public bool SendForApproval(int id)
        {
            var budget = _ctx.FinancialYearBudgets.Single(c => c.BudgetId == id);

            budget.StatusId = (Int32)ApplicationPreferences.Validation_Details.CHANDA_BUDGET_STATUS_INITIATE;

            _ctx.Entry(budget).State = EntityState.Modified;

            return true;
        }

        public bool NextLevelApproval(FinancialYearBudget financialYearBudget, int levelId)
        {
            var budget = _ctx.FinancialYearBudgets.Single(c => c.BudgetId == financialYearBudget.BudgetId);

            switch (levelId)
            {
                case (Int32)ApplicationPreferences.Validation_Details.CHANDA_BUDGET_STATUS_INITIATE:
                    budget.ApprovalByStep1 = 1;
                    budget.ApprovalByStep1Date = financialYearBudget.ApprovalByStep1Date;
                    budget.ApprovalByStep1Remarks = financialYearBudget.ApprovalByStep1Remarks;

                    break;
                case (Int32)ApplicationPreferences.Validation_Details.CHANDA_BUDGET_STATUS_FIRST_APPROVAL:
                    budget.ApprovalByStep2 = 2;
                    budget.ApprovalByStep2Date = financialYearBudget.ApprovalByStep2Date;
                    budget.ApprovalByStep2Remarks = financialYearBudget.ApprovalByStep2Remarks;

                    break;
                case (Int32)ApplicationPreferences.Validation_Details.CHANDA_BUDGET_STATUS_SECOND_APPROVAL:
                    budget.ApprovalByStep3 = 3;
                    budget.ApprovalByStep3Date = financialYearBudget.ApprovalByStep3Date;
                    budget.ApprovalByStep3Remarks = financialYearBudget.ApprovalByStep3Remarks;

                    break;

                case (Int32)ApplicationPreferences.Validation_Details.CHANDA_BUDGET_STATUS_APPROVED:

                    break;

            }

            _ctx.Entry(budget).State = EntityState.Modified;


            return true;
        }
    }

}
