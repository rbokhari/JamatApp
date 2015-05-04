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
    [Authorize]
    public class TajneedController : ApiController
    {
        public ITajneedRepository _repo;

        public TajneedController(ITajneedRepository repository)
        {
            _repo = repository;
        }

        [Route("api/tajneed/")]
        [HttpGet]
        public Task<IQueryable<Tajneed>> Get()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            var tajneeds = _repo.GetTajneedList();

            return tajneeds;
        }

        [Route("api/tajneed/getTajneedCount")]
        public Task<Int32> GetTajneedCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            return _repo.GetTajneedCount();
        }

        
        [Route("api/tajneed/getTajneedAuxilary")]
        public IQueryable<TajneedCount> GetTajneedAuxilaryCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            return _repo.GetTajneedAuxilaryCount();
            
        }

        
        [Route("api/tajneed/getTajneedRegion")]
        public IQueryable<TajneedCount> GetTajneedRegionCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            return _repo.GetTajneedRegionCount();

        }

        
        [Route("api/tajneed/getTajneedNationality")]
        public IQueryable<TajneedCount> GetTajneedNationalityCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            return _repo.GetTajneedNationalityCount();

        }

        
        [Route("api/tajneed/getTajneedWassiyat")]
        public IQueryable<TajneedCount> GetTajneedWassiyatCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            return _repo.GetTajneedWassiyatCount();

        }

        
        public IQueryable<Tajneed> Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var tajneed = _repo.GetTajneed(id);

            if (tajneed == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return tajneed;
        }

        [Route("api/tajneed/")]
        [HttpPost]
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

        [ActionName("PostTajneedIncome")]
        [HttpPost]
        public HttpResponseMessage AddTajneedIncome([FromBody] TajneedIncome newIncome)
        {
            if (ModelState.IsValid)
            {
                if (newIncome.IncomeId == 0)
                {
                    //if (Request.Headers.Contains("userId"))
                    //{
                    //    newIncome.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    //}
                    newIncome.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddIncome(newIncome) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newIncome);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newIncome.IncomeId != 0)
                {
                    //if (Request.Headers.Contains("userId"))
                    //{
                    //    newIncome.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    //}
                    newIncome.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateIncome(newIncome) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newIncome);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }


        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
        
    }
}


