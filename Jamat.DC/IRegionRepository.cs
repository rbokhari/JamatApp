using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public interface IRegionRepository
    {
        Task<IQueryable<Region>> GetRegions(int id);

        Region GetRegion(int id);

        bool Save();

        bool AddRegion(Region newRegion);
    }
}
