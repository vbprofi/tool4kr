#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
#endregion using

namespace DBTest.modules.migration
{
    public class NullMigrationStep<T> : IMigrationStep<T> where T : DbContext
    {
        private NullMigrationStep()
        {
        }

        public void MigrateStructure(T context)
        {
            // Do nothing, skipp
        }

        public void MigrateData(T context)
        {
            // Do nothing, skipp
        }

        public static NullMigrationStep<T> GetInstance()
        {
            return new NullMigrationStep<T>();
        }

    }//end class
}//end namespace
