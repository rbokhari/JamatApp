using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC.Interface
{
    public interface IChandaRepository
    {
        Task<IQueryable<ChandaSubHead>> GetChandaSubHead(int id);


        bool Save();

        bool AddChanda(Chanda chanda, ICollection<ChandaDetail> chandaDetails);
    }
}
