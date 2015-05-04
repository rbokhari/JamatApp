using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    [Table("AC_Role")]
    public class Role : TableStrutcture
    {
        [Key]
        //[ForeignKey("YearBudget")]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int StatusId { get; set; }


        [MaxLength(500)]
        public string Description { get; set; }

    }

}


