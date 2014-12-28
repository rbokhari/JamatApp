using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public class CountryRepository : ICountryRepository
    {
        private DbEntityContext _ctx;

        public CountryRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<Country>> GetCountries()
        {
            return await Task.Run(() => _ctx.Countries);
        }

        public Country GetCountry(int id)
        {
            return _ctx.Countries.Single(r => r.CountryId == id);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool AddCountry(Country newCountry)
        {
            try
            {
                newCountry.CreatedBy = 1;
                newCountry.CreatedOn = DateTime.UtcNow;
                _ctx.Countries.Add(newCountry);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}

