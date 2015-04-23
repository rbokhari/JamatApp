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
    [RoutePrefix("api")]
    public class FinanceController : ApiController
    {

        public IFinanceRepository _repo;

        public FinanceController(IFinanceRepository repository)
        {
            _repo = repository;
        }

        [Route("finance")]
        [HttpGet]
        public async Task<IQueryable<FinancialYear>> Get()
        {
            var years = _repo.GetFiancialYears();

            return await years;
        }

        [Route("finance")]
        public async Task<HttpResponseMessage> Post([FromBody] FinancialYear newYear)
        {
            if (ModelState.IsValid)
            {
                if (newYear.YearId == 0)
                {
                    if (_repo.AddFinancialYear(newYear) && _repo.Save())
                    {
                        newYear = await _repo.GetFiancialYear(newYear.YearId);
                        return Request.CreateResponse(HttpStatusCode.Created, newYear);
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
