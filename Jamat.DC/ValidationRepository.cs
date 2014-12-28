using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public class ValidationRepository: IValidationRepository
    {
        private DbEntityContext _ctx;

        public ValidationRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<ValidationDetail> GetValidationDetails(int id)
        {
            return _ctx.ValidationDetails.Where(r => r.ValidationId == id);
        }

        public EntityFramework.ValidationDetail GetValidationDetail(int vId)
        {
            return _ctx.ValidationDetails.Single(r => r.Id == vId);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool AddValidationDetail(ValidationDetail newValidationDetail)
        {
            try
            {
                newValidationDetail.CreatedBy = 1;
                newValidationDetail.CreatedOn = DateTime.UtcNow;
                _ctx.ValidationDetails.Add(newValidationDetail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
