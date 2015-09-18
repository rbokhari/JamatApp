using System;
using System.Collections.Generic;
using System.Data.Entity;
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


        public bool AddValidationDetail(ValidationDetail newValidationDetail, List<ChandaSubHead> chandaSubHeads)
        {
            try
            {

                //newValidationDetail.CreatedBy = 1;
                //newValidationDetail.CreatedOn = DateTime.UtcNow;
                _ctx.ValidationDetails.Add(newValidationDetail);

                if (Save())
                {
                    if (chandaSubHeads != null && chandaSubHeads.Count > 0)
                    {
                        List<ChandaSubHead> subs = chandaSubHeads;
                        foreach (var sub in subs)
                        {
                            if (sub.SubHeadId == 0)
                            {
                                sub.ChandaHeadId = newValidationDetail.Id;
                            }
                            _ctx.ChandaSubHeads.Add(sub);
                        }
                        
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateValidationDetail(ValidationDetail updateValidationDetail, List<ChandaSubHead> chandaSubHeads)
        {
            try
            {
                _ctx.Entry(updateValidationDetail).State = EntityState.Modified;

                if (chandaSubHeads != null && chandaSubHeads?.Count > 0)
                {
                    List<ChandaSubHead> subs = chandaSubHeads;

                    foreach (var sub in subs)
                    {
                        if (sub.SubHeadId == 0)
                        {
                            sub.ChandaHeadId = updateValidationDetail.Id;
                            _ctx.ChandaSubHeads.Add(sub);
                        }
                        else if (sub.SubHeadId != 0)
                        {
                            _ctx.Entry(sub).State = EntityState.Modified;
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }

        }
    }
}
