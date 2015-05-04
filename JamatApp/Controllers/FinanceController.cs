using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [Authorize]
        [Route("finance")]
        [HttpGet]
        public async Task<IQueryable<FinancialYear>> Get()
        {
            var years = _repo.GetFiancialYears();

            return await years;
        }


        [Authorize]
        [Route("finance/{id}")]
        [HttpGet]
        public async Task<FinancialYear> Get(int id)
        {
            var year = _repo.GetFiancialYear(id);

            return await year;
        }


        [Authorize]
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


        [Authorize]
        [Route("finance/getAuxilaryIncome/{id}")]
        public IQueryable GetAuxilaryIncome(int id)
        {
            return _repo.GetTajneedAuxilaryIncome(id);
        }

        [Authorize]
        [Route("finance/setAuxilaryBudget/{yearId}/{notes}")]
        public async Task<HttpResponseMessage> SetAuxilaryBudget(int yearId, string notes)
        {
            var year = await _repo.GetFiancialYear(yearId);

            var income = _repo.GetTajneedAuxilaryIncome(year.AuxilaryId);
            double incomeTotal= 0;

            foreach (var abc in income)
            {
                var total = abc.GetType().GetProperty("incomeTotal").GetValue(abc, null);
                incomeTotal = Convert.ToDouble(total);
            }

            var budget = new FinancialYearBudget()
            {
                YearId = yearId,
                TotalIncome = Convert.ToDecimal(incomeTotal),
                ApprovedAmount = ((incomeTotal * 12) / 100) + ((incomeTotal * 2.5) / 100),
                MarkazShare = (((incomeTotal * 12) / 100) * 30) / 100,
                LocalShare = (((incomeTotal * 12) / 100) * 30) / 100,
                Description =  notes
            };

            if (_repo.AddFinancialYearBudget(budget) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, budget);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());

        }

        [Authorize]
        [Route("finance/getBudget/{yearId}")]
        [HttpGet]
        public async Task<FinancialYearBudget> GetBudget(int yearId)
        {
            var budget = await _repo.GetFiancialYearBudget(yearId);

            return budget;
        }



        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }


    }
}
