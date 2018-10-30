#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion using

namespace DBTest.modules.migration
{
    public class migrationStep2 : IMigrationStep<DatabaseContext>
    {
        public void MigrateStructure(DatabaseContext context)
        {
            //Die zuvor in Step 1 (Version 1) hinzugefügte Spalte entfernen
            //SQLite3 erlaubt das löschen einzelner Spalten nicht.
            //Im Beispiel wird die original Tabelle unbenannt, eine neue Tabelle mit dem selben Namen erstellt und die Datensätze von der unbenannten/alten Tabelle in die neue kopiert.
            /*
                        context.Database.ExecuteSqlCommand(@"
            ALTER TABLE abo RENAME TO temp_abo;

            CREATE TABLE abo
            (
              [id] integer NOT NULL PRIMARY KEY AUTOINCREMENT,
  [ausgabe_von] integer NOT NULL,
  [ausgabe_bis] integer NOT NULL,
  [bezahlt_am] integer NOT NULL,
  [bezahlt_von] integer NOT NULL,
  [bezahlt_bis] integer NOT NULL,
  [kunden_id] integer NOT NULL,
  [bemerkung_id] integer NOT NULL,
  FOREIGN KEY(kunden_id) REFERENCES kunden(id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

            INSERT INTO abo([id], [ausgabe_von], [ausgabe_bis], [bezahlt_am], [bezahlt_von], [bezahlt_bis], [kunden_id], [kunden_id1], [bemerkung_id]) 
SELECT [id], [ausgabe_von], [ausgabe_bis], [bezahlt_am], [bezahlt_von], [bezahlt_bis], [kunden_id], [kunden_id1], [bemerkung_id] FROM temp_abo;

            DROP TABLE temp_abo;");
            */
        }

        public void MigrateData(DatabaseContext context)
        {
            // No data changes bei initialization
        }

    }//end class
}//end namespace
