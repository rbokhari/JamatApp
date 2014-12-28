﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public class RegionRepository : IRegionRepository
    {
        private DbEntityContext _ctx;

        public RegionRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
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

        public async Task<IQueryable<Region>> GetRegions()
        {
            return _ctx.Regions;
        }

        public Region GetRegion(int id)
        {
            return _ctx.Regions.Single(r => r.RegionId == id);
        }

        public bool AddRegion(Region newRegion)
        {
            try
            {
                newRegion.CreatedBy = 1;
                newRegion.CreatedOn = DateTime.UtcNow;
                _ctx.Regions.Add(newRegion);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}
