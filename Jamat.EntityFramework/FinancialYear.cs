using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public int? ApprovalByStep1 { get; set; }

        public DateTime? ApprovalByStep1Date { get; set; }

        [MaxLength(500)]
        public String ApprovalByStep1Remarks { get; set; }


        public int? ApprovalByStep2 { get; set; }

        public DateTime? ApprovalByStep2Date { get; set; }

        [MaxLength(500)]
        public String ApprovalByStep2Remarks { get; set; }


        public int? ApprovalByStep3 { get; set; }

        public DateTime? ApprovalByStep3Date { get; set; }

        [MaxLength(500)]
        public String ApprovalByStep3Remarks { get; set; }

        public int StatusId { get; set; }

        //public virtual FinancialYear FinancialYearDetail { get; set; }

        public virtual ICollection<FinancialYearBudgetSub> FinancialYearBudgetSub { get; set; }
    }


    public class FinancialYearBudgetSub : TableStrutcture
    {
        [Key]
        public int BudgetSubId { get; set; }

        [Required]
        public int BudgetId { get; set; }

        [MaxLength(500)]
        public string SubTitle { get; set; }


        public decimal SubAmount { get; set; }


    }

}


