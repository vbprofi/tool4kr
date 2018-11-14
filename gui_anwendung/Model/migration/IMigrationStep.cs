#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
#endregion using

namespace KRTool.Model.migration
{
    public interface IMigrationStep<T> where T : DbContext
    {
        void MigrateStructure(T context);

        void MigrateData(T context);
    }
    
}//end namespace
