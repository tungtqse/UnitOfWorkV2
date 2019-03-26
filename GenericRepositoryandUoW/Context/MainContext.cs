using GenericRepositoryandUoW.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryandUoW.Context
{
    public class MainContext :DbContext
    {
        public MainContext() { }
        public MainContext(string connectionStringName) : base(connectionStringName) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
