﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jamat.DC;
using Jamat.EntityFramework;

namespace JamatApp.Controllers
{
    public class ValidationController : ApiController
    {
        public IValidationRepository _repo;

        public ValidationController(IValidationRepository pRepository)
        {
            _repo = pRepository;
        }

        [ActionName("GetValidationDetailByValidationId")]
        [HttpGet]
        public IQueryable<ValidationDetail> ValiationDetailsByValidationId(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var validationDetails = _repo.GetValidationDetails(id);

            return validationDetails;
        }

        public ValidationDetail Get(int vId)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var validationDetail = _repo.GetValidationDetail(vId);

            if (validationDetail == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return validationDetail;
        }

        public HttpResponseMessage Post([FromBody] ValidationDetail newValidationDetail)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddValidationDetail(newValidationDetail) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newValidationDetail);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        public HttpResponseMessage Put(int id, [FromBody] ValidationDetail updateValidationDetail)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                //if (_repo(updateDepartment) && _repo.Save())
                //{
                //    return Request.CreateResponse(HttpStatusCode.Created, updateDepartment);
                //    //return new HttpResponseMessage(HttpStatusCode.OK);
                //}
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
