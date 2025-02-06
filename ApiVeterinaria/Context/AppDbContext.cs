using ApiVeterinaria.Models;

using Microsoft.EntityFrameworkCore;

namespace ApiVeterinaria.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options)
            {

        }
        public DbSet<DataAnimals> DataAnimals { get; set; }

        public DbSet<DataUser> DataUser { get; set; }

        
    }
}
