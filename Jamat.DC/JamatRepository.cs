using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.DC.Interface;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public class JamatRepository : IJamatRepository
    {
        DbEntityContext _ctx;

        public JamatRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }



        public string SetAutoNumberFormat(int screenId)
        {
            var autoSetting = _ctx.AutoSettings.SingleOrDefault(c => c.ScreenId == screenId);
            string autoNumber;

            if (autoSetting == null) return string.Empty;
            autoNumber = autoSetting.CurrentValue.ToString();
            short autoLength = autoSetting.CodeLength;

            autoNumber = new string('0', autoLength - autoNumber.Length) + autoNumber;

            autoNumber = string.Format("{0}{1}", autoSetting.CodePrefix, autoNumber);

            autoSetting.CurrentValue += 1;

            _ctx.Entry(autoSetting).State = EntityState.Modified;
            if (_ctx.SaveChanges() > 0)
            {

            }

            return autoNumber;
        }
    }
}
