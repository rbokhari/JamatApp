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

        [Key]
        public int ChandaDetailId { get; set; }

        public int ChandaId { get; set; }

        public int YearId { get; set; }

        public int MonthId { get; set; }

        public int TypeId { get; set; }

        public int SubTypeId { get; set; }

        public decimal ChandaAmount { get; set; }

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


