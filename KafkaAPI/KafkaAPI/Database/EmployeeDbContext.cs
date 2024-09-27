using KafkaAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace KafkaAPI.Database
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions): base(dbContextOptions) {
            
        }

        public DbSet<Employee>Employees { get; set; }

       
    }
}
