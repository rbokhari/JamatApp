using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Jamat.DC;
using Jamat.EntityFramework;

namespace JamatApp.Controllers
{
    public class CountryController : ApiController
    {
        public ICountryRepository _repo;

        public CountryController(ICountryRepository repository)
        {
            _repo = repository;
        }


        public async Task<IQueryable<Country>> Get()
        {
            var countries = _repo.GetCountries();

            return await countries;
        }

    }
}

