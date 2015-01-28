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
        public async Task<IQueryable<JalsaDay>> Get(int id, int day)
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
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

    }
}
