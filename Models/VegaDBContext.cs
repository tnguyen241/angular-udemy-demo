using Microsoft.EntityFrameworkCore;

namespace angular_udemy_demo.Models
{
    public class VegaDBContext:DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleFeature> VehicleFeatures { get; set; }
        public VegaDBContext(DbContextOptions options):base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new {vf.VehicleId,vf.FeatureId});
        }

       
    }
}