using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public interface IValidationRepository
    {
        IQueryable<ValidationDetail> GetValidationDetails(int id);

        ValidationDetail GetValidationDetail(int vId);

        bool Save();

        bool AddValidationDetail(ValidationDetail newValidationDetail);
    }
}
