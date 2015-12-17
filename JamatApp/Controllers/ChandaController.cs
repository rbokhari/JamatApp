using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Jamat.DC.Interface;
using Jamat.EntityFramework;

namespace JamatApp.Controllers
{
    [RoutePrefix("api/chanda")]
    public class ChandaController : ApiController
    {
        private IChandaRepository _repo;

        public ChandaController(IChandaRepository repository)
        {
            _repo = repository;
        }

        [Route("GetChandaSubHead/{id}")]
        public async Task<IQueryable<ChandaSubHead>> GetChandaSubHead(int id)
        {
            try
            {
                return await _repo.GetChandaSubHead(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage AddChanda(Chanda chanda)
        {
            var _chanda = new Chanda
            {
                TajneedId = chanda.TajneedId,
                PeriodId = 0,
                BookId = chanda.BookId,
                ReceiptNo = chanda.ReceiptNo,
                TotalAmount = chanda.TotalAmount,
                IssuedBy = chanda.IssuedBy,
                IssuedOn = chanda.IssuedOn,
                CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First()),
                CreatedOn = DateTime.UtcNow
            };

            List<ChandaDetail> items =  chanda.ChandaDetails as List<ChandaDetail>;
            int? nullValue = null;
            items?.ForEach(
                c =>
                {
                    c.SubTypeId = c.SubTypeId == 0 ? nullValue : c.SubTypeId;
                    c.CreatedOn = DateTime.UtcNow;
                    c.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                });

            if (_repo.AddChanda(_chanda, items) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, _chanda);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
        }


        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
