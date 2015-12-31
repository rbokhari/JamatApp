using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.Models
{
    public class ValidationDetailModel
    {
        public int Id { get; set; }

        public int ValidationId { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }

        public string Description { get; set; }

        public int IsActive { get; set; }

        public virtual ICollection<ChandaSubHead> SubTypeDetails { get; set; }

    }
}
