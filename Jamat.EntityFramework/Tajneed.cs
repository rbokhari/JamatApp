using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Mandatory field missing !")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mandatory field missing !")]
        public string FatherName { get; set; }

        public string HusbandName { get; set; }

        [Required(ErrorMessage = "Mandatory field missing !")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Mandatory field missing !")]
        public int AuxilaryId { get; set; }

        public int NationalityId { get; set; }

        public int CountryId { get; set; }

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

    }

    public class TajneedIncome : TableStrutcture
    {
        [Key]
        public int IncomeId { get; set; }


        public int TajneedId { get; set; }

        public int TajneedTypeId { get; set; }

        public string CompanyName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal IncomeAmount { get; set; }

        public int StatusId { get; set; }

    }

    public class TajneedCard : TableStrutcture
    {
        [Key]
        public int CardId { get; set; }

        public int TajneedId { get; set; }

        public int CardTypeId { get; set; }

        public string CardNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }



    }

}
