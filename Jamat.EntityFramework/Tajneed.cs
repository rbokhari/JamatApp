using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    public class Tajneed : TableStrutcture
    {
        public int Id { get; set; }

        public string TajneedCode { get; set; }

        [Required(ErrorMessage = "Mandatory field missing !")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public string HusbandName { get; set; }

        public DateTime? BirthDate { get; set; }

        [ForeignKey("AuxilaryDetail")]
        [Required(ErrorMessage = "Mandatory field missing !")]
        public int AuxilaryId { get; set; }

        [ForeignKey("NationalityDetail")]
        public int NationalityId { get; set; }

        [ForeignKey("CountryDetail")]
        public int? CountryId { get; set; }

        [ForeignKey("RegionDetail")]
        public int RegionId { get; set; }

        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        public string BusinessNumber { get; set; }

        public string ResidentNumber { get; set; }

        public string PassportNumber { get; set; }

        public string CurrentAddress { get; set; }

        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Mandatory field missing !")]
        public int IsMosi { get; set; }

        public string WassiyatNumber { get; set; }

        public decimal CurrentIncome { get; set; }

        public int StatusId { get; set; }

        public virtual Country CountryDetail { get; set; }

        public virtual Region RegionDetail { get; set; }

        public virtual ValidationDetail AuxilaryDetail { get; set; }

        public virtual ValidationDetail NationalityDetail { get; set; }

        public virtual ICollection<TajneedIncome> TajneedIncomes { get; set; }

        public virtual ICollection<Chanda> Chandas { get; set; }


        public virtual ICollection<TajneedCard> Cards { get; set; }

    }

    public class TajneedIncome : TableStrutcture
    {
        [Key]
        public int IncomeId { get; set; }

        public int TajneedId { get; set; }

        [ForeignKey("TypeName")]
        public int TajneedTypeId { get; set; }

        public string CompanyName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal IncomeAmount { get; set; }

        public int StatusId { get; set; }

        public virtual ValidationDetail TypeName { get; set; }
    }

    public class TajneedCard : TableStrutcture
    {
        [Key]
        public int CardId { get; set; }

        public int TajneedId { get; set; }

        public int CardTypeId { get; set; } // e.g. Picture, passport , id card etc

        [Column(TypeName = "image")]
        public byte[] CardImage { get; set; }


        public int StatusId { get; set; }



    }

}
