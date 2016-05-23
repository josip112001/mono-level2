using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using Project.Service.Models;

namespace Project.Service.DAL
{
    public class VehicleContext : DbContext
    {
        public VehicleContext() : base("Vehicles")
        {
            Database.SetInitializer<VehicleContext>(new CreateDatabaseIfNotExists<VehicleContext>());
        }

        public virtual DbSet<VehicleModel> Models { get; set; }
        public virtual DbSet<VehicleMake> Makes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }


}
