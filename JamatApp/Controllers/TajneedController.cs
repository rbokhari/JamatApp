using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Jamat.DC;
using Jamat.EntityFramework;

namespace JamatApp.Controllers
{
    public class TajneedController : ApiController
    {
        public ITajneedRepository _repo;

        public TajneedController(ITajneedRepository repository)
        {
            _repo = repository;
        }
        public Task<IQueryable<Tajneed>> Get()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            var tajneeds = _repo.GetTajneedList();

            return tajneeds;
        }

        public Tajneed Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var tajneed = _repo.GetTajneed(id);

            if (tajneed == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return tajneed;
        }

        public HttpResponseMessage Post([FromBody] Tajneed newTajneed)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddTajneed(newTajneed) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newTajneed);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        public HttpResponseMessage Put(int id, [FromBody] Tajneed updateTajneed)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (_repo.UpdateTajneed(updateTajneed) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateTajneed);
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


