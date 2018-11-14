#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion using

namespace KRTool.Model.migration
{
    public class migrationStep1 : IMigrationStep<DatabaseContext>
    {
        public void MigrateStructure(DatabaseContext context)
        {
            //Änderung der Datenbankstruktur wird hier vorgenommen
            //context.Database.ExecuteSqlCommand("ALTER TABLE abo ADD COLUMN [bemerkung_id1] INT"); //Spalte hinzufügen
                    }

        public void MigrateData(DatabaseContext context)
        {
                        // No data changes bei initialization
        }

    }//end class
}//end namespace
