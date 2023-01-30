using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Entities;

namespace SP23.P01.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet <TrainStation> TrainStation{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrainStation>()
                .Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            modelBuilder.Entity<TrainStation>()
                .Property(x => x.Address)
                .IsRequired();
        }
    }
}  
      
