using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    public class Country : TableStrutcture
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string CountryNameAr { get; set; }

        public int StatusId { get; set; }

    }

    public class Region : TableStrutcture
    {

        [Key]
        public int RegionId { get; set; }

        public int CountryId { get; set; }

        public string RegionName { get; set; }

        public string RegionNameAr { get; set; }

        public int StatusId { get; set; }
    }
}
