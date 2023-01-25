using Microsoft.EntityFrameworkCore;
namespace SP23.P01.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
            

        public DbSet <TrainStation> TrainStation{ get; set; }
        

    }
}  
      
