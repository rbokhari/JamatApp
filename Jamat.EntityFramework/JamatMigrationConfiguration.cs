using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace Jamat.EntityFramework
{
    public class JamatMigrationConfiguration : DbMigrationsConfiguration<DbEntityContext>
    {
        public JamatMigrationConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;  //Dangerous using carefully, read on internet
            this.AutomaticMigrationsEnabled = true;            
        }

        protected override void Seed(DbEntityContext context)
        {
            base.Seed(context);

#if DEBUG

#endif

        }
    }
}
