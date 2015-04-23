using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    public class FinancialYear : TableStrutcture
    {
        [Key]
        public int YearId { get; set; }


        [ForeignKey("ChandaTypeDetail")]
        public int ChandaTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }

        public string Description { get; set; }

        public virtual ValidationDetail ChandaTypeDetail { get; set; }

    }

    public class FinancialYearBudget : TableStrutcture
    {
        [Key]
        public int BudgetId { get; set; }

        [ForeignKey("FinancialYearDetail")]
        public int YearId { get; set; }

        public decimal ApprovedAmount { get; set; }

        public decimal MarkazShare { get; set; }


        public decimal LocalShare { get; set; }

        public string Description { get; set; }


        public virtual FinancialYear FinancialYearDetail { get; set; }


    }

}


