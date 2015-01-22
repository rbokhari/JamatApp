using Jamat.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Jamat.DC
{
    public class JalsaRepository : IJalsaRepository, IDisposable
    {
        private DbEntityContext _ctx;

        public JalsaRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<Jalsa>> GetJalsas(int id)
        {
            return
                await
                    Task.Run(
                        () =>
                            _ctx.Jalsas.Include(c => c.JalsaDays.Select(e => e.RegionDetail))
                                .Where(c => c.JalsaId == id));
        }

        public Jalsa GetJalsaById(int id)
        {
            return _ctx.Jalsas.Single(c => c.JalsaId == id);
        }

        public bool SaveJalsa()
        {
            return true;
        }

        public bool AddJalsa()
        {
            return true;
        }

        public bool AddJalsaDay()
        {
            return true;
        }


        public async Task<Jalsa> GetJalsaDays(int id)
        {
            return await Task.Run(() => _ctx.Jalsas.Include(c=>c.JalsaDays).Single(c => c.JalsaId == id));
        }


        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
