using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    public class Chanda : TableStrutcture
    {
        [Key]
        public int ChandaId { get; set; }

        public String ComputerCode { get; set; }

        public int TajneedId { get; set; }

        public int PeriodId { get; set; }       // yearid

        public int BookId { get; set; }

        public int ReceiptNo { get; set; }

        public decimal TotalAmount { get; set; }

        public int IssuedBy { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ChandaDetail> ChandaDetails { get; set; }

    }

    public class ChandaDetail : TableStrutcture
    {
        public ChandaDetail()
        {
            
        }

        [Key]
        public int ChandaDetailId { get; set; }

        public int ChandaId { get; set; }

        [ForeignKey("YearDetail")]
        public int YearId { get; set; }

        [ForeignKey("MonthDetail")]
        public int MonthId { get; set; }

        [ForeignKey("TypeDetail")]
        public int TypeId { get; set; }

        [ForeignKey("SubTypeDetail")]
        public int SubTypeId { get; set; }

        public decimal ChandaAmount { get; set; }

        public virtual FinancialYear YearDetail { get; set; }
        public virtual ValidationDetail MonthDetail { get; set; }
        public virtual ValidationDetail TypeDetail { get; set; }
        public virtual ChandaSubHead SubTypeDetail { get; set; }

    }


    public class ChandaSubHead : TableStrutcture
    {
        [Key]
        public int SubHeadId { get; set; }

        public int ChandaHeadId  { get; set; } // Coming from ValidationDetail Id

        public string SubHeadName { get; set; }

        public String Description { get; set; }

        public int StatusId { get; set; }

    }

}


