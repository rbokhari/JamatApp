using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;
using System.Data.Entity;

namespace Jamat.DC
{
    public class TajneedRepository : ITajneedRepository
    {
        DbEntityContext _ctx;

        public TajneedRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<EntityFramework.Tajneed>> GetTajneedList()
        {
            return await Task.Run(() => _ctx.Tajneeds);
        }

        public EntityFramework.Tajneed GetTajneed(int id)
        {
            return _ctx.Tajneeds.Single(r => r.Id == id);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false ;
            }
        }

        public bool AddTajneed(Tajneed newTajneed)
        {
            try
            {
                newTajneed.CreatedBy = 1;
                newTajneed.CreatedOn = DateTime.Now;
                _ctx.Tajneeds.Add(newTajneed);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }

        public bool UpdateTajneed(Tajneed updateTajneed)
        {
            try
            {
                _ctx.Entry(updateTajneed).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }
    }
}
