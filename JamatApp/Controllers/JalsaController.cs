using Jamat.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Jamat.EntityFramework;

namespace JamatApp.Controllers
{
    [RoutePrefix("api/jalsa")]
    public class JalsaController : ApiController
    {
        public IJalsaRepository _repo;

        public JalsaController(IJalsaRepository repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IQueryable<Jalsa> Get()
        {
            var jalsas = _repo.GetJalsaList();
            return jalsas;
        }

        [HttpGet]
        public async Task<IQueryable<JalsaDay>> Get(int id, int day)
        {
            var jalsas = await _repo.GetJalsaDays(id, day);
            return jalsas;
        }

        [HttpGet]
        public Jalsa GetJalsaById(int id)
        {
            return _repo.GetJalsaById(id);
        }


        [HttpGet]
        [Route("GetCount")]
        public IQueryable GetCount(int id)
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            var jalsas = _repo.GetJalsaSummary(id);

            return jalsas;
        }

        [HttpGet]
        [Route("GetCountByCountry")]
        public IQueryable GetCountByCountry(int id)
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            var jalsas = _repo.GetJalsaSummaryByCountry(id);

            return jalsas;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Jalsa newJalsa)
        {
            if (ModelState.IsValid)
            {
                if (newJalsa.JalsaId == 0)
                {
                    if (_repo.AddJalsa(newJalsa) && _repo.SaveJalsa())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newJalsa);
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [HttpPost]
        [Route("SaveAttendance")]
        public HttpResponseMessage SaveAttendance([FromBody] JalsaDay newJalsaDay)
        {
            if (ModelState.IsValid)
            {
                if (newJalsaDay.JalsaDayId == 0)
                {
                    if (_repo.AddJalsaDay(newJalsaDay) && _repo.SaveJalsa())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newJalsaDay);
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }


    }
}
