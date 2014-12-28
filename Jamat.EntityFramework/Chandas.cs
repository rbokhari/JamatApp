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

        public int TajneedId { get; set; }

        public int PeriodId { get; set; }

        public int BookId { get; set; }

        public int ReceiptNo { get; set; }

        public decimal TotalAmount { get; set; }

        public int IssuedBy { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Description { get; set; }

    }

    public class ChandaDetail : TableStrutcture
    {

        [Key]
        public int ChandaDetailId { get; set; }

        public int ChandaId { get; set; }

        public int ChandaTypeId { get; set; }

        public decimal ChandaAmount { get; set; }

        public DateTime PaidDate { get; set; }

    }

}


