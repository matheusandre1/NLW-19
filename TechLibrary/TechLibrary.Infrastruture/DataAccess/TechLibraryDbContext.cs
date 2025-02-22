using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infrastruture.DataAccess
{
    public class TechLibraryDbContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\\Users\\memba\\OneDrive\\Área de Trabalho\\TechLibraryDb.db");
        }
    }
}
