using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDPress.Dal.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;

namespace ZDPress.Dal
{
    public class DBLocalContext : DbContext
    {
        static string ConnectionString1
        {
            get { return ConfigurationManager.ConnectionStrings["ZDPress1"].ConnectionString; }
        }
        public DBLocalContext(): base("ZDPress") 
        {
            Database.SetInitializer<DBLocalContext>(new CreateDatabaseIfNotExists<DBLocalContext>());
        }

        public DbSet<PressOperation> PressOperation { get; set; } 
        public DbSet<PressOperationData> PressOperationData { get; set; }

       
    }
}
