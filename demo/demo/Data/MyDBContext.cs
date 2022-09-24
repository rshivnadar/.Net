
using Demo.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace demo.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Employee> Employees { get; set; }
        
        
    



    }
}
