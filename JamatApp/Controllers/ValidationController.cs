using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jamat.DC;
using Jamat.EntityFramework;
using Jamat.Models;

namespace JamatApp.Controllers
{
    [RoutePrefix("api/validation")]
    public class ValidationController : ApiController
    {
        public IValidationRepository _repo;

        public ValidationController(IValidationRepository pRepository)
        {
            _repo = pRepository;
        }

        [ActionName("GetValidationDetailByValidationId")]
        [HttpGet]
        [Route("{id}/GetValidationDetailByValidationId")]
        public IQueryable<ValidationDetail> ValiationDetailsByValidationId(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var validationDetails = _repo.GetValidationDetails(id);

            return validationDetails;
        }

        public ValidationDetail Get(int id)
        {
            var validationDetail = _repo.GetValidationDetail(id);
            if (validationDetail == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return validationDetail;
        }

        public HttpResponseMessage Post([FromBody] ValidationDetailModel newValidationDetail)
        {
            if (ModelState.IsValid)
            {
                var validationDetail = new ValidationDetail
                {
                    ValidationId = newValidationDetail.ValidationId,
                    NameEn = newValidationDetail.NameEn,
                    NameAr = newValidationDetail.NameAr,
                    Description = newValidationDetail.Description,
                    IsActive = newValidationDetail.IsActive,
                    MachineName = "",
                    MachineUser = "",
                    CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First()),
                    CreatedOn = DateTime.Now
                };

                List<ChandaSubHead> chandaSubHeadList = null;
                if (newValidationDetail.SubTypeDetails!=null || newValidationDetail.SubTypeDetails?.Count> 0)
                {
                    chandaSubHeadList = newValidationDetail.SubTypeDetails.Select(sub => new ChandaSubHead
                    {
                        ChandaHeadId = 0,
                        SubHeadName = sub.SubHeadName,
                        Description = "",
                        StatusId = sub.StatusId,
                        MachineName = "",
                        MachineUser = "",
                        CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First()),
                        CreatedOn = DateTime.Now

                    }).ToList();
                }

                if (_repo.AddValidationDetail(validationDetail, chandaSubHeadList) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newValidationDetail);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        public HttpResponseMessage Put([FromBody] ValidationDetailModel updateValidationDetail)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                var validationDetail = new ValidationDetail
                {
                    Id = updateValidationDetail.Id,
                    ValidationId = updateValidationDetail.ValidationId,
                    NameEn = updateValidationDetail.NameEn,
                    NameAr = updateValidationDetail.NameAr,
                    Description = updateValidationDetail.Description,
                    IsActive = updateValidationDetail.IsActive,
                    MachineName = "",
                    MachineUser = "",
                    ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First()),
                    ModifiedOn = DateTime.Now
                };

                List<ChandaSubHead> chandaSubHeadList = null;
                if (updateValidationDetail.SubTypeDetails != null || updateValidationDetail.SubTypeDetails?.Count > 0)
                {
                    chandaSubHeadList = updateValidationDetail.SubTypeDetails.Select(sub => new ChandaSubHead
                    {
                        SubHeadId = sub.SubHeadId,
                        ChandaHeadId = sub.ChandaHeadId,
                        SubHeadName = sub.SubHeadName,
                        Description = sub.Description,
                        StatusId = sub.StatusId,
                        MachineName = "",
                        MachineUser = "",
                        CreatedBy = sub.CreatedBy,
                        CreatedOn = sub.CreatedOn, 
                        ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First()),
                        ModifiedOn = DateTime.Now

                    }).ToList();
                }

                if (_repo.UpdateValidationDetail(validationDetail, chandaSubHeadList) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, validationDetail);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }   
    }
}
