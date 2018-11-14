#region Überschrift
/*******
 * Diese Datei wurde manuell erstellt und beinhaltet drei Klassen:
 * SQLiteProviderInvariantName, SQLiteDbDependencyResolver und SqLiteDbConfiguration 
 * Alle drei Klassen konfigurieren den SQLite-Adapter für das Entity Framework (EF6) mit der CodeFirst-Lib
 *******/
#endregion Überschrift
#region Using
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Diagnostics;
#endregion Using

namespace KRTool.Model
{
        public class SQLiteProviderInvariantName : IProviderInvariantName
    {
        public static readonly SQLiteProviderInvariantName Instance = new SQLiteProviderInvariantName();
        private SQLiteProviderInvariantName() { }
        public const string ProviderName = "System.Data.SQLite.EF6";
        public string Name { get { return ProviderName; } }
    }

    class SQLiteDbDependencyResolver : IDbDependencyResolver
    {
        public object GetService(Type type, object key)
        {
            if (type == typeof(IProviderInvariantName)) return SQLiteProviderInvariantName.Instance;
            if (type == typeof(DbProviderFactory)) return SQLiteProviderFactory.Instance;
            return SQLiteProviderFactory.Instance.GetService(type);
        }

        public IEnumerable<object> GetServices(Type type, object key)
        {
            var service = GetService(type, key);
            if (service != null) yield return service;
        }
    }

    class SqLiteDbConfiguration : DbConfiguration
    {
        public SqLiteDbConfiguration()
        {
            AddDependencyResolver(new SQLiteDbDependencyResolver());
            /*
                        SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
            */
//HACK
var EF6ProviderServicesType = typeof(System.Data.SQLite.EF6.SQLiteProviderFactory).Assembly.GetTypes().First(x => x.Name == "SQLiteProviderServices");
        var EF6ProviderServices = (DbProviderServices)Activator.CreateInstance(EF6ProviderServicesType);
        SetProviderServices("System.Data.SQLite.EF6", EF6ProviderServices);
        SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);
        SetProviderFactory("System.Data.SQLite.EF6", System.Data.SQLite.EF6.SQLiteProviderFactory.Instance);
        SetProviderFactory("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
        }
    }
    
}//end namespace
