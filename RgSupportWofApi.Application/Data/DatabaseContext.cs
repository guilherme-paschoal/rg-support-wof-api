using Microsoft.EntityFrameworkCore;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Data
{
    public class DatabaseContext : DbContext
    {
        // A parameterless constructor is needed for unit testing repositories
        public DatabaseContext() {}

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        // Making the DbSets virtual allow us to mock them and unit test the repositories
        public virtual DbSet<Engineer> Engineers { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
    }
}
