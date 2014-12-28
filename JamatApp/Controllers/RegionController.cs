using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jamat.DC;
using Jamat.EntityFramework;
using System.Threading.Tasks;

namespace JamatApp.Controllers
{
    public class RegionController : ApiController
    {
        public IRegionRepository _repo;

        public RegionController(IRegionRepository repository)
        {
            _repo = repository;
        }


        public async Task<IQueryable<Region>> Get()
        {
            var regions = _repo.GetRegions();

            return await regions;
        }

    }
}
