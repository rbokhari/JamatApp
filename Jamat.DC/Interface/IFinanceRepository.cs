using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public interface IFinanceRepository
    {
        Task<IQueryable<FinancialYear>> GetFiancialYears();

        Task<FinancialYear> GetFiancialYear(int id);

        bool Save();

        bool AddFinancialYear(FinancialYear newFinancialYear);


    }
}
