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
        //[ForeignKey("YearBudget")]
        public int YearId { get; set; }

        [ForeignKey("ChandaTypeDetail")]
        public int ChandaTypeId { get; set; }

        [ForeignKey("AuxilaryDetail")]
        public int AuxilaryId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ValidationDetail ChandaTypeDetail { get; set; }

        public virtual ValidationDetail AuxilaryDetail { get; set; }

        public virtual ICollection<FinancialYearBudget> YearBudget { get; set; }

    }

    public class FinancialYearBudget : TableStrutcture
    {
        [Key]
        public int BudgetId { get; set; }

        //[ForeignKey("FinancialYearDetail")]
        [Required]
        public int YearId { get; set; }

        public decimal TotalIncome { get; set; }

        public double ApprovedAmount { get; set; }

        public double MarkazShare { get; set; }

        public double LocalShare { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        //public virtual FinancialYear FinancialYearDetail { get; set; }
    }

}


