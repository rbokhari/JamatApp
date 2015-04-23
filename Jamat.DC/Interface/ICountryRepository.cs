using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public interface ICountryRepository
    {
        Task<IQueryable<Country>> GetCountries();

        Country GetCountry(int id);

        bool Save();

        bool AddCountry(Country newCountry);
    }
}
