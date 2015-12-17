using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.DC.Interface;
using Jamat.EntityFramework;
using Jamat.GLobalVariables;

namespace Jamat.DC
{
    public class ChandaRepository : IChandaRepository
    {
        private DbEntityContext _ctx;

        public ChandaRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<ChandaSubHead>> GetChandaSubHead(int id)
        {
            return await Task.Run(() => _ctx.ChandaSubHeads.Where(c => c.ChandaHeadId == id));
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddChanda(Chanda chanda, ICollection<ChandaDetail> chandaDetails)
        {
            try
            {
                chanda.ComputerCode = new JamatRepository(_ctx).SetAutoNumberFormat((int)ApplicationPreferences.ScreenId.Chanda_Add);

                _ctx.Chandas.Add(chanda);

                if (Save())
                {
                    foreach (var detail in chandaDetails)
                    {
                        detail.ChandaId = chanda.ChandaId;
                        _ctx.ChandaDetails.Add(detail);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
