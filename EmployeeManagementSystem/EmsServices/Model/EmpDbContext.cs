using Microsoft.EntityFrameworkCore;

namespace EmsServices.Model
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions contextOptions):base(contextOptions) 
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
