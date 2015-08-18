using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    public class FinancialYearTajneedBudget : TableStrutcture
    {
        [Key]
        public int TajneedBudgetId { get; set; }

        public int YearId { get; set; }

        public int TajneedId { get; set; }

        public decimal TajneedIncome { get; set; }

        public int StatusId { get; set; }

    }


    public class FinancialYearTajneedPaid : TableStrutcture
    {
        [Key]
        public int TajneedPaidId { get; set; }

        public int TajneedBudgetId { get; set; }

        [MaxLength(50)]
        public String ReceiptNo { get; set; }

        public decimal ChandaTypeId { get; set; }

        public decimal PaidAmount { get; set; }

        
    }
}
