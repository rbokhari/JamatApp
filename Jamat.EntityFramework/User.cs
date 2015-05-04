using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    [Table("AC_User")]
    public class User : TableStrutcture
    {
        [Key]
        //[ForeignKey("YearBudget")]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public int RoleId { get; set; }

        public int TajneedId { get; set; }

        public int StatusId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

    }

}


