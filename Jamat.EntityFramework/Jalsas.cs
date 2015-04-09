using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    public class Jalsa : TableStrutcture
    {
        [Key]
        public int JalsaId { get; set; }

        [StringLength(255)]
        public string JalsaName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int StatusId { get; set; }

        public virtual ICollection<JalsaDay> JalsaDays { get; set; }
    }

    public class JalsaDay : TableStrutcture
    {

        [Key]
        public int JalsaDayId { get; set; }

        public int JalsaId { get; set; }

        public int DayId { get; set; }

        public DateTime? JalsaDate { get; set; }

        [StringLength(500)]
        public string FmailyPersonName { get; set; }

        public int Ansar { get; set; }

        public int Khuddam { get; set; }

        public int Atfal { get; set; }

        public int Lajnaat { get; set; }

        public int Nassrat { get; set; }

        public int Child { get; set; }

        [ForeignKey("CountryDetail")]
        public int CountryId { get; set; }

        [ForeignKey("RegionDetail")]
        public int RegionId { get; set; }

        [StringLength(255)]
        public string ContactNo { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual Country CountryDetail { get; set; }

        public virtual Region RegionDetail { get; set; }
    }

}
