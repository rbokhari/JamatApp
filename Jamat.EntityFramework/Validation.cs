﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    public class Validation : TableStrutcture
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }

        public string Description { get; set; }

        public int IsActive { get; set; }

        public int IsSystem { get; set; }

        public ICollection<ValidationDetail> ValidationDetails { get; set; }
    }

    public class ValidationDetail : TableStrutcture
    {
        public int Id { get; set; }

        public int ValidationId { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }

        public string Description { get; set; }

        public int IsActive { get; set; }

    }
}
